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
using System.Windows.Shapes;

namespace WolfKillen
{
    /// <summary>
    /// FightPanel.xaml 的交互逻辑
    /// </summary>
    public partial class FightPanel : Window
    {

        public int playersMode = 0;
        public int jobId = 1;
        public string[] JobImgPaths = new string[8] { "resc/theWolf.png", "resc/villager.png", "resc/witch.png", "resc/hunter.png", "resc/police.png", "resc/prophet.png", "resc/stupid.png", "resc/guard.png" };
        public string[] JobNames = new string[8] { "狼人", "平民", "女巫", "猎人", "警长", "预言家", "白痴", "守卫" };
        public string[] PlayerNames = new string[12] { "我", "Bot1", "Bot2", "Bot3", "Bot4", "Bot5", "Bot6", "Bot7", "Bot8", "Bot9", "Bot10", "Bot11" };
        public bool[] PlayerLives = new bool[12] { true, true, true, true, true, true, true, true, true, true, true, true };
        public int[] PlayerJobs = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public bool onlinePlay = false;
        public int days = 1;
        public bool isNight = true;
        public int selectedPlId = 1;
        public int btn1Mode = 0;
        public FightPanel()
        {
            InitializeComponent();
            if(onlinePlay)
            { 
                //这里是在线游玩的
            }
            else
            {
                LocalPlay();
            }
        }

        public void GetPlayParameter(int playersMode_,int jobId_,string[] jobImgPaths_,string[] jobNames_,bool onlinePlay_)
        {
            playersMode = playersMode_;
            jobId = jobId_;
            JobImgPaths = jobImgPaths_;
            JobNames = jobNames_;
            onlinePlay = onlinePlay_;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mw = new MainWindow();
            if (!onlinePlay)
            {
                MessageBoxResult result = MessageBox.Show("当前模式下，退出表示结束对局。你确定吗？", "请注意", MessageBoxButton.YesNo, MessageBoxImage.Question);

                //关闭窗口
                if (result == MessageBoxResult.Yes)
                //退出之前先唤起主窗口
                mw.Show();
                e.Cancel = false;

                //不关闭窗口
                if (result == MessageBoxResult.No)
                e.Cancel = true;
                
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("当前模式下，离开服务器将在管理员开启下一对局后才能加入\n你将被替换为人机，仍要这样吗？", "请注意", MessageBoxButton.YesNo, MessageBoxImage.Question);

                //关闭窗口
                if (result == MessageBoxResult.Yes)
                    //退出之前先唤起主窗口
                    mw.Show();
                mw.windowRelax = false;
                e.Cancel = false;

                //不关闭窗口
                if (result == MessageBoxResult.No)
                    e.Cancel = true;
            }
        }

        public void LocalPlay()
        {
            time.Content = "第 1 天夜晚";
            BotsActive ba = new BotsActive();
            if(playersMode==6)
            {
                //六人模式下的分组
                //rule 女巫 猎人 白痴 一民 二狼
                PlayerJobs[0] = jobId;
                PlayerJobs = ba.AssignPlayerRole(playersMode, PlayerJobs);
            }
            DayToNight();

        }
        private void rightChClicked(object sender, MouseButtonEventArgs e)
        {
            selectedPlId++;
            if (selectedPlId > playersMode)
            {
                selectedPlId = playersMode;
            }
            playerName.Text = PlayerNames[selectedPlId - 1];
        }

        private void leftChClicked(object sender, MouseButtonEventArgs e)
        {
            selectedPlId--;
            if (selectedPlId <= 0)
            {
                selectedPlId = 1;
            }
            playerName.Text = PlayerNames[selectedPlId - 1];
        }

        //白天转黑夜
        private void DayToNight()
        {
            jobImage.Visibility = Visibility.Hidden;
            leftCh.Visibility = Visibility.Hidden;
            playerName.Visibility = Visibility.Hidden;
            rightCh.Visibility = Visibility.Hidden;
            ObjectHint.Text = "天黑请闭眼";
            choosetitle.Text = "你的选择是";
            userbtn2.Visibility = Visibility.Hidden;
            userbtn1Tx.Text = "闭眼";
            btn1Mode = 1;
        }

        //狼人睁眼
        private void WolfOpenEye()
        {
            ObjectHint.Text = "狼人请睁眼";
            System.Threading.Thread.Sleep(1000);
            ObjectHint.Text = "狼人，请选择你要带走的玩家";
            
        }
        private void userbtn1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(btn1Mode == 1)
            {

            }
        }

        private void userbtn2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
