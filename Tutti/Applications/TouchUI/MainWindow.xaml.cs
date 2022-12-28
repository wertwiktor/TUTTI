using Services.DataServiceSql;
using System.Windows;

namespace TouchUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var dbService = new DataServiceSql();
            var allUsers = dbService.GetAllUsers();
        }
    }
}
