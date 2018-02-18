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
    public class SellerService
    {
        IUnitOfWork dataBase;
        public SellerService(string name)
        {
            dataBase = new EntityUnitOfWork(name);
        }

        public void AddCarToSeller(int sellerId, CarViewModel car)
        {
            var seller = dataBase.Sellers.Get(sellerId);

            // Конфигурировани AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<CarViewModel, Car>());
            // Отображение объекта CarViewModel на объект Car
            var c = Mapper.Map<Car>(car);
            // Добавить студента
            seller.Cars.Add(c);
            // Сохранить изменения
            dataBase.Save();
        }

        public void CreateSeller(SellerViewModel seller) { throw new NotImplementedException(); }

        public void DeleteSeller(int sellerId) { throw new NotImplementedException(); }

        public SellerViewModel Get(int id) { throw new NotImplementedException(); }

        public ObservableCollection<SellerViewModel> GetAll()
        {
            // Конфигурирование AutoMapper
            Mapper.Initialize(cfg => { cfg.CreateMap<Seller, SellerViewModel>(); cfg.CreateMap<Car, CarViewModel>(); });
            // Отображение List<Seller> на ObservableCollection<SellerViewModel>
            var sellers = Mapper.Map<ObservableCollection<SellerViewModel>>(dataBase.Sellers.GetAll()); return sellers;
        }

        public void RemoveCarFromSeller(int sellerId, int carId) { throw new NotImplementedException(); }

        public void UpdateSeller(SellerViewModel seller) { throw new NotImplementedException(); }
    }
}
