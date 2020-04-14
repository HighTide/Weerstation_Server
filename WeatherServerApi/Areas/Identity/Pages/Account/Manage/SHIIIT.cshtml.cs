using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using WeatherServerApi.Data;

namespace WeatherServerApi.Areas.Identity.Pages.Account.Manage
{
    public partial class StationsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public StationsModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public long Id { get; set; }

        public Guid Api { get; set; }
        public String Name { get; set; }
        public virtual IdentityUser Owner { get; set; }

        public String Color { get; set; }
        public double Coordinate_X { get; set; }
        public double Coordinate_Y { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public bool HasStation { get; private set; }

        public class InputModel
        {
            [Display(Name = "Weather Station ID (DO NOT USE THIS FOR POSTS)")]
            public long Id { get; set; }
            
            [Display(Name = "POST API Key")]
            public Guid Api { get; set; }
            
            [Required]
            [Display(Name = "Weather Station Name")]
            public string Name { get; set; }
            
            [Display(Name = "Owner")]
            public virtual IdentityUser Owner { get; set; }
            
            [Display(Name = "Color on the map")]
            public string Color { get; set; }
            
            [Display(Name = "X Coordinate")]
            public double Coordinate_X { get; set; }
            
            [Display(Name = "Y Coordinate")]
            public double Coordinate_Y { get; set; }
            
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var station = await _context.Stations.FirstOrDefaultAsync(Station => Station.Owner.Id == user.Id);

            Input = new InputModel
            {
                Id = station.Id,
                Api = station.Api,
                Name = station.Name,
                Color = station.Color,
                Coordinate_X = station.Coordinate_X,
                Coordinate_Y = station.Coordinate_Y
            };

            HasStation = (station != null);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        //public async Task<IActionResult> OnPostChangeEmailAsync()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        await LoadAsync(user);
        //        return Page();
        //    }

        //    var email = await _userManager.GetEmailAsync(user);
        //    if (Input.NewEmail != email)
        //    {
        //        var userId = await _userManager.GetUserIdAsync(user);
        //        var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
        //        var callbackUrl = Url.Page(
        //            "/Account/ConfirmEmailChange",
        //            pageHandler: null,
        //            values: new { userId = userId, email = Input.NewEmail, code = code },
        //            protocol: Request.Scheme);
        //        await _emailSender.SendEmailAsync(
        //            Input.NewEmail,
        //            "Confirm your email",
        //            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //        StatusMessage = "Confirmation link to change email sent. Please check your email.";
        //        return RedirectToPage();
        //    }

        //    StatusMessage = "Your email is unchanged.";
        //    return RedirectToPage();
        //}

        //public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        await LoadAsync(user);
        //        return Page();
        //    }

        //    var userId = await _userManager.GetUserIdAsync(user);
        //    var email = await _userManager.GetEmailAsync(user);
        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //    var callbackUrl = Url.Page(
        //        "/Account/ConfirmEmail",
        //        pageHandler: null,
        //        values: new { area = "Identity", userId = userId, code = code },
        //        protocol: Request.Scheme);
        //    await _emailSender.SendEmailAsync(
        //        email,
        //        "Confirm your email",
        //        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //    StatusMessage = "Verification email sent. Please check your email.";
        //    return RedirectToPage();
        //}
    }
}
