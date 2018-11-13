using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Models
{
    public class TodoContext : DbContext
    {
        //Isto serve para que tenhamos acesso ao contexto por meio dos controllers MVC
        //via DI
        public TodoContext(DbContextOptions<TodoContext> options)
            :base(options)
        {
        }

        public DbSet<TodoItem> Items { get; set; }
    }
}
