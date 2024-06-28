using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class FolderRepository : IFolderRepository
    {
        // Inyeccion de dependencias de la base de datos
        private readonly BaseContext _baseContext;
        private readonly IMapper _mapper;
        public FolderRepository(BaseContext baseContext, IMapper mapper)
        {
            _baseContext = baseContext;
            _mapper = mapper;
        } 

        #region  Metodo para crea un Folder
        public async Task<Folder> CreateFolderAsync(CreateFolderDto createFolderDto)
        {
            // Creamos un nuevo Folder y lo mapeamos con los datos del Dto
            var folder = _mapper.Map<Folder>(createFolderDto);

            // Añadimos propiedades adicionales
            folder.DateCreated = DateTime.UtcNow;
            folder.Status = "Active";

            // Añadimos el newFolder a la base de datos
            _baseContext.Folders.Add(folder);
            await _baseContext.SaveChangesAsync();
            return folder;
        }

        public async Task<IEnumerable<Folder>> GetAllFoldersAsync()
        {
            // Buscamos los objetos en la base de datos
            var folders = await _baseContext.Folders
                                    .Include(f => f.DataFiles) // Incluir los archivos de datos si es necesario
                                    .Include(f => f.ParentFolder) // Incluir la carpeta padre si es necesario
                                    .ToListAsync();

            if (folders == null || !folders.Any())
            {
                // REtorna una colección vacia
                return Enumerable.Empty<Folder>();
            }

            // retorna la colección con los objetos encontrados
            return folders;
        }

        public Task<Folder> GetByIdFolderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Folder> UpdateFolderAsync(CreateFolderDto createFolderDto)
        {
            throw new NotImplementedException();
        }
        #endregion


          public async Task<bool> DeleteFolderAsync(int id)
    {
        // Encuentra la carpeta en la base de datos
        var folder = await _baseContext.Folders
                                       .Include(f => f.ChildFolders)
                                       .Include(f => f.DataFiles)
                                       .FirstOrDefaultAsync(f => f.Id == id);
        if (folder == null)
        {
            return false; // No se encontró la carpeta
        }

        // Elimina los archivos de datos relacionados
        if (folder.DataFiles != null && folder.DataFiles.Any())
        {
            _baseContext.DataFiles.RemoveRange(folder.DataFiles);
        }

        // Elimina las carpetas hijas relacionadas
        if (folder.ChildFolders != null && folder.ChildFolders.Any())
        {
            foreach (var childFolder in folder.ChildFolders)
            {
                await DeleteFolderAsync(childFolder.Id);
            }
        }

        // Elimina la carpeta
        _baseContext.Folders.Remove(folder);
        await _baseContext.SaveChangesAsync();

        return true; // Eliminación exitosa
    }


    }
}