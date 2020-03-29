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

    // Difference b/w Create & Create2
    // In Create2
    // 1. Input Model is not used in Rendering the Roles/Claims Checkboxes in Create2, seperate variable are used
    // 2. Input Model is not used in Capturing the Roles n Claims Data on post back, Action Parameters are used
    // 3. For Rendering Checkboxes, seperate Lists are used in Create2
    // 4. For Capturing Action parameters are used in Create2
    // 5. Wodering how Selected Roles & Claims Checkboxes are mapped to Action Parameters in Create2
    // 6 Check Box Rendering is different in Creat & Creat2
    // 7. Applying Validation on Checkbox is hard in Create2

    // In Create
    // 1. Input Model Lists are used in Rendering the Roles/Claims Checkboxes in Create
    // 2. Input Model Lists are used in Capturing the Roles/Claims Data on post back, Action Parameters are used
    // 3. For Rendering Checkboxes, seperate Lists are used in Create2
    // 4. For Capturing Action parameters are used in Create2
    // 5. Wodering how Selected Roles & Claims Checkboxes are mapped to Action Parameters in Create2
    // 6 Check Box Rendering is different in Creat & Creat2
    // 7. Applying Validation on Checkbox is hard in Create2

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

        public IList<ApplicationRole> AllRolesList { get; set; }
        public IList<Claim> AllClaimsList { get; set; }


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

        }



        // Just show the blank page
        // ViewModel Properties are used to render View
        public async Task<IActionResult> OnGetAsync()
        {
            await Load_Form_Reference_Data();
            return Page();

        }


        // OnPost(), ViewModel Properties filled and available
        // No need to catch then in Controller Action paramters
        public async Task<IActionResult> OnPostAsync(string[] SelectedRoles, string[] SelectedClaims)
        {

            // Model is VM Prperties
            if (!ModelState.IsValid)
            {
                // If Clien side JS is diablied, form will be postedback
                // and we need following two lines               
                await Load_Form_Reference_Data();
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
                Add_Model_Errors(result);
                await Load_Form_Reference_Data();
                return Page();
            }


            // New User Added Successfully now add it roles
            if (SelectedRoles != null && SelectedRoles.Length > 0)
            {
                // If some roles are selected for New User, Add those roles
                result = await UserManager.AddToRolesAsync(user, SelectedRoles);

                if (!result.Succeeded)
                {
                    // Error occurs while adding roles
                    Add_Model_Errors(result);
                    await Load_Form_Reference_Data();
                    return Page();
                }
            }


            if (SelectedClaims != null && SelectedClaims.Length > 0)
            {
                List<Claim> selectedClaimsOnForm = ClaimsStore.AllClaims
                    .Where(claim => SelectedClaims.Contains(claim.Value)).ToList();

                // Adding Claim Array
                foreach (var claim in selectedClaimsOnForm)
                { result = await UserManager.AddClaimAsync(user, claim); }

                if (!result.Succeeded)
                {
                    // Error occurs while adding claims                                                                     
                    Add_Model_Errors(result);
                    await Load_Form_Reference_Data();
                    return Page();
                }
            }

            //ViewBag.Message = "Record(s) addded successfully.";
            // Show List Page
            return RedirectToPage("./Index");
        }


        private async Task<bool> Load_Form_Reference_Data()
        {
            AllRolesList = await RoleManager.Roles.ToListAsync();
            AllClaimsList = ClaimsStore.AllClaims;
            return true;

        }

        private void Add_Model_Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }




    }
}