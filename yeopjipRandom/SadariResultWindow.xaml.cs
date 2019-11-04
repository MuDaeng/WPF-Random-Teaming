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
    /// SadariResultWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SadariResultWindow : Window
    {
        private Sadari sadari;
        public SadariResultWindow(List<string> sadariList, int memberNum)
        {
            InitializeComponent();
            this.sadari = new Sadari(sadariList, memberNum);
            ShowTeams();
        }
        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            ShowTeams();
        }
        private void GotoTeamRandomSettingWindow_Click(object sender, RoutedEventArgs e)
        {
            new TeamRandomSettingWindow(sadari.sadariList).Show();
            this.Close();
        }
        private void GotoMain_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void ShowTeams()
        {
            List<Member> teams;
            teams = sadari.getTeams();
            panel.Children.Clear();
            foreach (Member item in teams)
            {
                Label tmp = new Label();
                tmp.Content = item.name + "\t" + item.team;
                tmp.FontSize = 18;
                tmp.Height = 80;
                panel.Children.Add(tmp);
            }
        }
    }
}
