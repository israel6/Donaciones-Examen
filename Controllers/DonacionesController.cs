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
    public class DonacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Donaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Donaciones.Include(d => d.Beneficiario).Include(d => d.Campana).Include(d => d.Donante);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Donaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donacionesModels = await _context.Donaciones
                .Include(d => d.Beneficiario)
                .Include(d => d.Campana)
                .Include(d => d.Donante)
                .FirstOrDefaultAsync(m => m.DonacionId == id);
            if (donacionesModels == null)
            {
                return NotFound();
            }

            return View(donacionesModels);
        }

        // GET: Donaciones/Create
        public IActionResult Create()
        {
            ViewData["BeneficiarioId"] = new SelectList(_context.Beneficiario, "BeneficiarioId", "Nombre");
            ViewData["CampanaId"] = new SelectList(_context.Campaña, "CampanaId", "Nombre");
            ViewData["DonanteId"] = new SelectList(_context.Donante, "DonanteId", "Nombre");
            return View();
        }

        // POST: Donaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonacionId,Fecha,Monto,DonanteId,CampanaId,BeneficiarioId")] DonacionesModels donacionesModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donacionesModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BeneficiarioId"] = new SelectList(_context.Beneficiario, "BeneficiarioId", "Nombre", donacionesModels.BeneficiarioId);
            ViewData["CampanaId"] = new SelectList(_context.Campaña, "CampanaId", "Nombre", donacionesModels.CampanaId);
            ViewData["DonanteId"] = new SelectList(_context.Donante, "DonanteId", "Nombre", donacionesModels.DonanteId);
            return View(donacionesModels);
        }

        // GET: Donaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donacionesModels = await _context.Donaciones.FindAsync(id);
            if (donacionesModels == null)
            {
                return NotFound();
            }
            ViewData["BeneficiarioId"] = new SelectList(_context.Beneficiario, "BeneficiarioId", "Nombre", donacionesModels.BeneficiarioId);
            ViewData["CampanaId"] = new SelectList(_context.Campaña, "CampanaId", "Nombre", donacionesModels.CampanaId);
            ViewData["DonanteId"] = new SelectList(_context.Donante, "DonanteId", "Nombre", donacionesModels.DonanteId);
            return View(donacionesModels);
        }

        // POST: Donaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonacionId,Fecha,Monto,DonanteId,CampanaId,BeneficiarioId")] DonacionesModels donacionesModels)
        {
            if (id != donacionesModels.DonacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donacionesModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonacionesModelsExists(donacionesModels.DonacionId))
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
            ViewData["BeneficiarioId"] = new SelectList(_context.Beneficiario, "BeneficiarioId", "Nombre", donacionesModels.BeneficiarioId);
            ViewData["CampanaId"] = new SelectList(_context.Campaña, "CampanaId", "Nombre", donacionesModels.CampanaId);
            ViewData["DonanteId"] = new SelectList(_context.Donante, "DonanteId", "Nombre", donacionesModels.DonanteId);
            return View(donacionesModels);
        }
               // GET: Donaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donacionesModels = await _context.Donaciones
                .Include(d => d.Beneficiario)
                .Include(d => d.Campana)
                .Include(d => d.Donante)
                .FirstOrDefaultAsync(m => m.DonacionId == id);
            if (donacionesModels == null)
            {
                return NotFound();
            }

            return View(donacionesModels);
        }
     
        // POST: Donaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donacionesModels = await _context.Donaciones.FindAsync(id);
            if (donacionesModels != null)
            {
                _context.Donaciones.Remove(donacionesModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonacionesModelsExists(int id)
        {
            return _context.Donaciones.Any(e => e.DonacionId == id);
        }

        // GET: Donaciones/ListaDonaciones
        public IActionResult ListaDonaciones()
        {
            var donaciones = _context.Donaciones
                .Include(d => d.Beneficiario)
                .Include(d => d.Campana)
                .Include(d => d.Donante)
                .Select(d => new
                {
                    d.DonacionId,
                    d.Fecha,
                    d.Monto,
                    BeneficiarioNombre = d.Beneficiario.Nombre,
                    CampanaNombre = d.Campana.Nombre,
                    DonanteNombre = d.Donante.Nombre
                })
                .ToList();
            return Json(donaciones);
        }
    }
}