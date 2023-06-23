using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SushiPopG5.Models;

namespace SushiPopG5.Controllers
{
    public class CarritoItemsController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CarritoItemsController(DbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: CarritoItems
        [Authorize(Roles = "ADMIN, CLIENTE")]
        public async Task<IActionResult> Index( )
        {
            var user = await _userManager.GetUserAsync(User);
            var carritoCliente = await _context.Carrito
                .Include(x => x.Cliente)
                .Include(x => x.CarritoItems)
                .Where(x => x.Cliente.Email.ToUpper() == user.NormalizedEmail && x.Cancelado == false && x.Procesado == false).FirstOrDefaultAsync();
            
            
            if (carritoCliente == null)
            {
                return RedirectToAction("Index", controllerName:"Home");
            }
            if (carritoCliente.CarritoItems.Count == 0)
            {
                return RedirectToAction("Index", controllerName:"Home");
            }
            return View(carritoCliente.CarritoItems);

        }

        // GET: CarritoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarritoItem == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carritoItem == null)
            {
                return NotFound();
            }

            return View(carritoItem);
        }

        // GET: CarritoItems/Create
        [Authorize(Roles = "ADMIN, CLIENTE")]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null) { }

            var producto = await _context.Producto.FindAsync(id);

            if (producto == null) { }
            
            var user = await _userManager.GetUserAsync(User);
            
            if(user == null){}
            
            // Si el usuario no es nulo

            var cliente = await _context.Cliente.Where(x => x.Email.ToUpper() == user.NormalizedEmail)
                .FirstOrDefaultAsync();

            //Antes de agregar un item hay que verificar que exista un carrito
            var carritoCliente = await _context.Carrito
                .Include(x => x.Cliente)
                .Include(x => x.CarritoItems)
                .Where(x => x.Cliente.Email.ToUpper() == user.NormalizedEmail && x.Cancelado == false && x.Procesado == false).FirstOrDefaultAsync();

            if (carritoCliente == null)
            {
                Carrito carrito = new Carrito();
                carrito.Procesado = false;
                carrito.Cancelado = false;
                carrito.ClienteId = cliente.Id;
                _context.Add(carrito);
                await _context.SaveChangesAsync();

                carritoCliente = await _context.Carrito
                    .Include(x => x.Cliente)
                    .Include(x => x.CarritoItems)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();
            }

            var precioProducto = producto.Precio;
            
            //Falta ver el dia
            int dia = 6;
            var descuento = await _context.Descuento.Where(x => x.ProductoId == producto.Id && x.Activo == true)
                .FirstOrDefaultAsync();

            if (descuento != null)
            {
                var descuentoAplicar = (1 - descuento.Porcentaje / 100);

                if (descuento.DescuentoMax <= descuentoAplicar)
                {
                    precioProducto = precioProducto * descuentoAplicar;
                }
                else
                {
                    precioProducto -= descuento.DescuentoMax;
                }
                
                precioProducto = precioProducto * (1 - descuento.Porcentaje / 100);
            }

            var itemBuscado = await _context.CarritoItem.Where(x => x.CarritoId == carritoCliente.Id && x.ProductoId == producto.Id).FirstOrDefaultAsync();

            if (itemBuscado == null)
            {
                CarritoItem carritoItem = new CarritoItem();
                carritoItem.Precio = precioProducto;
                carritoItem.Cantidad = 1;
                carritoItem.CarritoId = carritoCliente.Id;
                carritoItem.ProductoId = producto.Id;
                carritoItem.NombreProducto = producto.Nombre;
                _context.Add(carritoItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                //Validar cantidad de stock
                
                itemBuscado.Cantidad += 1;
                _context.Update(itemBuscado);
                await _context.SaveChangesAsync();
            }



            return RedirectToAction("Index");
        }

        // GET: CarritoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarritoItem == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem.FindAsync(id);
            if (carritoItem == null)
            {
                return NotFound();
            }
            return View(carritoItem);
        }

        // POST: CarritoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Precio,Cantidad")] CarritoItem carritoItem)
        {
            if (id != carritoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoItemExists(carritoItem.Id))
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
            return View(carritoItem);
        }
        
        [Authorize(Roles = "ADMIN, CLIENTE")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarritoItem == null)
            {
                return Problem("Entity set 'DbContext.CarritoItem'  is null.");
            }
            var carritoItem = await _context.CarritoItem.FindAsync(id);
            if (carritoItem != null)
            {
                _context.CarritoItem.Remove(carritoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> BorrarCarrito()
        {
            var user = await _userManager.GetUserAsync(User);
            var carritoCliente = await _context.Carrito
                .Include(x => x.Cliente)
                .Include(x => x.CarritoItems)
                .Where(x => x.Cliente.Email.ToUpper() == user.NormalizedEmail && x.Cancelado == false && x.Procesado == false).FirstOrDefaultAsync();

            carritoCliente.Cancelado = true;
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index), controllerName:"Home");
        }

        public async Task<IActionResult> ComprarCarrito()
        {
            var user = await _userManager.GetUserAsync(User);
            var carritoCliente = await _context.Carrito
                .Include(x => x.Cliente)
                .Include(x => x.CarritoItems)
                .Where(x => x.Cliente.Email.ToUpper() == user.NormalizedEmail && x.Cancelado == false && x.Procesado == false).FirstOrDefaultAsync();

            carritoCliente.Procesado = true;
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index), controllerName:"Home");
        }

        private bool CarritoItemExists(int id)
        {
          return (_context.CarritoItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
    }
}
