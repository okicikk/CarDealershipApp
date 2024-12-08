using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarDealershipApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CarsSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "ImageUrls", "IsDeleted", "ListedOn", "Mileage", "ModelId", "Price", "ReleaseYear", "SellerId", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 2, "A reliable Toyota Corolla with excellent fuel efficiency.", "[\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRsKhK18-GZ4VQLvl5XkQ0KfV0tikZCdKAGOA\\u0026s\",\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRGBEBTWe0ZYNqPpTROzVSE0nWAd1tP1xgO8g\\u0026s\",\"https://di-uploads-pod12.dealerinspire.com/beavertoyotaofcumming/uploads/2019/02/Screen-Shot-2019-02-07-at-8.18.09-AM.png\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 45000, 1, 42000.50m, 2020, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1400.5 },
                    { 2, 2, 3, "A stylish Ford Mustang with impressive performance.", "[\"https://www.mustangspecs.com/wp-content/uploads/2022/01/Race-Red-2022-Ford-Mustang-1-1400x933.webp\",\"https://www.prestonford.com/static/dealer-19389/PF-LPI-22Mustang-HERO.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 12000, 8, 65000.75m, 2022, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1500.0 },
                    { 3, 3, 1, "Chevrolet Impala with spacious interiors and smooth handling.", "[\"https://www.bluediamondgm.com/static/brand-chevrolet/vehicle/2019/Chevrolet/Impala/MRP/01.jpg\",\"https://www.martin-chevy.com/blogs/808/wp-content/uploads/2019/05/2019-Chevy-Impala-Crystal-Lake-IL.jpg\",\"https://cars.usnews.com/static/images/Auto/izmo/i92367443/2019_chevrolet_impala_dashboard.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 38000, 14, 48000.00m, 2019, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1600.0 },
                    { 4, 4, 2, "BMW M3 with dynamic handling and advanced features.", "[\"https://cdn.motor1.com/images/mgl/o6xgE/s1/2021-bmw-m3-exterior.jpg\",\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUjaHBP8n-W4k4UaMGF7uNuBrKtds8XBMUvQ\\u0026s\",\"https://cimg0.ibsrv.net/ibimg/hgm/1920x1080-1/100/762/2021-bmw-3-series_100762324.jpg\",\"https://cdn.bmwblog.com/wp-content/uploads/2020/09/2021-bmw-m3-competition-exterior-42.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 15000, 22, 82000.99m, 2021, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1550.7 },
                    { 5, 5, 4, "Audi Q5 with a luxurious design and great performance.", "[\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTPoPhHOQ__Np_7IECuI1DWHEV-bE8Kym_XtQ\\u0026s\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 30000, 29, 62000.25m, 2020, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1450.3 },
                    { 6, 6, 3, "Mercedes-Benz E-Class with premium comfort and safety.", "[\"https://s3.envato.com/files/350663522/preview%20image/2.jpg\",\"https://www.platinumautohaus.com/imagetag/14754/2/l/Used-2022-Mercedes-Benz-E-Class-E-350.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000, 33, 105000.75m, 2022, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1700.0 },
                    { 7, 7, 1, "Honda Civic, an economical choice with modern features.", "[\"https://wallpapers.com/images/hd/honda-civic-pictures-1920-x-1080-20gy3dz4i08ofh3c.jpg\",\"https://di-uploads-pod6.dealerinspire.com/hondauniverse/uploads/2019/05/2019-Honda-Civic-Sedan-Interior-seating.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 40000, 38, 40000.00m, 2019, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1400.0 },
                    { 8, 8, 4, "Nissan Rogue with great versatility and fuel economy.", "[\"https://blog.consumerguide.com/wp-content/uploads/sites/2/2020/02/Screen-Shot-2020-02-13-at-3.16.30-PM.png\",\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ4S1VVSnengNt2tMXz9M0uVRYE5NNMTFdWbg\\u0026s\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 35000, 45, 45000.75m, 2020, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1500.0 },
                    { 9, 9, 2, "Hyundai Santa Fe with a sleek design and excellent durability.", "[\"https://vicimus-glovebox7.s3.us-east-2.amazonaws.com/photos/FS0mAAjrxQqA/KM8S5DA14MU005124/000ca0b0b8ba8517cc5193473d18eafc.jpg\",\"https://di-uploads-pod27.dealerinspire.com/greghublerhyundai/uploads/2021/04/cd6Kwe9AvexQyFemXUFILTqq1ED6kIo8iXPSflx6.jpeg\",\"https://www.hyundaioman.com/content/dam/hyundai/template_en/en/images/find-a-car/pip/santa-fe-2021/interior/interior-color-3-original.png\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 32000, 49, 48000.99m, 2021, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1480.5 },
                    { 10, 10, 3, "Kia Sportage with efficient performance and spacious interiors.", "[\"https://hips.hearstapps.com/hmg-prod/images/2021-kia-sportage-mmp-2-1601580481.jpg\",\"https://media.ed.edmunds-media.com/kia/sportage/2020/oem/2020_kia_sportage_4dr-suv_sx-turbo_fq_oem_1_1600.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 29000, 53, 43000.50m, 2021, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1430.8 },
                    { 11, 3, 4, "Chevrolet Equinox with impressive handling and comfort.", "[\"https://www.carscoops.com/wp-content/uploads/2020/02/2021-Chevrolet-Equinox-1-1.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 25000, 17, 60000.00m, 2021, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1600.0 },
                    { 12, 1, 2, "Toyota Land Cruiser with rugged capability and comfort.", "[\"https://i.pinimg.com/736x/45/84/82/458482adaacfa98fae14b7c1b3d9037d.jpg\",\"https://www.autopediame.com/storage/images/Toyota/Land%20Cruiser/Toyota-Land_Cruiser-2022-1024-0a.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 22000, 4, 85000.75m, 2022, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1900.0 },
                    { 13, 2, 3, "Ford F-150 with outstanding towing and hauling capacity.", "[\"https://di-sitebuilder-assets.s3.amazonaws.com/Ford/MLP/f150/2023/color-Atlas-Blue.jpg\",\"https://www.ford.ca/is/image/content/dam/vdm_ford/live/en_ca/ford/nameplate/f-150commercial/2023/collections/dm/22_FRD_F15_49146_pk.tif?croppathe=1_3x2\\u0026wid=720\\u0026fit=crop\\u0026hei=480\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 12000, 9, 100000.00m, 2023, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 2000.0 },
                    { 14, 7, 1, "Honda Accord, perfect balance of luxury and practicality.", "[\"https://www.accordxclub.com/attachments/b11e2328-e12a-4956-954c-a1ce55166603-jpeg.7159/\",\"https://dealerinspire-image-library-prod.s3.us-east-1.amazonaws.com/images/I12X3wbNlM3AoiU5oEXJR459Q7fPIRbtZIRxYGe6.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 50000, 39, 42000.25m, 2018, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1350.0 },
                    { 15, 9, 3, "Hyundai Sonata with advanced safety features and great mileage.", "[\"https://www.zimbrickfishhatcheryroad.com/blogs/2133/wp-content/uploads/2019/07/2019-hyundai-sonata-fish-hatchery.jpg\",\"https://www.formulaimports.com/imagetag/3778/8/l/Used-2019-Hyundai-Sonata-SE-1720454838.jpg\"]", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 36000, 47, 46000.50m, 2019, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1400.0 }
                });

            migrationBuilder.InsertData(
                table: "CarsFeatures",
                columns: new[] { "CarId", "FeatureId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 5 },
                    { 2, 3 },
                    { 2, 10 },
                    { 2, 15 },
                    { 3, 7 },
                    { 3, 20 },
                    { 3, 25 },
                    { 4, 8 },
                    { 4, 11 },
                    { 4, 31 },
                    { 5, 6 },
                    { 5, 13 },
                    { 5, 21 },
                    { 6, 9 },
                    { 6, 12 },
                    { 6, 13 },
                    { 7, 4 },
                    { 7, 17 },
                    { 7, 28 },
                    { 8, 5 },
                    { 8, 19 },
                    { 8, 26 },
                    { 9, 10 },
                    { 9, 15 },
                    { 9, 27 },
                    { 10, 2 },
                    { 10, 18 },
                    { 10, 24 },
                    { 11, 14 },
                    { 11, 20 },
                    { 11, 31 },
                    { 12, 16 },
                    { 12, 22 },
                    { 12, 30 },
                    { 13, 3 },
                    { 13, 15 },
                    { 13, 25 },
                    { 14, 4 },
                    { 14, 14 },
                    { 14, 23 },
                    { 15, 7 },
                    { 15, 17 },
                    { 15, 29 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 2, 15 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 3, 20 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 3, 25 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 4, 11 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 4, 31 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 5, 13 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 5, 21 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 6, 9 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 6, 12 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 6, 13 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 7, 17 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 7, 28 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 8, 19 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 8, 26 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 9, 10 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 9, 15 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 9, 27 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 10, 18 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 10, 24 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 11, 14 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 11, 20 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 11, 31 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 12, 16 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 12, 22 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 12, 30 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 13, 3 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 13, 15 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 13, 25 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 14, 4 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 14, 14 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 14, 23 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 15, 7 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 15, 17 });

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumns: new[] { "CarId", "FeatureId" },
                keyValues: new object[] { 15, 29 });

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
