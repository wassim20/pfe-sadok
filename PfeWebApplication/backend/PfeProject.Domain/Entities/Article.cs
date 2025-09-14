public class Article
{
    public int Id { get; set; } // Id_article
    public string CodeProduit { get; set; }
    public string Designation { get; set; }
    public DateTime DateAjout { get; set; }
    public bool IsActive { get; set; } = true;

    // 🔁 Relations
    public ICollection<DetailPicklist> DetailPicklists { get; set; } = new HashSet<DetailPicklist>();
    public ICollection<ReturnLine> ReturnLines { get; set; } = new HashSet<ReturnLine>();
}
