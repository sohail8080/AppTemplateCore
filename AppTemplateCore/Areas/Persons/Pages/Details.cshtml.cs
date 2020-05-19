using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppTemplateCore.Areas.Persons.Models;
using AppTemplateCore.Data;

namespace AppTemplateCore.Areas.Persons.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly AppTemplateCore.Data.ApplicationDbContext _context;

        public DetailsModel(AppTemplateCore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.Persons
                .Include(p => p.Country).FirstOrDefaultAsync(m => m.PersonID == id);

            if (Person == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
