using CarDealership.ViewModels.Models.UserCarViewModels;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CarDealershipApp.Services
{
	public class UserCarService : IUserCarService
	{
		private readonly IRepository<UserCar> userCarRepository;
		private readonly IRepository<Car> carRepository;

		public UserCarService(IRepository<UserCar> userCarRepository, IRepository<Car> carRepo)
		{
			this.userCarRepository = userCarRepository;
			this.carRepository = carRepo;
		}

		public async Task AddAsync(int carId, string userId)
		{
			Car car = await carRepository.GetByIdAsync(carId); 
			if (car.IsDeleted)
			{
				throw new InvalidOperationException("Invalid car!");
			}

			await userCarRepository.AddAsync(new UserCar()
			{
				CarId = carId,
				UserId = userId
			});
		}

		public async Task<bool> DeleteAsync(int carId, string userId)
		{
			var userCar = await userCarRepository
				.GetAllQueryable()
				.Where(x => x.UserId == userId && x.CarId == carId)
				.FirstOrDefaultAsync();

			if (userCar is null)
			{
				return false;
			}
			await userCarRepository.DeleteAsync(userCar);
			return true;
		}

		public async Task<bool> DeleteByCarAsync(int carId)
		{
			List<UserCar> usersCars = await userCarRepository
				.GetAllQueryable()
				.Where(x => x.CarId == carId)
				.ToListAsync();

			foreach (var userCar in usersCars)
			{
				bool currentResult = await userCarRepository.DeleteAsync(userCar);
				if (currentResult == false)
				{
					break;
				}
			}

			return true;
		}


		public async Task<bool> DeleteByUserAsync(string userId)
		{
			List<UserCar> usersCars = await userCarRepository
				.GetAllQueryable()
				.Where(x => x.UserId == userId)
				.ToListAsync();

			foreach (var userCar in usersCars)
			{
				bool currentResult = await userCarRepository.DeleteAsync(userCar);
				if (currentResult == false)
				{
					break;
				}
			}

			return true;
		}

		public async Task<IEnumerable<UserCarIndexViewModel>> LoadUserCarsAsync(string userId)
		{
			List<UserCar> usersCars = await userCarRepository
				.GetAllQueryable()
				.Where(uc => uc.UserId == userId)
				.Include(uc => uc.Car)
				.ThenInclude(c=>c.Brand)
				.Include(b => b.Car)
				.ThenInclude(c=>c.Model)
				.ToListAsync();

			List<UserCarIndexViewModel> userCarIndexViewModels = usersCars.Select(x => new UserCarIndexViewModel()
			{
				UserId = x.UserId,
				CarId = x.CarId,
				CarName = $"{x.Car.Brand.Name} {x.Car.Model.Name}",
				CarPrice = x.Car.Price.ToString(),
				ImageUrl = x.Car.ImageUrls?[0] ?? ""
			}).ToList();
			return userCarIndexViewModels;
		}
	}
}
