using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class DataFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int File_Id { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime DateCreated { get; set; } = DateTime.Now;

        //Propiedades de navegación
        public Folder Folder { get; set; }
    }
}