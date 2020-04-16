using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppTemplateCore.Areas.Movies.Models;
using AppTemplateCore.Data;
using AppTemplateCore.Areas.AccessControl.Pages.Movies;

namespace AppTemplateCore.Areas.Movies.Pages
{
    public class DetailsModel : MoviePageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            Movie = await _context.Movies.FirstOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }
            }
            return Page();
        }
    }
}
