using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AfterlifeApp.Data;
using AfterlifeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AfterlifeApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
            if (isAdmin)
            {
                return View(await _context.Order.Include(o => o.Game).Include(m => m.User).ToListAsync());
            }

            return _context.Order != null ?
                        View(await _context.Order.Where(m => m.User == user).Include(o => o.Game).Include(m => m.User).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Order'  is null.");
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.User != user && !isAdmin)
            {
                // If not the owner or an admin, return an unauthorized view or redirect
                return Forbid();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,GameId")] Order order)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            order.User = user;
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Name", order.GameId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.User != user && !isAdmin)
            {
                // If not the owner or an admin, return an unauthorized view or redirect
                return Forbid();
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Name", order.GameId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,GameId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Name", order.GameId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.User != user && !isAdmin)
            {
                // If not the owner or an admin, return an unauthorized view or redirect
                return Forbid();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Download/5
        public async Task<IActionResult> Download(int? id)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Game)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            if (user == null || order.User != user)
            {
                // If the current user is not the owner, return an unauthorized view or redirect
                return Forbid();
            }

            return View(order);
        }

        [HttpPost, ActionName("DownloadConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadConfirmed(int id)
        {
            // Implement download logic here (e.g., initiate download process)

            // For now, redirect back to the list after confirming download
            return View();
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
