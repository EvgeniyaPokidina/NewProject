using System.Numerics;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;

Random random = new Random();
Console.Write("Введите имя: ");
string name = Console.ReadLine()!;
Console.Write("Введите возраст: ");
int age = int.Parse(Console.ReadLine()!);
Console.Write("Введите баланс: ");
decimal balance = decimal.Parse(Console.ReadLine()!);
Console.Write("Введите число от 1 до 3: ");
int number = int.Parse(Console.ReadLine()!);
int randomNumber = random.Next(1, 4);
IUser user = new User(name, balance, age);

if (randomNumber == number)
{
    Console.WriteLine("Вы счастливый покупатель, ваш баланс удвоился");
    user = new HappyUser(name, balance, age);
}


CPU cpu1=new CPU("Intel Core i7", 27000, "China", 3200);
CPU cpu2 = new CPU("AMD", 25000, "China", 3000);
CoolingSystem coolingSystem= new CoolingSystem("Depcool", 600, "China", 100);
CoolingSystem coolingSystem1 = new CoolingSystem("Cooler master", 890, "China", 100);
List<Product> products = new List<Product>() {cpu1, cpu2, coolingSystem, coolingSystem1 };
public class Product
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public string? Manufacturer { get; set; }
    public Product(string? name, decimal? price, string? manufacturer)
    {
        Name = name;
        Price = price;
        Manufacturer = manufacturer;
    }
    public virtual decimal? GetDiscount(IUser user, DateOnly dateSale)
    {
        if (user.Spend > 50000) Price *= 0.9M;
        else if (user.Spend > 10000) Price *= 0.95M;
        Dictionary<int, int> calendar = new Dictionary<int, int>()
    {
    { 1,7},
    { 2,14},
    { 3,8},
    { 4,15},
    { 5,9},
    { 6,1},
    { 7,12},
    { 8,9},
    { 9,1},
    { 10,12},
    { 11,21},
    { 12,31}
    };
        foreach (var cal in calendar)
        {
            if (cal.Value == dateSale.Day && cal.Key == dateSale.Month) Price *= 0.8M;
        }
        return Price;
    }
}
public interface IUser
{ 
    public string Name { get; set; }
    public decimal? Balance { get; set; }
    public decimal? Spend { get; set; }
    public int? Age { get; set; }
    public void ReduceBalace(decimal price, int quantity);
}
public class User : IUser
{
    public string Name { get; set; }
    public decimal? Balance { get; set; }
    public decimal? Spend { get; set; }
    public int? Age { get; set; }
    public User(string name, decimal? balance, int? age)
    {
        Name = name;
        Balance = balance;
        Age = age;
        Spend = 0;
    }

    public void ReduceBalace(decimal price, int quantity)
    {
        decimal total = price * quantity;
        Spend += total;
        Balance -= total;
    }
}
public class CPU : Product
{
    public double Frequency { get; set; }
    public CPU(string? name, decimal? price, string? manufacturer, double frequency) : base(name, price, manufacturer)
    {
        Frequency = frequency;
    }
}
public class CoolingSystem : Product
{
    public double TDP { get; set; }
    public CoolingSystem(string? name, decimal? price, string? manufacturer, double tDP) : base(name, price, manufacturer)
    {
        TDP = tDP;
    }
    public override decimal? GetDiscount(IUser user, DateOnly dateSale)
    {
        if (user.Age > 30)
        {
            Price *= 0.75m;
        }
        return Price;
    }
}
public class HappyUser : User
{
    public HappyUser(string name, decimal? balance, int? age) : base(name, balance, age)
    {
        Balance = balance * 3;
    }
}
