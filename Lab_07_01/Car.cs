using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Lab_07_01
{
    public class Car
    {
        /// <summary>
        /// ID записи
        /// </summary>
        public int CarId { get; set; }
        /// <summary>
        /// Бренд
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Модель
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Год выпуска
        /// </summary>
        public DateTime Year { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Продан ли автомобиль
        /// </summary>
        public bool Sold { get; set; }

        static SqlConnection connection;

        public Car()
        {
            var connString = ConfigurationManager.ConnectionStrings["CarDbConnection"].ConnectionString;
            connection = new SqlConnection(connString);
        }

        static Car()
        {
            var connString = ConfigurationManager.ConnectionStrings["CarDbConnection"].ConnectionString;
            connection = new SqlConnection(connString);
        }

        /// <summary>
        /// Переопределение метода ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("id={0} - Brand: {1} - Model: {2} - Year: {3} - Cost: {4} - Sold: {5}",CarId,Brand,Model,Year,Cost,Sold);
        }


        /// <summary>
        /// Получение списка всех автомобилей
        /// </summary>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<Car> GetAllCars()
        {
            var commandString = "SELECT CarId,Brand,Model,Year,Cost,Sold FROM Cars";
            SqlCommand getAllCommand = new SqlCommand(commandString, connection);

            connection.Open();
            var reader = getAllCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var carId = reader.GetInt32(0);
                    var brand = reader.GetString(1);
                    var model = reader.GetString(2);
                    var year = reader.GetDateTime(3);
                    var cost = reader.GetSqlDecimal(4);
                    var sold = reader.GetBoolean(5);

                    var car = new Car
                    {
                        CarId = carId,
                        Brand = brand,
                        Model = model,
                        Year = year,
                        Cost = cost.IsNull == true ? 0 : cost.Value,
                        Sold = sold
                    };
                    yield return car;
                }
            }

            connection.Close();
        }

        /// <summary>
        /// Добавление записи в базу данных
        /// </summary>
        public void Insert()
        {
            var commandString = "INSERT INTO Cars (Brand,Model,Year,Cost,Sold)" + "VALUES(@brand,@model,@year,@cost,@sold)";
            SqlCommand insertCommand = new SqlCommand(commandString, connection);

            insertCommand.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("brand",Brand),
                new SqlParameter("model",Model),
                new SqlParameter("year",Year),
                new SqlParameter("cost",Cost),
                new SqlParameter("sold",Sold)
            });

            connection.Open();
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Получение записи по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Car GetCar(int id)
        {
            foreach (var car in GetAllCars())
            {
                if (car.CarId == id)
                    return car;
            }
            return null;
        }

        /// <summary>
        /// Обновление текущей записи
        /// </summary>
        public void Update()
        {
            var commandString = "UPDATE Cars SET Brand=@brand,Model=@model,Year=@year,Cost=@cost,Sold=@sold WHERE(CarId=@id)";
            SqlCommand updateCommand = new SqlCommand(commandString, connection);

            updateCommand.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("brand",Brand),
                new SqlParameter("model",Model),
                new SqlParameter("year",Year),
                new SqlParameter("cost",Cost),
                new SqlParameter("sold",Sold),
                new SqlParameter("id",CarId)
            });

            connection.Open();
            updateCommand.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Удаление записи из базы данных
        /// </summary>
        /// <param name="id">СarId удаляемой записи</param>
        public static void Delete(int id)
        {
            var commandString = "DELETE FROM Cars WHERE(CarId=@id)";
            SqlCommand deleteCommand = new SqlCommand(commandString, connection);

            deleteCommand.Parameters.AddWithValue("id", id);
            connection.Open();
            deleteCommand.ExecuteNonQuery();
            connection.Close();
        }

    }
}
