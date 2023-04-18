using AutoMapper;
using MediaManager.API.Data;
using MediaManager.API.Helpers;
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
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper;
        }

        public async Task< ActionResult<IEnumerable<VolumeWithoutM3us>>> GetVolumes()
        {
            try
            {
                var results = await repository.GetVolumesAsync();
                return Ok(mapper.Map<IEnumerable<VolumeWithoutM3us>>(results));
            }
            catch (Exception ex)
            {
                logger.LogCritical("[VolumesController] Exception in GET Volumes method '{Message}'.", ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }

        [HttpGet("{moniker}", Name = "GetVolume")]
        public async Task< ActionResult<VolumeDto>> GetVolume(string moniker, bool includeM3us=false)
        {
            try
            {
                var result = await repository.GetVolumeAsync(moniker, includeM3us);
                if (result is null)
                {
                    logger.LogInformation("[VolumesController] Volume with moniker {moniker} not found.", moniker);
                    return NotFound();
                }
                if (includeM3us)
                {
                    return Ok(mapper.Map<VolumeDto>(result));
                }
                return Ok(mapper.Map<VolumeWithoutM3us>(result));
            }
            catch (Exception ex)
            {
                logger.LogCritical("[VolumesController] Exception in GET method Volume with moniker: {moniker} '{Message}'.", moniker, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }

        //[HttpPost]
        //public async Task< ActionResult<VolumeDto>> CreateVolume(VolumeForUpsert volume)
        //{
        //    try
        //    {
        //        var maxId = volumesDataStore
        //            .Volumes.Max(x => x.Id);

        //        var moniker = String.IsNullOrWhiteSpace(volume.Moniker) ? volume.Title.GenerateMoniker() : volume.Moniker.Trim().ToLower();

        //        var existingVolume = volumesDataStore.Volumes.FirstOrDefault(v => v.Moniker.ToLower() == moniker);
        //        if (existingVolume is not null)
        //        {
        //            return BadRequest($"Volume with Moniker: '{existingVolume.Moniker}' already in use (volumeId={existingVolume.Id}");
        //        }

        //        var volumeResponse = new VolumeDto()
        //        {
        //            Id = ++maxId,
        //            Title = volume.Title,
        //            Created = DateTime.Now,
        //            Moniker = moniker
        //        };

        //        volumesDataStore.Volumes.Add(volumeResponse);

        //        return CreatedAtRoute("GetVolume",
        //             new
        //             {
        //                 moniker = moniker,
        //             },
        //             volumeResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogCritical("[VolumesController] Exception in POST method: '{Message}'.", ex.Message);
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
        //    }
        //}
    }
}
