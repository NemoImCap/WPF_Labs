using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_10_01
{
    [Serializable]
    public class Order
    {
        private int number;

        [XmlAttribute]
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private string manager;

        public string Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        private decimal cost;

        public decimal Cost
        {
            get { return cost; }
        }

        public void increaseCost(decimal incCost)
        {
            if (cost <= 4000) cost += incCost;
        }

        public void dicreaseCost(decimal decCost)
        {
            cost = cost == 0 ? 0 : cost - decCost;
        }

        public Order()
        {
        }

        public Order(int number, DateTime date, string manager)
        {
            this.number = number;
            this.date = date;
            this.manager = manager;
            this.cost = 0;
        }

        public override string ToString()
        {
            return string.Format("Номер: {0} Дата: {1} Менеджер: {2} Цена: {3}",number,date,manager,cost);
        }
    }
}
