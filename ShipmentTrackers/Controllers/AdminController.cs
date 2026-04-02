using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentTrackers.Data;
using System;

namespace ShipmentTrackerse.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) { _db = db; }

        // Dashboard – show all shipments
        public async Task<IActionResult> Dashboard()
        {
            var shipments = await _db.Shipments
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            return View(shipments);
        }

        // Update status form
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var shipment = await _db.Shipments.FindAsync(id);
            if (shipment == null) return NotFound();
            return View(shipment);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var shipment = await _db.Shipments.FindAsync(id);
            if (shipment == null) return NotFound();

            shipment.Status = status;
            shipment.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        // Delete shipment
        public async Task<IActionResult> Delete(int id)
        {
            var shipment = await _db.Shipments.FindAsync(id);
            if (shipment != null) _db.Shipments.Remove(shipment);
            await _db.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }
    }
}

