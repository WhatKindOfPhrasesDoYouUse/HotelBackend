using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

[Table(name: "amenity_review", Schema = "core")]
public partial class AmenityReview
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "comment")]
    public string? Comment { get; set; }

    [Column(name: "publication_date")]
    [Required(ErrorMessage = "Поле PublicationDate модели AmenityReview является обязательным")]
    public DateOnly PublicationDate { get; set; }

    [Column(name: "publication_time")]
    [Required(ErrorMessage = "Поле PublicationTime модели AmenityReview является обязательным")]
    public TimeOnly PublicationTime { get; set; }

    [Column(name: "rating")]
    [Required(ErrorMessage = "Поле Rating модели AmenityReview является обязательным")]
    [Range(1, 5, ErrorMessage = "Поле Rating модели AmenityReview должно быть в пределах от 1 до 5.")]
    public int Rating { get; set; }

    [Column(name: "guest_id")]
    [Required(ErrorMessage = "Поле QuestId модели AmenityReview является обязательным")]
    public long GuestId { get; set; }

    [Column(name: "amenity_id")]
    [Required(ErrorMessage = "Поле HotelId модели AmenityReview является обязательным")]
    public long AmenityId { get; set; }

    [ForeignKey(nameof(AmenityId))]
    public virtual Amenity Amenity { get; set; } = null!;

    [ForeignKey(nameof(GuestId))]
    public virtual Guest Guest { get; set; } = null!;
}
