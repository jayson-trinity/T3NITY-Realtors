using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T3NITY_Realtors.Migrations
{
    /// <inheritdoc />
    public partial class payments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceGiud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ListingsId = table.Column<int>(type: "int", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Listings_ListingsId",
                        column: x => x.ListingsId,
                        principalTable: "Listings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Password", "Role", "UpdatedDate", "Username" },
                values: new object[] { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "07930d5a59dce0e71a7739f638263e7045cfdaca7e934ec929c5943f954d12bd", "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "PhoneNumber", "UpdatedDate", "UsersId" },
                values: new object[] { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "AdminF", "AdminL", "00000000000", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ListingsId",
                table: "Payments",
                column: "ListingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UsersId",
                table: "Payments",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
