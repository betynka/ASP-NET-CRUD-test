using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kolli.Data;
using kolli.Models;

namespace kolli.Controllers
{
    public class FlowersController : Controller
    {
        private readonly kolliContext _context;

        public FlowersController(kolliContext context)
        {
            _context = context;
        }

        // GET: Flowers
        public async Task<IActionResult> Index()
        {
              return _context.Flowers != null ? 
                          View(await _context.Flowers.ToListAsync()) :
                          Problem("Entity set 'kolliContext.Flowers'  is null.");
        }

        // GET: Flowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flowers == null)
            {
                return NotFound();
            }

            var flowers = await _context.Flowers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flowers == null)
            {
                return NotFound();
            }

            return View(flowers);
        }

        // GET: Flowers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Type,Color,Seson_blooming")] Flowers flowers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flowers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flowers);
        }

        // GET: Flowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flowers == null)
            {
                return NotFound();
            }

            var flowers = await _context.Flowers.FindAsync(id);
            if (flowers == null)
            {
                return NotFound();
            }
            return View(flowers);
        }

        // POST: Flowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Type,Color,Seson_blooming")] Flowers flowers)
        {
            if (id != flowers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flowers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowersExists(flowers.Id))
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
            return View(flowers);
        }

        // GET: Flowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Flowers == null)
            {
                return NotFound();
            }

            var flowers = await _context.Flowers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flowers == null)
            {
                return NotFound();
            }

            return View(flowers);
        }

        // POST: Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Flowers == null)
            {
                return Problem("Entity set 'kolliContext.Flowers'  is null.");
            }
            var flowers = await _context.Flowers.FindAsync(id);
            if (flowers != null)
            {
                _context.Flowers.Remove(flowers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlowersExists(int id)
        {
          return (_context.Flowers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
