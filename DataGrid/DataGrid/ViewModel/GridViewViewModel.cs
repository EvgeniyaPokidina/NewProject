using DataGrid.Infrastructure.Commands;
using DataGrid.Models;
using DataGrid.Services;
using DataGrid.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DataGrid.ViewModel
{
    public class GridViewViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                if (value != _value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }


        private string _goodName;
        private string _goodDescription;
        private double _goodPrice;
        private int _goodQuantity;

        public string GoodName 
        { 
            get => _goodName; 
            set
            {
                _goodName = value;
                OnPropertyChanged(nameof(GoodName));
            }
        }
        public string GoodDescription 
        { 
            get => _goodDescription; 
            set
            {
                _goodDescription = value;
                OnPropertyChanged(nameof(GoodDescription));
            }
        }
        public double GoodPrice 
        { 
            get => _goodPrice; 
            set
            {
                _goodPrice = value;
                OnPropertyChanged(nameof(GoodPrice));
            }
        }
        public int GoodQuantity 
        { 
            get => _goodQuantity; 
            set
            {
                _goodQuantity = value;
                OnPropertyChanged(nameof(GoodQuantity));
            }
        }




        public ICommand AddNode { get; }
        public void OnAddNodeExecute(object obj)
        {
            Products.Add(new(Products[^1].Id + 1, GoodName, GoodDescription, GoodPrice, GoodQuantity));


            StringBuilder builder = new StringBuilder();

            foreach (Product product in Products)
            {
                builder.Append(product.Name + '\n');
            }

            MessageBox.Show(builder.ToString());
        }




        public ICommand ShowValue { get; }
        public void OnShowValueExecute(object obj)
        {
            Value = Products[4].Name;
        }




        public GridViewViewModel()
        {
            Products = ProductListCreator.GetProducts();
            ShowValue = new GetValueFromGridCommand((p) => true, OnShowValueExecute);
            AddNode = new GetValueFromGridCommand((p) => true, OnAddNodeExecute);
        }
    }
}
