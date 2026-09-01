using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Domain.Entities;

public class Category
{
    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Active = true;
    }

    public Guid Id { get; init; }
    public string Name { get; private set; }
    public bool Active { get; private set; }

    public void AtualizarNome(string name)
    {
        Name = name;
    }

    public void Desativar()
    {
        Active = false;
    }
}
