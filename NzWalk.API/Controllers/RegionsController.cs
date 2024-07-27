using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalk.API.Data;
using NzWalk.API.Models.Domain;
using NzWalk.API.Models.DTO;
using NzWalk.API.Repositories;

namespace NzWalk.API.Controllers
{
    // this is pointing to http://localhost:8080/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalkDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        //Injecting Repository pattern over here
        public RegionsController(NzWalkDbContext dbContext, IRegionRepository regionRepository, IMapper
             mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
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
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Models to
            // As we are using the AutoMapper below /* */ code is not required
            /*
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
            */
            //var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            // Retrun the DTO back to client
            //return Ok(regionsDto);
            // Just retrun in single statement
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
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
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map or Convert Region Domain to Region DTO
            //var regionsDto = mapper.Map<RegionDto>(regionDomain);
            /*
            var regionsDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            */
            // Return the DTO to client
            //return Ok(regionsDto);
            //Single line mapping
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // Post to create New Region
        // POST: https://localhost:portnumber/api/regions

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or convert DTO to Domina model
            var regionDomain = mapper.Map<Region>(addRegionRequestDto);
            // commenting below code to implement Automapper
            /*
            var regionDomain = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            */
            // Use Domain model to create region in Database
            regionDomain = await regionRepository.CreateAsync(regionDomain);

            //Map doamin model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            /*
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            }; 
            */
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update Region
        // PUT: https://localhost:portnumber/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // MAP DTO to Domain Model
            var regionDomain = mapper.Map<Region>(updateRegionRequestDto);
            // Commenting to use AutoMapper
            /*
            var regionDomain = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
            */

            //check if region exist
            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Convert doamin model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            // Commenting the below code to use AutoMapper
            /*
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl

            };
            */

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
            var regionDomain = await regionRepository.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //If you want to send the deleted item back
            // Map or Cover from Domain to DTO

            var regionDto = mapper.Map<RegionDto>(regionDomain);
            // Commenting below code to use AutoMapper
            /*
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            */
            return Ok(regionDto);
        }
    }
} 
