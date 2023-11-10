using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razoWeb.models;

namespace ValidateAsp.Pages_Blog
{
    public class DeleteModel : PageModel
    {
        private readonly razoWeb.models.MyWebContext _context;

        public DeleteModel(razoWeb.models.MyWebContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.article == null)
            {
                return NotFound();
            }

            var article = await _context.article.FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }
            else 
            {
                Article = article;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.article == null)
            {
                return NotFound();
            }
            var article = await _context.article.FindAsync(id);

            if (article != null)
            {
                Article = article;
                _context.article.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
