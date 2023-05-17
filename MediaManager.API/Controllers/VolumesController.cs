using AutoMapper;
using MediaManager.API.Data;
using MediaManager.API.Model;
using MediaManager.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaManager.API.Controllers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="VolumesController.cs" company="IRANYE">
    //   Copyright (c) IRANYE. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------
    [ApiController]
    [Route("api/volumes")]
    public class VolumesController : ControllerBase
    {
        private readonly ILogger<VolumesController> logger;
        private readonly IMediaManagerRepository repository;
        private readonly IMapper mapper;

        public VolumesController(ILogger<VolumesController> logger, IMediaManagerRepository repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<VolumeWithoutM3usDto>>> GetVolumes()
        {
            try
            {
                var results = await repository.GetVolumesAsync();
                return Ok(mapper.Map<IEnumerable<VolumeWithoutM3usDto>>(results));
            }
            catch (Exception ex)
            {
                logger.LogCritical("[VolumesController] Exception in GET Volumes method '{Message}'.", ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }

        [HttpGet("{moniker}")]
        public ActionResult<VolumeDto> GetVolume(string moniker)
        {
            try
            {
                var result = VolumesDataStore.Current.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
