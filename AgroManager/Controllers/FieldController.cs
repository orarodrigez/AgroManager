using AgroManager.Data;
using AgroManager.Models.Domain;
using AgroManager.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace AgroManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {

        private readonly AgroDBContext agroDBContext;
        public FieldController(AgroDBContext agroDBContext)
        {
            this.agroDBContext = agroDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetFields()
        {
       
            var fieldDomain = await agroDBContext.Fields.ToListAsync();
            var fieldDto = new List<Field>();
            foreach (var field in fieldDomain) {
                fieldDto.Add(new Field
                {
                    Id = field.Id,
                    Name = field.Name,
                    Area = field.Area,
                    CropType = field.CropType,
                    LastFertilizationDate = field.LastFertilizationDate,
                    LastIrrigationDate = field.LastIrrigationDate,
                    OwnerUserId = field.OwnerUserId,
                
                });
            }
            // Placeholder implementation
            return Ok(fieldDomain);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetFieldById([FromRoute] Guid id)
        {
            var fieldDomain = await agroDBContext.Fields.FirstOrDefaultAsync(x=>x.Id==id);
            if (fieldDomain == null)
            {
                return NotFound();
            }
            var fieldDto = new FieldDTO
                {
                    Id = fieldDomain.Id,
                    Name = fieldDomain.Name,
                    Area = fieldDomain.Area,
                    CropType = fieldDomain.CropType,
                    LastFertilizationDate = fieldDomain.LastFertilizationDate,
                    LastIrrigationDate = fieldDomain.LastIrrigationDate,
                    OwnerUserId = fieldDomain.OwnerUserId,
                   
                };
            
            // Placeholder implementation
            return Ok(fieldDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddFieldDTO field)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //map or convert dto to domain
            var fieldDomain = new Field
            {
                Id = Guid.NewGuid(),
                Name = field.Name,
                Area = field.Area,
                CropType = field.CropType,
                LastFertilizationDate = field.LastFertilizationDate,
                LastIrrigationDate = field.LastIrrigationDate,
                OwnerUserId = field.OwnerUserId,
              
            };
            await agroDBContext.Fields.AddAsync(fieldDomain);
            await agroDBContext.SaveChangesAsync();
            //map Domain back to dto
            var fieldDto = new FieldDTO
            {
                Id = fieldDomain.Id,
                Name = fieldDomain.Name,
                Area = fieldDomain.Area,
                CropType = fieldDomain.CropType,
                LastFertilizationDate = fieldDomain.LastFertilizationDate,
                LastIrrigationDate = fieldDomain.LastIrrigationDate,
                OwnerUserId = fieldDomain.OwnerUserId,
               
            };
            return CreatedAtAction(nameof(GetFieldById), new { id = fieldDomain.Id }, fieldDomain);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateFieldDTO field)
        {
            var fieldDomain = await agroDBContext.Fields.FirstOrDefaultAsync(x => x.Id == id);
            if (fieldDomain == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //map or convert dto to domain
            fieldDomain.Name = field.Name;
            fieldDomain.Area = field.Area;
            fieldDomain.CropType = field.CropType;
            fieldDomain.LastFertilizationDate = field.LastFertilizationDate;
            fieldDomain.LastIrrigationDate = field.LastIrrigationDate;
            fieldDomain.OwnerUserId = field.OwnerUserId;
           
            await agroDBContext.SaveChangesAsync();
            //map Domain back to dto
            var fieldDto = new FieldDTO
            {
                Id = fieldDomain.Id,
                Name = fieldDomain.Name,
                Area = fieldDomain.Area,
                CropType = fieldDomain.CropType,
                LastFertilizationDate = fieldDomain.LastFertilizationDate,
                LastIrrigationDate = fieldDomain.LastIrrigationDate,
                OwnerUserId = fieldDomain.OwnerUserId
               
            };
            return Ok(fieldDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var fieldDomain = await agroDBContext.Fields.FirstOrDefaultAsync(x => x.Id == id);
            if (fieldDomain == null)
            {
                return NotFound();
            }
            agroDBContext.Remove(fieldDomain);
            await agroDBContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
