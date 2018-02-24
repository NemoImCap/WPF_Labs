using Lab_09_01.BusinessLayer.Models;
using System.Collections.ObjectModel;

namespace Lab_09_01.BusinessLayer.Interfaces
{
    public interface ISellerService
    {
        ObservableCollection<SellerViewModel> GetAll();
        SellerViewModel Get(int id);
        void AddCarToSeller(int sellerId, CarViewModel car);
        void RemoveCarFromSeller(int sellerId,int carId);
        void CreateSeller(SellerViewModel seller);
        void DeleteSeller(int sellerId);
        void UpdateSeller(SellerViewModel seller);
        void UpdateCar(CarViewModel car);
    }
}
