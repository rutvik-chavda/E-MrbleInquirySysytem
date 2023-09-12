using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Rocky.Models;
using Rocky.Data;

namespace Rocky.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly icountrepo _icountrepo;
        private readonly ApplicationDbContext _applicationDbContext;

        public LogoutModel(SignInManager<IdentityUser> signInManager,
            icountrepo icountrepo,
            ApplicationDbContext applicationDbContext,
            ILogger<LogoutModel> logger)
        {
            _icountrepo = icountrepo;
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            _icountrepo.addcount(0);
            int i=_icountrepo.getcount();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
