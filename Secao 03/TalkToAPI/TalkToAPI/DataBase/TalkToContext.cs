using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkToAPI.V1.Models;

namespace TalkToAPI.DataBase
{
    public class TalkToContext : IdentityDbContext<AplicationUser>
    {
        public TalkToContext(DbContextOptions<TalkToContext> options) : base(options)
        { 
        
        }

        public DbSet<Mensagem> Mensagem { get; set; }
    }
}
