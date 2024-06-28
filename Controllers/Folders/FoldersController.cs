using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Folders
{
    [ApiController]
    [Route("api/folders")]
    public class FoldersController : ControllerBase
    {
        // Inyeccion de dependencias
        private readonly IFolderRepository _folderRepository;
        public FoldersController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        // GET api/folders
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllFolders()
        {
            // enviamos los datos al repository
            var folders = await _folderRepository.GetAllFoldersAsync();
            if (folders == null || !folders.Any())
            {
                // Retorna si la respuesta esta vacia
                return NotFound("No se encontraron ningun archivo o carpeta.");
            }
            // Retorna si encontro algo
            return Ok(folders);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdFolders(int id)
        {
            var folder = await _folderRepository.GetByIdFolderAsync(id);
            if (folder == null)
            {
                // Retorna si la respuesta esta vacia
                return NotFound($"No se encontraron archivos con el Id {id}.");
            }
            // Retorna si encontro algo
            return Ok(folder);
        }
    }
}