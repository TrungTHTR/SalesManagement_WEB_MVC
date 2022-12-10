using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;

namespace eStore.Controllers
{
    public class TblOrdersController : Controller
    {
        private readonly SalesManagementContext _context;

        public TblOrdersController(SalesManagementContext context)
        {
            _context = context;
        }

        // GET: TblOrders
        public async Task<IActionResult> Index(string? startDate, string? endDate)
        {
            var salesManagementContext = await _context.TblOrders.Include(t => t.Member).ToListAsync();
            if (!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate))
            {
                DateTime Start = DateTime.Parse(startDate);
                DateTime End = DateTime.Parse(endDate);
                salesManagementContext = await _context.TblOrders.Include(t => t.Member).OrderByDescending(o => o.OrderDate.Date >= Start && o.OrderDate.Date <= End).ToListAsync();
                return View(salesManagementContext);
            }
            return View(salesManagementContext);
        }

        // GET: TblOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders
                .Include(t => t.Member)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tblOrder == null)
            {
                return NotFound();
            }

            return View(tblOrder);
        }

        // GET: TblOrders/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.TblMembers, "MemberId", "City");
            return View();
        }

        // POST: TblOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,MemberId,OrderDate,RequiredDate,ShippedDate,Freight")] TblOrder tblOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.TblMembers, "MemberId", "City", tblOrder.MemberId);
            return View(tblOrder);
        }

        // GET: TblOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders.FindAsync(id);
            if (tblOrder == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.TblMembers, "MemberId", "City", tblOrder.MemberId);
            return View(tblOrder);
        }

        // POST: TblOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,MemberId,OrderDate,RequiredDate,ShippedDate,Freight")] TblOrder tblOrder)
        {
            if (id != tblOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOrderExists(tblOrder.OrderId))
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
            ViewData["MemberId"] = new SelectList(_context.TblMembers, "MemberId", "City", tblOrder.MemberId);
            return View(tblOrder);
        }

        // GET: TblOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders
                .Include(t => t.Member)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tblOrder == null)
            {
                return NotFound();
            }

            return View(tblOrder);
        }

        // POST: TblOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblOrder = await _context.TblOrders.FindAsync(id);
            _context.TblOrders.Remove(tblOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOrderExists(int id)
        {
            return _context.TblOrders.Any(e => e.OrderId == id);
        }
    }
}
