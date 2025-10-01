﻿
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhasTarefasAPI.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string FullName { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
