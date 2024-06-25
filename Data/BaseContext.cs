using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class BaseContext: DbContext
    {
        //Construtor de la conexion 
        public BaseContext(DbContextOptions<BaseContext> options) : base(options){}

        //Registramos los modelos 
        public DbSet<DataFile> DataFiles { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}