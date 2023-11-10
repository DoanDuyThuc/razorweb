using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razoWeb.models;

namespace ValidateAsp.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly razoWeb.models.MyWebContext _context;

        public IndexModel(razoWeb.models.MyWebContext context)
        {
            _context = context;
        }

        public const int ITEM_PERS_PAGE = 10;

        //tự động binding theo ?p = currenpage
        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage {set;get;}

        public int countPages {set;get;}

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync(string SearchString)
        {
            //tổng bài viết
            var totalArticle = await _context.article.CountAsync();

            countPages = (int)Math.Ceiling((double)totalArticle / ITEM_PERS_PAGE);

            if(currentPage < 1){
                currentPage = 1;
            }
            if(currentPage > countPages){
                currentPage = countPages;
            }

            var qr = (from a in _context.article
                     select a)
                     .Skip( (currentPage - 1) * 10 )
                     .Take(ITEM_PERS_PAGE);

            if(!string.IsNullOrEmpty(SearchString)){
                Article = _context.article.Where(a => a.Title.Contains(SearchString)).ToList();    
            }else{

                Article = await qr.ToListAsync();
            }
        }
    }
}
