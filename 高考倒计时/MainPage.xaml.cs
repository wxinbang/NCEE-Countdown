using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace 高考倒计时
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string nowTimeString;
        private string leftTotalDays;
        private DateTime nowTime;
        private readonly DateTime finalTime = new DateTime(2023, 6, 7, 9, 0, 0);
        private readonly DateTime finalDay = new DateTime(2023, 6, 7);
        private TimeSpan leftTime;
        private TimeSpan leftDays;
        private string leftTimeString;
        private int deleteChars = 8;
        private readonly DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 980);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            nowTime = DateTime.Now;
            nowTimeString = nowTime.ToString();
            NowTime.Text = "现在是:" + nowTimeString;

            leftTime = finalTime - nowTime;

            leftTimeString = leftTime.ToString();
            leftTimeString = leftTimeString.Substring(0, leftTimeString.Length - deleteChars);

            LeftTime.Text = "剩余时间:" + leftTimeString;

            leftDays = finalDay - nowTime;
            leftTotalDays = Math.Floor(leftDays.TotalDays).ToString();
            LeftDay.Text = "广义剩余" + leftTotalDays + "天";
        }

        private void FastMode_Toggled(object sender, RoutedEventArgs e)
        {
            if (FastMode.IsOn == true)
            {
                timer.Interval = new TimeSpan(0, 0, 0, 0, 9);
                deleteChars = 0;
            }
            else if (FastMode.IsOn == false)
            {
                deleteChars = 8;
                timer.Interval = new TimeSpan(0, 0, 0, 0, 980);
                Timer_Tick(sender, e);
            }
        }
    }
}
