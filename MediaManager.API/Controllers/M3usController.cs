using AutoMapper;
using MediaManager.API.Model;
using MediaManager.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
                var volumeExists = await repository.VolumeExistsAsync(moniker);
                if (!volumeExists)
                {
                    logger.LogInformation("[M3usController] Volume with moniker {moniker} not found.", moniker);
                    return NotFound();
                }
                var result = await repository.GetM3usByVolumeAsync(moniker, includeFileEntries);
                if (includeFileEntries)
                {
                    return Ok(mapper.Map<IEnumerable<M3uFileDto>>(result));
                }
                return Ok(mapper.Map<IEnumerable<M3uWithoutFileEntriesDto>>(result));
            }
            catch (Exception ex)
            {
                logger.LogCritical("[M3usController] Exception in GET method Volume with moniker {moniker} '{Message}'.", moniker, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }

        //[HttpGet("{m3uId}", Name = "GetM3uFile")]
        //public async Task<ActionResult<M3uFileDto>> GetM3uFile(string moniker, int m3uId)
        //{
        //    try
        //    {
        //        var volume = volumesDataStore.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
        //        if (volume is null)
        //        {
        //            return NotFound();
        //        }
        //        var m3uFile = volume.M3uFiles.FirstOrDefault(m => m.Id == m3uId);
        //        if (m3uFile is null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(m3uFile);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogCritical("[M3usController] Exception in GET method Volume with moniker {moniker} '{Message}'.", moniker, ex.Message);
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult<M3uFileDto>> CreateM3uFile(string moniker, M3uFileDtoForUpsert m3uFile)
        //{
        //    try
        //    {
        //        var volume = volumesDataStore.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
        //        if (volume is null)
        //        {
        //            return NotFound();
        //        }

        //        var maxId = volumesDataStore
        //            .Volumes.SelectMany(c => c.M3uFiles).Max(x => x.Id);

        //        var m3uFileResponse = new M3uFileDto()
        //        {
        //            Id = ++maxId,
        //            Title = m3uFile.Title,
        //            Created = DateTime.Now,
        //            FilesInM3U = m3uFile.FilesInM3U
        //        };

        //        volume.M3uFiles.Add(m3uFileResponse);

        //        return CreatedAtRoute("GetM3uFile",
        //             new
        //             {
        //                 moniker = moniker,
        //                 m3uId = m3uFileResponse.Id
        //             },
        //             m3uFileResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogCritical("[M3usController] Exception in POST method Volume with moniker {moniker} '{Message}'.", moniker, ex.Message);
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
        //    }
        //}

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
        //        logger.LogCritical("[M3usController] Exception in PUT method Volume with moniker: {moniker}, m3uId: {m3uId}, '{Message}'.", moniker, m3uId, ex.Message);
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
        //        logger.LogCritical("[M3usController] Exception in PATCH method Volume with moniker: {moniker}, m3uId: {m3uId}, '{Message}'.", moniker, m3uId, ex.Message);
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
        //        logger.LogCritical("[M3usController] Exception in DELETE method Volume with moniker: {moniker}, m3uId: {m3uId}, '{Message}'.", moniker, m3uId, ex.Message);
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
        //    }
        //}
    }
}
