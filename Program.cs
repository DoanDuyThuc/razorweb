using Album.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using razoWeb.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("MyWebContext");
builder.Services.AddDbContext<MyWebContext>(options => 
    options.UseMySQL(connectionString)
);

// đăng ký dịch vụ mail
var mailSetting = builder.Configuration.GetSection("MailSettings");
//đăng ký cấu hình của 1 lớp
builder.Services.Configure<MailSettings>(mailSetting);
// đăng ký dịch vụ SendMailService
builder.Services.AddSingleton<IEmailSender, SendMailService>();

//đăng ký dịch vụ Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<MyWebContext>()
    .AddDefaultTokenProviders();

// builder.Services.AddDefaultIdentity<AppUser>()
//     .AddEntityFrameworkStores<MyWebContext>()
//     .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = true;
});


builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.AccessDeniedPath = "/KhongDuocTruyCap";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//đảm bảo phải có pv các thông tin đăng nhập xác thực
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
