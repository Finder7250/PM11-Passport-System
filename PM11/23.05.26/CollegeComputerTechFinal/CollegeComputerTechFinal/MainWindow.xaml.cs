using System.Windows;
using CollegeComputerTechFinal.Pages;

namespace CollegeComputerTechFinal
{
    public partial class MainWindow : Window
    {
        // Статические поля для хранения данных текущего пользователя
        public static int LoggedUserId { get; set; }
        public static string LoggedUserFullName { get; set; }
        public static string LoggedUserRole { get; set; }
        public static int? LoggedUserCabinetId { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            txtStatus.Text = $"Добро пожаловать, {LoggedUserFullName}!";
            MainFrame.Navigate(new NewsPage());
        }

        private void News_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new NewsPage());
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage());
        }

        private void Cabinets_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CabinetsPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}