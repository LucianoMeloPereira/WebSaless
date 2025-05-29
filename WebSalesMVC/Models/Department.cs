using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebSalesMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();


        public Department()
        {

        }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeleer(Seller seller)
        {
            Sellers.Add(seller);
        }

        public void RemoveSeleer(Seller seller)
        {
            Sellers.Remove(seller);
        }

        public double TotalSales(DateTime inital, DateTime final)
        {
            return Sellers.Sum(total => total.TotalSales(inital, final));
        }
    }
}
