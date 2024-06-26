using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos
{
    public class CreateFilesDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Folder_Id { get; set; }
    }
}