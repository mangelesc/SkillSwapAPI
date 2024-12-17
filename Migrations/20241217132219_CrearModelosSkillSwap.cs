using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Migrations
{
    /// <inheritdoc />
    public partial class CrearModelosSkillSwap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FotoPerfil = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habilidades_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Intercambios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioOfrecidoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioSolicitadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    HabilidadOfrecidaId = table.Column<int>(type: "INTEGER", nullable: false),
                    HabilidadSolicitadaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intercambios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intercambios_Habilidades_HabilidadOfrecidaId",
                        column: x => x.HabilidadOfrecidaId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Intercambios_Habilidades_HabilidadSolicitadaId",
                        column: x => x.HabilidadSolicitadaId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Intercambios_Usuarios_UsuarioOfrecidoId",
                        column: x => x.UsuarioOfrecidoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Intercambios_Usuarios_UsuarioSolicitadoId",
                        column: x => x.UsuarioSolicitadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habilidades_UsuarioId",
                table: "Habilidades",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Intercambios_HabilidadOfrecidaId",
                table: "Intercambios",
                column: "HabilidadOfrecidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Intercambios_HabilidadSolicitadaId",
                table: "Intercambios",
                column: "HabilidadSolicitadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Intercambios_UsuarioOfrecidoId",
                table: "Intercambios",
                column: "UsuarioOfrecidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Intercambios_UsuarioSolicitadoId",
                table: "Intercambios",
                column: "UsuarioSolicitadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intercambios");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
