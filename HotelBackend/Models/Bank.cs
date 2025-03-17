using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HotelBackend.ValidationTypes;

namespace HotelBackend.Models;

[Table(name: "bank", Schema = "core")]
public partial class Bank
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Bank являеться обязательным")]
    [NameValidation]
    public string Name { get; set; } = null!;

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}