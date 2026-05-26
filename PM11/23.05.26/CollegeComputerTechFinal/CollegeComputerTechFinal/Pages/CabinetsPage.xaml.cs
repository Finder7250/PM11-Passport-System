using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Controls;
using CollegeComputerTechFinal.DAL;

namespace CollegeComputerTechFinal.Pages
{
    public partial class CabinetsPage : Page
    {
        public CabinetsPage()
        {
            InitializeComponent();
            LoadCabinets();
        }

        private void LoadCabinets()
        {
            List<CabinetItem> cabinets = new List<CabinetItem>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT к.номер_кабинета, к.местонахождение, к.ответственный,
                                        COUNT(а.код_арм) as КоличествоАРМ
                                 FROM Кабинет к
                                 LEFT JOIN АРМ а ON к.код_кабинета = а.код_кабинета
                                 GROUP BY к.номер_кабинета, к.местонахождение, к.ответственный
                                 ORDER BY к.номер_кабинета";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cabinets.Add(new CabinetItem
                        {
                            Number = reader.GetString(0),
                            Location = reader.GetString(1),
                            Responsible = reader.GetString(2),
                            ArmsCount = reader.GetInt32(3)
                        });
                    }
                }
            }

            CabinetsList.ItemsSource = cabinets;
        }
        private void CabinetsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetsList.SelectedItem is CabinetItem selected)
            {
                // Нужно передать ID кабинета и номер
                // Пока временно: ищем ID по номеру кабинета
                int cabinetId = GetCabinetIdByNumber(selected.Number);

                CabinetDetailWindow detailWindow = new CabinetDetailWindow(cabinetId, selected.Number);
                detailWindow.ShowDialog();
            }
        }

        private int GetCabinetIdByNumber(string cabinetNumber)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT код_кабинета FROM Кабинет WHERE номер_кабинета = @number";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@number", cabinetNumber);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }

    public class CabinetItem
    {
        public string Number { get; set; }
        public string Location { get; set; }
        public string Responsible { get; set; }
        public int ArmsCount { get; set; }
    }
}