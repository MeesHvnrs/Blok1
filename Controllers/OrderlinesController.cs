using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blok1.Data;
using Blok1.Data.Models;

namespace Blok1.Controllers
{
    public class OrderlinesController : Controller
    {
        private readonly MeesDbContext _context;

        public OrderlinesController(MeesDbContext context)
        {
            _context = context;
        }

        // GET: Orderlines
        public async Task<IActionResult> Index()
        {
            var meesDbContext = _context.OrderProducts.Include(o => o.Order).Include(o => o.Product);
            return View(await meesDbContext.ToListAsync());
        }

        // GET: Orderlines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderline = await _context.OrderProducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderline == null)
            {
                return NotFound();
            }

            return View(orderline);
        }

        // GET: Orderlines/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Orderlines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,Quantity,CustomName,CustomColor")] Orderline orderline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderline);
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderline.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderline.ProductId);
            return View(orderline);
        }

        // GET: Orderlines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderline = await _context.OrderProducts.FindAsync(id);
            if (orderline == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderline.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderline.ProductId);
            return View(orderline);
        }

        // POST: Orderlines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderProductId,OrderId,ProductId,Quantity,CustomName,CustomColor")] Orderline orderline)
        {
            if (id != orderline.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderlineExists(orderline.OrderId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderline.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderline.ProductId);
            return View(orderline);
        }

        // GET: Orderlines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderline = await _context.OrderProducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderline == null)
            {
                return NotFound();
            }

            return View(orderline);
        }

        // POST: Orderlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderline = await _context.OrderProducts.FindAsync(id);
            if (orderline != null)
            {
                _context.OrderProducts.Remove(orderline);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderlineExists(int id)
        {
            return _context.OrderProducts.Any(e => e.OrderId == id);
        }
    }
}
