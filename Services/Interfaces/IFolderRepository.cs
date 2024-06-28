using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IFolderRepository
    {
        Task<Folder> CreateFolderAsync(CreateFolderDto createFolderDto);
        Task<Folder> UpdateFolderAsync(CreateFolderDto createFolderDto);
        Task<IEnumerable<Folder>> GetAllFoldersAsync();
        Task<Folder> GetByIdFolderAsync(int id);
        Task<bool> DeleteFolderAsync(int id);
    }
}