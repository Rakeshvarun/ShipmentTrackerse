using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentTrackers.Data;
using ShipmentTrackers.Models;
using System;

namespace ShipmentTrackerse.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly AppDbContext _db;

        public ShipmentController(AppDbContext db)
        {
            _db = db;
        }

        // ─── CREATE ───────────────────────────────────────────
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Shipment shipment)
        {
            if (!ModelState.IsValid) return View(shipment);

            // Auto-generate Tracking ID
            shipment.TrackingId = "TRK" + DateTime.Now.ToString("yyyyMMddHHmmss")
                                  + new Random().Next(100, 999);
            shipment.CreatedAt = DateTime.Now;
            shipment.UpdatedAt = DateTime.Now;
            shipment.Status = "Pending";

            _db.Shipments.Add(shipment);
            await _db.SaveChangesAsync();

            TempData["TrackingId"] = shipment.TrackingId;
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            ViewBag.TrackingId = TempData["TrackingId"];
            return View();
        }

        // ─── TRACK ────────────────────────────────────────────
        public IActionResult Track() => View();

        [HttpPost]
        public async Task<IActionResult> Track(string trackingId)
        {
            var shipment = await _db.Shipments
                .FirstOrDefaultAsync(s => s.TrackingId == trackingId);

            if (shipment == null)
                ViewBag.Error = "No shipment found with this Tracking ID.";

            return View(shipment);
        }

        // ─── HISTORY ─────────────────────────────────────────
        public async Task<IActionResult> History()
        {
            var list = await _db.Shipments
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            return View(list);
        }
    }
}
