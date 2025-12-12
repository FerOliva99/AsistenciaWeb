using AsistenciaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly MiDbContext _context;

    public AccountController(MiDbContext context)
    {
        _context = context;
    }

    // GET: /Account/Login
    public IActionResult Login() => View();

    // POST: /Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string correo, string contrasena)
    {
        var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.correo == correo);
        if (docente == null || !docente.CheckPassword(contrasena))
        {
            ModelState.AddModelError("", "Correo o contraseña incorrectos");
            return View();
        }

        // Crear claims para almacenar datos del usuario loggeado y que se envian en la cookie
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, docente.nombre + " " + docente.apellido),
            new Claim("DocenteId", docente.id_docente.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("MisGrupos", "Docentes");
    }

    // Borra la sesión
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
