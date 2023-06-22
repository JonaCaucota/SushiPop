
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SushiPopG5.Models;
using Microsoft.AspNetCore.Identity;

namespace SushiPopG5.Controllers
{
    public class ReclamosController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReclamosController(DbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reclamos
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Index()
        {
            return _context.Reclamo != null ?
                        View(await _context.Reclamo.ToListAsync()) :
                        Problem("Entity set 'DbContext.Reclamo'  is null.");
        }

        // GET: Reclamos/Details/5
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reclamo == null)
            {
                return NotFound();
            }

            var reclamo = await _context.Reclamo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reclamo == null)
            {
                return NotFound();
            }

            return View(reclamo);
        }

        // GET: Reclamos/Create
        [Authorize(Roles = "CLIENTE, ADMIN, EMPLEADO")]
        public async Task<IActionResult> Create()
        {
            string emailUsuario = "";
            string numeroTelefonoUsuario = "";
            string nombreUsuario = "";
            string usuarioId = _userManager.GetUserId(User);

            var usuario = await _context.Usuario.FindAsync(usuarioId);

            if (usuario != null)
            {
                emailUsuario = usuario.UserName;
                numeroTelefonoUsuario = usuario.Telefono;
                nombreUsuario = usuario.Nombre + " " + usuario.Apellido;
            }

            ViewBag.EmailUsuario = emailUsuario;
            ViewBag.NumeroTelefonoUsuario = numeroTelefonoUsuario;
            ViewBag.NombreUsuario = nombreUsuario;

            return View();
        }

        // POST: Reclamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CLIENTE, ADMIN")]
        public async Task<IActionResult> Create([Bind("NombreCompleto,Email,Telefono,Pedido,DetalleReclamo")] Reclamo reclamo)
        {
            if (ModelState.IsValid)
            {
                if (reclamo.Pedido != null)
                {
                    int id = reclamo.Pedido.Id;
                    if (NumPedidoExists(id))
                    {
                        _context.Add(reclamo);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                string mensajeError = "Error, el numero de pedido no existe, ingrese uno correcto";
                TempData["ErrorMessage"] = mensajeError;
            }
            return View(reclamo);
        }

        // GET: Reclamos/Edit/5
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reclamo == null)
            {
                return NotFound();
            }

            var reclamo = await _context.Reclamo.FindAsync(id);
            if (reclamo == null)
            {
                return NotFound();
            }
            return View(reclamo);
        }

        // POST: Reclamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,Email,Telefono,DetalleReclamo")] Reclamo reclamo)
        {
            if (id != reclamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reclamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReclamoExists(reclamo.Id))
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
            return View(reclamo);
        }

        // GET: Reclamos/Delete/5
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reclamo == null)
            {
                return NotFound();
            }

            var reclamo = await _context.Reclamo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reclamo == null)
            {
                return NotFound();
            }

            return View(reclamo);
        }

        // POST: Reclamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reclamo == null)
            {
                return Problem("Entity set 'DbContext.Reclamo'  is null.");
            }
            var reclamo = await _context.Reclamo.FindAsync(id);
            if (reclamo != null)
            {
                _context.Reclamo.Remove(reclamo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReclamoExists(int id)
        {
            return (_context.Reclamo?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool NumPedidoExists(int numPedido)
        {
            return (_context.Pedido?.Any(e => e.Id == numPedido)).GetValueOrDefault();
        }
    }
}
