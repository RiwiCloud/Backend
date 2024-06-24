using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backeng.Models;
using Microsoft.EntityFrameworkCore;

namespace Backeng.Data
{
    public class BaseContext: DbContext
    {
         public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
          
        }
        public DbSet<User> Users { get; set; }
    }
}