using HotelBackend.ValidationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая гостя в системе.
/// </summary>
[Table(name: "hotel_type", Schema = "core")]
public partial class Guest
{
    /// <summary>
    /// Первичный ключ гостя.
    /// </summary>
    /// <remarks>
    /// Это поле является первичным ключом для таблицы.
    /// </remarks>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Город проживания гостя.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "city_of_residence")]
    [Required(ErrorMessage = "Поле CityOfResidence модели Guest является обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля CityOfResidence модели Guest")]
    [MaxLength(50, ErrorMessage = "Длина поля CityOfResidence модели Guest не может превышать 50 символов")]
    public string CityOfResidence { get; set; } = null!;

    /// <summary>
    /// Дата рождения гостя.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, дата должна быть не позднее текущей даты.
    /// </remarks>
    [Column(name: "date_of_birth")]
    [Required(ErrorMessage = "Поле DateOfBirth модели Guest является обязательным")]
    [DataType(DataType.Date)]
    [CheckDateOfBirth]
    public DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Хеш серии паспорта гостя.
    /// </summary>
    [Column(name: "passport_series_hash")]
    [Required(ErrorMessage = "Поле PassportSeriesHash модели Guest является обязательным")]
    [MaxLength(150, ErrorMessage = "Длина поля PassportSeriesHash модели Guest не может превышать 150 символов")]
    public string PassportSeriesHash { get; set; } = null!;

    /// <summary>
    /// Хеш номера паспорта гостя.
    /// </summary>
    [Column(name: "passport_number_hash")]
    [Required(ErrorMessage = "Поле PassportNumberHash модели Guest является обязательным")]
    [MaxLength(150, ErrorMessage = "Длина поля PassportNumberHash модели Guest не может превышать 150 символов")]
    public string PassportNumberHash { get; set; } = null!;

    /// <summary>
    /// Статус лояльности гостя.
    /// </summary>
    [Column(name: "loyalty_status")]
    [Required(ErrorMessage = "Поле LoyaltyStatus модели Guest является обязательным")]
    [MaxLength(50, ErrorMessage = "Длина поля LoyaltyStatus модели Guest не может превышать 50 символов")]
    public string LoyaltyStatus { get; set; } = "Базовый";

    /// <summary>
    /// Идентификатор карты гостя.
    /// </summary>
    [Column(name: "card_id")]
    public int? CardId { get; set; }

    /// <summary>
    /// Идентификатор клиента, к которому привязан гость.
    /// </summary>
    [Column(name: "client_id")]
    [Required(ErrorMessage = "Поле ClientId модели Guest является обязательным")]
    public int ClientId { get; set; }

    /// <summary>
    /// Связь с картой гостя.
    /// </summary>
    [ForeignKey("CardId")]
    public virtual Card? Card { get; set; }

    /// <summary>
    /// Связь с клиентом.
    /// </summary>
    [ForeignKey("ClientId")]
    public virtual Client Client { get; set; } = null!;

    /// <summary>
    /// Коллекция бронирований дополнительных услуг.
    /// </summary>
    public virtual ICollection<AmenityBooking> AmenityBookings { get; set; } = new List<AmenityBooking>();

    /// <summary>
    /// Коллекция отзывов об удобствах.
    /// </summary>
    public virtual ICollection<AmenityReview> AmenityReviews { get; set; } = new List<AmenityReview>();

    /// <summary>
    /// Коллекция отзывов о гостиницах.
    /// </summary>
    public virtual ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();

    /// <summary>
    /// Коллекция бронирований номеров.
    /// </summary>
    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();
}
