using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebSalesMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Deparment Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Deparment department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSeles(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSeles(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime inital, DateTime final)
        {
            return Sales.Where(x => x.Date >= inital && x.Date <= final).Sum(x => x.Amount);
        }
    }
}