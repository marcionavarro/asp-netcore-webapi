using MinhasTarefasAPI.V1.Models;
using System;
using System.Collections.Generic;

namespace MinhasTarefasAPI.Repositories.V1.Contracts
{
    public interface ITarefaRepository
    {
        List<Tarefa> Sincronizacao(List<Tarefa> tarefas);
        List<Tarefa> Restauracao(ApplicationUser usuario,  DateTime dataUltimaSincronizacao);
    }
}
