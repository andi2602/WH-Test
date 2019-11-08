using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;
using WarehouseManagement.WarehouseModels;

namespace WarehouseManagement.Controllers
{
    [Authorize(Roles= "WarehouseManager")]
    public class WarehouseController : Controller
    {
        private readonly WarehouseContext _context;
        private IOrderServiceWarehouse _service;

        public WarehouseController(WarehouseContext context, IOrderServiceWarehouse warehouse)
        {
            _context = context;
            _service = warehouse;
        }

        // GET: Warehouse
        public async Task<IActionResult> Index(string searchString)
        {
            var items = from i in _context.wItems
                         select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString));
            }

            return View(await items.ToListAsync());
        }

        // GET: Warehouse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wItems = await _context.wItems
                .FirstOrDefaultAsync(m => m.wItemId == id);
            if (wItems == null)
            {
                return NotFound();
            }

            return View(wItems);
        }

        // GET: Warehouse/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("wItemId,Name,Description,Manufacturer,Price,Quantity")] wItems wItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wItems);
        }

        // GET: Warehouse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wItems = await _context.wItems.FindAsync(id);
            if (wItems == null)
            {
                return NotFound();
            }
            return View(wItems);
        }

        // POST: Warehouse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("wItemId,Name,Description,Manufacturer,Price,Quantity")] wItems wItems)
        {
            if (id != wItems.wItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!wItemsExists(wItems.wItemId))
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
            return View(wItems);
        }

        // GET: Warehouse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wItems = await _context.wItems
                .FirstOrDefaultAsync(m => m.wItemId == id);
            if (wItems == null)
            {
                return NotFound();
            }

            return View(wItems);
        }

        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wItems = await _context.wItems.FindAsync(id);
            _context.wItems.Remove(wItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        
        public IActionResult OrderAll(int quantity=15)
        {
            _service.OrderItemsForWarehouse(quantity);
            var Items = _context.wItems.ToListAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Order (int? id,int quantity=5)
        {
            var item = _context.wItems.FirstOrDefault(x=>x.wItemId==id);

            _service.OrderSpecificItem(item,quantity);



            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Restock()
        {
            _service.RestockWarehouse();
            return RedirectToAction(nameof(Index));
        }


        private bool wItemsExists(int id)
        {
            return _context.wItems.Any(e => e.wItemId == id);
        }
    }
}
