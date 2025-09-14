using PfeProject.Domain.Entities;
public class Status
{
    public int Id { get; set; }
    public string Description { get; set; }

    public ICollection<Picklist> Picklists { get; set; } = [];
    public ICollection<ReturnLine> ReturnLines { get; set; } = [];
    public ICollection<PicklistUs> PicklistUsList { get; set; } = [];
    public ICollection<DetailPicklist> DetailPicklists { get; set; } = [];
}
