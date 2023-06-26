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
    public class ProductosController : Controller
    {
        private readonly DbContext _context;

        public ProductosController(DbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            bool esEmpleado = false;

            if (User.IsInRole("EMPLEADO") || User.IsInRole("ADMIN"))
            {
                esEmpleado = true;
            }

            ViewData["EsEmpleado"] = esEmpleado;
            
            return _context.Producto != null ?
                          View(await _context.Producto.ToListAsync()) :
                          Problem("Entity set 'DbContext.Producto'  is null.");
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Create()

        {
            if (_context.Categoria == null)
            {
                return Problem("Entity set 'DbContext.Categoria'  is null.");
            }

            ViewData["CATEGORIAS"] = await _context.Categoria.ToListAsync();
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,Costo,Foto,Stock,CategoriaId")] Producto producto)
        {
            if (_context.Categoria == null || _context.Producto == null)
            {
                return Problem("Las entidades requeridas son null.");
            }

            if (ModelState.IsValid)
            {
                var categoria = await _context.Categoria.FirstOrDefaultAsync(m => m.Id == producto.CategoriaId);
                producto.Categoria = categoria;

                var productoExistente = await _context.Producto.FirstOrDefaultAsync(p => p.Nombre == producto.Nombre || p.Foto == producto.Foto);

                if (productoExistente != null)
                {
                    if (productoExistente.Nombre == producto.Nombre)
                    {
                        ModelState.AddModelError("Nombre", "Ya existe un producto con el mismo nombre.");
                    }
                    if (productoExistente.Foto == producto.Foto)
                    {
                        ModelState.AddModelError("Foto", "Ya se está utilizando esta foto para otro producto.");
                    }

                    ViewData["CATEGORIAS"] = await _context.Categoria.ToListAsync();
                    return View(producto);
                }

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CATEGORIAS"] = await _context.Categoria.ToListAsync();
            return View(producto);
        }

        // GET: Productos/Edit/5
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Precio,Costo,Foto,Stock")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Producto == null)
            {
                return Problem("Entity set 'DbContext.Producto'  is null.");
            }
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return (_context.Producto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
