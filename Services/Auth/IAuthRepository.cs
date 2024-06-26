using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services.Auth
{
    public interface IAuthRepository
    {
        // Método para verificar si la contraseña coincide con el hash almacenado
        bool VeriryPassword(string Password, string HashedPassword);

        // Método para generar un token JWT para el usuario proporcionado
        string GenerateToken(User user);
    }
}