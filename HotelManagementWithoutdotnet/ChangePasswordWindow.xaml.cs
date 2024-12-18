using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HotelManagementWithoutdotnet.Models;


namespace HotelManagementWithoutdotnet
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private readonly int _userId;
        public ChangePasswordWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            string currentPassword = txtCurrentPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmNewPassword = txtConfirmPassword.Password;

            if (string.IsNullOrEmpty(currentPassword) ||
                string.IsNullOrEmpty(newPassword) ||
                string.IsNullOrEmpty(confirmNewPassword))
            {
                MessageBox.Show("Все поля обязательны для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Новый пароль и введеный в подтверждении не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using(var context = new HotelManagementContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Id == _userId);

                    if(user == null)
                    {
                        MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (user.Password != currentPassword)
                    {
                        MessageBox.Show("Текущий пароль неверен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    user.Password = newPassword;
                    user.IsFirstLogin = false;
                    context.SaveChanges();

                    MessageBox.Show("Пароль успешно изменен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении пароля: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
