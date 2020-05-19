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
    public class IndexModel : PageModel
    {
        private readonly AppTemplateCore.Data.ApplicationDbContext _context;

        public IndexModel(AppTemplateCore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync()
        {
            Person = await _context.Persons
                .Include(p => p.Country).ToListAsync();
        }
    }
}
