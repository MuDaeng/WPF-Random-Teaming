using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class TeamRandomSettingWindow : Window
    {
        private List<string> memberList;
        private List<string> sadariList;
        private int teamNum;
        public TeamRandomSettingWindow()
        {
            InitializeComponent();
            teamNum = 0;
            memberList = MemberService.GetInstance().LoadAll();
            sadariList = MemberService.GetInstance().LoadFavorite();
            foreach(string item in sadariList)
            {
                memberList.Remove(item);
            }
            memberListFromResource.ItemsSource = memberList;
            memberListFromResource.SelectionMode = SelectionMode.Single;
            memberListFromResource.Items.Refresh();
            memberListForSadari.ItemsSource = sadariList;
        }
        public TeamRandomSettingWindow(List<string> sadariList)
        {
            InitializeComponent();
            teamNum = 0;
            memberList = MemberService.GetInstance().LoadAll();
            this.sadariList = sadariList;
            foreach (string item in sadariList)
            {
                memberList.Remove(item);
            }
            memberListFromResource.ItemsSource = memberList;
            memberListFromResource.SelectionMode = SelectionMode.Single;
            memberListFromResource.Items.Refresh();
            memberListForSadari.ItemsSource = this.sadariList;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(memberListFromResource.SelectedItem != null)
            {
                sadariList.Add(memberListFromResource.SelectedItem.ToString());
                memberList.RemoveAt(memberListFromResource.SelectedIndex);
                memberListForSadari.Items.Refresh();
            }
            memberListFromResource.Items.Refresh();
            GetTeamNumAndMemberNum();
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(memberListForSadari.SelectedItem != null)
            {
                memberList.Add(memberListForSadari.SelectedItem.ToString());
                sadariList.RemoveAt(memberListForSadari.SelectedIndex);
                memberListFromResource.Items.Refresh();
            }
            memberListForSadari.Items.Refresh();
            GetTeamNumAndMemberNum();
        }

        private void TmpMemberAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(tmpMemberTxt.Text.Length > 0)
            {
                sadariList.Add(tmpMemberTxt.Text);
                memberListForSadari.Items.Refresh();
                tmpMemberTxt.Text = "";
            }
            else
            {
                MessageBox.Show("이름 적고 버튼 누르쇼");
            }
            GetTeamNumAndMemberNum();
        }
        private void GetTeamNumAndMemberNum()
        {
            memberNumLabel.Content = sadariList.Count;
            if (eachTeamMemberNumLabel.Text.Length == 0)
            {
                return;
            }
            teamNum = sadariList.Count / Int32.Parse(eachTeamMemberNumLabel.Text);
            if (sadariList.Count % Int32.Parse(eachTeamMemberNumLabel.Text) != 0)
            {
                teamNum += 1;
            }            if(eachTeamMemberNumLabel.Text.Length == 0)
            {
                return;
            }
            teamNumLabel.Content = teamNum.ToString();
        }
        private void MemberNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(eachTeamMemberNumLabel.Text.Length > 2)
            {
                eachTeamMemberNumLabel.Text = eachTeamMemberNumLabel.Text.Remove(0, eachTeamMemberNumLabel.Text.Length - 1);
            }
            try
            {
                GetTeamNumAndMemberNum();
            }catch(FormatException fe) {
                eachTeamMemberNumLabel.Text = "";
            }
        }
        private bool IsNumeric(string source)
        {
            Regex regex = new Regex("[^1-9.-]+");
            return !regex.IsMatch(source);
        }

        private void MemberNum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void GotoResultWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            int memberNum = 1;
            try
            {
                if (Int32.Parse(eachTeamMemberNumLabel.Text) > 0)
                {
                    memberNum = Int32.Parse(eachTeamMemberNumLabel.Text);
                    new SadariResultWindow(sadariList, memberNum).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("팀인원 잘못 적음");
                }
            }catch(FormatException fe)
            {
                MessageBox.Show("팀인원은 몇명?");
            }
        }

        private void GotoBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = false;
            //Do whatever you want here..
 //           mainWindow.Close();
        }
    }
}
