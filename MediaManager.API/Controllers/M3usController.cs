using AutoMapper;
using MediaManager.API.Data.Entities;
using MediaManager.API.Model;
using MediaManager.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace MediaManager.API.Controllers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="M3usController.cs" company="IRANYE">
    //   Copyright (c) IRANYE. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------
    [ApiController]
    [Route("api/volumes/{moniker}/m3us")]
    public class M3usController:ControllerBase
    {
        private readonly ILogger<M3usController> logger;
        private readonly IMediaManagerRepository repository;
        private readonly IMapper mapper;

        public M3usController(ILogger<M3usController> logger, IMediaManagerRepository repository, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetM3uFiles(string moniker, bool includeFileEntries=false)
        {
            try
            {
                IEnumerable<M3uFile>? result = null;
                if (moniker.Trim().ToLower() == "all")
                {
                    result = await repository.GetM3usAsync(includeFileEntries);
                }
                else
                {
                    var volumeExists = await repository.VolumeExistsAsync(moniker);
                    if (!volumeExists)
                    {
                        logger.LogInformation("[M3usController] Volume with moniker '{moniker}' not found.", moniker);
                        return NotFound();
                    }
                    result = await repository.GetM3usByVolumeAsync(moniker, includeFileEntries);
                }
                if (includeFileEntries)
                {
                    return Ok(mapper.Map<IEnumerable<M3uFileDto>>(result));
                }
                return Ok(mapper.Map<IEnumerable<M3uWithoutFileEntriesDto>>(result));
            }
            catch (Exception ex)
            {
                logger.LogCritical("[M3usController] Exception in GET method Volume with moniker '{moniker}' '{Message}'.", moniker, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }

        [HttpGet("{m3uId}", Name = "GetM3uFile")]
        public async Task<ActionResult<M3uFileDto>> GetM3uFile(string moniker, int m3uId)
        {
            try
            {
                var result = await repository.GetM3uByIdAsync(m3uId);
                if (result == null)
                {
                    logger.LogInformation("[M3usController] M3u with Id {m3uId} not found.", m3uId);
                    return NotFound();
                }
                if (moniker.Trim().ToLower() != "all")
                {
                    if (result != null && result.Volume?.Moniker == moniker)
                    {
                        return Ok(mapper.Map<M3uFileDto>(result));
                    }
                    var volumeExists = await repository.VolumeExistsAsync(moniker);
                    if (!volumeExists)
                    {
                        logger.LogInformation("[M3usController] Volume '{moniker}' not found.", moniker);
                        return NotFound();
                    }
                    else
                    {
                        logger.LogInformation("[M3usController] Volume '{moniker}' with M3ufile Id '{m3uId}' not found.", moniker, m3uId);
                        return NotFound();
                    }
                }
                return Ok(mapper.Map<M3uFileDto>(result));
            }
            catch (Exception ex)
            {
                logger.LogCritical("[M3usController] Exception in GET method Volume with moniker '{moniker}' '{Message}'.", moniker, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<M3uFileDto>> CreateM3uFile(string moniker, M3uFileDtoForUpsert m3uFile)
        {
            try
            {
                if (m3uFile.FilesInM3U.Any(f => f.Id > 0))
                {
                    return BadRequest($"Id(s) must be 0 for INSERT");
                }
                var m3uIsAttached = moniker.Trim().ToLower() != "all";
                Volume? volume = m3uIsAttached ? await repository.GetVolumeAsync(moniker) : null;
                if (m3uIsAttached)
                {
                    if (volume is null)
                    {
                        logger.LogInformation("[M3usController] Volume with moniker '{moniker}' not found.", moniker);
                        return NotFound($"Volume with moniker '{moniker}' not found.");
                    }
                }

                // TODO: support "force" op to allow creation of duplicate M3u(s).
                var m3uExists = await repository.M3uExistsAsync(m3uFile.Title);
                if (m3uExists)
                {
                    return BadRequest($"M3u with Title: '{m3uFile.Title}' already in use");
                }

                var finalM3u = mapper.Map<M3uFile>(m3uFile);
                finalM3u.Volume = volume;
                repository.Add(finalM3u);
                if (await repository.SaveChangesAsync())
                {
                    return CreatedAtRoute("GetM3uFile",
                         new
                         {
                             moniker = moniker,
                             m3uId = finalM3u.Id
                         },
                         finalM3u);
                }

                return BadRequest("Failed to save new M3u");
            }
            catch (Exception ex)
            {
                logger.LogCritical("[M3usController] Exception in POST method Volume with moniker '{moniker}' '{Message}'.", moniker, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }

        //[HttpPut("{m3uId}")]
        //public async Task<ActionResult> UpdateM3u(string moniker, int m3uId, M3uFileDtoForUpsert m3uFile)
        //{
        //    try
        //    {
        //        var volume = volumesDataStore.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
        //        if (volume is null)
        //        {
        //            return NotFound();
        //        }

        //        // find point of interest
        //        var m3uFileFromStore = volume.M3uFiles.FirstOrDefault(c => c.Id == m3uId);
        //        if (m3uFileFromStore == null)
        //        {
        //            return NotFound();
        //        }

        //        m3uFileFromStore.Title = m3uFile.Title;
        //        m3uFileFromStore.FilesInM3U = m3uFile.FilesInM3U;
        //        m3uFileFromStore.LastModified = DateTime.Now;

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogCritical("[M3usController] Exception in PUT method Volume with moniker: '{moniker}', m3uId: {m3uId}, '{Message}'.", moniker, m3uId, ex.Message);
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
        //    }
        //}

        //[HttpPatch("{m3uId}")]
        //public async Task<ActionResult> PartiallyUpdateM3uFile(string moniker, int m3uId, JsonPatchDocument<M3uFileDtoForUpsert> patchDocument)
        //{
        //    try
        //    {
        //        var volume = volumesDataStore.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
        //        if (volume is null)
        //        {
        //            return NotFound();
        //        }

        //        var m3uFileFromStore = volume.M3uFiles.FirstOrDefault(c => c.Id == m3uId);
        //        if (m3uFileFromStore == null)
        //        {
        //            return NotFound();
        //        }

        //        var m3uFileToPatch = new M3uFileDtoForUpsert()
        //        {
        //            Title = m3uFileFromStore.Title,
        //            FilesInM3U = m3uFileFromStore.FilesInM3U
        //        };

        //        patchDocument.ApplyTo(m3uFileToPatch, ModelState);

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        if (!TryValidateModel(m3uFileToPatch))
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        m3uFileFromStore.Title = m3uFileToPatch.Title;
        //        m3uFileFromStore.FilesInM3U = m3uFileToPatch.FilesInM3U;
        //        m3uFileFromStore.LastModified = DateTime.Now;

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogCritical("[M3usController] Exception in PATCH method Volume with moniker: '{moniker}', m3uId: {m3uId}, '{Message}'.", moniker, m3uId, ex.Message);
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
        //    }
        //}

        //[HttpDelete("{m3uId}")]
        //public async Task<ActionResult> DeleteM3uFile(string moniker, int m3uId)
        //{
        //    try
        //    {
        //        var volume = volumesDataStore.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
        //        if (volume is null)
        //        {
        //            return NotFound();
        //        }

        //        var m3uFileFromStore = volume.M3uFiles.FirstOrDefault(c => c.Id == m3uId);
        //        if (m3uFileFromStore == null)
        //        {
        //            return NotFound();
        //        }

        //        volume.M3uFiles.Remove(m3uFileFromStore);
        //        return NoContent();
        //    }                
        //    catch (Exception ex)
        //    {
        //        logger.LogCritical("[M3usController] Exception in DELETE method Volume with moniker: '{moniker}', m3uId: {m3uId}, '{Message}'.", moniker, m3uId, ex.Message);
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
        //    }
        //}
    }
}
