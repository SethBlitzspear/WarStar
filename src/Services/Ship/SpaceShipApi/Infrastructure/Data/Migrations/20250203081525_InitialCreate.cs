using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Armour = table.Column<int>(type: "int", nullable: false),
                    StructuralIntegrity = table.Column<int>(type: "int", nullable: false),
                    MinPowerDraw = table.Column<int>(type: "int", nullable: false),
                    MaxPowerDraw = table.Column<int>(type: "int", nullable: false),
                    LifeSupport = table.Column<bool>(type: "bit", nullable: false),
                    Mass = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaceShips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceShips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Armour = table.Column<int>(type: "int", nullable: false),
                    StructuralIntegrity = table.Column<int>(type: "int", nullable: false),
                    MinPowerDraw = table.Column<int>(type: "int", nullable: false),
                    MaxPowerDraw = table.Column<int>(type: "int", nullable: false),
                    LifeSupport = table.Column<bool>(type: "bit", nullable: false),
                    Mass = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpaceShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BottomComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeftComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RightComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Connections = table.Column<int>(type: "int", nullable: false),
                    PowerCouplings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_ComponentTypes_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Components_Components_BottomComponentId",
                        column: x => x.BottomComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Components_Components_LeftComponentId",
                        column: x => x.LeftComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Components_Components_RightComponentId",
                        column: x => x.RightComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Components_Components_TopComponentId",
                        column: x => x.TopComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Components_SpaceShips_SpaceShipId",
                        column: x => x.SpaceShipId,
                        principalTable: "SpaceShips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ComponentTypes",
                columns: new[] { "Id", "Armour", "LifeSupport", "Mass", "MaxPowerDraw", "MinPowerDraw", "Name", "Price", "Properties", "ShortName", "StructuralIntegrity", "Type" },
                values: new object[,]
                {
                    { new Guid("06cea7a9-90ef-4cfe-be6d-fe6be63583d1"), 1, true, 250, 1, 0, "PowerCore Fusion Pro", 7000.0, null, "PC-FP", 10, "Reactor" },
                    { new Guid("89abef19-f35d-4aa7-ba0d-fff02967069c"), 2, false, 50, 100, 0, "Type 4 Beam Laser", 2000.0, null, "BL-4", 20, "Weapon" },
                    { new Guid("96ea7006-3195-4ebb-882a-1823858bb161"), 10, false, 1000, 0, 0, "Keelster 1000", 12000.0, null, "K1000", 100, "Keel" },
                    { new Guid("a46766a0-0a4f-4314-a2b8-0af36163fec9"), 2, true, 200, 1000, 10, "ThrustMaster Max", 5000.0, null, "T-Max", 30, "Engine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_BottomComponentId",
                table: "Components",
                column: "BottomComponentId",
                unique: true,
                filter: "[BottomComponentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentTypeId",
                table: "Components",
                column: "ComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_LeftComponentId",
                table: "Components",
                column: "LeftComponentId",
                unique: true,
                filter: "[LeftComponentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Components_RightComponentId",
                table: "Components",
                column: "RightComponentId",
                unique: true,
                filter: "[RightComponentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Components_SpaceShipId",
                table: "Components",
                column: "SpaceShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_TopComponentId",
                table: "Components",
                column: "TopComponentId",
                unique: true,
                filter: "[TopComponentId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "ComponentTypes");

            migrationBuilder.DropTable(
                name: "SpaceShips");
        }
    }
}
