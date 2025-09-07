using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerDeviceController : ControllerBase
    {
        private readonly AgroManager.Data.AgroDBContext agroDBContext;
                public ControllerDeviceController(AgroManager.Data.AgroDBContext agroDBContext)
        {
            this.agroDBContext = agroDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetControllerDevices()
        {
            var controllerDevices = await agroDBContext.ControllerDevices.ToListAsync();
            List<Models.DTO.ControllerDeviceDTO> controllerDeviceDTOs = new List<Models.DTO.ControllerDeviceDTO>();
            // convert domain to dto
            foreach (var device in controllerDevices)
            {
                controllerDeviceDTOs.Add(new Models.DTO.ControllerDeviceDTO
                {
                    Id = device.Id,
                    Name = device.Name,
                    Model = device.Model,
                    InstallationDate = device.InstallationDate,
                    FirmwareVersion = device.FirmwareVersion,
                    OwnerUserId = device.OwnerUserId
                });
         
            }
            return Ok(controllerDeviceDTOs);
        }
        [HttpGet]
        [Route("{id:guid}")]    
        public async Task<IActionResult> GetControllerDeviceById([FromRoute] Guid id)
        {
            var controllerDevice = await agroDBContext.ControllerDevices.FirstOrDefaultAsync(x => x.Id == id);
            if (controllerDevice == null)
            {
                return NotFound();
            }
            //convert domain to dto
            var controllerDeviceDTO = new Models.DTO.ControllerDeviceDTO
            {
                Id = controllerDevice.Id,
                Name = controllerDevice.Name,
                Model = controllerDevice.Model,
                InstallationDate = controllerDevice.InstallationDate,
                FirmwareVersion = controllerDevice.FirmwareVersion,
                OwnerUserId = controllerDevice.OwnerUserId
            };
            return Ok(controllerDeviceDTO);
        }
        [HttpGet]
        [Route("field/{fieldId:guid}")]
        public async Task<IActionResult> GetControllerDevicesByFieldId([FromRoute] Guid fieldId)
        {
            var controllerDevices = await agroDBContext.ControllerDevices.Where (x => x.FieldId == fieldId).ToListAsync();
            return Ok(controllerDevices);
        }
        [HttpPost]
        public async Task<IActionResult> AddControllerDevice(Models.DTO.AddControllerDeviceDTO addControllerDeviceDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var controllerDevice = new Models.Domain.ControllerDevice
            {
                Id = Guid.NewGuid(),
                Name = addControllerDeviceDTO.Name,
                Model = addControllerDeviceDTO.Model,
                InstallationDate = addControllerDeviceDTO.InstallationDate,
                FirmwareVersion = addControllerDeviceDTO.FirmwareVersion,
                OwnerUserId = addControllerDeviceDTO.OwnerUserId // Placeholder, replace with actual user ID
            };
            await agroDBContext.ControllerDevices.AddAsync(controllerDevice);
           await agroDBContext.SaveChangesAsync();
            //convert domain to dto
            addControllerDeviceDTO = new Models.DTO.AddControllerDeviceDTO
            {
                Name = controllerDevice.Name,
                Model = controllerDevice.Model,
                InstallationDate = controllerDevice.InstallationDate,
                FirmwareVersion = controllerDevice.FirmwareVersion,
                OwnerUserId = controllerDevice.OwnerUserId
            };
            return CreatedAtAction(nameof(GetControllerDeviceById), new { id = controllerDevice.Id }, controllerDevice);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateControllerDevice([FromRoute] Guid id, Models.DTO.UpdateControllerDeviceDTO updateControllerDeviceDTO)
        {
            var controllerDevice = await agroDBContext.ControllerDevices.FirstOrDefaultAsync(x => x.Id == id);
            if (controllerDevice == null)
            {
                return NotFound();
            }
            controllerDevice.Name = updateControllerDeviceDTO.Name;
            controllerDevice.Model = updateControllerDeviceDTO.Model;
            controllerDevice.InstallationDate = updateControllerDeviceDTO.InstallationDate;
            controllerDevice.FirmwareVersion = updateControllerDeviceDTO.FirmwareVersion;
            await agroDBContext.SaveChangesAsync();
            //convert domain to dto
            updateControllerDeviceDTO = new Models.DTO.UpdateControllerDeviceDTO
            {
                Name = controllerDevice.Name,
                Model = controllerDevice.Model,
                InstallationDate = controllerDevice.InstallationDate,
                FirmwareVersion = controllerDevice.FirmwareVersion
            };
            return NoContent();
        }
        [HttpDelete]
        [Route("{id:guid}")]    
        public async Task<IActionResult> DeleteControllerDevice([FromRoute] Guid id)
        {
            var controllerDevice = await agroDBContext.ControllerDevices.FirstOrDefaultAsync(x => x.Id == id);
            if (controllerDevice == null)
            {
                return NotFound();
            }
             agroDBContext.ControllerDevices.Remove(controllerDevice);
            await agroDBContext.SaveChangesAsync();
            return NoContent();
        }
            

    }
}
