using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasCore6.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    IdAutor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.IdAutor);
                });

            migrationBuilder.CreateTable(
                name: "Editorial",
                columns: table => new
                {
                    IdEditorial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sede = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editorial", x => x.IdEditorial);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    IdLibro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEditorial = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sipnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Npaginas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.IdLibro);
                    table.ForeignKey(
                        name: "FK_Libros_Editorial_IdEditorial",
                        column: x => x.IdEditorial,
                        principalTable: "Editorial",
                        principalColumn: "IdEditorial",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutoresLibros",
                columns: table => new
                {
                    IdAutor = table.Column<int>(type: "int", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoresLibros", x => new { x.IdAutor, x.IdLibro });
                    table.ForeignKey(
                        name: "FK_AutoresLibros_Autor_IdAutor",
                        column: x => x.IdAutor,
                        principalTable: "Autor",
                        principalColumn: "IdAutor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoresLibros_Libros_IdLibro",
                        column: x => x.IdLibro,
                        principalTable: "Libros",
                        principalColumn: "IdLibro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Autor",
                columns: new[] { "IdAutor", "Apellidos", "Nombre" },
                values: new object[,]
                {
                    { 1, "Apellido 1", "Nombre 1" },
                    { 2, "Apellido 2", "Nombre 2" },
                    { 3, "Apellido 3", "Nombre 3" },
                    { 4, "Apellido 4", "Nombre 4" },
                    { 5, "Apellido 5", "Nombre 5" }
                });

            migrationBuilder.InsertData(
                table: "Editorial",
                columns: new[] { "IdEditorial", "Nombre", "Sede" },
                values: new object[,]
                {
                    { 1, "Editorial 1", "Sede 1" },
                    { 2, "Editorial 2", "Sede 2" },
                    { 3, "Editorial 3", "Sede 3" },
                    { 4, "Editorial 4", "Sede 4" },
                    { 5, "Editorial 5", "Sede 5" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "IdUsuario", "Apellidos", "Direccion", "Email", "Nombre", "Password" },
                values: new object[,]
                {
                    { 1, "apellido 1", "Direccion 1", "1email@dominio.com ", "usuario 1", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92" },
                    { 2, "apellido 2", "Direccion 2", "2email@dominio.com ", "usuario 2", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92" },
                    { 3, "apellido 3", "Direccion 3", "3email@dominio.com ", "usuario 3", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92" },
                    { 4, "apellido 4", "Direccion 4", "4email@dominio.com ", "usuario 4", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92" },
                    { 5, "apellido 5", "Direccion 5", "5email@dominio.com ", "usuario 5", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92" }
                });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "IdLibro", "IdEditorial", "Npaginas", "Sipnosis", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, 29, "sipnosis libro 1", "titulo libro 1" },
                    { 2, 1, 11, "sipnosis libro 2", "titulo libro 2" },
                    { 3, 1, 69, "sipnosis libro 3", "titulo libro 3" },
                    { 4, 1, 36, "sipnosis libro 4", "titulo libro 4" },
                    { 5, 1, 33, "sipnosis libro 5", "titulo libro 5" },
                    { 6, 2, 17, "sipnosis libro 1", "titulo libro 1" },
                    { 7, 2, 64, "sipnosis libro 2", "titulo libro 2" },
                    { 8, 2, 32, "sipnosis libro 3", "titulo libro 3" },
                    { 9, 2, 48, "sipnosis libro 4", "titulo libro 4" },
                    { 10, 2, 89, "sipnosis libro 5", "titulo libro 5" },
                    { 11, 3, 76, "sipnosis libro 1", "titulo libro 1" },
                    { 12, 3, 28, "sipnosis libro 2", "titulo libro 2" },
                    { 13, 3, 6, "sipnosis libro 3", "titulo libro 3" },
                    { 14, 3, 24, "sipnosis libro 4", "titulo libro 4" },
                    { 15, 3, 93, "sipnosis libro 5", "titulo libro 5" },
                    { 16, 4, 52, "sipnosis libro 1", "titulo libro 1" },
                    { 17, 4, 46, "sipnosis libro 2", "titulo libro 2" },
                    { 18, 4, 70, "sipnosis libro 3", "titulo libro 3" },
                    { 19, 4, 53, "sipnosis libro 4", "titulo libro 4" },
                    { 20, 4, 40, "sipnosis libro 5", "titulo libro 5" },
                    { 21, 5, 18, "sipnosis libro 1", "titulo libro 1" },
                    { 22, 5, 47, "sipnosis libro 2", "titulo libro 2" },
                    { 23, 5, 81, "sipnosis libro 3", "titulo libro 3" },
                    { 24, 5, 33, "sipnosis libro 4", "titulo libro 4" },
                    { 25, 5, 59, "sipnosis libro 5", "titulo libro 5" }
                });

            migrationBuilder.InsertData(
                table: "AutoresLibros",
                columns: new[] { "IdAutor", "IdLibro" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 8 },
                    { 1, 9 },
                    { 1, 11 },
                    { 1, 21 },
                    { 1, 23 },
                    { 1, 25 },
                    { 2, 1 },
                    { 2, 4 },
                    { 2, 10 },
                    { 2, 15 },
                    { 2, 19 },
                    { 2, 20 },
                    { 2, 22 },
                    { 3, 2 },
                    { 3, 7 },
                    { 3, 12 },
                    { 3, 14 },
                    { 3, 18 },
                    { 4, 13 },
                    { 4, 16 },
                    { 4, 17 },
                    { 4, 24 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoresLibros_IdLibro",
                table: "AutoresLibros",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_IdEditorial",
                table: "Libros",
                column: "IdEditorial");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoresLibros");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Editorial");
        }
    }
}
