using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
   public class Product
    {
        private int ıd;
        private string name;
        private decimal unitPrice;
        private int stockAmount;

        public int Id { get => ıd; set => ıd = value; }
        public string Name { get => name; set => name = value; }
        public decimal UnitPrice { get => unitPrice; set => unitPrice = value; }
        public int StockAmount { get => stockAmount; set => stockAmount = value; }
    }
}
