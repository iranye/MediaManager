using AutoMapper;
using MediaManager.API.Model;
using MediaManager.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaManager.API.Controllers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="FileEntriesController.cs" company="IRANYE">
    //   Copyright (c) IRANYE. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/m3us/{m3uId}/fileentries")]
    public class FileEntriesController : ControllerBase
    {
        private readonly ILogger<FileEntriesController> logger;
        private readonly IMediaManagerRepository repository;
        private readonly IMapper mapper;

        public FileEntriesController(ILogger<FileEntriesController> logger, IMediaManagerRepository repository, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileEntryDto>>> GetM3uFiles(int m3uId)
        {
            try
            {
                var m3u = await repository.GetM3uByIdAsync(m3uId);

                if (m3u is null)
                {
                    logger.LogInformation("[FileEntriesController] M3u with id {m3uId} not found.", m3uId);
                    return NotFound($"M3u for id={m3uId} not found");
                }

                return Ok(mapper.Map<IEnumerable<FileEntryDto>>(m3u.FilesInM3U));
            }
            catch (Exception ex)
            {
                logger.LogCritical("[FileEntriesController] Exception in GET method FileEntries with m3uId '{m3uId}' '{Message}'.", m3uId, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
        }

        [HttpDelete("{fileEntryId}")]
        public async Task<ActionResult> DeletePointOfInterest(int m3uId, int fileEntryId)
        {
            try
            {
                var m3u = await repository.GetM3uByIdAsync(m3uId);

                if (m3u is null)
                {
                    logger.LogInformation("[FileEntriesController] M3u with id '{m3uId}' not found.", m3uId);
                    return NotFound($"M3u for id={m3uId} not found");
                }

                var fileEntryFromStore = m3u.FilesInM3U.FirstOrDefault(f => f.Id == fileEntryId);
                if (fileEntryFromStore == null)
                {
                    logger.LogInformation("[FileEntriesController] FileEntry with m3uId '{m3uId}' and FileEntryId '{fileEntryId}' not found.", m3uId, fileEntryId);
                    return NotFound($"FileEntry with m3uId '{m3uId}' and FileEntryId '{fileEntryId}' not found.");
                }

                m3u.FilesInM3U.Remove(fileEntryFromStore);
                m3u.LastModified = new DateTimeOffset(DateTime.UtcNow);
                if (await repository.SaveChangesAsync())
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("[FileEntriesController] Exception in DELETE method City with m3uId {m3uId}, fileEntryId: {fileEntryId} '{Message}'.", m3uId, fileEntryId, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
            }
            return BadRequest("Failed to update M3u (or no changes to apply)");
        }
    }
}

