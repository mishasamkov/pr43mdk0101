using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;
using System.Windows;
using System.Xml.Linq;
using WpfApp1.Classes.DataBase;
using WpfApp1.Classes;

namespace WpfApp1.Context
{
    public class BooksContext : Books
    {
        private bool _isNew = true;
        public BooksContext(bool save = false)
        {
            if (save) save = true;
            Author = new Authors();
        }
        public static ObservableCollection<BooksContext> AllBooks()
        {
            ObservableCollection<BooksContext> allBooks = new ObservableCollection<BooksContext>();
            ObservableCollection<AuthorsContext> allAuthors = AuthorsContext.AllAuthors();
            SqlConnection connection;
            SqlDataReader dataItems = Connection.Query("Select * from [dbo].[Books]", out connection);
            while (dataItems.Read())
            {
                allBooks.Add(new BooksContext()
                {
                    Id = dataItems.GetInt32(0),
                    Name = dataItems.GetString(1),
                    Author = dataItems.IsDBNull(2) ? null : allAuthors.Where(x => x.Id == dataItems.GetInt32(2)).First(),
                    Description = dataItems.GetString(3),
                });
            }
            Connection.CloseConnection(connection);
            return allBooks;
        }
        public void Save()
        {
            SqlConnection connection;
            if (_isNew)
            {
                try
                {
                    SqlDataReader dataItems = Connection.Query("Insert into " +
                        "[dbo].[Books](" +
                        "Name, " +
                        "Author, " +
                        "Description) " +
                        "OUTPUT Inserted.Id " +
                        "Values (" +
                        $"N'{Name}', " +
                        $"{Author.Id}, " +
                        $"N'{Description}')", out connection);
                    dataItems.Read();
                    Id = dataItems.GetInt32(0);
                    Connection.CloseConnection(connection);
                    MessageBox.Show("Действие выполнено!", "Уведмление");
                }
                catch
                {
                    MessageBox.Show("Не удалось выполнить действие!", "Уведмление");
                }
            }
            else
            {
                try
                {
                    Connection.Query("Update [dbo].[Books] " +
                        "Set " +
                        $"Name = N'{Name}', " +
                        $"Author = {Author.Id}, " +
                        $"Description = N'{Description}' " +
                        "Where " +
                        $"Id = {Id}", out connection);
                    Connection.CloseConnection(connection);
                    MessageBox.Show("Действие выполнено!", "Уведмление");
                }
                catch
                {
                    MessageBox.Show("Не удалось выполнить действие!", "Уведмление");
                }
            }
            MainWindow.MW.frame.Navigate(MainWindow.Main);
        }
        public void Delete()
        {
            try
            {
                SqlConnection connection;
                Connection.Query("Delete from [dbo].[Books] " +
                    "Where " +
                    $"Id = {Id}", out connection);
                Connection.CloseConnection(connection);
                MessageBox.Show("Действие выполнено!", "Уведмление");
            }
            catch
            {
                MessageBox.Show("Не удалось выполнить действие!", "Уведмление");
            }
        }
        public RelayCommand OnEdit
        {
            get
            {
                _isNew = false;
                return new RelayCommand(obj => MainWindow.MW.frame.Navigate(new View.AddBook(this)));
            }
        }
        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Author = AuthorsContext.AllAuthors().Where(x => x.Id == Author.Id).First();
                    Save();
                    _isNew = true;
                });
            }
        }
        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Delete();
                    (MainWindow.Main.Books.DataContext as ViewModell.VMBooks).Books.Remove(this);
                });
            }
        }
    }
    public class BooksContext : Books
    {
        private bool _isNew = true;
        public BooksContext(bool save = false)
        {
            if (save) save = true;
            Author = new Authors();
        }
        public static ObservableCollection<BooksContext> AllBooks()
        {
            ObservableCollection<BooksContext> allBooks = new ObservableCollection<BooksContext>();
            ObservableCollection<AuthorsContext> allAuthors = AuthorsContext.AllAuthors();
            SqlConnection connection;
            SqlDataReader dataItems = Connection.Query("Select * from [dbo].[Books]", out connection);
            while (dataItems.Read())
            {
                allBooks.Add(new BooksContext()
                {
                    Id = dataItems.GetInt32(0),
                    Name = dataItems.GetString(1),
                    Author = dataItems.IsDBNull(2) ? null : allAuthors.Where(x => x.Id == dataItems.GetInt32(2)).First(),
                    Description = dataItems.GetString(3),
                });
            }
            Connection.CloseConnection(connection);
            return allBooks;
        }
        public void Save()
        {
            SqlConnection connection;
            if (_isNew)
            {
                try
                {
                    SqlDataReader dataItems = Connection.Query("Insert into " +
                    "[dbo].[Books](" +
                        "Name, " +
                        "Author, " +
                        "Description) " +
                        "OUTPUT Inserted.Id " +
                        "Values (" +
                        $"N'{Name}', " +
                        $"{Author.Id}, " +
                        $"N'{Description}')", out connection);
                    dataItems.Read();
                    Id = dataItems.GetInt32(0);
                    Connection.CloseConnection(connection);
                    MessageBox.Show("Действие выполнено!", "Уведмление");
                }
                catch
                {
                    MessageBox.Show("Не удалось выполнить действие!", "Уведмление");
                }
            }
            else
            {
                try
                {
                    Connection.Query("Update [dbo].[Books] " +
                        "Set " +
                        $"Name = N'{Name}', " +
                        $"Author = {Author.Id}, " +
                        $"Description = N'{Description}' " +
                        "Where " +
                        $"Id = {Id}", out connection);
                    Connection.CloseConnection(connection);
                    MessageBox.Show("Действие выполнено!", "Уведмление");
                }
                catch
                {
                    MessageBox.Show("Не удалось выполнить действие!", "Уведмление");
                }
            }
            MainWindow.MW.frame.Navigate(MainWindow.Main);
        }
        public void Delete()
        {
            try
            {
                SqlConnection connection;
                Connection.Query("Delete from [dbo].[Books] " +
                    "Where " +
                    $"Id = {Id}", out connection);
                Connection.CloseConnection(connection);
                MessageBox.Show("Действие выполнено!", "Уведмление");
            }
            catch
            {
                MessageBox.Show("Не удалось выполнить действие!", "Уведмление");
            }
        }
        public RelayCommand OnEdit
        {
            get
            {
                _isNew = false;
                return new RelayCommand(obj => MainWindow.MW.frame.Navigate(new View.AddBook(this)));
            }
        }
        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Author = AuthorsContext.AllAuthors().Where(x => x.Id == Author.Id).First();
                    Save();
                    _isNew = true;
                });
            }
        }
        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Delete();
                    (MainWindow.Main.Books.DataContext as ViewModell.VMBooks).Books.Remove(this);
                });
            }
        }
    }
}
