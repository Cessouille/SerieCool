namespace SerieCool.Models.EntityFramework;

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

    public Serie(string titre, string? resume, int? nbsaisons, int? nbepisodes, int? anneecreation, string? network)
    {
        Titre = titre;
        Resume = resume;
        Nbsaisons = nbsaisons;
        Nbepisodes = nbepisodes;
        Anneecreation = anneecreation;
        Network = network;
    }

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

    public override int GetHashCode()
    {
        return HashCode.Combine(Serieid, Titre, Resume, Nbsaisons, Nbepisodes, Anneecreation, Network);
    }
}
