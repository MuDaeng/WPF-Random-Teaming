using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace yeopjipRandom
{
    /// <summary>
    /// MembersSettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MembersSettingWindow : Window
    {
        private List<string> memberList;
        private List<string> favoriteList;
        private MemberService memberService;
        public MembersSettingWindow()
        {
            InitializeComponent();
            memberService = MemberService.GetInstance();
            memberList = memberService.LoadAll();
            favoriteList = memberService.LoadFavorite();
            memberListFromResource.ItemsSource = memberList;
            favoriteListFromResource.ItemsSource = favoriteList;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GotoBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void AddMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            string member = addMemberText.Text;
            if(member.Length > 0)
            {
                memberList.Add(member);
                memberService.SaveAll(memberList);
                memberListFromResource.Items.Refresh();
                addMemberText.Text = "";
            }
            else
            {
                MessageBox.Show("이름 적고 버튼 누르쇼");
            }
        }

        private void DeleteMemberText_Click(object sender, RoutedEventArgs e)
        {
            if(memberListFromResource.SelectedItem != null)
            {
                if (favoriteList.Contains(memberListFromResource.SelectedItem.ToString()))
                {
                    favoriteList.Remove(memberListFromResource.SelectedItem.ToString());
                    memberService.SaveFavorite(favoriteList);
                    favoriteListFromResource.Items.Refresh();
                }
                memberList.RemoveAt(memberListFromResource.SelectedIndex);
                memberService.SaveAll(memberList);
                memberListFromResource.Items.Refresh();
            }
        }

        private void AddFavoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(memberListFromResource.SelectedItem != null)
            {
                if (!favoriteList.Contains(memberListFromResource.SelectedItem.ToString()))
                {
                    favoriteList.Add(memberListFromResource.SelectedItem.ToString());
                    memberService.SaveFavorite(favoriteList);
                    favoriteListFromResource.Items.Refresh();
                }
            }
        }

        private void DeleteFavoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(favoriteListFromResource.SelectedItem != null)
            {
                favoriteList.RemoveAt(favoriteListFromResource.SelectedIndex);
                memberService.SaveFavorite(favoriteList);
                favoriteListFromResource.Items.Refresh();
            }
        }
    }
}
