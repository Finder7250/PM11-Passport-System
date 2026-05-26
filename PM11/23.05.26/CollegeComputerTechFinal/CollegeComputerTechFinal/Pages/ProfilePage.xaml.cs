using System.Data.SqlClient;
using System.Windows.Controls;
using CollegeComputerTechFinal.DAL;

namespace CollegeComputerTechFinal.Pages
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadProfile();
        }

        private void LoadProfile()
        {
            txtFullName.Text = MainWindow.LoggedUserFullName;

            string roleName = MainWindow.LoggedUserRole == "admin" ? "Администратор" :
                              MainWindow.LoggedUserRole == "technic" ? "Техник" : "Преподаватель";
            txtRole.Text = roleName;

            if (MainWindow.LoggedUserCabinetId.HasValue)
            {
                txtCabinet.Text = $"Кабинет: привязан (код {MainWindow.LoggedUserCabinetId.Value})";

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM АРМ WHERE код_кабинета = @cabId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cabId", MainWindow.LoggedUserCabinetId.Value);
                        int count = (int)cmd.ExecuteScalar();
                        txtStats.Text = $"Компьютеров в кабинете: {count}";
                    }
                }
            }
            else
            {
                txtCabinet.Text = "Кабинет: не привязан";

                if (MainWindow.LoggedUserRole == "admin")
                    txtStats.Text = "Полный доступ к системе";
                else if (MainWindow.LoggedUserRole == "technic")
                    txtStats.Text = "Доступ к заявкам и оборудованию";
            }
        }
    }
}