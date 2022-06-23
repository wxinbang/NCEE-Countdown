﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace NCEE_Countdown_WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string nowTimeString;
		private string leftTotalDays;
		private DateTime nowTime;
		private readonly int nowYear;
		private DateTime finalTime;
		private DateTime finalDay;
		private TimeSpan leftTime;
		private TimeSpan leftDays;
		private string leftTimeString;
		private int deleteChars = 8;
		private readonly DispatcherTimer timer = new();

		private readonly List<int> years = new();

		public MainWindow()
		{
			InitializeComponent();

			nowTime = DateTime.Now;
			nowYear = DateTime.Now.Year;
			finalTime = new DateTime(nowYear, 6, 7, 9, 0, 0);
			finalDay = new DateTime(nowYear, 6, 7);

			if ((finalTime - nowTime).TotalMinutes >= 0) years.Add(nowYear);
			else
			{
				finalTime = new DateTime(++nowYear, 6, 7, 9, 0, 0);
				finalDay = new DateTime(nowYear, 6, 7);
				years.Add(nowYear);
			}

			years.Add(++nowYear);
			years.Add(++nowYear);

			Years.ItemsSource = years;
			Years.SelectedIndex = 0;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			timer.Interval = new TimeSpan(0, 0, 0, 0, 980);
			timer.Tick += Timer_Tick;
			timer.Start();
			Timer_Tick(sender, e);
		}

		private void Timer_Tick(object sender, object e)
		{
			nowTime = DateTime.Now;
			nowTimeString = nowTime.ToString();
			NowTime.Text = "现在是:" + nowTimeString;

			leftTime = finalTime - nowTime;

			if (leftTime.TotalSeconds > 0)
			{
				leftDays = finalDay - nowTime;
				leftTotalDays = Math.Floor(leftDays.TotalDays).ToString();
				LeftDay.Text = "广义剩余" + (leftTotalDays == "-1" ? "0" : leftTotalDays) + "天";
			}
			else leftTime = new TimeSpan(0, 0, 0, 0, 0);
			leftTimeString = leftTime.ToString();
			leftTimeString = leftTimeString[..^deleteChars];

			LeftTime.Text = "剩余时间:" + leftTimeString;
		}

		private void FastMode_Toggled(object sender, RoutedEventArgs e)
		{
			if (FastMode.IsChecked == true)
			{
				timer.Interval = new TimeSpan(0, 0, 0, 0, 9);
				deleteChars = 0;
			}
			else if (FastMode.IsChecked == false)
			{
				deleteChars = 8;
				timer.Interval = new TimeSpan(0, 0, 0, 0, 980);
				Timer_Tick(sender, e);
			}
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			finalTime = new DateTime((int)Years.SelectedItem, 6, 7, 9, 0, 0);
			finalDay = new DateTime((int)Years.SelectedItem, 6, 7);
			Timer_Tick(sender, e);
		}

	}
}
