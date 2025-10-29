using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealershipApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class carImageAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "CarImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarImage_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e32b02a-046c-40be-bfeb-327c900e6bb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc36e662-1890-40aa-affe-c6822f409bd4", "AQAAAAIAAYagAAAAEBw2ynkkUEC/ekV1gYHXc0TSHgD1W5FJAaKkq98REAWT0Va2rWoqWF1pxZ3aBaAJhw==", "96f3e59d-1be5-4264-bf19-794a2ce90891" });

            migrationBuilder.CreateIndex(
                name: "IX_CarImage_CarId",
                table: "CarImage",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarImage");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e32b02a-046c-40be-bfeb-327c900e6bb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f568a3a5-bfeb-464a-8619-32cc4dad0c58", "AQAAAAIAAYagAAAAEFKXRKRu2tvjXpuXCN9Shm/pwylM0R7exxPO+Tu9KRvfRKHN5ZLbIZc6ltPLS3WE2A==", "7680d3ea-f6cf-47a9-998d-cf28a8f741e8" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrls",
                value: "[\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRsKhK18-GZ4VQLvl5XkQ0KfV0tikZCdKAGOA\\u0026s\",\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRGBEBTWe0ZYNqPpTROzVSE0nWAd1tP1xgO8g\\u0026s\",\"https://di-uploads-pod12.dealerinspire.com/beavertoyotaofcumming/uploads/2019/02/Screen-Shot-2019-02-07-at-8.18.09-AM.png\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrls",
                value: "[\"https://www.mustangspecs.com/wp-content/uploads/2022/01/Race-Red-2022-Ford-Mustang-1-1400x933.webp\",\"https://www.prestonford.com/static/dealer-19389/PF-LPI-22Mustang-HERO.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrls",
                value: "[\"https://www.bluediamondgm.com/static/brand-chevrolet/vehicle/2019/Chevrolet/Impala/MRP/01.jpg\",\"https://www.martin-chevy.com/blogs/808/wp-content/uploads/2019/05/2019-Chevy-Impala-Crystal-Lake-IL.jpg\",\"https://cars.usnews.com/static/images/Auto/izmo/i92367443/2019_chevrolet_impala_dashboard.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrls",
                value: "[\"https://cdn.motor1.com/images/mgl/o6xgE/s1/2021-bmw-m3-exterior.jpg\",\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUjaHBP8n-W4k4UaMGF7uNuBrKtds8XBMUvQ\\u0026s\",\"https://cimg0.ibsrv.net/ibimg/hgm/1920x1080-1/100/762/2021-bmw-3-series_100762324.jpg\",\"https://cdn.bmwblog.com/wp-content/uploads/2020/09/2021-bmw-m3-competition-exterior-42.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrls",
                value: "[\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTPoPhHOQ__Np_7IECuI1DWHEV-bE8Kym_XtQ\\u0026s\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrls",
                value: "[\"https://s3.envato.com/files/350663522/preview%20image/2.jpg\",\"https://www.platinumautohaus.com/imagetag/14754/2/l/Used-2022-Mercedes-Benz-E-Class-E-350.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrls",
                value: "[\"https://wallpapers.com/images/hd/honda-civic-pictures-1920-x-1080-20gy3dz4i08ofh3c.jpg\",\"https://di-uploads-pod6.dealerinspire.com/hondauniverse/uploads/2019/05/2019-Honda-Civic-Sedan-Interior-seating.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrls",
                value: "[\"https://blog.consumerguide.com/wp-content/uploads/sites/2/2020/02/Screen-Shot-2020-02-13-at-3.16.30-PM.png\",\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ4S1VVSnengNt2tMXz9M0uVRYE5NNMTFdWbg\\u0026s\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrls",
                value: "[\"https://vicimus-glovebox7.s3.us-east-2.amazonaws.com/photos/FS0mAAjrxQqA/KM8S5DA14MU005124/000ca0b0b8ba8517cc5193473d18eafc.jpg\",\"https://di-uploads-pod27.dealerinspire.com/greghublerhyundai/uploads/2021/04/cd6Kwe9AvexQyFemXUFILTqq1ED6kIo8iXPSflx6.jpeg\",\"https://www.hyundaioman.com/content/dam/hyundai/template_en/en/images/find-a-car/pip/santa-fe-2021/interior/interior-color-3-original.png\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrls",
                value: "[\"https://hips.hearstapps.com/hmg-prod/images/2021-kia-sportage-mmp-2-1601580481.jpg\",\"https://media.ed.edmunds-media.com/kia/sportage/2020/oem/2020_kia_sportage_4dr-suv_sx-turbo_fq_oem_1_1600.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrls",
                value: "[\"https://www.carscoops.com/wp-content/uploads/2020/02/2021-Chevrolet-Equinox-1-1.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrls",
                value: "[\"https://i.pinimg.com/736x/45/84/82/458482adaacfa98fae14b7c1b3d9037d.jpg\",\"https://www.autopediame.com/storage/images/Toyota/Land%20Cruiser/Toyota-Land_Cruiser-2022-1024-0a.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrls",
                value: "[\"https://di-sitebuilder-assets.s3.amazonaws.com/Ford/MLP/f150/2023/color-Atlas-Blue.jpg\",\"https://www.ford.ca/is/image/content/dam/vdm_ford/live/en_ca/ford/nameplate/f-150commercial/2023/collections/dm/22_FRD_F15_49146_pk.tif?croppathe=1_3x2\\u0026wid=720\\u0026fit=crop\\u0026hei=480\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrls",
                value: "[\"https://www.accordxclub.com/attachments/b11e2328-e12a-4956-954c-a1ce55166603-jpeg.7159/\",\"https://dealerinspire-image-library-prod.s3.us-east-1.amazonaws.com/images/I12X3wbNlM3AoiU5oEXJR459Q7fPIRbtZIRxYGe6.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrls",
                value: "[\"https://www.zimbrickfishhatcheryroad.com/blogs/2133/wp-content/uploads/2019/07/2019-hyundai-sonata-fish-hatchery.jpg\",\"https://www.formulaimports.com/imagetag/3778/8/l/Used-2019-Hyundai-Sonata-SE-1720454838.jpg\"]");
        }
    }
}
