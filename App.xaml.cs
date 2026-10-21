using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Autoservice3
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                var loginWindow = new LoginWindow();

                // ShowDialog() блокирует запуск дальше, пока окно не закроется
                if (loginWindow.ShowDialog() == true)
                {
                    // Успешный вход: запускаем основное окно
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                }
                else
                {
                    // Вход не удался: можно показать сообщение или просто закрыть приложение
                    MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    Shutdown(); // Завершаем приложение
                }
            }
            catch (Exception ex) {
                var msg = $"Ошибка инициализации приложения:\n{ex.Message}\n\nВнутреннее исключение:\n{ex.InnerException?.Message}";
                System.Windows.MessageBox.Show(msg, "Критическая ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                throw; // чтобы приложение всё равно упало и ты видел стек в отладчике
            }
        }
    }
}
