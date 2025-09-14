public class Line
{
    public int Id { get; set; } // Id_ligne
    public string Description { get; set; } // Description_ligne
    public bool IsActive { get; set; } = true;


    // 🔁 Relation : une ligne peut avoir plusieurs picklistes
    public ICollection<Picklist> Picklists { get; set; } = new HashSet<Picklist>();
}
