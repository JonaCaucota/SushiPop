using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SushiPopG5.Models;

namespace SushiPopG5.Controllers
{
    public class ReclamosController : Controller
    {
        private readonly DbContext _context;

        public ReclamosController(DbContext context)
        {
            _context = context;
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
        public IActionResult Create()
        {
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
                bool pedidoExists = reclamo.Pedido != null ? true : false;
                if (pedidoExists)
                {
                    _context.Add(reclamo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
