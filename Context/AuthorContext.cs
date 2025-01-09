using WpfApp1.Classes;
using WpfApp1.Classes.DataBase;
using WpfApp1.Modell;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace WpfApp1.Context
{
    public class AuthorsContext : Authors
    {
        private bool _isNew = true;
        public AuthorsContext(bool save = false)
        {
            if (save) save = true;
        }
        public static ObservableCollection<AuthorsContext> AllAuthors()
        {
            ObservableCollection<AuthorsContext> allAuthors = new ObservableCollection<AuthorsContext>();
            SqlConnection connection;
            SqlDataReader dataItems = Connection.Query("Select * from [dbo].[Authors]", out connection);
            while (dataItems.Read())
            {
                allAuthors.Add(new AuthorsContext()
                {
                    Id = dataItems.GetInt32(0),
                    Surname = dataItems.GetString(1),
                    Name = dataItems.GetString(2),
                    Lastname = dataItems.GetString(3),
                });
            }
            Connection.CloseConnection(connection);
            return allAuthors;
        }
        public void Save()
        {
            SqlConnection connection;
            if (_isNew)
            {
                try
                {
                    SqlDataReader dataItems = Connection.Query("Insert into " +
                        "[dbo].[Authors](" +
                        "Surname, " +
                        "Name, " +
                        "Lastname) " +
                        "OUTPUT Inserted.Id " +
                        "Values (" +
                        $"N'{Surname}', " +
                        $"N'{Name}', " +
                        $"N'{Lastname}')", out connection);
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
                    Connection.Query("Update [dbo].[Authors] " +
                        "Set " +
                        $"Surname = N'{Surname}', " +
                        $"Name = N'{Name}', " +
                        $"Lastname = N'{Lastname}' " +
                        "Where " +
                        $"Id = {Id}", out connection);
                    Connection.CloseConnection(connection);
                }
                catch
                {
                    MessageBox.Show("Не удалось выполнить действие!", "Уведмление");
                }
            }
            _isNew = true;
            MainWindow.MW.frame.Navigate(MainWindow.Main);
        }
        private void Delete(bool withBooks)
        {
            SqlConnection connection;
            if (withBooks)
            {
                Connection.Query("Delete from [dbo].[Books] " +
                            "Where " +
                            $"Author = {Id}", out connection);
            }
            Connection.Query("Delete from [dbo].[Authors] " +
                "Where " +
                $"Id = {Id}", out connection);
            Connection.CloseConnection(connection);
            (MainWindow.Main.Authors.DataContext as ViewModell.VMAuthors).Authors.Remove(this);
            MessageBox.Show("Действие выполнено!", "Уведмление");
        }
        public RelayCommand OnEdit
        {
            get
            {
                _isNew = false;
                return new RelayCommand(obj => MainWindow.MW.frame.Navigate(new View.AddAuthors(this)));
            }
        }
        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Save();
                });
            }
        }
        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить этого автора?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (BooksContext.AllBooks().Where(x => x.Author.Id == this.Id).ToList().Count > 0)
                        {
                            if (MessageBox.Show("У этого автора есть книги, вы действительно хотите удалить его?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                Delete(true);
                            }
                        }
                        else Delete(false);
                    }
                });
            }
        }
    }
}
