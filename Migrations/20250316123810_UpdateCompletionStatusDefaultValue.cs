using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompletionStatusDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "bank",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("bank_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("client_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comfort",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("comfort_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employee_type",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_type_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hotel",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    year_of_construction = table.Column<int>(type: "integer", nullable: true),
                    rating = table.Column<double>(type: "double precision", nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("hotel_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hotel_type",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("hotel_type_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_type",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payment_type_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_schedule",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    work_date = table.Column<DateOnly>(type: "date", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("work_schedule_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "card",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    card_number = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    card_date = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    bank_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("card_pkey", x => x.id);
                    table.ForeignKey(
                        name: "card_bank_id_fkey",
                        column: x => x.bank_id,
                        principalSchema: "core",
                        principalTable: "bank",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "room",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    room_number = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<int>(type: "integer", nullable: false),
                    hotel_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("room_pkey", x => x.id);
                    table.ForeignKey(
                        name: "room_hotel_id_fkey",
                        column: x => x.hotel_id,
                        principalSchema: "core",
                        principalTable: "hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hire_date = table.Column<DateOnly>(type: "date", nullable: false),
                    base_salary = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    employee_type_id = table.Column<int>(type: "integer", nullable: false),
                    work_schedule_id = table.Column<int>(type: "integer", nullable: true),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_pkey", x => x.id);
                    table.ForeignKey(
                        name: "employee_client_id_fkey",
                        column: x => x.client_id,
                        principalSchema: "core",
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "employee_employee_type_id_fkey",
                        column: x => x.employee_type_id,
                        principalSchema: "core",
                        principalTable: "employee_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "employee_work_schedule_id_fkey",
                        column: x => x.work_schedule_id,
                        principalSchema: "core",
                        principalTable: "work_schedule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "guest",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    passport_series_hash = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    passport_number_hash = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    card_id = table.Column<int>(type: "integer", nullable: true),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("guest_pkey", x => x.id);
                    table.ForeignKey(
                        name: "guest_card_id_fkey",
                        column: x => x.card_id,
                        principalSchema: "core",
                        principalTable: "card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "guest_client_id_fkey",
                        column: x => x.client_id,
                        principalSchema: "core",
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amenity",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true),
                    unit_price = table.Column<int>(type: "integer", nullable: false),
                    room_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("amenity_pkey", x => x.id);
                    table.ForeignKey(
                        name: "amenity_room_id_fkey",
                        column: x => x.room_id,
                        principalSchema: "core",
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "room_comfort",
                schema: "core",
                columns: table => new
                {
                    room_id = table.Column<int>(type: "integer", nullable: false),
                    comfort_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("room_comfort_pkey", x => new { x.room_id, x.comfort_id });
                    table.ForeignKey(
                        name: "room_comfort_comfort_id_fkey",
                        column: x => x.comfort_id,
                        principalSchema: "core",
                        principalTable: "comfort",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "room_comfort_room_id_fkey",
                        column: x => x.room_id,
                        principalSchema: "core",
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotel_review",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    comment = table.Column<string>(type: "text", nullable: true),
                    publication_date = table.Column<DateOnly>(type: "date", nullable: false),
                    publication_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    guest_id = table.Column<int>(type: "integer", nullable: false),
                    hotel_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("hotel_review_pkey", x => x.id);
                    table.ForeignKey(
                        name: "hotel_review_guest_id_fkey",
                        column: x => x.guest_id,
                        principalSchema: "core",
                        principalTable: "guest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "hotel_review_hotel_id_fkey",
                        column: x => x.hotel_id,
                        principalSchema: "core",
                        principalTable: "hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "room_booking",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    check_in_date = table.Column<DateOnly>(type: "date", nullable: false),
                    check_out_date = table.Column<DateOnly>(type: "date", nullable: false),
                    check_in_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    check_out_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    quest_id = table.Column<int>(type: "integer", nullable: false),
                    room_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("room_booking_pkey", x => x.id);
                    table.ForeignKey(
                        name: "room_booking_quest_id_fkey",
                        column: x => x.quest_id,
                        principalSchema: "core",
                        principalTable: "guest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "room_booking_room_id_fkey",
                        column: x => x.room_id,
                        principalSchema: "core",
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "amenity_booking",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_date = table.Column<DateOnly>(type: "date", nullable: false),
                    order_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    ready_date = table.Column<DateOnly>(type: "date", nullable: false),
                    ready_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    completion_status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValueSql: "'‚ ®¦Ё¤ ­ЁЁ Ї®¤вўҐа¦¤Ґ­Ёп'::character varying"),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    amenity_id = table.Column<int>(type: "integer", nullable: false),
                    guest_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("amenity_booking_pkey", x => x.id);
                    table.ForeignKey(
                        name: "amenity_booking_amenity_id_fkey",
                        column: x => x.amenity_id,
                        principalSchema: "core",
                        principalTable: "amenity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "amenity_booking_guest_id_fkey",
                        column: x => x.guest_id,
                        principalSchema: "core",
                        principalTable: "guest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amenity_review",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    comment = table.Column<string>(type: "text", nullable: true),
                    publication_date = table.Column<DateOnly>(type: "date", nullable: false),
                    publication_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    guest_id = table.Column<int>(type: "integer", nullable: false),
                    amenity_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("amenity_review_pkey", x => x.id);
                    table.ForeignKey(
                        name: "amenity_review_amenity_id_fkey",
                        column: x => x.amenity_id,
                        principalSchema: "core",
                        principalTable: "amenity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "amenity_review_guest_id_fkey",
                        column: x => x.guest_id,
                        principalSchema: "core",
                        principalTable: "guest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "room_payment",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payment_date = table.Column<DateOnly>(type: "date", nullable: false),
                    payment_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    payment_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    payment_type_id = table.Column<int>(type: "integer", nullable: false),
                    room_booking_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("room_payment_pkey", x => x.id);
                    table.ForeignKey(
                        name: "room_payment_payment_type_id_fkey",
                        column: x => x.payment_type_id,
                        principalSchema: "core",
                        principalTable: "payment_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "room_payment_room_booking_id_fkey",
                        column: x => x.room_booking_id,
                        principalSchema: "core",
                        principalTable: "room_booking",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "amenity_payment",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payment_date = table.Column<DateOnly>(type: "date", nullable: false),
                    payment_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    total_cost = table.Column<int>(type: "integer", nullable: false),
                    payment_status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, defaultValueSql: "NULL::character varying"),
                    payment_type_id = table.Column<int>(type: "integer", nullable: false),
                    amenity_booking_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("amenity_payment_pkey", x => x.id);
                    table.ForeignKey(
                        name: "amenity_payment_amenity_booking_id_fkey",
                        column: x => x.amenity_booking_id,
                        principalSchema: "core",
                        principalTable: "amenity_booking",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "amenity_payment_payment_type_id_fkey",
                        column: x => x.payment_type_id,
                        principalSchema: "core",
                        principalTable: "payment_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_amenity_room_id",
                schema: "core",
                table: "amenity",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_amenity_booking_amenity_id",
                schema: "core",
                table: "amenity_booking",
                column: "amenity_id");

            migrationBuilder.CreateIndex(
                name: "IX_amenity_booking_guest_id",
                schema: "core",
                table: "amenity_booking",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_amenity_payment_amenity_booking_id",
                schema: "core",
                table: "amenity_payment",
                column: "amenity_booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_amenity_payment_payment_type_id",
                schema: "core",
                table: "amenity_payment",
                column: "payment_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_amenity_review_amenity_id",
                schema: "core",
                table: "amenity_review",
                column: "amenity_id");

            migrationBuilder.CreateIndex(
                name: "IX_amenity_review_guest_id",
                schema: "core",
                table: "amenity_review",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "card_card_number_key",
                schema: "core",
                table: "card",
                column: "card_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_card_bank_id",
                schema: "core",
                table: "card",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "client_email_key",
                schema: "core",
                table: "client",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "client_phone_number_key",
                schema: "core",
                table: "client",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_client_id",
                schema: "core",
                table: "employee",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employee_type_id",
                schema: "core",
                table: "employee",
                column: "employee_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_work_schedule_id",
                schema: "core",
                table: "employee",
                column: "work_schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_guest_card_id",
                schema: "core",
                table: "guest",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "IX_guest_client_id",
                schema: "core",
                table: "guest",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "hotel_email_key",
                schema: "core",
                table: "hotel",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "hotel_phone_number_key",
                schema: "core",
                table: "hotel",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hotel_review_guest_id",
                schema: "core",
                table: "hotel_review",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_hotel_review_hotel_id",
                schema: "core",
                table: "hotel_review",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_hotel_id",
                schema: "core",
                table: "room",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_booking_quest_id",
                schema: "core",
                table: "room_booking",
                column: "quest_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_booking_room_id",
                schema: "core",
                table: "room_booking",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_comfort_comfort_id",
                schema: "core",
                table: "room_comfort",
                column: "comfort_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_payment_payment_type_id",
                schema: "core",
                table: "room_payment",
                column: "payment_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_payment_room_booking_id",
                schema: "core",
                table: "room_payment",
                column: "room_booking_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amenity_payment",
                schema: "core");

            migrationBuilder.DropTable(
                name: "amenity_review",
                schema: "core");

            migrationBuilder.DropTable(
                name: "employee",
                schema: "core");

            migrationBuilder.DropTable(
                name: "hotel_review",
                schema: "core");

            migrationBuilder.DropTable(
                name: "hotel_type",
                schema: "core");

            migrationBuilder.DropTable(
                name: "room_comfort",
                schema: "core");

            migrationBuilder.DropTable(
                name: "room_payment",
                schema: "core");

            migrationBuilder.DropTable(
                name: "amenity_booking",
                schema: "core");

            migrationBuilder.DropTable(
                name: "employee_type",
                schema: "core");

            migrationBuilder.DropTable(
                name: "work_schedule",
                schema: "core");

            migrationBuilder.DropTable(
                name: "comfort",
                schema: "core");

            migrationBuilder.DropTable(
                name: "payment_type",
                schema: "core");

            migrationBuilder.DropTable(
                name: "room_booking",
                schema: "core");

            migrationBuilder.DropTable(
                name: "amenity",
                schema: "core");

            migrationBuilder.DropTable(
                name: "guest",
                schema: "core");

            migrationBuilder.DropTable(
                name: "room",
                schema: "core");

            migrationBuilder.DropTable(
                name: "card",
                schema: "core");

            migrationBuilder.DropTable(
                name: "client",
                schema: "core");

            migrationBuilder.DropTable(
                name: "hotel",
                schema: "core");

            migrationBuilder.DropTable(
                name: "bank",
                schema: "core");
        }
    }
}
