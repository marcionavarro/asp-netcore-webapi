﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhasTarefasAPI.Migrations
{
    public partial class Token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RefreshToken = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true),
                    Utilizado = table.Column<bool>(nullable: false),
                    ExpirtaionToken = table.Column<DateTime>(nullable: false),
                    ExpirtaionRefreshToken = table.Column<DateTime>(nullable: false),
                    DataCriado = table.Column<DateTime>(nullable: false),
                    Atualizado = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Token_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Token_UsuarioId",
                table: "Token",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token");
        }
    }
}
