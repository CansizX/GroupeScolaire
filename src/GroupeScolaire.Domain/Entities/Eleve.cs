using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GroupeScolaire.Domain.Entities;

public class Eleve
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public DateOnly DateNaissance { get; set; }
    public Guid ClasseId { get; set; }
}