using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SushiPopG5.Models;

namespace SushiPopG5.Controllers
{
    public class ReservasController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ReservasController(DbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            return _context.Reserva != null ?
                        View(await _context.Reserva.ToListAsync()) :
                        Problem("Entity set 'DbContext.Reserva'  is null.");
        }

        // GET: Reservas/Details/5
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        [Authorize(Roles = "CLIENTE, ADMIN")]
        public async Task<IActionResult> Create()
        {
            var usuarioLogeado = await _userManager.GetUserAsync(User);
            var user = await _context.Usuario.FirstOrDefaultAsync(x => x.Email.ToUpper().Equals(usuarioLogeado.Email.ToUpper()));
            string nombre = "";
            string apellido = "";
            string clienteId = "";

            if (user != null)
            {
                nombre = user.Nombre;
                apellido = user.Apellido;
                clienteId = user.Id;
            }


            ViewData["Nombre"] = nombre;
            ViewData["Apellido"] = apellido;
            ViewData["ClienteId"] = clienteId;
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CLIENTE, ADMIN")]
        public async Task<IActionResult> Create([Bind("Id,Local,FechaYHora,ClienteId")] Reserva reserva)
        {
            if (_context.Reserva == null)
            {
                return Problem("Entity set 'DbContext.Producto' is null.");
            }

            bool hayReservaMismoDia = await _context.Reserva.AnyAsync(r => r.ClienteId == reserva.ClienteId && r.FechaYHora.Date == reserva.FechaYHora.Date);

            if (hayReservaMismoDia)
            {
                string usuarioId = _userManager.GetUserId(User);
                var usuario = await _context.Usuario.FindAsync(usuarioId);
                if (usuario != null)
                {
                    ViewData["Nombre"] = usuario.Nombre;
                    ViewData["Apellido"] = usuario.Apellido;
                    ViewData["ClienteId"] = usuario.Id;
                }
                ModelState.AddModelError("FechaYHora", "Ya hiciste una reserva para este dia.");
                return View(reserva);
            }

            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Local,FechaYHora,Confirmada")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reserva == null)
            {
                return Problem("Entity set 'DbContext.Reserva'  is null.");
            }
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva != null)
            {
                _context.Reserva.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ReservaExists(int id)
        {
            return (_context.Reserva?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
