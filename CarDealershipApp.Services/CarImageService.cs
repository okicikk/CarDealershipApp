using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealershipApp.Services
{
    public class CarImageService : ICarImageService
    {
        private readonly IRepository<CarImage> carImageRepository;

        public CarImageService(IRepository<CarImage> carImageRepository)
        {
            this.carImageRepository = carImageRepository;
        }

        public async Task<CarImage> AddAsync(int carId, IFormFile file, string webRootPath)
        {
            if (carId <= 0) throw new ArgumentException(nameof(carId));
            if (file == null || file.Length == 0) throw new ArgumentException(nameof(file));

            var uploadsFolder = Path.Combine(webRootPath, "images", "cars");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var safeName = Path.GetFileName(file.FileName);
            var uniqueName = $"{Guid.NewGuid()}_{safeName}";
            var fullPath = Path.Combine(uploadsFolder, uniqueName);

            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            var carImage = new CarImage
            {
                CarId = carId,
                FilePath = $"/images/cars/{uniqueName}"
            };

            await carImageRepository.AddAsync(carImage);
            return carImage;
        }

        public async Task AddRangeAsync(int carId, IEnumerable<IFormFile> files, string webRootPath)
        {
            foreach (var file in files.Where(f => f?.Length > 0))
            {
                await AddAsync(carId, file, webRootPath);
            }
        }

        public async Task<bool> DeleteByCarImageAsync(int carImageId, string webRootPath)
        {
            if (carImageId <= 0) return false;

            var entity = await carImageRepository.GetByIdAsync(carImageId);
            if (entity == null) return false;

            // delete DB
            var result = await carImageRepository.DeleteByIdAsync(carImageId);

            // best-effort remove file after successful DB delete
            if (result && !string.IsNullOrWhiteSpace(entity.FilePath))
            {
                try
                {
                    var trimmed = entity.FilePath.TrimStart('/');
                    var fullDiskPath = Path.Combine(webRootPath, trimmed.Replace('/', Path.DirectorySeparatorChar));
                    if (File.Exists(fullDiskPath)) File.Delete(fullDiskPath);
                }
                catch
                {
                    // swallow or log: non-fatal
                }
            }

            return result;
        }

        public async Task DeleteByCarIdAsync(int carId, string webRootPath)
        {
            if (carId <= 0) return;

            var allImages = await carImageRepository.GetAllAsync();
            var imagesForCar = allImages.Where(ci => ci.CarId == carId).ToList();
            if (!imagesForCar.Any()) return;

            foreach (var img in imagesForCar)
            {
                await DeleteByCarImageAsync(img.Id, webRootPath);
            }
        }
    }

}
