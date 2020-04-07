using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppTemplateCore.Areas.Movies.Models;
using AppTemplateCore.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTemplateCore.Areas.Movies.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MovieViewModel Movie { get; set; }


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


        public IActionResult OnGet()
        {
            return Page();
        }

        // Save Operation
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            { return Page();}

            var movie = MapToMovie(Movie);

            _context.Movies.Add(movie);
            var result = await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


        private Movie MapToMovie(MovieViewModel viewModel)
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

        private MovieViewModel MapToMovieViewModel(Movie movie)
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