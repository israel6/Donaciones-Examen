using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Donaciones.Data;
using Donaciones.Models;
using Microsoft.AspNetCore.Authorization;

namespace Donaciones.Controllers
{
    [Authorize(Roles = "Administrador,Donante")]
    public class CampañaController : Controller
    
    {
        private readonly ApplicationDbContext _context;

        public CampañaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Campaña
        public async Task<IActionResult> Index()
        {
            return View(await _context.Campaña.ToListAsync());
        }

        // GET: Campaña/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campañaModels = await _context.Campaña
                .FirstOrDefaultAsync(m => m.CampanaId == id);
            if (campañaModels == null)
            {
                return NotFound();
            }

            return View(campañaModels);
        }
       
        // GET: Campaña/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campaña/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CampanaId,Nombre,Objetivo")] CampañaModels campañaModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campañaModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campañaModels);
        }

        // GET: Campaña/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campañaModels = await _context.Campaña.FindAsync(id);
            if (campañaModels == null)
            {
                return NotFound();
            }
            return View(campañaModels);
        }

        // POST: Campaña/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CampanaId,Nombre,Objetivo")] CampañaModels campañaModels)
        {
            if (id != campañaModels.CampanaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campañaModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampañaModelsExists(campañaModels.CampanaId))
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
            return View(campañaModels);
        }

        // GET: Campaña/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campañaModels = await _context.Campaña
                .FirstOrDefaultAsync(m => m.CampanaId == id);
            if (campañaModels == null)
            {
                return NotFound();
            }

            return View(campañaModels);
        }

        // POST: Campaña/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campañaModels = await _context.Campaña.FindAsync(id);
            if (campañaModels != null)
            {
                _context.Campaña.Remove(campañaModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampañaModelsExists(int id)
        {
            return _context.Campaña.Any(e => e.CampanaId == id);
        }

        // GET: Campaña/ListaCampañas
        public IActionResult ListaCampañas()
        {
            var campañas = _context.Campaña.ToList(); // Obtener la lista de campañas desde la base de datos
            return Json(campañas); // Devolver la lista como JSON
        }
    }
}