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
    [Authorize(Roles = "Administrador,Beneficiario,Donante")]
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

        // GET: Beneficiario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiario = await _context.Beneficiario
                .FirstOrDefaultAsync(m => m.BeneficiarioId == id);

            if (beneficiario == null)
            {
                return NotFound();
            }

            return View(beneficiario);
        }

        // GET: Beneficiario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beneficiario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BeneficiarioId,Nombre")] BeneficiarioModels beneficiario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beneficiario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beneficiario);
        }

        // GET: Beneficiario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiario = await _context.Beneficiario.FindAsync(id);
            if (beneficiario == null)
            {
                return NotFound();
            }
            return View(beneficiario);
        }

        // POST: Beneficiario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BeneficiarioId,Nombre")] BeneficiarioModels beneficiario)
        {
            if (id != beneficiario.BeneficiarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beneficiario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficiarioExists(beneficiario.BeneficiarioId))
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
            return View(beneficiario);
        }

        // GET: Beneficiario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiario = await _context.Beneficiario
                .FirstOrDefaultAsync(m => m.BeneficiarioId == id);

            if (beneficiario == null)
            {
                return NotFound();
            }

            return View(beneficiario);
        }

        // POST: Beneficiario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beneficiario = await _context.Beneficiario.FindAsync(id);
            if (beneficiario != null)
            {
                // Verificar si existen Donaciones asociadas a este Beneficiario
                var donacionesAsociadas = await _context.Donaciones.Where(d => d.BeneficiarioId == id).ToListAsync();

                if (donacionesAsociadas.Any())
                {
                    // Si existen donaciones asociadas, no permitir la eliminación y mostrar un mensaje
                    // Puedes redirigir a una página de error o mostrar un mensaje en la vista Index
                    TempData["ErrorMessage"] = "No se puede eliminar el beneficiario porque existen donaciones asociadas.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Beneficiario.Remove(beneficiario);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Método para responder a la solicitud AJAX (sin correcciones necesarias en la lógica principal)
        public JsonResult ObtenerBeneficiario()
        {
            var beneficiarios = _context.Beneficiario.ToList();
            return Json(beneficiarios);
        }

        // POST: Beneficiario/DeleteConfirmedAjax/5 (para AJAX)
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmedAjax(int id)
        {
            var beneficiario = await _context.Beneficiario.FindAsync(id);
            if (beneficiario == null)
            {
                return Json(new { success = false, message = "Beneficiario no encontrado" });
            }

            // Verificar si existen Donaciones asociadas a este Beneficiario
            var donacionesAsociadas = await _context.Donaciones.Where(d => d.BeneficiarioId == id).ToListAsync();

            if (donacionesAsociadas.Any())
            {
                return Json(new { success = false, message = "No se puede eliminar el beneficiario porque existen donaciones asociadas." });
            }

            _context.Beneficiario.Remove(beneficiario);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Beneficiario eliminado correctamente" });
        }

        private bool BeneficiarioExists(int id)
        {
            return _context.Beneficiario.Any(e => e.BeneficiarioId == id);
        }
    }
}