using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int User_Id { get; set; }

        public string Status { get; set; } = "Active";
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Propiedades de navegación
        public User User { get; set; }
        public List<DataFile> DataFiles { get; set; }
    }
}