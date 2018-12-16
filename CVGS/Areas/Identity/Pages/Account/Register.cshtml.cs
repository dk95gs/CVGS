using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Models;
using CVGS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CVGS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

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

            [Display(Name ="Display Name")]
            [Required]
            public string UserName { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public string Gender { get; set; }
            [Required]
            public DateTime BirthDate { get; set; }
           
            [Display(Name ="Is Employee")]
            public bool isEmployee { get; set; }
            [Display(Name = "Is Super Admin")]
            public bool isSuperAdmin { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            string role = "";
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                if(Input.isSuperAdmin)
                {
                    role = SD.SuperAdminUser;
                }
                else if (Input.isEmployee)
                {
                    role = SD.EmployeeUser;
                }
                else
                {
                    role = SD.MemberUser;
                }
                var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, Gender = Input.Gender, BirthDate = Input.BirthDate, Role = role  };
                var exists = _db.Users.Any(p => p.Email == Input.Email);
                if(exists)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    
                    return Page();
                }
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if(!await _roleManager.RoleExistsAsync(SD.SuperAdminUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.EmployeeUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.EmployeeUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.MemberUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.MemberUser));
                    }

                    if (Input.isEmployee)
                    {
                        await _userManager.AddToRoleAsync(user, SD.EmployeeUser);
                    }
                    else if (Input.isSuperAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, SD.SuperAdminUser);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.MemberUser);
                    }
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if(!User.Identity.IsAuthenticated)
                            await _signInManager.SignInAsync(user, isPersistent: false);
                        
                        return LocalRedirect(returnUrl);
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
