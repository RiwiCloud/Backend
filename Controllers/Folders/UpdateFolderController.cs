using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Folders
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateFolderController : ControllerBase
    {

        private readonly IFolderRepository _folderRepository;
        public UpdateFolderController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

         [HttpPut]
         public async Task<IActionResult> UpdateFolder([FromBody] UpdateFolderDto updateFolderDto)
        {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Si el modelo no es válido, retornar un error 400
        }

        var updatedFolder = await _folderRepository.UpdateFolderAsync(updateFolderDto);
        if (updatedFolder == null)
        {
            return NotFound(); // No se encontró la carpeta
        }

        return Ok(updatedFolder); // Retorna la carpeta actualizada
        }
        
    }
}