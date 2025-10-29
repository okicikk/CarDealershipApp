using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealershipApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class carsImagesDb_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarImage_Cars_CarId",
                table: "CarImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarImage",
                table: "CarImage");

            migrationBuilder.RenameTable(
                name: "CarImage",
                newName: "CarsImages");

            migrationBuilder.RenameIndex(
                name: "IX_CarImage_CarId",
                table: "CarsImages",
                newName: "IX_CarsImages_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarsImages",
                table: "CarsImages",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e32b02a-046c-40be-bfeb-327c900e6bb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26e78cb9-1fea-4478-8b7d-589c8bf7c935", "AQAAAAIAAYagAAAAEEgbzzUyLrUOS9KxyBCYQkoxZCMrHnw/9NJbCCBw81nPX7Tj4QnouuSYk5OETsuy7g==", "2330d0aa-2559-497b-a67b-6d0bb86e8ee8" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarsImages_Cars_CarId",
                table: "CarsImages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsImages_Cars_CarId",
                table: "CarsImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarsImages",
                table: "CarsImages");

            migrationBuilder.RenameTable(
                name: "CarsImages",
                newName: "CarImage");

            migrationBuilder.RenameIndex(
                name: "IX_CarsImages_CarId",
                table: "CarImage",
                newName: "IX_CarImage_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarImage",
                table: "CarImage",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e32b02a-046c-40be-bfeb-327c900e6bb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc36e662-1890-40aa-affe-c6822f409bd4", "AQAAAAIAAYagAAAAEBw2ynkkUEC/ekV1gYHXc0TSHgD1W5FJAaKkq98REAWT0Va2rWoqWF1pxZ3aBaAJhw==", "96f3e59d-1be5-4264-bf19-794a2ce90891" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarImage_Cars_CarId",
                table: "CarImage",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
