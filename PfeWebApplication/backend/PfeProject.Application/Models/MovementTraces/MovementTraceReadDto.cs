public class MovementTraceReadDto
{
    public int Id { get; set; }
    public string UsNom { get; set; }
    public string Quantite { get; set; }
    public DateTime DateMouvement { get; set; }
    public int UserId { get; set; }
    public int DetailPicklistId { get; set; }
    public bool IsActive { get; set; }
}
