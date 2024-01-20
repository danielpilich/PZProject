using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AfterlifeApp.Data;
using AfterlifeApp.Models;

namespace AfterlifeApp.Controllers
{
    public class BundlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BundlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bundles
        public async Task<IActionResult> Index()
        {
              return _context.Bundle != null ? 
                          View(await _context.Bundle.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bundle'  is null.");
        }

        // GET: Bundles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bundle == null)
            {
                return NotFound();
            }

            var bundle = await _context.Bundle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bundle == null)
            {
                return NotFound();
            }

            return View(bundle);
        }

        // GET: Bundles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bundles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Bundle bundle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bundle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bundle);
        }

        // GET: Bundles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bundle == null)
            {
                return NotFound();
            }

            var bundle = await _context.Bundle.FindAsync(id);
            if (bundle == null)
            {
                return NotFound();
            }
            return View(bundle);
        }

        // POST: Bundles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Bundle bundle)
        {
            if (id != bundle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bundle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BundleExists(bundle.Id))
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
            return View(bundle);
        }

        // GET: Bundles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bundle == null)
            {
                return NotFound();
            }

            var bundle = await _context.Bundle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bundle == null)
            {
                return NotFound();
            }

            return View(bundle);
        }

        // POST: Bundles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bundle == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bundle'  is null.");
            }
            var bundle = await _context.Bundle.FindAsync(id);
            if (bundle != null)
            {
                _context.Bundle.Remove(bundle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BundleExists(int id)
        {
          return (_context.Bundle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
