using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppTemplateCore.Areas.Movies.Models;
using AppTemplateCore.Data;

namespace AppTemplateCore.Areas.Movies.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppTemplateCore.Data.ApplicationDbContext _context;

        public IndexModel(AppTemplateCore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movies.ToListAsync();
        }
    }
}
