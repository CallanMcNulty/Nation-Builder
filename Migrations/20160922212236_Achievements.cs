using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NationBuilder.Migrations
{
    public partial class Achievements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    oneHundredMillionDebt = table.Column<bool>(nullable: false),
                    oneHundredMillionPeople = table.Column<bool>(nullable: false),
                    oneHundredOfEverything = table.Column<bool>(nullable: false),
                    oneHundredStability = table.Column<bool>(nullable: false),
                    twoHundredOil = table.Column<bool>(nullable: false),
                    userId = table.Column<string>(nullable: true),
                    zeroPopulation = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");
        }
    }
}
