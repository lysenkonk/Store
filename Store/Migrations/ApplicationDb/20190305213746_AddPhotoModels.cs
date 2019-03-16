using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Store.Migrations.ApplicationDb
{
    public partial class AddPhotoModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_Products_ProductID",
                table: "FileModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileModels",
                table: "FileModels");

            migrationBuilder.RenameTable(
                name: "FileModels",
                newName: "FileModel");

            migrationBuilder.RenameIndex(
                name: "IX_FileModels_ProductID",
                table: "FileModel",
                newName: "IX_FileModel_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PhotoModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoModels", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_Products_ProductID",
                table: "FileModel",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_Products_ProductID",
                table: "FileModel");

            migrationBuilder.DropTable(
                name: "PhotoModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel");

            migrationBuilder.RenameTable(
                name: "FileModel",
                newName: "FileModels");

            migrationBuilder.RenameIndex(
                name: "IX_FileModel_ProductID",
                table: "FileModels",
                newName: "IX_FileModels_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileModels",
                table: "FileModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_Products_ProductID",
                table: "FileModels",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
