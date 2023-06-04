using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SushiPopG5.Models;

namespace SushiPopG5.Controllers
{
    public class DescuentosController : Controller
    {
        private readonly DbContext _context;

        public DescuentosController(DbContext context)
        {
            _context = context;
        }

        // GET: Descuentos
        public async Task<IActionResult> Index()
        {
            return _context.Descuento != null ?
                        View(await _context.Descuento.ToListAsync()) :
                        Problem("Entity set 'DbContext.Descuento'  is null.");
        }

        // GET: Descuentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Descuento == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // GET: Descuentos/Create
        public IActionResult Create()
        {
            var productos = _context.Producto.ToList();
            ViewData["Productos"] = new SelectList(productos, "Id", "Nombre");

            return View();
        }

        // POST: Descuentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dia,Porcentaje,DescuentoMax,Activo,ProductoId")] Descuento descuento)
        {
            bool existeDescuento = await _context.Descuento.AnyAsync(d => d.ProductoId == descuento.ProductoId && d.Dia == descuento.Dia);

            if (existeDescuento)
            {
                ModelState.AddModelError("Dia", "Ya existe un descuento para este producto en el día especificado.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(descuento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var productos = await _context.Producto.ToListAsync();
            ViewData["Productos"] = new SelectList(productos, "Id", "Nombre");
            return View(descuento);
        }


        // GET: Descuentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Descuento == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuento.FindAsync(id);
            if (descuento == null)
            {
                return NotFound();
            }
            return View(descuento);
        }

        // POST: Descuentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dia,Porcentaje,DescuentoMax,Activo")] Descuento descuento)
        {
            if (id != descuento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(descuento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescuentoExists(descuento.Id))
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
            return View(descuento);
        }

        // GET: Descuentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Descuento == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // POST: Descuentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Descuento == null)
            {
                return Problem("Entity set 'DbContext.Descuento'  is null.");
            }
            var descuento = await _context.Descuento.FindAsync(id);
            if (descuento != null)
            {
                _context.Descuento.Remove(descuento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescuentoExists(int id)
        {
            return (_context.Descuento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
