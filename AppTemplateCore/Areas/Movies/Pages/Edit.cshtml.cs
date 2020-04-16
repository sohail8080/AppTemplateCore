using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTemplateCore.Areas.Movies.Models;
using AppTemplateCore.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppTemplateCore.Areas.AccessControl.Pages.Movies;

namespace AppTemplateCore.Areas.Movies.Pages
{
    public class EditModel : MoviePageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MovieViewModel Movie_ViewModel { get; set; }

        public class MovieViewModel
        {
            public int ID { get; set; }

            [StringLength(60, MinimumLength = 3)]
            [Required]
            public string Title { get; set; }

            [Display(Name = "Release Date")]
            [DataType(DataType.Date)]
            public DateTime ReleaseDate { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
            [Required]
            [StringLength(30)]
            public string Genre { get; set; }

            [Range(1, 100)]
            [DataType(DataType.Currency)]
            [Column(TypeName = "decimal(18, 2)")]
            public decimal Price { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
            [StringLength(5)]
            [Required]
            public string Rating { get; set; }

        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            //var movieDB = await _context.Movies.FirstOrDefaultAsync(m => m.ID == id);
            var movieDB = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);

            if (movieDB == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            Movie_ViewModel = MapToMovie_ViewModel(movieDB);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            { return Page(); }

            // Approach 1
            var movieDB = await _context.Movies.FirstOrDefaultAsync(m => m.ID == Movie_ViewModel.ID);
            ModifyMovie_DomainClass(movieDB);

            // Approach 2
            //var movieDB = MapToMovie_DomainClass(Movie_ViewModel);
            //_context.Attach(movieDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie_ViewModel.ID))
                { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, Movie_ViewModel.ID); return NotFound(); }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }




        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }

        private void ModifyMovie_DomainClass(Movie movie)
        {           
            movie.Title = Movie_ViewModel.Title;
            movie.ReleaseDate = Movie_ViewModel.ReleaseDate;
            movie.Genre = Movie_ViewModel.Genre;
            movie.Price = Movie_ViewModel.Price;
            movie.Rating = Movie_ViewModel.Rating;
        }

        private Movie MapToMovie_DomainClass(MovieViewModel viewModel)
        {
            var movie = new Movie();

            movie.ID = viewModel.ID;
            movie.Title = viewModel.Title;
            movie.ReleaseDate = viewModel.ReleaseDate;
            movie.Genre = viewModel.Genre;
            movie.Price = viewModel.Price;
            movie.Rating = viewModel.Rating;
            return movie;
        }

        private MovieViewModel MapToMovie_ViewModel(Movie movie)
        {
            var viewModel = new MovieViewModel();

            viewModel.ID = movie.ID;
            viewModel.Title = movie.Title;
            viewModel.ReleaseDate = movie.ReleaseDate;
            viewModel.Genre = movie.Genre;
            viewModel.Price = movie.Price;
            viewModel.Rating = movie.Rating;

            return viewModel;
        }


    }
}
