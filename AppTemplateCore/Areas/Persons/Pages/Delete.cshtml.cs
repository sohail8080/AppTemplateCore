﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly AppTemplateCore.Data.ApplicationDbContext _context;

        public DeleteModel(AppTemplateCore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.Persons.FindAsync(id);

            if (Person != null)
            {
                _context.Persons.Remove(Person);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
