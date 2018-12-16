using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CVGS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVGS.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;
        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _db = db;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]

            public string PhoneNumber { get; set; }
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Gender")]
            public string Gender { get; set; }

            [Display(Name = "Date of Birth")]
            public DateTime BirthDate { get; set; }

            [Display(Name = "Receive Promotional Emails")]
            public bool isReceivePromotionalEmails { get; set; }

            [Display(Name = "Prefered Gaming Platform")]
            public string PreferedGamingPlatform { get; set; }

            [Display(Name = "Prefered Genre")]
            public string PreferedGamingGenre { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var loggedUser = _db.ApplicationUser.Where(m => m.Id == user.Id).FirstOrDefault();
            Username = userName;

            Input = new InputModel
            {
                Email = loggedUser.Email,
                PhoneNumber = loggedUser.PhoneNumber,
                FirstName = loggedUser.FirstName,
                LastName = loggedUser.LastName,
                Gender = loggedUser.Gender,
                BirthDate = loggedUser.BirthDate,
                isReceivePromotionalEmails = loggedUser.isReceivePromotionalEmails,
                PreferedGamingPlatform = loggedUser.PreferedGamingPlateForm,
                PreferedGamingGenre = loggedUser.PreferedGamingGenre

             
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            var loggedUser = _db.ApplicationUser.Where(m => m.Id == user.Id).FirstOrDefault();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
                var email = await _userManager.GetEmailAsync(user);
                if (Input.Email != email)
                {
                var exists = _db.Users.Any(p => p.Email == Input.Email);
                if (!exists)
                {
                    var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                    if (!setEmailResult.Succeeded)
                    {
                        var userId = await _userManager.GetUserIdAsync(user);
                        throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email already exists");

                    return Page();
                }
               
                }
                ModelState.Clear();
            
            
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
            
            if(Input.FirstName != loggedUser.FirstName)
            {
                loggedUser.FirstName = Input.FirstName;
            }
            if (Input.LastName != loggedUser.LastName)
            {
                loggedUser.LastName = Input.LastName;
            }
            if (Input.Gender != loggedUser.Gender)
            {
                loggedUser.Gender = Input.Gender;
            }
            if (Input.BirthDate != loggedUser.BirthDate)
            {
                loggedUser.BirthDate = Input.BirthDate;
            }
            if (Input.isReceivePromotionalEmails != loggedUser.isReceivePromotionalEmails)
            {
                loggedUser.isReceivePromotionalEmails = Input.isReceivePromotionalEmails;
               
            }
            if (Input.PreferedGamingPlatform != loggedUser.PreferedGamingPlateForm)
            {
                loggedUser.PreferedGamingPlateForm = Input.PreferedGamingPlatform;

            }
            if (Input.PreferedGamingGenre != loggedUser.PreferedGamingGenre)
            {
                loggedUser.PreferedGamingGenre = Input.PreferedGamingGenre;

            }
            await _db.SaveChangesAsync();
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
