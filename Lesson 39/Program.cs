using System.ComponentModel.Design;
using System.Security.Authentication;
List<Person> persons = new List<Person>()
{
    new Person(){Id=1, Name="Иван",Age=35},
    new Person(){Id=2,Name="Борис",Age=24},
    new Person(){Id=3,Name="Федор",Age=59}
};
List<Product> priceList = new List<Product>
{
    new Product(){Name="Asus",ProductType=TypeOfProduct.MotherBoard,Price=15000,Id=1},
    new Product(){Name="Intel",ProductType=TypeOfProduct.Cpu,Price=9000,Id=2},
    new Product(){Name="NVidia",ProductType=TypeOfProduct.VideoCard,Price=12000,Id=3},
    new Product(){Name="GeForce",ProductType=TypeOfProduct.MotherBoard,Price=17500,Id=4},
    new Product(){Name="AMD",ProductType=TypeOfProduct.Cpu,Price=11000,Id=5},
    new Product(){Name="MegaByte",ProductType=TypeOfProduct.VideoCard,Price=14000,Id=6},
    new Product(){Name="MSI",ProductType=TypeOfProduct.PowerUnit,Price=500,Id=7,Discount=0.5M},
    new Product(){Name="Tower",ProductType=TypeOfProduct.Frame,Price=800,Id=8},
    new Product(){Name="Water",ProductType=TypeOfProduct.CoolingSystem,Price=200,Id=9}
};
Console.Write("Введите ваше имя:");
string name = Console.ReadLine()!;
Console.Write("Введите ваш баланс:");
decimal balance = decimal.Parse(Console.ReadLine()!);
Person person = persons.FirstOrDefault(p => p.Name == name)!;
decimal discount = 0;
DateOnly dateSale = new DateOnly();
Console.Write("Введите дату продажи в формате YYYY.MM.dd:");
dateSale = DateOnly.Parse(Console.ReadLine()!);
Dictionary<int, int> calendar = new Dictionary<int, int>()
{
        {1,7},
        {2,14},
        {3,8},
        {4,15},
        {5,9},
        {6,1},
        {7,12},
        {8,9},
        {9,1},
        {10,12},
        {11,21},
        {12,31}
};
if (person == null)
    Console.WriteLine("Такого пользователя нет!");
else
{

    person.Balance = balance;
    decimal Startbalance = balance;
    SalePerson salePerson = new SalePerson();
    salePerson.Customer = person;
    salePerson.Sales = new List<Sale>();
    do
    {
        Console.Clear();
        Console.WriteLine("Ваше имя:" + person.Name);
        Console.WriteLine("Ваш возраст:" + person.Age);
        Console.WriteLine("Сегодня:" + dateSale);
        Console.WriteLine($"Ваш баланс:{person.Balance}");
        Console.WriteLine("Список товаров:");
        foreach (var item in priceList)
        {
            foreach (var cal in calendar)
            {
                discount = (cal.Value == dateSale.Day && cal.Key == dateSale.Month) ? 0.2M : 0;
            }
            if (Startbalance - person.Balance > 50000)
            {
                discount = discount + 0.1M;
            }
            else if (Startbalance - person.Balance > 10000 && Startbalance - person.Balance <= 50000)
            {
                discount = discount + 0.05M;
            }

            if (item.Discount == 0)
                Console.WriteLine($"Id:{item.Id}. Name:{item.Name} Type:{item.ProductType}" +
                $" Price:{item.Price - item.Price * discount}");
            else
            {
                if (item.Name == "Water" && person.Age > 30)
                {
                    Console.WriteLine($"Id:{item.Id}. Name:{item.Name} Type:{item.ProductType}" +
                   $" Price:{item.Price - item.Price * (discount + 0.25M)}");
                }
                else if (calendar.ContainsValue(dateSale.Day) && calendar.ContainsKey(dateSale.Month) == false)
                    Console.WriteLine($"Id:{item.Id}. Name:{item.Name} Type:{item.ProductType}" +
                    $" Price:{item.Price - item.Price * (item.Discount)}");
                else Console.WriteLine($"Id:{item.Id}. Name:{item.Name} Type:{item.ProductType}" +
                      $" Price:{item.Price - item.Price * (item.Discount + 0.2M)}");

            }
        }
        Console.WriteLine("Список покупок:");
        foreach (var item in salePerson.Sales)
        {
            Console.WriteLine(item.Product.Name + " " + item.Count + " " + item.Total());
        }
        if (person.Balance > 0)
        {
            Console.Write("Введите номер товара:");
            int id = int.Parse(Console.ReadLine()!);
            Console.Write("Введите его количество:");
            int count = int.Parse(Console.ReadLine()!);
            Sale sale = new Sale();
            sale.Product = priceList.First(p => p.Id == id);
            sale.Count = count;
            if (person.Balance >= sale.Total())
            {
                if (sale.Product.Discount == 0)
                    sale.Discount = discount;
                else
                    sale.Discount = sale.Product.Discount;
                salePerson.Sales.Add(sale);
                person.Balance -= sale.Total();
            }
            else
            {
                Console.WriteLine("Не хватает средств для покупки!");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("На счету нет денег!");
            Console.ReadKey();
        }
    }
    while (true);
}
enum TypeOfProduct
{
    MotherBoard,
    Cpu,
    VideoCard,
    PowerUnit,
    Frame,
    CoolingSystem
}
class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TypeOfProduct ProductType { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public int Age { get; set; }
}
class Sale
{
    public Product Product { get; set; }
    public int Count { get; set; }
    public decimal Discount { get; set; }
    public decimal Total() => Count * Product.Price - Count * Product.Price * Discount;
}
class SalePerson
{
    public Person Customer { get; set; }
    public List<Sale> Sales { get; set; }
    public decimal GetTotal(int count) => Sales.Sum(p => p.Total());
}
