using System;
using System.Collections.ObjectModel;
using Lab_09_01.BusinessLayer.Interfaces;
using Lab_09_01.DataLayer.Entities;
using Lab_09_01.BusinessLayer.Models;
using Lab_09_01.DataLayer.Interfaces;
using Lab_09_01.DataLayer.Repositories;
using AutoMapper;

namespace Lab_09_01.BusinessLayer.Services
{
    public class SellerService : ISellerService
    {
        IUnitOfWork dataBase;

        public SellerService(string name)
        {
            dataBase = new EntityUnitOfWork(name);
        }

        public void AddCarToSeller(int sellerId, CarViewModel car)
        {
            var seller = dataBase.Sellers.Get(sellerId);

            Mapper.Reset();

            // Конфигурировани AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<CarViewModel, Car>());
            // Отображение объекта CarViewModel на объект Car
            var c = Mapper.Map<Car>(car);
            // Добавить авмтомобиль
            seller.Cars.Add(c);
            // Сохранить изменения
            dataBase.Save();
        }

        public void CreateSeller(SellerViewModel seller) { throw new NotImplementedException(); }

        public void DeleteSeller(int sellerId) { throw new NotImplementedException(); }

        public SellerViewModel Get(int id) { throw new NotImplementedException(); }

        public ObservableCollection<SellerViewModel> GetAll()
        {
            Mapper.Reset();
            // Конфигурирование AutoMapper
            Mapper.Initialize(cfg => { cfg.CreateMap<Seller, SellerViewModel>(); cfg.CreateMap<Car, CarViewModel>(); });
            // Отображение List<Seller> на ObservableCollection<SellerViewModel>
            var sellers = Mapper.Map<ObservableCollection<SellerViewModel>>(dataBase.Sellers.GetAll()); return sellers;
        }

        public void RemoveCarFromSeller(int sellerId, int carId)
        {
            //var seller = dataBase.Sellers.Get(sellerId);
            var car = dataBase.Cars.Get(carId);

            Console.WriteLine(car.Model);

            dataBase.Cars.Delete(carId);
            ///////  
            //delete

            dataBase.Save();
        }

        public void UpdateCar(CarViewModel car)
        {
            var c = dataBase.Cars.Get(car.CarId);
            dataBase.Cars.Update(c);

            dataBase.Save();
        }

        public void UpdateSeller(SellerViewModel seller)
        {
            throw new NotImplementedException();
        }
    }
}
