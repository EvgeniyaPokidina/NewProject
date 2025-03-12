using DataGrid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.Services
{
    public class ProductListCreator
    {
        public static ObservableCollection<Product> GetProducts()
        {
            return new ObservableCollection<Product>
            {
                new(1, "Телефон", "Мощный телефон для игр", 5000, 2),
                new(2, "Планшет", "Выбор дизайнеров", 15000, 1),
                new(3, "Ноутбук", "Ноутбук для работы", 30000, 3),
                new(4, "Монитор", "Разрешение 4К", 12000, 4),
                new(5, "Клавиатура", "Механическая белая", 2500, 5),
                new(6, "Мышь", "С подсветкой", 1800, 6),
                new(7, "Принтер", "Цветной лазерный", 8000, 7),
                new(8, "Колонки", "Чистый хороший звук", 4000, 8),
                new(9, "Флешка", "Объем 10 петабайт", 2000, 9),
            };
        }
    }
}
