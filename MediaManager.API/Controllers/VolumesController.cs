﻿using AutoMapper;
using MediaManager.API.Data.Entities;
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

        public async Task<ActionResult<IEnumerable<VolumeWithoutM3us>>> GetVolumes()
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
        public async Task<IActionResult> GetVolume(string moniker, bool includeM3us=false)
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

        [HttpPost]
        public async Task<ActionResult<VolumeDto>> CreateVolume(VolumeForUpsert volume)
        {
            try
            {
                var moniker = String.IsNullOrWhiteSpace(volume.Moniker) ? volume.Title.GenerateMoniker() : volume.Moniker.Trim().ToLower();

                var existingVolume = await repository.GetVolumeAsync(moniker);
                if (existingVolume is not null)
                {
                    return BadRequest($"Volume with Moniker: '{existingVolume.Moniker}' already in use (volumeId={existingVolume.Id}");
                }

                var volumeModel = mapper.Map<Volume>(volume);

                repository.AddVolume(volumeModel);
                await repository.SaveChangesAsync();

                var newVolume = mapper.Map<VolumeDto>(volumeModel);

                return CreatedAtRoute("GetVolume",
                     new
                     {
                         moniker = moniker,
                     },
                     newVolume);
            }
            catch (Exception ex)
            {
                logger.LogCritical("[VolumesController] Exception in POST method: '{Message}'.", ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }
    }
}
