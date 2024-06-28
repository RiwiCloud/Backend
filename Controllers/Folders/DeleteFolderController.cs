using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers.Folders
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteFolderController : ControllerBase
    {

        private readonly IFolderRepository _folderRepository;
        public DeleteFolderController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

           [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            var result = await _folderRepository.DeleteFolderAsync(id);
            if (!result)
            {
                return NotFound(); // No se encontró la carpeta
            }

            return NoContent(); // Eliminación exitosa
        }
    }
}