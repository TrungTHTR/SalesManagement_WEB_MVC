using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace eStore
{
    public class TblMembersController : Controller
    {
        private readonly SalesManagementContext _context;

        public TblMembersController(SalesManagementContext context)
        {
            _context = context;
        }
        public TblMember readJson()
        {
            IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            var strConn = config["DefaultUser"];
            TblMember ac = JsonSerializer.Deserialize<TblMember>(strConn); // đọc file Json
            return ac;
        }


        // GET: TblMembers
        public async Task<IActionResult> Index(string? email)
        {
            if(email == null)
            {
                return NotFound();
            }else if (email == "admin@estore.com")
            {
                return View(await _context.TblMembers.ToListAsync());
            }
            return View(await _context.TblMembers.Where(m=> m.Email == email).ToListAsync());
        }


        // GET: TblMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMember = await _context.TblMembers
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (tblMember == null)
            {
                return NotFound();
            }

            return View(tblMember);
        }

        // GET: TblMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,Password,Email,CompanyName,City,Country")] TblMember tblMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblMember);
        }

        // GET: TblMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMember = await _context.TblMembers.FindAsync(id);
            if (tblMember == null)
            {
                return NotFound();
            }
            return View(tblMember);
        }

        // POST: TblMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Password,Email,CompanyName,City,Country")] TblMember tblMember)
        {
            if (id != tblMember.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMemberExists(tblMember.MemberId))
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
            return View(tblMember);
        }

        // GET: TblMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMember = await _context.TblMembers
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (tblMember == null)
            {
                return NotFound();
            }

            return View(tblMember);
        }

        // POST: TblMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMember = await _context.TblMembers.FindAsync(id);
            _context.TblMembers.Remove(tblMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMemberExists(int id)
        {
            return _context.TblMembers.Any(e => e.MemberId == id);
        }
    }
}
