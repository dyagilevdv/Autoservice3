using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Autoservice3
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            var login = txtLogin.Text;
            var password = pwdPassword.Password;

            // 1. Проверка, что поля не пустые
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Используем контекст только для проверки — без любой инициализации
                using (var db = new Autoservice3.Models.AppDbContext())
                {
                    // Запрос к БД: ищем пользователя по логину
                    var user = db.Users.FirstOrDefault(u => u.Login == login);

                    if (user == null)
                    {
                        ShowError("Неверный логин или пароль");
                        ClearFields();
                        return;
                    }

                    // Сравниваем пароль из БД с введённым
                    if (user.Password != password)
                    {
                        ShowError("Неверный логин или пароль");
                        ClearFields();
                        return;
                    }

                    // Если всё совпало — пускаем дальше
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                // Ошибка подключения к БД (нет сервера, неверная строка подключения и т.п.)
                MessageBox.Show(
                    "Не удалось подключиться к базе данных. Проверьте настройки.",
                    "Критическая ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Любая другая ошибка
                MessageBox.Show(
                    "Произошла непредвиденная ошибка.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Button_Esc(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ClearFields()
        {
            txtLogin.Clear();
            pwdPassword.Clear();
            txtLogin.Focus();
        }
    }
}
