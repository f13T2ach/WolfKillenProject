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

namespace WolfKillen
{
    /// <summary>
    /// LogConsole.xaml 的交互逻辑
    /// </summary>
    public partial class LogConsole : Window
    {
        public LogConsole()
        {
            InitializeComponent();

        }

        private MainWindow mainwindow;


        public LogConsole(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;
        }

        // 隐藏窗口 而不是退出它
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //以时间戳存储日志
            System.IO.File.WriteAllText(@".\"+ (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000 + "Log.txt", OutputBlock.Text);
            OutputBlock.Text = OutputBlock.Text+("["+ DateTime.Now.TimeOfDay.ToString() +"]"+"已输出到根目录\n");
        }
    }
}
