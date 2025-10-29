// ICarImageService (partial)
using CarDealershipApp.Data.Models;
using Microsoft.AspNetCore.Http;

public interface ICarImageService
{
    Task<CarImage> AddAsync(int carId, IFormFile file, string webRootPath);
    Task AddRangeAsync(int carId, IEnumerable<IFormFile> files, string webRootPath);
    Task<bool> DeleteByCarImageAsync(int carImageId, string webRootPath); // deletes DB entry + file
    Task DeleteByCarIdAsync(int carId, string webRootPath); // deletes all images for car (DB + files)
}
