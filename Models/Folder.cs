using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
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
        public int? ParentFolder_Id { get; set; } 
        public string Status { get; set; } = "Active";
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Propiedades de navegación
        [JsonIgnore]
        public User User { get; set; }

        public Folder ParentFolder { get; set; }
        public IEnumerable<Folder> ChildFolders { get; set; }

        [JsonIgnore]
        public IEnumerable<DataFile> DataFiles { get; set; }
    }
}