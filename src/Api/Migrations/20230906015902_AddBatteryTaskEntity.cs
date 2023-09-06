﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Musala.Drones.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBatteryTaskEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatteryAudit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DroneId = table.Column<int>(type: "integer", nullable: false),
                    BatteryCapacity = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RowVersion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryAudit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatteryAudit_Drones_DroneId",
                        column: x => x.DroneId,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatteryAudit_DroneId",
                table: "BatteryAudit",
                column: "DroneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatteryAudit");
        }
    }
}
