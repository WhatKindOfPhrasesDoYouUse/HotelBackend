using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая номер в отеле.
/// </summary>
[Table(name: "room", Schema = "core")]
public partial class Room
{
    /// <summary>
    /// Первичный ключ номера.
    /// </summary>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Номер комнаты.
    /// </summary>
    /// <remarks>
    /// Поле является обязательным и должно быть уникальным для каждого отеля.
    /// </remarks>
    [Column(name: "room_number")]
    [Required(ErrorMessage = "Поле RoomNumber модели Room является обязательным")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле RoomNumber модели Room должно быть больше 0")]
    public int RoomNumber { get; set; }

    /// <summary>
    /// Описание номера.
    /// </summary>
    /// <remarks>
    /// Поле является необязательным, максимальная длина 1000 символов.
    /// </remarks>
    [Column(name: "description")]
    public string? Description { get; set; }

    /// <summary>
    /// Вместительность номера.
    /// </summary>
    /// <remarks>
    /// Поле является обязательным и должно быть уникальным для каждого отеля.
    /// </remarks>
    [Column(name: "сapacity")]
    [Required(ErrorMessage = "Поле Capacity модели Room является обязательным")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле Capacity модели Room должно быть больше 0")]
    public int Capacity { get; set; }

    /// <summary>
    /// Цена за день.
    /// </summary>
    /// <remarks>
    /// Поле является обязательным и должно быть уникальным для каждого отеля.
    /// </remarks>
    [Column(name: "unit_price")]
    [Required(ErrorMessage = "Поле UnitPrice модели Room является обязательным")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле UnitPrice модели Room должно быть больше 0")]
    public int UnitPrice { get; set; }

    /// <summary>
    /// Внешний ключ на отель.
    /// </summary>
    /// <remarks>
    /// Поле является обязательным, указывает, к какому отелю принадлежит номер.
    /// </remarks>
    [Column(name: "hotel_id")]
    [Required(ErrorMessage = "Поле HotelId модели Room является обязательным")]
    public long HotelId { get; set; }

    /// <summary>
    /// Связь с отелем.
    /// </summary>
    /// <remarks>
    /// Отель, к которому относится данный номер.
    /// </remarks>
    [Column(name: "HotelId")]
    public virtual Hotel Hotel { get; set; } = null!;

    /// <summary>
    /// Список удобств в номере.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество удобств, предоставляемых в номере.
    /// </remarks>
    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();

    /// <summary>
    /// Список бронирований для данного номера.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество бронирований.
    /// </remarks>
    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();

    /// <summary>
    /// Список удобств-комфортов для данного номера.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество характеристик комфорта.
    /// </remarks>
    public virtual ICollection<Comfort> Comforts { get; set; } = new List<Comfort>();
}
