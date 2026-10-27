using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotheca.Domain;

public class LiteraryWork
{
    [Key]
    [Required]
    public int RecordId { get; set; }

    [MaxLength(50)]
    [Column("title")]
    public string? WorkTitle { get; set; }

    [MaxLength(50)]
    [Column("author")]
    public string? Writer { get; set; }

    [MaxLength(50)]
    [Column("description")]
    public string? Synopsis { get; set; }

    [MaxLength(50)]
    [Column("publisher")]
    public string? PublishingHouse { get; set; }

    [MaxLength(50)]
    [Column("status")]
    public string? AvailabilityState { get; set; }

    [MaxLength(50)]
    [Column("category")]
    public string? Genre { get; set; }
}
