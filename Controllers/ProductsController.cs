﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blok1.Data;
using Blok1.Data.Models;
using Blok1.ViewModels;

namespace Blok1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MeesDbContext _context;

        public ProductsController(MeesDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index2()
        {
            var meesDbContext = _context.Products.Include(p => p.Category);
            return View(await meesDbContext.ToListAsync());
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var meesDbContext = _context.Products.Include(p => p.Category);
            return View(await meesDbContext.ToListAsync());
        }

        public async Task<IActionResult> OverviewRevenue()
        {
            // Eerst data ophalen uit db en verwerken
            var revenue = _context.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Where(o => o.Status == Enums.OrderStatus.Done)
                .SelectMany(o => o.OrderProducts.Select(op => op.Product!.Price))
                .Sum();

            return View(new ProductRevenueViewModel
            {
                ProductName = "X",
                Revenue = revenue
            });
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Comment,Price,ColorChange,CategoryId")] Product product, IFormFile GifFile)
        {
            // Valideer alleen het GIF-bestand zonder het in ModelState te zetten
            if (GifFile == null || GifFile.Length == 0)
            {
                ModelState.AddModelError("GifFile", "Het GIF-bestand is verplicht.");
            }

            if (ModelState.IsValid)
            {
                // Verwerk het GIF-bestand
                if (GifFile != null && GifFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(GifFile.FileName);
                    var filePath = Path.Combine("wwwroot/Gifs", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GifFile.CopyToAsync(stream);
                    }

                    // Sla het pad van het bestand op in het productmodel
                    product.GifPath = "/Gifs/" + fileName;
                }

                // Sla het product op in de database
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Laad de categorieën opnieuw als er een fout is
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GifPath,Comment,Price,ColorChange,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}