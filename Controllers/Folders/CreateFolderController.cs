using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Folders
{
    [ApiController]
    [Route("api/CreateFolder")]
    public class CreateFolderController : ControllerBase
    {
        private readonly IFolderRepository _folderRepository;
        public CreateFolderController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        // POST api/CreateFolder
        [HttpPost]
        public async Task<ActionResult<Folder>> CreateFolder(CreateFolderDto createFolderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newFolder = await _folderRepository.CreateFolderAsync(createFolderDto);
                return Ok(newFolder);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error creating folder: {ex.Message}");
            }
        }
    }
}