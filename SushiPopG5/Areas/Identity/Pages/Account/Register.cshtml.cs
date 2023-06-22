// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiPopG5.Models;
using SushiPopG5.Utils;

namespace SushiPopG5.Areas.Identity.Pages.Account
{
  public class RegisterModel : PageModel
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly DbContext _context;
    //private readonly IEmailSender _emailSender;
    private readonly RoleManager<IdentityRole> _roleManager;
    public RegisterModel(
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        SignInManager<IdentityUser> signInManager,
        ILogger<RegisterModel> logger,
        //IEmailSender emailSender
        RoleManager<IdentityRole> roleManager,
        DbContext dbContext)
    {
      _userManager = userManager;
      _userStore = userStore;
      _emailStore = GetEmailStore();
      _signInManager = signInManager;
      _logger = logger;
      //_emailSender = emailSender;
      _context = dbContext;
      _roleManager = roleManager;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; }

      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }
      
      [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido) ]
      [MaxLength(30, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
      [MinLength(2, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
      public string Nombre { get; set; }

      [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
      [MaxLength(30, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
      [MinLength(2, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
      public string Apellido { get; set; }

      [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
      [MaxLength(100, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
      [Display(Name = "Dirección")]
      public string Direccion { get; set; }

      [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
      [MaxLength(10, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
      [MinLength(10, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
      [Display(Name = "Teléfono")]
      public string Telefono { get; set; }

      [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
      [DataType(DataType.Date)]
      [Display(Name = "Fecha de nacimiento")]
      public DateTime FechaNacimiento { get; set; }

      [DataType(DataType.Date)]
      [Display(Name = "Fecha de alta")]
      public DateTime? FechaAlta { get; set; }


    }


    public async Task OnGetAsync(string returnUrl = null)
    {
      // si los roles no existen, los creo aca
      if (!_roleManager.RoleExistsAsync("ADMIN").GetAwaiter().GetResult())
      {
        _roleManager.CreateAsync(new IdentityRole("ADMIN")).GetAwaiter().GetResult();
      }
      if (!_roleManager.RoleExistsAsync("EMPLEADO").GetAwaiter().GetResult())
      {
        _roleManager.CreateAsync(new IdentityRole("EMPLEADO")).GetAwaiter().GetResult();
      }
      if (!_roleManager.RoleExistsAsync("CLIENTE").GetAwaiter().GetResult())
      {
        _roleManager.CreateAsync(new IdentityRole("CLIENTE")).GetAwaiter().GetResult();

      }

      //
      //
      IdentityUser user = CreateUser();
      string email, usuario;
      email = usuario = "admin@ort.edu.ar";
      await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
      await _emailStore.SetEmailAsync(user, usuario, CancellationToken.None);
      var result = await _userManager.CreateAsync(user, "Password1!");

      if (result.Succeeded)
      {
        await _userManager.AddToRoleAsync(user, "ADMIN");
      }

    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
                    {
                      returnUrl ??= Url.Content("~/");
                      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      if (ModelState.IsValid)
      {
        var ultimoNumeroCliente = await _context.Cliente.MaxAsync(c => c.NumeroCliente);
        if (ultimoNumeroCliente == null)
        {
          ultimoNumeroCliente = 0;
        }
        var user = new Cliente
        {
          Nombre = Input.Nombre,
          Apellido = Input.Apellido,
          Direccion = Input.Direccion,
          Telefono = Input.Telefono,
          FechaNacimiento = Input.FechaNacimiento,
          Email = Input.Email,
          NumeroCliente = ultimoNumeroCliente + 1,
        };

        await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
        
        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
          await _userManager.AddToRoleAsync(user, "CLIENTE");
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

    private IdentityUser CreateUser()
    {
      try
      {
        return Activator.CreateInstance<Usuario>();
      }
      catch
      {
        throw new InvalidOperationException($"Can't create an instance of '{nameof(Usuario)}'. " +
            $"Ensure that '{nameof(Usuario)}' is not an abstract class and has a parameterless constructor, or alternatively " +
            $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
      }
    }

    private IUserEmailStore<IdentityUser> GetEmailStore()
    {
      if (!_userManager.SupportsUserEmail)
      {
        throw new NotSupportedException("The default UI requires a user store with email support.");
      }
      return (IUserEmailStore<IdentityUser>)_userStore;
    }
  }
}
