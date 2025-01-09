using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.View.Elements
{
    /// <summary>
    /// Логика взаимодействия для TabElement.xaml
    /// </summary>
    public partial class TabElement : UserControl
    {
        private Main _main;
        public enum Lists { Books, Authors }
        public TabElement(string name, string source, Lists list, Main main)
        {
            InitializeComponent();
            _main = main;
            switch (list)
            {
                case Lists.Books:
                    this.MouseDown += (s, a) =>
                    {
                        _main.Books = new BookList();
                        _main.frame.Navigate(_main.Books);
                    };
                    break;
                case Lists.Authors:
                    this.MouseDown += (s, a) =>
                    {
                        _main.Authors = new AuthorList();
                        _main.frame.Navigate(_main.Authors);
                    };
                    break;
            }
            DataContext = new
            {
                Name = name,
                Source = source,

            };
        }
    }
}
