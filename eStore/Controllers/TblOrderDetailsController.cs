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
    public class TblOrderDetailsController : Controller
    {
        private readonly SalesManagementContext _context;

        public TblOrderDetailsController(SalesManagementContext context)
        {
            _context = context;
        }

        // GET: TblOrderDetails
        public async Task<IActionResult> Index(int? id)
        {
            var salesManagementContext = _context.TblOrderDetails.Include(t => t.Order).Include(t => t.Product).Where(od=> od.OrderId == id);
            return View(await salesManagementContext.ToListAsync());
        }

        // GET: TblOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrderDetail = await _context.TblOrderDetails
                .Include(t => t.Order)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tblOrderDetail == null)
            {
                return NotFound();
            }

            return View(tblOrderDetail);
        }

        // GET: TblOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.TblOrders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "ProductName");
            return View();
        }

        // POST: TblOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] TblOrderDetail tblOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.TblOrders, "OrderId", "OrderId", tblOrderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "ProductName", tblOrderDetail.ProductId);
            return View(tblOrderDetail);
        }

        // GET: TblOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrderDetail = await _context.TblOrderDetails.FindAsync(id);
            if (tblOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.TblOrders, "OrderId", "OrderId", tblOrderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "ProductName", tblOrderDetail.ProductId);
            return View(tblOrderDetail);
        }

        // POST: TblOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] TblOrderDetail tblOrderDetail)
        {
            if (id != tblOrderDetail.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOrderDetailExists(tblOrderDetail.OrderId))
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
            ViewData["OrderId"] = new SelectList(_context.TblOrders, "OrderId", "OrderId", tblOrderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "ProductId", "ProductName", tblOrderDetail.ProductId);
            return View(tblOrderDetail);
        }

        // GET: TblOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrderDetail = await _context.TblOrderDetails
                .Include(t => t.Order)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tblOrderDetail == null)
            {
                return NotFound();
            }

            return View(tblOrderDetail);
        }

        // POST: TblOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblOrderDetail = await _context.TblOrderDetails.FindAsync(id);
            _context.TblOrderDetails.Remove(tblOrderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOrderDetailExists(int id)
        {
            return _context.TblOrderDetails.Any(e => e.OrderId == id);
        }
    }
}
