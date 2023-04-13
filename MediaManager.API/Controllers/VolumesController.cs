using MediaManager.API.Data;
using MediaManager.API.Model;
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
        private readonly VolumesDataStore volumesDataStore;

        public VolumesController(ILogger<VolumesController> logger, VolumesDataStore volumesDataStore)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.volumesDataStore = volumesDataStore ?? throw new ArgumentNullException(nameof(volumesDataStore));
        }

        public ActionResult<IEnumerable<VolumeDto>> GetVolumes(bool includeM3us = false)
        {
            try
            {
                var results = volumesDataStore.Volumes;
                return Ok(results);
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
                var result = volumesDataStore.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
                if (result is null)
                {
                    logger.LogInformation("[VolumesController] Volume with moniker {moniker} not found.", moniker);
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogCritical("[VolumesController] Exception in GET method Volume with moniker: {moniker} '{Message}'.", moniker, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }
    }
}
