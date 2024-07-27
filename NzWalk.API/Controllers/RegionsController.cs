using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalk.API.Data;
using NzWalk.API.Models.Domain;
using NzWalk.API.Models.DTO;

namespace NzWalk.API.Controllers
{
    // this is pointing to http://localhost:8080/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalkDbContext dbContext;

        public RegionsController(NzWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // Get all regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Ackland Region",
            //        Code = "AKL",
            //        RegionImageUrl = "abc"
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Wellington Region",
            //        Code = "WLG",
            //        RegionImageUrl = "abc"
            //    }
            //};

            //Get Data from Database - Using Domain Models
            var regionsDomain = await dbContext.Regions.ToListAsync();

            //Map Domain Models to DTO
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            // Retrun the DTO back to client
            return Ok(regionsDto);
        }

        // Get single region by Id
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Find only use Primary key 
            //var region = dbContext.Regions.Find(id);

            //this way can be done for all the tables fields 
            // Get region Domain modle from Database
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map or Convert Region Domain to Region DTO

            var regionsDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return the DTO to client
            return Ok(regionsDto);
        }

        // Post to create New Region
        // POST: https://localhost:portnumber/api/regions

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or convert DTO to Domina model
            var regionDomain = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain model to create region in Database
            await dbContext.Regions.AddAsync(regionDomain);
            await dbContext.SaveChangesAsync();

            //Map doamin model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            }; 
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update Region
        // PUT: https://localhost:portnumber/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //check if region exist
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            // MAP DTO to Domain Model
            regionDomain.Code = updateRegionRequestDto.Code;
            regionDomain.Name = updateRegionRequestDto.Name;
            regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            //Convert doamin model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl

            };

            // Pass DTO back to client
            return Ok(regionDto);

        }


        //Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            //Check if region exist
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //Delete region Remember Remove does not have Asnyc fuction
            dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();

            //If you want to send the deleted item back
            // Map or Cover from Domain to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
} 
