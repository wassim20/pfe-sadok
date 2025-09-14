using PfeProject.Domain.Entities;

public class Warehouse
{
    public int Id { get; set; } // Id_magasin
    public string Name { get; set; } // Nom_magasin
    public string Description { get; set; } // Description_magasin
    public bool IsActive { get; set; } = true;


    // 🔗 Relations
    public ICollection<Location> Locations { get; set; } = new HashSet<Location>();
    public ICollection<Picklist> Picklists { get; set; } = new HashSet<Picklist>();
}
