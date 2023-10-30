using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VK_Library;
using VKViews_Library;

namespace VK_Bot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ResponseContent
        {
            get { return (string)GetValue(ResponseContentProperty); }
            set { SetValue(ResponseContentProperty, value); }
        }

        public static readonly DependencyProperty ResponseContentProperty =
            DependencyProperty.Register("ResponseContent", typeof(string), typeof(MainWindow));


        public ObservableCollection<VKUserInfo> Users { get; set; }
            = new ObservableCollection<VKUserInfo>();


        public MainWindow()
        {
            InitializeComponent();
        }

        async private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            ResponseContent = "Downloading...";
            var result = await Utility.FetchMembersInfo(txtGroupId.Text);
            ResponseContent = Utility.PrettyJson(result.rawContent);

            Users.Clear();
            if (result.response.response != null )
            {
                foreach (var user in result.response.response.items)
                {
                    Users.Add(user);
                }
            }
        }
    }
}
