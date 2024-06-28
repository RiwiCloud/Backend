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

        // Configuracion de relaciones entre tablas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación User-Folder (uno a muchos)
            modelBuilder.Entity<Folder>()
                .HasOne(f => f.User)
                .WithMany(u => u.Folders)
                .HasForeignKey(f => f.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación Folder-DataFile (uno a muchos)
            modelBuilder.Entity<DataFile>()
                .HasOne(df => df.Folder)
                .WithMany(f => f.DataFiles)
                .HasForeignKey(df => df.Folder_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación Folder-ParentFolder (auto-referencia, uno a muchos)
            modelBuilder.Entity<Folder>()
                .HasOne(f => f.ParentFolder)
                .WithMany(f => f.ChildFolders)
                .HasForeignKey(f => f.ParentFolder_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar la propiedad de navegación inversa para las carpetas hijas
            modelBuilder.Entity<Folder>()
                .HasMany(f => f.ChildFolders)
                .WithOne(f => f.ParentFolder)
                .HasForeignKey(f => f.ParentFolder_Id);
        }
    }
}