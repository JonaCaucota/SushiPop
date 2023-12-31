﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SushiPopG5.Models;

namespace SushiPopG5.Controllers
{
    public class ContactosController : Controller
    {
        private readonly DbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public ContactosController(DbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Contactos
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Index()
        {
            return _context.Contacto != null ?
                        View(await _context.Contacto.ToListAsync()) :
                        Problem("Entity set 'DbContext.Contacto'  is null.");
        }

        // GET: Contactos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contacto == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contacto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            contacto.Leido = true;
            _context.Update(contacto);
            await _context.SaveChangesAsync();

            return View(contacto);
        }

        // GET: Contactos/Create
        public async Task<IActionResult> Create()
        {
            bool esEmpleado = false;
            string emailUsuario = "";
            string numeroTelefonoUsuario = "";
            string nombreUsuario = "";
            string usuarioId = _userManager.GetUserId(User);

            var usuario = await _context.Usuario.FindAsync(usuarioId);

            if (usuario != null)
            {
                esEmpleado = await _userManager.IsInRoleAsync(usuario, "EMPLEADO");
                emailUsuario = usuario.UserName;
                numeroTelefonoUsuario = usuario.Telefono;
                nombreUsuario = usuario.Nombre + " " + usuario.Apellido;
            }

            ViewBag.EsEmpleado = esEmpleado;
            ViewBag.EmailUsuario = emailUsuario;
            ViewBag.NumeroTelefonoUsuario = numeroTelefonoUsuario;
            ViewBag.NombreUsuario = nombreUsuario;

            return View();
        }

        // POST: Contactos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,Email,Telefono,Mensaje")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                contacto.Leido = false;
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThankYou");
            }
            return View(contacto);
        }

        // GET: Contactos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contacto == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        // POST: Contactos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,Email,Telefono,Mensaje,Leido")] Contacto contacto)
        {
            if (id != contacto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contacto);
        }

        // GET: Contactos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contacto == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contacto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contacto == null)
            {
                return Problem("Entity set 'DbContext.Contacto'  is null.");
            }
            var contacto = await _context.Contacto.FindAsync(id);
            if (contacto != null)
            {
                _context.Contacto.Remove(contacto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ThankYou()
        {
            return View("ThankYou");
        }

        private bool ContactoExists(int id)
        {
            return (_context.Contacto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
