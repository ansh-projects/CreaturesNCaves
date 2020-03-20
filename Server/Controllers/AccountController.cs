using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Models;

namespace Server.Controllers
{

  public class RegisterData
  {
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    public bool isValid()
    {
      return (
        !string.IsNullOrEmpty(this.UserName) &&
        !string.IsNullOrEmpty(this.Password) &&
        !string.IsNullOrEmpty(this.Email)
      );
    }
  }

  [ApiController]
  [Route("account")]
  public class AccountController : ControllerBase
  {
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    // private readonly IEmailSender _emailSender;

    public AccountController(
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      ILogger<AccountController> logger,
      RoleManager<IdentityRole> roleManager
    // IEmailSender emailSender
    )
    {
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
      // _emailSender = emailSender ?? throw new ArgumentNullException(nameof(_emailSender));
    }

    [HttpGet]
    public IActionResult Get()
    {
      return new OkObjectResult(new { msg = "Account" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterData input)
    {
      _logger.LogInformation(JsonSerializer.Serialize(input));
      if (input.isValid())
      {
        var user = new User { UserName = input.Email, Email = input.Email, PasswordHash = input.Password };
        var createUserResult = await _userManager.CreateAsync(user);
        if (createUserResult.Succeeded)
        {
          if (!_roleManager.RoleExistsAsync("NormalUser").Result)
          {
            IdentityRole role = new IdentityRole();
            role.Name = "NormalUser";
            IdentityResult createRoleResult = await _roleManager.CreateAsync(role);
            if (!createRoleResult.Succeeded)
            {
              _logger.LogError("Error while creating role");
              return new StatusCodeResult(500);
            }
          }
          await _userManager.AddToRoleAsync(user, "NormalUser");
          _logger.LogInformation("User created a new account.");
          // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
          // var callbackUrl = Url.Page(
          //   "/Account/ConfirmEmail",
          //   pageHandler: null,
          //   values: new { area = "Identity", userId = user.Id, code = code },
          //   protocol: Request.Scheme);

          // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
          //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

          // if (_userManager.Options.SignIn.RequireConfirmedAccount)
          // {
          //   return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
          // } else
          // await _signInManager.SignInAsync(user, isPersistent: false);
          return LocalRedirect(Url.Content("~/"));
        }
      }
      
      return new StatusCodeResult(400);
    }
  }
}
