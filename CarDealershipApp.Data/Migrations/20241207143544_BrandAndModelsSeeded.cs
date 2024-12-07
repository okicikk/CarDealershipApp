using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarDealershipApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BrandAndModelsSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "ImageUrl", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "https://static.vecteezy.com/system/resources/thumbnails/020/927/378/small_2x/toyota-brand-logo-car-symbol-with-name-black-design-japan-automobile-illustration-free-vector.jpg", false, "Toyota" },
                    { 2, "https://e7.pngegg.com/pngimages/113/140/png-clipart-ford-motor-company-car-ford-f-series-ford-mondeo-auto-parts-emblem-logo-thumbnail.png", false, "Ford" },
                    { 3, "https://i.pinimg.com/736x/d2/59/dc/d259dcd272a3ca9b538e68aedf721fb3.jpg", false, "Chevrolet" },
                    { 4, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQRVqlZCGRqalei2z79Kc3CzWKWkvb9p1U0A&s", false, "BMW" },
                    { 5, "https://uploads.audi-mediacenter.com/system/production/media/1282/images/bde751ee18fe149036c6b47d7595f6784f8901f8/AL090142_web_2880.jpg?1698171883", false, "Audi" },
                    { 6, "https://thumbs.dreamstime.com/b/web-136350849.jpg", false, "Mercedes-Benz" },
                    { 7, "https://cdn.pixabay.com/photo/2016/08/15/18/18/honda-1596081_640.png", false, "Honda" },
                    { 8, "https://wieck-nissanao-production.s3.amazonaws.com/photos/87dd7b734e373e65761492bd446b20efd0a56737/thumbnail-364x204.jpg", false, "Nissan" },
                    { 9, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR_bLDZ_uRSEueAUuNQjCnwIJKe7Ml71Kq66g&s", false, "Hyundai" },
                    { 10, "https://static.vecteezy.com/system/resources/previews/020/500/396/non_2x/kia-logo-brand-symbol-black-design-south-korean-car-automobile-illustration-free-vector.jpg", false, "Kia" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, 1, false, "Corolla" },
                    { 2, 1, false, "Camry" },
                    { 3, 1, false, "RAV4" },
                    { 4, 1, false, "Land Cruiser" },
                    { 5, 1, false, "Highlander" },
                    { 6, 1, false, "Tacoma" },
                    { 7, 2, false, "Focus" },
                    { 8, 2, false, "Mustang" },
                    { 9, 2, false, "F-150" },
                    { 10, 2, false, "Explorer" },
                    { 11, 2, false, "Fusion" },
                    { 12, 2, false, "Edge" },
                    { 13, 3, false, "Malibu" },
                    { 14, 3, false, "Impala" },
                    { 15, 3, false, "Tahoe" },
                    { 16, 3, false, "Camaro" },
                    { 17, 3, false, "Equinox" },
                    { 18, 3, false, "Suburban" },
                    { 20, 4, false, "3 Series" },
                    { 21, 4, false, "X5" },
                    { 22, 4, false, "M3" },
                    { 23, 4, false, "5 Series" },
                    { 24, 4, false, "X3" },
                    { 25, 4, false, "7 Series" },
                    { 26, 5, false, "A4" },
                    { 27, 5, false, "Q7" },
                    { 28, 5, false, "A6" },
                    { 29, 5, false, "Q5" },
                    { 30, 5, false, "A3" },
                    { 31, 5, false, "Q3" },
                    { 32, 6, false, "C-Class" },
                    { 33, 6, false, "E-Class" },
                    { 34, 6, false, "S-Class" },
                    { 35, 6, false, "GLC" },
                    { 36, 6, false, "A-Class" },
                    { 37, 6, false, "GLA" },
                    { 38, 7, false, "Civic" },
                    { 39, 7, false, "Accord" },
                    { 40, 7, false, "CR-V" },
                    { 41, 7, false, "Pilot" },
                    { 42, 8, false, "Altima" },
                    { 43, 8, false, "Maxima" },
                    { 44, 8, false, "Murano" },
                    { 45, 8, false, "Rogue" },
                    { 46, 9, false, "Elantra" },
                    { 47, 9, false, "Sonata" },
                    { 48, 9, false, "Tucson" },
                    { 49, 9, false, "Santa Fe" },
                    { 50, 10, false, "Forte" },
                    { 51, 10, false, "Optima" },
                    { 52, 10, false, "Sorento" },
                    { 53, 10, false, "Sportage" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
