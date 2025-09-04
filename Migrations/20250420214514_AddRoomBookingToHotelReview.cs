using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomBookingToHotelReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "employee_work_schedule_id_fkey",
                schema: "core",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "guest_card_id_fkey",
                schema: "core",
                table: "guest");

            migrationBuilder.DropForeignKey(
                name: "room_hotel_id_fkey",
                schema: "core",
                table: "room");

            migrationBuilder.DropForeignKey(
                name: "room_booking_quest_id_fkey",
                schema: "core",
                table: "room_booking");

            migrationBuilder.DropForeignKey(
                name: "room_comfort_comfort_id_fkey",
                schema: "core",
                table: "room_comfort");

            migrationBuilder.DropForeignKey(
                name: "room_comfort_room_id_fkey",
                schema: "core",
                table: "room_comfort");

            migrationBuilder.DropPrimaryKey(
                name: "room_comfort_pkey",
                schema: "core",
                table: "room_comfort");

            migrationBuilder.DropIndex(
                name: "IX_room_booking_quest_id",
                schema: "core",
                table: "room_booking");

            migrationBuilder.DropPrimaryKey(
                name: "room_pkey",
                schema: "core",
                table: "room");

            migrationBuilder.DropIndex(
                name: "IX_guest_card_id",
                schema: "core",
                table: "guest");

            migrationBuilder.DropIndex(
                name: "IX_guest_client_id",
                schema: "core",
                table: "guest");

            migrationBuilder.DropPrimaryKey(
                name: "comfort_pkey",
                schema: "core",
                table: "comfort");

            migrationBuilder.DropColumn(
                name: "card_id",
                schema: "core",
                table: "guest");

            migrationBuilder.DropColumn(
                name: "base_salary",
                schema: "core",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "hire_date",
                schema: "core",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "total_cost",
                schema: "core",
                table: "amenity_payment");

            migrationBuilder.RenameColumn(
                name: "quest_id",
                schema: "core",
                table: "room_booking",
                newName: "number_of_guests");

            migrationBuilder.RenameColumn(
                name: "work_schedule_id",
                schema: "core",
                table: "employee",
                newName: "WorkScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_employee_work_schedule_id",
                schema: "core",
                table: "employee",
                newName: "IX_employee_WorkScheduleId");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "work_schedule",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "room_booking_id",
                schema: "core",
                table: "room_payment",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "payment_type_id",
                schema: "core",
                table: "room_payment",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "room_payment",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "comfort_id",
                schema: "core",
                table: "room_comfort",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "room_id",
                schema: "core",
                table: "room_comfort",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "room_id",
                schema: "core",
                table: "room_booking",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "room_booking",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "confirmation_time",
                schema: "core",
                table: "room_booking",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "core",
                table: "room_booking",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<long>(
                name: "guest_id",
                schema: "core",
                table: "room_booking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "is_confirmed",
                schema: "core",
                table: "room_booking",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "hotel_id",
                schema: "core",
                table: "room",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "room",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "payment_type",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "core",
                table: "payment_type",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "hotel_type",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "core",
                table: "hotel_type",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "hotel_id",
                schema: "core",
                table: "hotel_review",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "guest_id",
                schema: "core",
                table: "hotel_review",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "hotel_review",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "room_booking_id",
                schema: "core",
                table: "hotel_review",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "hotel",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "city",
                schema: "core",
                table: "hotel",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "hotel_type_id",
                schema: "core",
                table: "hotel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "core",
                table: "hotel",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_of_birth",
                schema: "core",
                table: "guest",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<long>(
                name: "client_id",
                schema: "core",
                table: "guest",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "guest",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "city_of_residence",
                schema: "core",
                table: "guest",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "loyalty_status",
                schema: "core",
                table: "guest",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "employee_type",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "core",
                table: "employee_type",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "employee_type_id",
                schema: "core",
                table: "employee",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "client_id",
                schema: "core",
                table: "employee",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "employee",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "WorkScheduleId",
                schema: "core",
                table: "employee",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "hotel_id",
                schema: "core",
                table: "employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "comfort",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "core",
                table: "comfort",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "client",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "core",
                table: "client",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "patronymic",
                schema: "core",
                table: "client",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "surname",
                schema: "core",
                table: "client",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "bank_id",
                schema: "core",
                table: "card",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "card",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "guest_id",
                schema: "core",
                table: "card",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "bank",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "core",
                table: "bank",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "guest_id",
                schema: "core",
                table: "amenity_review",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "amenity_id",
                schema: "core",
                table: "amenity_review",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "amenity_review",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "payment_type_id",
                schema: "core",
                table: "amenity_payment",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "amenity_booking_id",
                schema: "core",
                table: "amenity_payment",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "amenity_payment",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "total_amount",
                schema: "core",
                table: "amenity_payment",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ready_time",
                schema: "core",
                table: "amenity_booking",
                type: "time without time zone",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ready_date",
                schema: "core",
                table: "amenity_booking",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<long>(
                name: "guest_id",
                schema: "core",
                table: "amenity_booking",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "completion_status",
                schema: "core",
                table: "amenity_booking",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValueSql: "'‚ 'Ожидается подтверждение'::character varying",
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldDefaultValueSql: "'‚ ®¦Ё¤ ­ЁЁ Ї®¤вўҐа¦¤Ґ­Ёп'::character varying");

            migrationBuilder.AlterColumn<long>(
                name: "amenity_id",
                schema: "core",
                table: "amenity_booking",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "amenity_booking",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "employee_id",
                schema: "core",
                table: "amenity_booking",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "room_id",
                schema: "core",
                table: "amenity",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "core",
                table: "amenity",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "employee_type_id",
                schema: "core",
                table: "amenity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "core",
                table: "amenity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_room_comfort",
                schema: "core",
                table: "room_comfort",
                columns: new[] { "room_id", "comfort_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_room",
                schema: "core",
                table: "room",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comfort",
                schema: "core",
                table: "comfort",
                column: "id");

            migrationBuilder.CreateTable(
                name: "additional_guest",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    passport_series_hash = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    passport_number_hash = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    room_booking_id = table.Column<long>(type: "bigint", nullable: false),
                    guest_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additional_guest", x => x.id);
                    table.ForeignKey(
                        name: "FK_additional_guest_guest_guest_id",
                        column: x => x.guest_id,
                        principalSchema: "core",
                        principalTable: "guest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_additional_guest_room_booking_room_booking_id",
                        column: x => x.room_booking_id,
                        principalSchema: "core",
                        principalTable: "room_booking",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_room_booking_guest_id",
                schema: "core",
                table: "room_booking",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_hotel_review_room_booking_id",
                schema: "core",
                table: "hotel_review",
                column: "room_booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hotel_hotel_type_id",
                schema: "core",
                table: "hotel",
                column: "hotel_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_guest_client_id",
                schema: "core",
                table: "guest",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_hotel_id",
                schema: "core",
                table: "employee",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_comfort_name",
                schema: "core",
                table: "comfort",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "card_guest_id_key",
                schema: "core",
                table: "card",
                column: "guest_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_amenity_booking_employee_id",
                schema: "core",
                table: "amenity_booking",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_amenity_employee_type_id",
                schema: "core",
                table: "amenity",
                column: "employee_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_additional_guest_guest_id",
                schema: "core",
                table: "additional_guest",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_additional_guest_room_booking_id",
                schema: "core",
                table: "additional_guest",
                column: "room_booking_id");

            migrationBuilder.AddForeignKey(
                name: "amenity_employee_type_id_fkey",
                schema: "core",
                table: "amenity",
                column: "employee_type_id",
                principalSchema: "core",
                principalTable: "employee_type",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "amenity_booking_employee_id_fkey",
                schema: "core",
                table: "amenity_booking",
                column: "employee_id",
                principalSchema: "core",
                principalTable: "employee",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "card_guest_id_fkey",
                schema: "core",
                table: "card",
                column: "guest_id",
                principalSchema: "core",
                principalTable: "guest",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_work_schedule_WorkScheduleId",
                schema: "core",
                table: "employee",
                column: "WorkScheduleId",
                principalSchema: "core",
                principalTable: "work_schedule",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "employee_hotel_id_fkey",
                schema: "core",
                table: "employee",
                column: "hotel_id",
                principalSchema: "core",
                principalTable: "hotel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hotel_hotel_type",
                schema: "core",
                table: "hotel",
                column: "hotel_type_id",
                principalSchema: "core",
                principalTable: "hotel_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "hotel_review_room_booking_id_fkey",
                schema: "core",
                table: "hotel_review",
                column: "room_booking_id",
                principalSchema: "core",
                principalTable: "room_booking",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_room_hotel_hotel_id",
                schema: "core",
                table: "room",
                column: "hotel_id",
                principalSchema: "core",
                principalTable: "hotel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "room_booking_guest_id_fkey",
                schema: "core",
                table: "room_booking",
                column: "guest_id",
                principalSchema: "core",
                principalTable: "guest",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_room_comfort_comfort_comfort_id",
                schema: "core",
                table: "room_comfort",
                column: "comfort_id",
                principalSchema: "core",
                principalTable: "comfort",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_room_comfort_room_room_id",
                schema: "core",
                table: "room_comfort",
                column: "room_id",
                principalSchema: "core",
                principalTable: "room",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "amenity_employee_type_id_fkey",
                schema: "core",
                table: "amenity");

            migrationBuilder.DropForeignKey(
                name: "amenity_booking_employee_id_fkey",
                schema: "core",
                table: "amenity_booking");

            migrationBuilder.DropForeignKey(
                name: "card_guest_id_fkey",
                schema: "core",
                table: "card");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_work_schedule_WorkScheduleId",
                schema: "core",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "employee_hotel_id_fkey",
                schema: "core",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "fk_hotel_hotel_type",
                schema: "core",
                table: "hotel");

            migrationBuilder.DropForeignKey(
                name: "hotel_review_room_booking_id_fkey",
                schema: "core",
                table: "hotel_review");

            migrationBuilder.DropForeignKey(
                name: "FK_room_hotel_hotel_id",
                schema: "core",
                table: "room");

            migrationBuilder.DropForeignKey(
                name: "room_booking_guest_id_fkey",
                schema: "core",
                table: "room_booking");

            migrationBuilder.DropForeignKey(
                name: "FK_room_comfort_comfort_comfort_id",
                schema: "core",
                table: "room_comfort");

            migrationBuilder.DropForeignKey(
                name: "FK_room_comfort_room_room_id",
                schema: "core",
                table: "room_comfort");

            migrationBuilder.DropTable(
                name: "additional_guest",
                schema: "core");

            migrationBuilder.DropPrimaryKey(
                name: "PK_room_comfort",
                schema: "core",
                table: "room_comfort");

            migrationBuilder.DropIndex(
                name: "IX_room_booking_guest_id",
                schema: "core",
                table: "room_booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_room",
                schema: "core",
                table: "room");

            migrationBuilder.DropIndex(
                name: "IX_hotel_review_room_booking_id",
                schema: "core",
                table: "hotel_review");

            migrationBuilder.DropIndex(
                name: "IX_hotel_hotel_type_id",
                schema: "core",
                table: "hotel");

            migrationBuilder.DropIndex(
                name: "IX_guest_client_id",
                schema: "core",
                table: "guest");

            migrationBuilder.DropIndex(
                name: "IX_employee_hotel_id",
                schema: "core",
                table: "employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comfort",
                schema: "core",
                table: "comfort");

            migrationBuilder.DropIndex(
                name: "IX_comfort_name",
                schema: "core",
                table: "comfort");

            migrationBuilder.DropIndex(
                name: "card_guest_id_key",
                schema: "core",
                table: "card");

            migrationBuilder.DropIndex(
                name: "IX_amenity_booking_employee_id",
                schema: "core",
                table: "amenity_booking");

            migrationBuilder.DropIndex(
                name: "IX_amenity_employee_type_id",
                schema: "core",
                table: "amenity");

            migrationBuilder.DropColumn(
                name: "confirmation_time",
                schema: "core",
                table: "room_booking");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "core",
                table: "room_booking");

            migrationBuilder.DropColumn(
                name: "guest_id",
                schema: "core",
                table: "room_booking");

            migrationBuilder.DropColumn(
                name: "is_confirmed",
                schema: "core",
                table: "room_booking");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "core",
                table: "payment_type");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "core",
                table: "hotel_type");

            migrationBuilder.DropColumn(
                name: "room_booking_id",
                schema: "core",
                table: "hotel_review");

            migrationBuilder.DropColumn(
                name: "city",
                schema: "core",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "hotel_type_id",
                schema: "core",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "core",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "city_of_residence",
                schema: "core",
                table: "guest");

            migrationBuilder.DropColumn(
                name: "loyalty_status",
                schema: "core",
                table: "guest");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "core",
                table: "employee_type");

            migrationBuilder.DropColumn(
                name: "hotel_id",
                schema: "core",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "core",
                table: "comfort");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "core",
                table: "client");

            migrationBuilder.DropColumn(
                name: "patronymic",
                schema: "core",
                table: "client");

            migrationBuilder.DropColumn(
                name: "surname",
                schema: "core",
                table: "client");

            migrationBuilder.DropColumn(
                name: "guest_id",
                schema: "core",
                table: "card");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "core",
                table: "bank");

            migrationBuilder.DropColumn(
                name: "total_amount",
                schema: "core",
                table: "amenity_payment");

            migrationBuilder.DropColumn(
                name: "employee_id",
                schema: "core",
                table: "amenity_booking");

            migrationBuilder.DropColumn(
                name: "employee_type_id",
                schema: "core",
                table: "amenity");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "core",
                table: "amenity");

            migrationBuilder.RenameColumn(
                name: "number_of_guests",
                schema: "core",
                table: "room_booking",
                newName: "quest_id");

            migrationBuilder.RenameColumn(
                name: "WorkScheduleId",
                schema: "core",
                table: "employee",
                newName: "work_schedule_id");

            migrationBuilder.RenameIndex(
                name: "IX_employee_WorkScheduleId",
                schema: "core",
                table: "employee",
                newName: "IX_employee_work_schedule_id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "work_schedule",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "room_booking_id",
                schema: "core",
                table: "room_payment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "payment_type_id",
                schema: "core",
                table: "room_payment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "room_payment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "comfort_id",
                schema: "core",
                table: "room_comfort",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "room_id",
                schema: "core",
                table: "room_comfort",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "room_id",
                schema: "core",
                table: "room_booking",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "room_booking",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "hotel_id",
                schema: "core",
                table: "room",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "room",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "payment_type",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "hotel_type",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "hotel_id",
                schema: "core",
                table: "hotel_review",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "guest_id",
                schema: "core",
                table: "hotel_review",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "hotel_review",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "hotel",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date_of_birth",
                schema: "core",
                table: "guest",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "client_id",
                schema: "core",
                table: "guest",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "guest",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "card_id",
                schema: "core",
                table: "guest",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "employee_type",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "employee_type_id",
                schema: "core",
                table: "employee",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "client_id",
                schema: "core",
                table: "employee",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "employee",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "work_schedule_id",
                schema: "core",
                table: "employee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "base_salary",
                schema: "core",
                table: "employee",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateOnly>(
                name: "hire_date",
                schema: "core",
                table: "employee",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "comfort",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "client",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "bank_id",
                schema: "core",
                table: "card",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "card",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "bank",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "guest_id",
                schema: "core",
                table: "amenity_review",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "amenity_id",
                schema: "core",
                table: "amenity_review",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "amenity_review",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "payment_type_id",
                schema: "core",
                table: "amenity_payment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "amenity_booking_id",
                schema: "core",
                table: "amenity_payment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "amenity_payment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "total_cost",
                schema: "core",
                table: "amenity_payment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ready_time",
                schema: "core",
                table: "amenity_booking",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ready_date",
                schema: "core",
                table: "amenity_booking",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "guest_id",
                schema: "core",
                table: "amenity_booking",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "completion_status",
                schema: "core",
                table: "amenity_booking",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValueSql: "'‚ ®¦Ё¤ ­ЁЁ Ї®¤вўҐа¦¤Ґ­Ёп'::character varying",
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldDefaultValueSql: "'‚ 'Ожидается подтверждение'::character varying");

            migrationBuilder.AlterColumn<int>(
                name: "amenity_id",
                schema: "core",
                table: "amenity_booking",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "amenity_booking",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "room_id",
                schema: "core",
                table: "amenity",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "core",
                table: "amenity",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "room_comfort_pkey",
                schema: "core",
                table: "room_comfort",
                columns: new[] { "room_id", "comfort_id" });

            migrationBuilder.AddPrimaryKey(
                name: "room_pkey",
                schema: "core",
                table: "room",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "comfort_pkey",
                schema: "core",
                table: "comfort",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_room_booking_quest_id",
                schema: "core",
                table: "room_booking",
                column: "quest_id");

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

            migrationBuilder.AddForeignKey(
                name: "employee_work_schedule_id_fkey",
                schema: "core",
                table: "employee",
                column: "work_schedule_id",
                principalSchema: "core",
                principalTable: "work_schedule",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "guest_card_id_fkey",
                schema: "core",
                table: "guest",
                column: "card_id",
                principalSchema: "core",
                principalTable: "card",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "room_hotel_id_fkey",
                schema: "core",
                table: "room",
                column: "hotel_id",
                principalSchema: "core",
                principalTable: "hotel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "room_booking_quest_id_fkey",
                schema: "core",
                table: "room_booking",
                column: "quest_id",
                principalSchema: "core",
                principalTable: "guest",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "room_comfort_comfort_id_fkey",
                schema: "core",
                table: "room_comfort",
                column: "comfort_id",
                principalSchema: "core",
                principalTable: "comfort",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "room_comfort_room_id_fkey",
                schema: "core",
                table: "room_comfort",
                column: "room_id",
                principalSchema: "core",
                principalTable: "room",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
