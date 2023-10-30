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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VK_Library;

namespace VKViews_Library
{
    public partial class UserInfoView : UserControl
    {


        public VKFullUser FullUserInfo
        {
            get { return (VKFullUser)GetValue(FullUserInfoProperty); }
            set { SetValue(FullUserInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FullUserInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FullUserInfoProperty =
            DependencyProperty.Register("FullUserInfo", typeof(VKFullUser), typeof(UserInfoView));



        public VKUserInfo Member
        {
            get { return (VKUserInfo)GetValue(MemberProperty); }
            set 
            { 
                SetValue(MemberProperty, value); 
                FetchFullUserInfo();
            }
        }

        public static readonly DependencyProperty MemberProperty = DependencyProperty.Register(
                "Member",
                typeof(VKUserInfo),
                typeof(UserInfoView),
                new PropertyMetadata(OnMemberChanged)
            );


        private static void OnMemberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var userInfo = sender as UserInfoView;
            userInfo.FetchFullUserInfo();
        }

        async public void FetchFullUserInfo()
        {
            var result = await Utility.FetchFullUserInfo(Member.id.ToString());
            this.FullUserInfo = result.response.response[0];
        }

        public UserInfoView()
        {
            InitializeComponent();
        }
    }
}
