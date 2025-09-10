
using System.ComponentModel.DataAnnotations;

namespace CSExam.Features.CreateTrip;

public record CreateTripPayload
{
    public Guid UserID { get; init; }

    [Required]
    [MaxLength(20)]
    public string Title { get; set; }

    [Required]
    [MaxLength(200)]
    [MinLength(40)]
    public string Description { get; set; }
}