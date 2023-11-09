using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razoWeb.models;

namespace ValidateAsp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private MyWebContext myWebContext;

    public IndexModel(ILogger<IndexModel> logger, MyWebContext _myWebContext)
    {
        _logger = logger;
        myWebContext = _myWebContext;
    }

    public void OnGet()
    {
        var posts = (from a in myWebContext.article 
                    orderby a.Created descending
                    select a).ToList();

        ViewData["posts"] = posts;
    }
}
