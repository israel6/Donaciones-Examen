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
    public class BeneficiarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeneficiarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Beneficiario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Beneficiario.ToListAsync());
        }
        public JsonResult ObtenerBeneficiarios()
        {
            var Beneficiario = _context.Beneficiario.ToList();
            return Json(Beneficiario);
        }
        // GET: Beneficiario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BeneficiarioModels = await _context.Beneficiario
                .FirstOrDefaultAsync(m => m.BeneficiarioId == id);
            if (BeneficiarioModels == null)
            {
                return NotFound();
            }

            return View(BeneficiarioModels);
        }
        
        // GET: Beneficiario/Create
        public IActionResult Create()
        {
           

            return View();
        }

        // POST: Beneficiario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BeneficiarioId,Nombre")] BeneficiarioModels BeneficiarioModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(BeneficiarioModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(BeneficiarioModels);
        }
                // GET: Beneficiario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BeneficiarioModels = await _context.Beneficiario.FindAsync(id);
            if (BeneficiarioModels == null)
            {
                return NotFound();
            }
            return View(BeneficiarioModels);
        }
      
        // POST: Beneficiario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BeneficiarioId,Nombre")] BeneficiarioModels BeneficiarioModels)
        {
            if (id != BeneficiarioModels.BeneficiarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(BeneficiarioModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficiarioModelsExists(BeneficiarioModels.BeneficiarioId))
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
            return View(BeneficiarioModels);
        }
        // GET: Beneficiario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BeneficiarioModels = await _context.Beneficiario
                .FirstOrDefaultAsync(m => m.BeneficiarioId == id);
            if (BeneficiarioModels == null)
            {
                return NotFound();
            }

            return View(BeneficiarioModels);
        }
               // POST: Beneficiario/Delete/5
       [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var BeneficiarioModels = await _context.Beneficiario.FindAsync(id);
            if (BeneficiarioModels != null)
            {
                _context.Beneficiario.Remove(BeneficiarioModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeneficiarioModelsExists(int id)
        {
            return _context.Beneficiario.Any(e => e.BeneficiarioId == id);
        }
    }
}
