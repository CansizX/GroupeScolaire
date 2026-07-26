using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeScolaire.Domain.Entities;

public class Tenant
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public bool EstActif { get; set; } = true;
}

