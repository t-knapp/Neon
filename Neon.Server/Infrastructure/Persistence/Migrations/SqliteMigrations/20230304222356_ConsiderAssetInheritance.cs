using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqliteMigrations
{
    public partial class ConsiderAssetInheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageAssets",
                table: "ImageAssets");

            migrationBuilder.RenameTable(
                name: "ImageAssets",
                newName: "Assets");

            migrationBuilder.RenameIndex(
                name: "IX_ImageAssets_Id",
                table: "Assets",
                newName: "IX_Assets_Id");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ThumbnailContent",
                table: "Assets",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Assets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                table: "Assets",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.RenameTable(
                name: "Assets",
                newName: "ImageAssets");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_Id",
                table: "ImageAssets",
                newName: "IX_ImageAssets_Id");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ThumbnailContent",
                table: "ImageAssets",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "ImageAssets",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                table: "ImageAssets",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageAssets",
                table: "ImageAssets",
                column: "Id");
        }
    }
}
