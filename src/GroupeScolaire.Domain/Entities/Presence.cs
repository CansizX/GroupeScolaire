using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeScolaire.Domain.Entities;

public class Presence
{
    public Guid Id { get; set; }
    public Guid PersonneId { get; set; }
    public string TypePersonne { get; set; } = string.Empty;
    public DateTime DateHeure { get; set; }
    public string Statut { get; set; } = string.Empty;
}