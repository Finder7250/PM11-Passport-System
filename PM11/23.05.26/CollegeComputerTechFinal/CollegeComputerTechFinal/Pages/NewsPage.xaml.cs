using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Controls;
using CollegeComputerTechFinal.DAL;

namespace CollegeComputerTechFinal.Pages
{
    public partial class NewsPage : Page
    {
        public NewsPage()
        {
            InitializeComponent();
            LoadNews();
        }

        private void LoadNews()
        {
            List<NewsItem> newsList = new List<NewsItem>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT заголовок, текст, дата_публикации 
                                 FROM Новость 
                                 WHERE актуальна = 1 
                                 ORDER BY дата_публикации DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        newsList.Add(new NewsItem
                        {
                            Title = reader.GetString(0),
                            Text = reader.GetString(1),
                            Date = reader.GetDateTime(2).ToString("dd.MM.yyyy")
                        });
                    }
                }
            }

            NewsList.ItemsSource = newsList;
        }
    }

    public class NewsItem
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
    }
}