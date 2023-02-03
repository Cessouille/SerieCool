using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SerieCool.Models.EntityFramework;

[Table("serie")]
public partial class Serie
{
    public Serie(int serieid, string titre, string? resume, int? nbsaisons, int? nbepisodes, int? anneecreation, string? network)
    {
        Serieid = serieid;
        Titre = titre;
        Resume = resume;
        Nbsaisons = nbsaisons;
        Nbepisodes = nbepisodes;
        Anneecreation = anneecreation;
        Network = network;
    }

    [Key]
    [Column("serieid")]
    public int Serieid { get; set; }

    [Column("titre")]
    [StringLength(100)]
    public string Titre { get; set; } = null!;

    [Column("resume")]
    public string? Resume { get; set; }

    [Column("nbsaisons")]
    public int? Nbsaisons { get; set; }

    [Column("nbepisodes")]
    public int? Nbepisodes { get; set; }

    [Column("anneecreation")]
    public int? Anneecreation { get; set; }

    [Column("network")]
    [StringLength(50)]
    public string? Network { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Serie serie &&
               Serieid == serie.Serieid &&
               Titre == serie.Titre &&
               Resume == serie.Resume &&
               Nbsaisons == serie.Nbsaisons &&
               Nbepisodes == serie.Nbepisodes &&
               Anneecreation == serie.Anneecreation &&
               Network == serie.Network;
    }
}
