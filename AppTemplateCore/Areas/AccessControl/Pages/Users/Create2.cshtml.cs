using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.AccessControl.Pages.Users
{
    public class CreateModel2 : PageModel
    {
        // Controller dependencies
        private readonly ApplicationDbContext Context;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<CreateModel> Logger;

        // Controller Contructor initializing Controller dependencies by 
        //DI Container

        public CreateModel2(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<CreateModel> logger)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
        }


        // ViewModel Properties
        // During OnGet() it will be blank
        // During OnPost() it will be filled by automatic model binding
        [BindProperty]
        public InputModel Input { get; set; }




        // This Model need to be Validated on POST
        // This Model is used to Render the View on GET
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First Name")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string LastName { get; set; }

            //public SelectList AllRolesSelectList { get; set; }
            //public SelectList AllClaimsSelectList { get; set; }

            public List<UserRole> AllRolesList { get; set; }
            public List<UserClaim> AllClaimsList { get; set; }

            //public string[] SelectedRoles { get; set; }
        }


        // Just show the blank page
        // ViewModel Properties are used to render View
        public async Task<IActionResult> OnGetAsync()
        {
            Input = new InputModel();

            Input.AllRolesList = RoleManager.Roles.ToList().Select(x => new UserRole()
            {
                IsSelected = false,
                RoleId = x.Id,
                RoleName = x.Name
            }).ToList();


            Input.AllClaimsList = ClaimsStore.AllClaims.Select(x => new UserClaim()
            {
                IsSelected = false,
                ClaimType = x.Type,
                ClaimValue = x.Value,
            }).ToList();

            //AllRolesSelectList = new SelectList(
            //        items: await RoleManager.Roles.ToListAsync(),
            //        dataValueField: "Name",
            //        dataTextField: "Name");

            //AllClaimsSelectList = new SelectList(
            //        items: ClaimsStore.AllClaims,
            //        dataValueField: "value",
            //        dataTextField: "type");


            return Page();
        }


        // OnPost(), ViewModel Properties filled and available
        // No need to catch then in Controller Action paramters
        public async Task<IActionResult> OnPostAsync()
        {
            // Model is VM Prperties
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName
            };

            // Use UserManager Service Class for storing User
            var result = await UserManager.CreateAsync(user, Input.Password);


            if (!result.Succeeded)
            {
                // Error occures while Adding New User                                            
                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }

                Input = new InputModel();

                Input.AllRolesList = RoleManager.Roles.ToList().Select(x => new UserRole()
                {
                    IsSelected = false,
                    RoleId = x.Id,
                    RoleName = x.Name
                }).ToList();

                Input.AllClaimsList = ClaimsStore.AllClaims.Select(x => new UserClaim()
                {
                    IsSelected = false,
                    ClaimType = x.Type,
                    ClaimValue = x.Value,
                }).ToList();

                return Page();
            }


            // New User Added Successfully now add it roles
            if (IsAnyRoleSelected() == true)
            {
                // If some roles are selected for New User, Add those roles
                result = await UserManager.AddToRolesAsync(user, GetSelectedRoles());


                if (!result.Succeeded)
                {
                    // Error occurs while adding roles
                    foreach (var error in result.Errors)
                    { ModelState.AddModelError("", error.Description); }

                    Input = new InputModel();

                    Input.AllRolesList = RoleManager.Roles.ToList().Select(x => new UserRole()
                    {
                        IsSelected = false,
                        RoleId = x.Id,
                        RoleName = x.Name
                    }).ToList();

                    Input.AllClaimsList = ClaimsStore.AllClaims.Select(x => new UserClaim()
                    {
                        IsSelected = false,
                        ClaimType = x.Type,
                        ClaimValue = x.Value,
                    }).ToList();


                    return Page();
                }
            }


            if (IsAnyClaimSelected() == true)
            {

                //List<Claim> selectedClaimsOnForm = ClaimsStore.AllClaims.Where(c => SelectedClaims.Contains(c.Value)).ToList();
                List<Claim> selectedClaimsOnForm = GetSelectedClaims();

                // Adding Claim Array
                foreach (var claim in selectedClaimsOnForm)
                { result = await UserManager.AddClaimAsync(user, claim); }

                if (!result.Succeeded)
                {   // Error occurs while adding claims                                                     
                    foreach (var error in result.Errors)
                    { ModelState.AddModelError("", error.Description); }

                    Input = new InputModel();

                    Input.AllRolesList = RoleManager.Roles.ToList().Select(x => new UserRole()
                    {
                        IsSelected = false,
                        RoleId = x.Id,
                        RoleName = x.Name
                    }).ToList();

                    Input.AllClaimsList = ClaimsStore.AllClaims.Select(x => new UserClaim()
                    {
                        IsSelected = false,
                        ClaimType = x.Type,
                        ClaimValue = x.Value,
                    }).ToList();


                    return Page();
                }
            }

            //ViewBag.Message = "Record(s) addded successfully.";
            // Show List Page
            return RedirectToPage("./Index");

        }



        public string[] GetSelectedRoles()
        {
            return Input.AllRolesList.Where<UserRole>(r => r.IsSelected == true).Select(s => s.RoleName).ToList().ToArray();
        }

        public List<Claim> GetSelectedClaims()
        {
            return Input.AllClaimsList.Where<UserClaim>(c => c.IsSelected == true).Select(s => new Claim(s.ClaimType, s.ClaimType)).ToList();
        }


        public bool IsAnyRoleSelected()
        {
            return Input.AllRolesList.Any<UserRole>(r => r.IsSelected == true);
        }

        public bool IsAnyClaimSelected()
        {
            return Input.AllClaimsList.Any<UserClaim>(c => c.IsSelected == true);
        }




    }
}