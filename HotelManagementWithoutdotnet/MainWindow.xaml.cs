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
using HotelManagementWithoutdotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementWithoutdotnet
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) )
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль");
                return;
            }

            using(var context = new HotelManagementContext())
            {
                var user = await context.Users
                    .Where(u => u.Username == username && u.Password == password)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    MessageBox.Show("Вы успешно авторизовались");

                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.Show();

                    if (adminPanel.IsActive == true)
                    {
                        MainWindow mainWindow = new MainWindow();
                        this.Close();
                    }
                    
                } else
                {
                    MessageBox.Show("Вы ввели неверный логи или пароль. Пожалуйста проверьте еще раз введенные данные.");
                }
            }
        }
    }
}