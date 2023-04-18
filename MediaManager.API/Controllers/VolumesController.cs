using MediaManager.API.Data;
using MediaManager.API.Helpers;
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

        [HttpGet("{moniker}", Name = "GetVolume")]
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

        [HttpPost]
        public ActionResult<VolumeDto> CreateVolume(VolumeForUpsert volume)
        {
            try
            {
                var maxId = volumesDataStore
                    .Volumes.Max(x => x.Id);

                var moniker = String.IsNullOrWhiteSpace(volume.Moniker) ? volume.Title.GenerateMoniker() : volume.Moniker.Trim().ToLower();

                var existingVolume = volumesDataStore.Volumes.FirstOrDefault(v => v.Moniker.ToLower() == moniker);
                if (existingVolume is not null)
                {
                    return BadRequest($"Volume with Moniker: '{existingVolume.Moniker}' already in use (volumeId={existingVolume.Id}");
                }

                var volumeResponse = new VolumeDto()
                {
                    Id = ++maxId,
                    Title = volume.Title,
                    Created = DateTime.Now,
                    Moniker = moniker
                };

                volumesDataStore.Volumes.Add(volumeResponse);

                return CreatedAtRoute("GetVolume",
                     new
                     {
                         moniker = moniker,
                     },
                     volumeResponse);
            }
            catch (Exception ex)
            {
                logger.LogCritical("[VolumesController] Exception in POST method: '{Message}'.", ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }
    }
}
