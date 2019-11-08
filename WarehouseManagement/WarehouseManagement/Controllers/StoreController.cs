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
using WarehouseManagement.StorefrontModels;

namespace WarehouseManagement.Controllers
{
    [Authorize(Roles= "StoreManager")]
    public class StoreController : Controller
    {
        private readonly StoreContext _context;
        private IOrderServiceStore _service;

        public StoreController(StoreContext context, IOrderServiceStore store)
        {
            _context = context;
            _service = store;
        }

        // GET: Store
        public async Task<IActionResult> Index(string searchString)
        {
            var items = from i in _context.storeItems
                        select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString));
            }

            return View(await items.ToListAsync());
        }

        // GET: Store/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeItems = await _context.storeItems
                .FirstOrDefaultAsync(m => m.storeItemId == id);
            if (storeItems == null)
            {
                return NotFound();
            }

            return View(storeItems);
        }

        // GET: Store/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("storeItemId,Name,Description,Manufacturer,Price,Quantity")] storeItems storeItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeItems);
        }

        // GET: Store/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeItems = await _context.storeItems.FindAsync(id);
            if (storeItems == null)
            {
                return NotFound();
            }
            return View(storeItems);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("storeItemId,Name,Description,Manufacturer,Price,Quantity")] storeItems storeItems)
        {
            if (id != storeItems.storeItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!storeItemsExists(storeItems.storeItemId))
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
            return View(storeItems);
        }

        // GET: Store/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeItems = await _context.storeItems
                .FirstOrDefaultAsync(m => m.storeItemId == id);
            if (storeItems == null)
            {
                return NotFound();
            }

            return View(storeItems);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeItems = await _context.storeItems.FindAsync(id);
            _context.storeItems.Remove(storeItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet ]
        
        public  IActionResult OrderAll(int quantity=15)
        {
            _service.fillWholeStore(quantity);
            var storeItems = _context.storeItems;
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Order(int? id, int quantity = 5)
        {
            var item = _context.storeItems.FirstOrDefault(x => x.storeItemId == id);

            _service.orderSpecificItem(quantity, item);



            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Restock()
        {
            _service.RestockStore();
            return RedirectToAction(nameof(Index));
        }



        private bool storeItemsExists(int id)
        {
            return _context.storeItems.Any(e => e.storeItemId == id);
        }
    }
}
