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
            // Relacion entre DataFile y Folder
            modelBuilder.Entity<DataFile>()
                .HasOne(d => d.Folder)
                .WithMany(f => f.DataFiles)
                .HasForeignKey(d => d.Folder_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Folder y User
            modelBuilder.Entity<Folder>()
                .HasOne(f => f.User)
                .WithMany(u => u.Folders)
                .HasForeignKey(f => f.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre User y Folder
            modelBuilder.Entity<User>()
                .HasMany(u => u.Folders)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.User_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}