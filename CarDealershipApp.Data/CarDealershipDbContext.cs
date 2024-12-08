﻿using CarDealershipApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Reflection.Emit;

namespace CarDealershipApp.Data
{
	public class CarDealershipDbContext : IdentityDbContext
	{
		public CarDealershipDbContext(DbContextOptions options)
			: base(options)
		{
		}



		public DbSet<Car> Cars { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Model> Models { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Feature> Features { get; set; }
		public DbSet<CarFeature> CarsFeatures { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<CarFeature>()
				.HasKey(cf => new { cf.CarId, cf.FeatureId });

			builder.Entity<CarFeature>()
				.HasOne(cf => cf.Car)
				.WithMany(f => f.CarsFeatures)
				.HasForeignKey(cf => cf.CarId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<CarFeature>()
			   .HasOne(cf => cf.Feature)
			   .WithMany(f => f.CarsFeatures)
			   .HasForeignKey(cf => cf.FeatureId)
			   .OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Car>()
				.HasOne(c => c.Model)
				.WithMany()
				.HasForeignKey(c => c.ModelId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Model>()
				.HasOne(m => m.Brand)
				.WithMany(b => b.Models)
				.HasForeignKey(m => m.BrandId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Car>()
				.Property(c => c.Price)
				.HasColumnType("decimal(18, 2)");

			var categories = LoadJsonData<Category>("../CarDealershipApp.Data/SeedData/Category.json");
			builder.Entity<Category>().HasData(categories);

			var features = LoadJsonData<Feature>("../CarDealershipApp.Data/SeedData/Feature.json");
			builder.Entity<Feature>().HasData(features);

			var brandAndModels = LoadJsonData<Brand>("../CarDealershipApp.Data/SeedData/BrandAndModels.json");

			foreach (var brand in brandAndModels)
			{
				builder.Entity<Brand>().HasData(new Brand
				{
					Id = brand.Id,
					Name = brand.Name,
					ImageUrl = brand.ImageUrl,
					IsDeleted = brand.IsDeleted
				});

				foreach (var model in brand.Models)
				{
					builder.Entity<Model>().HasData(new Model
					{
						Id = model.Id,
						Name = model.Name,
						BrandId = brand.Id,
						IsDeleted = model.IsDeleted
					});
				}

			}

			var cars = LoadJsonData<Car>("../CarDealershipApp.Data/SeedData/Car.json");

			foreach (var car in cars)
			{
				int carId = car.Id;
				Car carToBeAdded = new Car()
				{
					Id = car.Id,
					BrandId = car.BrandId,
					ModelId = car.ModelId,
					SellerId = car.SellerId,
					CategoryId = car.CategoryId,
					Price = car.Price,
					Weight = car.Weight,
					Description = car.Description,
					Mileage = car.Mileage,
					ImageUrls = car.ImageUrls,
					ReleaseYear = car.ReleaseYear,
					ListedOn = car.ListedOn,
					IsDeleted = false,
				};

				builder.Entity<Car>()
					.HasData(carToBeAdded);

				builder.Entity<CarFeature>()
					.HasData(car.CarsFeatures
						.Select(x => new CarFeature()
						{
							CarId = carId,
							FeatureId = x.FeatureId,
						})
					.ToList());

			}
		}

		private List<T> LoadJsonData<T>(string filePath)
		{
			string json = File.ReadAllText(filePath);
			return JsonConvert.DeserializeObject<List<T>>(json);
		}
	}
}
