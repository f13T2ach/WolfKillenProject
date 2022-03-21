//WolfKillen狼人杀
//CSharp Application
//Made by F13T2ach in China.
//图标网站：https://www.iconfont.cn/
//引入图标教程：https://blog.csdn.net/YouyoMei/article/details/86703021
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

namespace WolfKillen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int playersMode;
        private string menuMode = "选择玩家人数";
        private bool isOnMenuPage = true;
        public int jobId = 1;
        public string[] JobImgPaths = new string[7] { "resc/theWolf.png", "resc/villager.png", "resc/witch.png", "resc/hunter.png", "resc/prophet.png", "resc/stupid.png", "resc/guard.png" };
        public string[] JobNames = new string[7] { "狼人", "平民", "女巫", "猎人", "预言家", "白痴", "守卫" };
        public bool onlinePlay = false;
        //是否打开了对决窗口
        public bool windowRelax = false;
        public MainWindow()
        {
            InitializeComponent();
            ThisTitle.Text = "狼人杀           " + menuMode;
            //为了方便 这里不用Frame封装一个小Page
            _10plbtn.Visibility = Visibility.Visible;
            _12plbtn.Visibility = Visibility.Visible;

            setJob.Visibility = Visibility.Hidden;

        }

        private void SixPlayers(object sender, MouseButtonEventArgs e)
        {
            FightPanel fp = new FightPanel();
            if (isOnMenuPage)
            {
                playersMode = 6;
                ShowJobSelectPanel();
            }
            else
            {
                //显示战斗面板
                fp.Show();
                fp.GetPlayParameter(playersMode, jobId, JobImgPaths, JobNames, onlinePlay);
                fp.LocalPlay();//这是蜜汁bug 如果不重新执行，就会发现上一行传入的值不存在了
                windowRelax = true;
                this.Close();
                
            }
        }

        private void TenPlayers(object sender, MouseButtonEventArgs e)
        {
            playersMode = 10;
            ShowJobSelectPanel();
        }

        private void TwelvePlayers(object sender, MouseButtonEventArgs e)
        {
            playersMode = 12;
            ShowJobSelectPanel();
        }

        private void ShowJobSelectPanel()
        {
            _10plbtn.Visibility = Visibility.Hidden;
            _12plbtn.Visibility = Visibility.Hidden;
            _6plbtnTx.Text = "以 狼人 开始";
            menuMode = "选择你的角色";
            ThisTitle.Text = "狼人杀           " + menuMode;
            creBtn.Visibility = Visibility.Hidden;
            conBtnTx.Text = "退回上一步";
            isOnMenuPage = false;
            setJob.Visibility = Visibility.Visible;
            //设置一下jobId 防止重新调节的时候出故障
            jobId = 1;
            _6plbtnTx.Text = "以 " + JobNames[jobId - 1] + " 开始";
            //设置资源的路径
            jobImage.Source = new BitmapImage(new Uri(JobImgPaths[jobId - 1], UriKind.Relative));
            //六人情况下的提示
            if(playersMode == 6 && onlinePlay == true)
            {
                tips.Content = "请注意，该模式下你只能由管理员为你选择角色。目标主机的玩家数量是 " + playersMode + "。\n因此，警长、预言家和守卫 已依照规则除去。";
                csTx.Text = "进入服务器";
            }
            else if(onlinePlay)
            {
                tips.Content = "请注意，该模式下你只能由管理员为你选择角色。\n目标主机的玩家数量是 "+playersMode;
                csTx.Text = "进入服务器";
            }
            else if(playersMode == 6)
            {
                tips.Content = "请注意，该模式下的 警长、预言家和守卫 已依照规则除去。";
            }
            else
            {
                tips.Content = "";
                csTx.Text = "让电脑为我挑选角色";
            }
            
        }

        private void ShowMenuPage()
        {
            isOnMenuPage = true;
            _10plbtn.Visibility = Visibility.Visible;
            _12plbtn.Visibility = Visibility.Visible;
            _6plbtnTx.Text = "6人游戏";
            menuMode = "选择玩家人数";
            ThisTitle.Text = "狼人杀           " + menuMode;
            creBtn.Visibility = Visibility.Visible;
            conBtnTx.Text = "连接到服务器";
            setJob.Visibility = Visibility.Hidden;
            //设置一下jobId 防止重新调节的时候出故障
            jobId = 1;
            //设置资源的路径
            jobImage.Source = new BitmapImage(new Uri(JobImgPaths[jobId - 1], UriKind.Relative));
        }

        private void conBtnTxClicked(object sender, MouseButtonEventArgs e)
        {
            if(!isOnMenuPage)
            {
                ShowMenuPage();
            }
            else 
            {
                MessageBox.Show("现在不支持服务器相关的内容\n于是这里被用来显示调试信息。\n玩家人数(最后一次点击):"+playersMode+"\n是否在选玩家页面:"+isOnMenuPage);
            }
        }

        private void rightChClicked(object sender, MouseButtonEventArgs e)
        {
            //六人游戏防止选到规则不允许的角色
            if (playersMode == 6)
            {
                if (jobId + 1 == 5)
                {
                    //这里是选择到预言家的情况
                    jobId = 6;
                }
                else if(jobId + 1 == 7)
                {
                    //这里是选择到守卫的情况
                    jobId = 6;
                }
                else
                {
                    jobId += 1;
                }
            }
            else if(playersMode == 10)
            {
                if(jobId + 1 == 6)
                {
                    jobId = 7;
                }
                else
                {
                    jobId++;
                }
            }
            else
            {
                jobId += 1;
            }
            //当选项超出界限
            if (jobId > JobNames.Length)
            {
                jobId = JobNames.Length;
            }
            _6plbtnTx.Text = "以 "+JobNames[jobId-1]+" 开始";
            //设置资源的路径
            jobImage.Source = new BitmapImage(new Uri(JobImgPaths[jobId-1], UriKind.Relative));
        }

        private void leftChClicked(object sender, MouseButtonEventArgs e)
        {
            //六人游戏防止选到规则不允许的角色
            if (playersMode == 6)
            {
                if (jobId - 1 == 5)
                {
                    //这里是选择到预言家的情况
                    jobId = 4;
                }
                else if (jobId - 1 == 7)
                {
                    //这里是选择到守卫的情况
                    jobId = 1;
                }
                else
                {
                    jobId -= 1;
                }

            }
            else if (playersMode == 10)
            {
                if (jobId - 1 == 6)
                {
                    jobId = 5;
                }
                else
                {
                    jobId--;
                }
            }
            else
            {
                jobId -= 1;
            }
            //当选项负超界限
            if (jobId <= 0 )
            {
                jobId = 1;
            }
            _6plbtnTx.Text = "以 "+JobNames[jobId-1]+" 开始";
            //设置资源的路径
            jobImage.Source = new BitmapImage(new Uri(JobImgPaths[jobId-1], UriKind.Relative));
        }

        private void computerSetClicked(object sender, MouseButtonEventArgs e)
        {
            Random rd = new Random();
            jobId = rd.Next(1, 9);
            //处理个例
            if(jobId == 6)
            {
                jobId = 4;
            }
            else if(jobId == 5 || jobId == 8)
            {
                jobId--;
            }
            //显示战斗面板
            FightPanel fp = new FightPanel();
            fp.Show();
            windowRelax = true;
            this.Close();
            
        }

        //如果没有关闭事件，那么关闭对决窗口后再退出就会出现无法完全结束进程的蜜汁bug
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //放这个是因为 弹出对决窗口也需要关闭主窗口，如果不加就会导致弹出对决窗口后进程直接自杀
            if (!windowRelax)
            {
                Environment.Exit(0);
            }
        }
    }
}
