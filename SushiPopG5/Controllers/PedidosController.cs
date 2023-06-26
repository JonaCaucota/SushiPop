﻿using System;
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
    public class PedidosController : Controller
    {
        private readonly DbContext _context;

        public PedidosController(DbContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        [Authorize(Roles = "CLIENTE, ADMIN")]
        public async Task<IActionResult> Index()
        {
              return _context.Pedido != null ? 
                          View(await _context.Pedido.ToListAsync()) :
                          Problem("Entity set 'DbContext.Pedido'  is null.");
        }

        // GET: Pedidos/Details/5
        [Authorize(Roles = "CLIENTE, ADMIN")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        [Authorize(Roles = "CLIENTE, ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CLIENTE, ADMIN")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NroPedido,Fecha,Subtotal,Descuento,GastoEnvio,Total,Estado")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        [Authorize(Roles = "CLIENTE, ADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CLIENTE, ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedido == null)
            {
                return Problem("Entity set 'DbContext.Pedido'  is null.");
            }
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // POST: Pedidos/Consulta/
        public async Task<IActionResult> Consulta(int numeroPedido)
        {
            if (_context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.Where(x => x.NroPedido == numeroPedido).FirstOrDefaultAsync();
            if (pedido != null)
            {
                var estado = pedido.Estado;
                 return RedirectToAction("Follow", new { estado = estado });
            }

            return View();
        }
        
        public async Task<IActionResult> Follow(int estado)
        {
            string msj = "";
            switch (estado)
            {
               case 1:
                   msj = "Sin confirmar";
                   break;
               case 2:
                   msj = "Confirmado";
                   break;
               case 3:
                   msj = "En preparación";
                   break;
               case 4:
                   msj = "En reparto";
                   break;
               case 5:
                   msj = "Entregado";
                   break;
               case 6:
                   msj = "Cancelado";
                   break;
               default:
                   msj = "Ha ocurrido un error";
                   break;
            }

            ViewData["Mensaje"] = msj;
            return View();
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedido?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
