using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NationBuilder.Migrations
{
    public partial class Nation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapitalRate",
                table: "Nations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoodRate",
                table: "Nations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OilRate",
                table: "Nations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PopulationRate",
                table: "Nations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrestigeRate",
                table: "Nations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StabilityRate",
                table: "Nations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WaterRate",
                table: "Nations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapitalRate",
                table: "Nations");

            migrationBuilder.DropColumn(
                name: "FoodRate",
                table: "Nations");

            migrationBuilder.DropColumn(
                name: "OilRate",
                table: "Nations");

            migrationBuilder.DropColumn(
                name: "PopulationRate",
                table: "Nations");

            migrationBuilder.DropColumn(
                name: "PrestigeRate",
                table: "Nations");

            migrationBuilder.DropColumn(
                name: "StabilityRate",
                table: "Nations");

            migrationBuilder.DropColumn(
                name: "WaterRate",
                table: "Nations");
        }
    }
}
