using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        LogConsole console = new LogConsole();
        public int playersMode = 0;
        public int jobId = 1;
        public string[] JobImgPaths = new string[7] { "resc/theWolf.png", "resc/villager.png", "resc/witch.png", "resc/hunter.png", "resc/prophet.png", "resc/stupid.png", "resc/guard.png" };
        public string[] JobNames = new string[7] { "狼人", "平民", "女巫", "猎人", "预言家", "白痴", "守卫" };
        public string[] PlayerNames = new string[12] { "我", "Bot1", "Bot2", "Bot3", "Bot4", "Bot5", "Bot6", "Bot7", "Bot8", "Bot9", "Bot10", "Bot11" };
        public bool[] PlayerLives = new bool[12] { true, true, true, true, true, true, true, true, true, true, true, true };
        public int[] PlayerJobs = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//职业是-1 表示死亡
        public string[] PlayerTags = new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" };
        public bool onlinePlay = false;
        public int days = 1;
        public bool isNight = true;
        public int selectedPlId = 1;//选择的玩家
        public int btn1Mode = 0;//按钮响应模式 1表示狼人操作 2表示投票操作 3表示女巫操作 4表示猎人操作 5表示投票操作 6表示白痴的亮出身份免死操作 7表示守护操作
        public int selcMode = 0;//选择模式 0表示名称选择 1表示图片选择
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

        /// <summary>
        /// 传入游戏模式
        /// </summary>
        /// <param name="playersMode_">游戏人数</param>
        /// <param name="jobId_">玩家的job</param>
        /// <param name="jobImgPaths_">这些job的图标位置</param>
        /// <param name="jobNames_">这些job的名称</param>
        /// <param name="onlinePlay_">是否是在线游玩（已弃用）</param>
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
            Assign assi = new Assign();
            if(playersMode==6)
            {
                //六人模式下的分组
                //rule 女巫 猎人 白痴 一民 二狼
                PlayerJobs[0] = jobId;
                PlayerJobs = assi.AssignPlayerRole(playersMode, PlayerJobs);
            }
            else if(playersMode == 10)
            {
                PlayerJobs[0] = jobId;
                PlayerJobs = assi.AssignPlayerRole(playersMode, PlayerJobs);
                //3狼,预言家,女巫,猎人，3民，守卫 第二天白日掉落一个警察头衔
            }
            else if(playersMode == 12)
            {
                PlayerJobs[0] = jobId;
                PlayerJobs = assi.AssignPlayerRole(playersMode, PlayerJobs);
                //https://zhuanlan.zhihu.com/p/63750422 4狼4民 预言家、女巫、猎人、白痴
            }
            PlayerStat();
            
            
            DayToNight();

        }

        public void PlayerStat()
        {
            playerNames.Content = "我\nbot1\nbot2\nbot3\nbot4\nbot5\nbot6\nbot7\nbot8\nbot9\nbot10\nbot11";
            if(PlayerJobs[0]==1)
            {

            }
        }

        //调节按钮
        #region
        private void rightChClicked(object sender, MouseButtonEventArgs e)
        {
            selectedPlId++;
            if (selectedPlId > playersMode)
            {
                selectedPlId = playersMode;
            }

            if (selcMode == 0)
            {
                playerName.Text = PlayerNames[selectedPlId - 1];
            }
            else
            {
                jobImage.Source = new BitmapImage(new Uri(JobImgPaths[selectedPlId - 1]));//图片的切换
            }
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
        #endregion

        //白天转黑夜
        private void DayToNight()
        {
            isNight = true;
            if (PlayerJobs[0] != 0) { MainPlayerRole.Text = JobNames[PlayerJobs[0] - 1]; };//因为1是JobNames下标0的内容，所以往前推移。
            jobImage.Visibility = Visibility.Hidden;
            leftCh.Visibility = Visibility.Hidden;
            playerName.Visibility = Visibility.Hidden;
            rightCh.Visibility = Visibility.Hidden;
            choosetitle.Text = "你没得选";
            userbtn2.Visibility = Visibility.Hidden;
            userbtn1.Visibility = Visibility.Hidden;
            Night();
            //复位btnmode
            btn1Mode = 1;
        }

        //晚上
        private void Night()
        {
            //狼人睁眼
            ThreadStart threadStart = new ThreadStart(WolfOpenEye);//通过ThreadStart委托告诉子线程执行什么方法　　
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        //白天
        private void Day()
        {
            days += 1;
            isNight = false;
        }

        //黑夜转白天
        private void NightToDay()
        {
            jobImage.Visibility = Visibility.Hidden;
            leftCh.Visibility = Visibility.Hidden;
            playerName.Visibility = Visibility.Hidden;
            rightCh.Visibility = Visibility.Hidden;
            ObjectHint.Text = "昨晚是个[0]夜，[1]被刀了";
            choosetitle.Text = "你没得选";
            userbtn2.Visibility = Visibility.Hidden;
            Day();
            btn1Mode = 1;
        }
        //狼人睁眼 用线程调用
        private void WolfOpenEye()
        {
            this.Dispatcher.Invoke(new Action(() => { ObjectHint.Text = "天黑请闭眼"; }));
            Thread.Sleep(1000);
            if (PlayerJobs[0] != 0)
            {
                this.Dispatcher.Invoke(new Action(() => { ObjectHint.Text = "狼人请睁眼"; }));
                Thread.Sleep(1000);
            }
            this.Dispatcher.Invoke(new Action(() => { ObjectHint.Text = "狼人，请选择你要带走的玩家"; }));
            if (PlayerJobs[0] == 1)//如果玩家是狼人。
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    SelectPanelCtr(1);
                }));
                this.Dispatcher.Invoke(new Action(() => { choosetitle.Text = "你要刀人还是跳过"; }));
                this.Dispatcher.Invoke(new Action(() => { userbtn1Tx.Text = "刀我选的人"; }));
                this.Dispatcher.Invoke(new Action(() => { userbtn2Tx.Text = "跳过"; }));
            }
        }



        /// <summary>
        /// 选择面板的控制
        /// </summary>
        /// <param name="type">0表示全消失 1表示全显示除jobimage 2表示全显示除playerName</param>
        private void SelectPanelCtr(int type)
        {
            if (type == 1)
            {
                leftCh.Visibility = Visibility.Visible;
                playerName.Visibility = Visibility.Visible;
                rightCh.Visibility = Visibility.Visible;
                userbtn2.Visibility = Visibility.Visible;
                userbtn1.Visibility = Visibility.Visible;
            }
            else if(type == 2)
            {
                leftCh.Visibility = Visibility.Visible;
                jobImage.Visibility = Visibility.Visible;
                rightCh.Visibility = Visibility.Visible;
                userbtn2.Visibility = Visibility.Visible;
                userbtn1.Visibility = Visibility.Visible;
            }
            else if(type == 0)
            {
                leftCh.Visibility = Visibility.Hidden;
                jobImage.Visibility = Visibility.Hidden;
                playerName.Visibility = Visibility.Hidden;
                rightCh.Visibility = Visibility.Hidden;
                userbtn2.Visibility = Visibility.Hidden;
                userbtn1.Visibility = Visibility.Hidden;
            }

        }
        private void userbtn1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(btn1Mode == 1)
            {
                if(selectedPlId == 1||PlayerJobs[selectedPlId]==1)
                {
                    //当刀人指向自己或者狼人
                    MessageBox.Show("对象不能是狼人", "请注意", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private int userbtn2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(btn1Mode==1)
            {
                return -1;//表示跳过
            }
            return -2;//表示出错
        }
        private void buttonLogConsole_Click(object sender, RoutedEventArgs e)
        {
            // window position
            console.Left = this.Left + 0;
            console.Top = this.Top + 0;
            console.Show();
        }

    }
}
