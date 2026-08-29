using EVChargingManagementAPI.Data;
using EVChargingManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EVChargingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleDocumentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public VehicleDocumentsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [HttpPost("upload/{vehicleId}")]
        public async Task<IActionResult> UploadDocument(int vehicleId, IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                return BadRequest("Please Select a File");
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v=>v.Id == vehicleId);
            
            if(vehicle == null)
            {
                return BadRequest("Vehicle Not Found");
            }
            var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", "vehicles");

            if(!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            { 
                await file.CopyToAsync(stream);
            }

            var document = new VehicleDocument
            {
                VehicleId = vehicleId,
                FileName = file.FileName,
                FilePath = "/uploads/vehicles/" + uniqueFileName,
                ContentType = file.ContentType,
                UploadedAt = DateTime.UtcNow,
            };
            _context.VehicleDocuments.Add(document);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "File Uploaded Successfully.",
                vehicleId = document.Id,
                fileName = document.FileName,
                filePath = document.FilePath
            });

        }
    }
}
