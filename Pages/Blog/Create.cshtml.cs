using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using razoWeb.models;

namespace ValidateAsp.Pages_Blog
{
    public class CreateModel : PageModel
    {
        private readonly razoWeb.models.MyWebContext _context;

        public CreateModel(razoWeb.models.MyWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //return lại trang razor bth
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.article == null || Article == null)
            {
                return Page();
            }

            _context.article.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
