using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Donaciones.Data;
using Donaciones.Models;
using Microsoft.AspNetCore.Authorization;

namespace Donaciones.Controllers
{
    [Authorize(Roles = "Administrador,Beneficiario")]
    public class DonanteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonanteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Donante
        public async Task<IActionResult> Index()
        {
            return View(await _context.Donante.ToListAsync());
        }

        // GET: Donante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donanteModels = await _context.Donante
                .FirstOrDefaultAsync(m => m.DonanteId == id);
            if (donanteModels == null)
            {
                return NotFound();
            }

            return View(donanteModels);
        }

        // GET: Donante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonanteId,Nombre,CorreoElectronico,MontoDonado")] DonanteModels donanteModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donanteModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donanteModels);
        }

        // GET: Donante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donanteModels = await _context.Donante.FindAsync(id);
            if (donanteModels == null)
            {
                return NotFound();
            }
            return View(donanteModels);
        }

        // POST: Donante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonanteId,Nombre,CorreoElectronico,MontoDonado")] DonanteModels donanteModels)
        {
            if (id != donanteModels.DonanteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donanteModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonanteModelsExists(donanteModels.DonanteId))
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
            return View(donanteModels);
        }

        // GET: Donante/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donanteModels = await _context.Donante
                .FirstOrDefaultAsync(m => m.DonanteId == id);
            if (donanteModels == null)
            {
                return NotFound();
            }

            return View(donanteModels);
        }

        // POST: Donante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donanteModels = await _context.Donante.FindAsync(id);
            if (donanteModels != null)
            {
                _context.Donante.Remove(donanteModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonanteModelsExists(int id)
        {
            return _context.Donante.Any(e => e.DonanteId == id);
        }
    }
}
