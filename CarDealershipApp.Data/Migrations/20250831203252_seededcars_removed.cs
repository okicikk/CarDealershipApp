using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarDealershipApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class seededcars_removed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e32b02a-046c-40be-bfeb-327c900e6bb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "919527f5-26a4-4b4b-98bf-3a11a2bc3a6e", "AQAAAAIAAYagAAAAEHHk7fyRDOFglYPSgeoyzDlZJpWEHq/73YYKwNJBpNODlvz0RG9bw1P3TUQKtBZrxg==", "27f06256-ac71-4ff2-a99f-5364d5c67238" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e32b02a-046c-40be-bfeb-327c900e6bb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26e78cb9-1fea-4478-8b7d-589c8bf7c935", "AQAAAAIAAYagAAAAEEgbzzUyLrUOS9KxyBCYQkoxZCMrHnw/9NJbCCBw81nPX7Tj4QnouuSYk5OETsuy7g==", "2330d0aa-2559-497b-a67b-6d0bb86e8ee8" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "IsDeleted", "ListedOn", "Mileage", "ModelId", "Price", "ReleaseYear", "SellerId", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 1, "A reliable Toyota Corolla with excellent fuel efficiency.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 45000, 1, 42000.50m, 2020, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1400.5 },
                    { 2, 2, 8, "A stylish Ford Mustang with impressive performance.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 12000, 8, 65000.75m, 2022, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1500.0 },
                    { 3, 3, 1, "Chevrolet Impala with spacious interiors and smooth handling.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 38000, 14, 48000.00m, 2019, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1600.0 },
                    { 4, 4, 1, "BMW M3 with dynamic handling and advanced features.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 15000, 22, 82000.99m, 2021, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1550.7 },
                    { 5, 5, 2, "Audi Q5 with a luxurious design and great performance.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 30000, 29, 62000.25m, 2020, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1450.3 },
                    { 6, 6, 1, "Mercedes-Benz E-Class with premium comfort and safety.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000, 33, 105000.75m, 2022, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1700.0 },
                    { 7, 7, 1, "Honda Civic, an economical choice with modern features.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 40000, 38, 40000.00m, 2019, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1400.0 },
                    { 8, 8, 2, "Nissan Rogue with great versatility and fuel economy.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 35000, 45, 45000.75m, 2020, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1500.0 },
                    { 9, 9, 2, "Hyundai Santa Fe with a sleek design and excellent durability.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 32000, 49, 48000.99m, 2021, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1480.5 },
                    { 10, 10, 2, "Kia Sportage with efficient performance and spacious interiors.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 29000, 53, 43000.50m, 2021, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1430.8 },
                    { 11, 3, 2, "Chevrolet Equinox with impressive handling and comfort.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 25000, 17, 60000.00m, 2021, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1600.0 },
                    { 12, 1, 2, "Toyota Land Cruiser with rugged capability and comfort.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 22000, 4, 85000.75m, 2022, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1900.0 },
                    { 13, 2, 3, "Ford F-150 with outstanding towing and hauling capacity.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 12000, 9, 100000.00m, 2023, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 2000.0 },
                    { 14, 7, 1, "Honda Accord, perfect balance of luxury and practicality.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 50000, 39, 42000.25m, 2018, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1350.0 },
                    { 15, 9, 1, "Hyundai Sonata with advanced safety features and great mileage.", false, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 36000, 47, 46000.50m, 2019, "4e32b02a-046c-40be-bfeb-327c900e6bb9", 1400.0 }
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
    }
}
