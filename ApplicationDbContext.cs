using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amenity> Amenities { get; set; }

    public virtual DbSet<AmenityBooking> AmenityBookings { get; set; }

    public virtual DbSet<AmenityPayment> AmenityPayments { get; set; }

    public virtual DbSet<AmenityReview> AmenityReviews { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Comfort> Comforts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<HotelReview> HotelReviews { get; set; }

    public virtual DbSet<HotelType> HotelTypes { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomBooking> RoomBookings { get; set; }

    public virtual DbSet<RoomPayment> RoomPayments { get; set; }

    public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }

    public virtual DbSet<RoomComfort> RoomsComforts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=hotel_database;Username=postgres;Password=root;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("amenity_pkey");

            entity.ToTable("amenity", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
            entity.Property(e => e.EmployeeTypeId).HasColumnName("employee_type_id");

            entity.HasOne(d => d.Room).WithMany(p => p.Amenities)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("amenity_room_id_fkey");

            entity.HasOne(d => d.EmployeeType).WithMany(p => p.Amenities)
                .HasForeignKey(d => d.EmployeeTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("amenity_employee_type_id_fkey"); 
        });

        modelBuilder.Entity<AmenityBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("amenity_booking_pkey");

            entity.ToTable("amenity_booking", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmenityId).HasColumnName("amenity_id");
            entity.Property(e => e.CompletionStatus)
                .HasMaxLength(30)
                .HasDefaultValueSql("'‚ 'Ожидается подтверждение'::character varying")
                .HasColumnName("completion_status");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderTime).HasColumnName("order_time");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReadyDate).HasColumnName("ready_date");
            entity.Property(e => e.ReadyTime).HasColumnName("ready_time");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Amenity).WithMany(p => p.AmenityBookings)
                .HasForeignKey(d => d.AmenityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("amenity_booking_amenity_id_fkey");

            entity.HasOne(d => d.Guest).WithMany(p => p.AmenityBookings)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("amenity_booking_guest_id_fkey");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.AmenityBookings) 
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull)  
                .HasConstraintName("amenity_booking_employee_id_fkey");
        });

        modelBuilder.Entity<AmenityPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("amenity_payment_pkey");

            entity.ToTable("amenity_payment", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmenityBookingId).HasColumnName("amenity_booking_id");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(30)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("payment_status");
            entity.Property(e => e.PaymentTime).HasColumnName("payment_time");
            entity.Property(e => e.PaymentTypeId).HasColumnName("payment_type_id");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");

            entity.HasOne(d => d.AmenityBooking).WithMany(p => p.AmenityPayments)
                .HasForeignKey(d => d.AmenityBookingId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("amenity_payment_amenity_booking_id_fkey");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.AmenityPayments)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("amenity_payment_payment_type_id_fkey");
        });

        modelBuilder.Entity<AmenityReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("amenity_review_pkey");

            entity.ToTable("amenity_review", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmenityId).HasColumnName("amenity_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.PublicationDate).HasColumnName("publication_date");
            entity.Property(e => e.PublicationTime).HasColumnName("publication_time");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Amenity).WithMany(p => p.AmenityReviews)
                .HasForeignKey(d => d.AmenityId)
                .HasConstraintName("amenity_review_amenity_id_fkey");

            entity.HasOne(d => d.Guest).WithMany(p => p.AmenityReviews)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("amenity_review_guest_id_fkey");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bank_pkey");

            entity.ToTable("bank", "core");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("card_pkey");

            entity.ToTable("card", "core");

            entity.HasIndex(e => e.CardNumber, "card_card_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankId).HasColumnName("bank_id");
            entity.Property(e => e.CardDate)
                .HasMaxLength(5)
                .HasColumnName("card_date");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .HasColumnName("card_number");

            entity.HasOne(d => d.Bank).WithMany(p => p.Cards)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("card_bank_id_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client", "core");

            entity.HasIndex(e => e.Email, "client_email_key").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "client_phone_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(150)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Guest)
               .WithOne(p => p.Client)
               .HasForeignKey<Guest>(d => d.ClientId) 
               .OnDelete(DeleteBehavior.Cascade)  
               .HasConstraintName("guest_client_id_fkey");
        });

        /*modelBuilder.Entity<Comfort>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comfort_pkey");

            entity.ToTable("comfort", "core");

            entity.Property(e => e.Id).HasColumnName("id");
        });*/

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_pkey");

            entity.ToTable("employee", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BaseSalary)
                .HasPrecision(10, 2)
                .HasColumnName("base_salary");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.EmployeeTypeId).HasColumnName("employee_type_id");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.WorkScheduleId).HasColumnName("work_schedule_id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("employee_client_id_fkey");

            entity.HasOne(d => d.EmployeeType).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employee_employee_type_id_fkey");

            entity.HasOne(d => d.WorkSchedule).WithMany(p => p.Employees)
                .HasForeignKey(d => d.WorkScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employee_work_schedule_id_fkey");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Employees)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employee_hotel_id_fkey");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_type_pkey");

            entity.ToTable("employee_type", "core");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("guest_pkey");

            entity.ToTable("guest", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.PassportNumberHash)
                .HasMaxLength(150)
                .HasColumnName("passport_number_hash");
            entity.Property(e => e.PassportSeriesHash)
                .HasMaxLength(150)
                .HasColumnName("passport_series_hash");

            entity.HasOne(d => d.Card).WithMany(p => p.Guests)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("guest_card_id_fkey");

            entity.HasOne(d => d.Client)
                .WithOne(p => p.Guest)
                .HasForeignKey<Guest>(d => d.ClientId)
                .HasConstraintName("guest_client_id_fkey");

            entity.HasIndex(e => e.ClientId).IsUnique();
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hotel_pkey");

            entity.ToTable("hotel", "core");

            entity.HasIndex(e => e.Email, "hotel_email_key").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "hotel_phone_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .HasColumnName("phone_number");
            entity.Property(e => e.Rating)
                .HasDefaultValueSql("1")
                .HasColumnName("rating");
            entity.Property(e => e.YearOfConstruction).HasColumnName("year_of_construction");

            entity.Property(e => e.HotelTypeId)
                .HasColumnName("hotel_type_id")
                .IsRequired();

            entity.HasOne(h => h.HotelType)
                .WithMany(ht => ht.Hotels)
                .HasForeignKey(h => h.HotelTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_hotel_hotel_type");
        });

        modelBuilder.Entity<HotelReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hotel_review_pkey");

            entity.ToTable("hotel_review", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.PublicationDate).HasColumnName("publication_date");
            entity.Property(e => e.PublicationTime).HasColumnName("publication_time");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Guest).WithMany(p => p.HotelReviews)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("hotel_review_guest_id_fkey");

            entity.HasOne(d => d.Hotel).WithMany(p => p.HotelReviews)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("hotel_review_hotel_id_fkey");
        });

        modelBuilder.Entity<HotelType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hotel_type_pkey");

            entity.ToTable("hotel_type", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasMany(ht => ht.Hotels)
                .WithOne(h => h.HotelType)
                .HasForeignKey(h => h.HotelTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_hotel_hotel_type");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_type_pkey");

            entity.ToTable("payment_type", "core");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        /*modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("room_pkey");

            entity.ToTable("room", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.RoomNumber).HasColumnName("room_number");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("room_hotel_id_fkey");

            // Настройка для связи с RoomComfort (сущность Room)
            entity.HasMany(d => d.RoomComforts)
                .WithOne(rc => rc.Room)  // У каждой RoomComfort есть один Room
                .HasForeignKey(rc => rc.RoomId)  // Внешний ключ для RoomId
                .HasConstraintName("room_comfort_room_id_fkey");

            // Настройка для связи с RoomComfort (сущность Comfort)
            entity.HasMany(d => d.RoomComforts)
                .WithOne(rc => rc.Comfort)  // У каждой RoomComfort есть один Comfort
                .HasForeignKey(rc => rc.ComfortId)  // Внешний ключ для ComfortId
                .HasConstraintName("room_comfort_comfort_id_fkey");
        });*/

        modelBuilder.Entity<Room>()
            .HasOne(h => h.Hotel)
            .WithMany(r => r.Rooms)
            .HasForeignKey(k => k.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comfort>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<RoomComfort>()
            .HasKey(rc => new { rc.RoomId, rc.ComfortId });

        modelBuilder.Entity<RoomComfort>()
            .HasOne(r => r.Room)
            .WithMany(c => c.RoomComforts)
            .HasForeignKey(k => k.RoomId);

        modelBuilder.Entity<RoomComfort>()
            .HasOne(c => c.Comfort)
            .WithMany(r => r.RoomComforts)
            .HasForeignKey(k => k.ComfortId);

        modelBuilder.Entity<RoomBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("room_booking_pkey");

            entity.ToTable("room_booking", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CheckInDate).HasColumnName("check_in_date");
            entity.Property(e => e.CheckInTime).HasColumnName("check_in_time");
            entity.Property(e => e.CheckOutDate).HasColumnName("check_out_date");
            entity.Property(e => e.CheckOutTime).HasColumnName("check_out_time");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");

            entity.HasOne(d => d.Guest).WithMany(p => p.RoomBookings)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("room_booking_quest_id_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomBookings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("room_booking_room_id_fkey");
        });

        modelBuilder.Entity<RoomPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("room_payment_pkey");

            entity.ToTable("room_payment", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasColumnName("payment_status");
            entity.Property(e => e.PaymentTime).HasColumnName("payment_time");
            entity.Property(e => e.PaymentTypeId).HasColumnName("payment_type_id");
            entity.Property(e => e.RoomBookingId).HasColumnName("room_booking_id");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasColumnName("total_amount");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.RoomPayments)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("room_payment_payment_type_id_fkey");

            entity.HasOne(d => d.RoomBooking).WithMany(p => p.RoomPayments)
                .HasForeignKey(d => d.RoomBookingId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("room_payment_room_booking_id_fkey");
        });

        modelBuilder.Entity<WorkSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("work_schedule_pkey");

            entity.ToTable("work_schedule", "core");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.WorkDate).HasColumnName("work_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
