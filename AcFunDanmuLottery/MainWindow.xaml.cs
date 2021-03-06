﻿using AcFunDanmuLottery.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcFunDanmuLottery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex NumberOnly = new Regex(@"^[\d]+$");


        private readonly Lottery lottery;

        public MainWindow()
        {
            InitializeComponent();

            lottery = new Lottery { CurrentStatus = "未连接" };
            DataContext = lottery;
        }

        private void CheckInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !NumberOnly.IsMatch(e.Text);
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            if (!lottery.Connected)
            {
                lottery.Connect();
            }
            else
            {
                lottery.Stop();
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if (lottery.SearchStart)
            {
                lottery.StopSearch();
            }
            else
            {
                lottery.StartSearch();
            }
        }

        private void Roll(object sender, RoutedEventArgs e)
        {
            lottery.Roll();
        }

        private void ShowUser(object sender, MouseButtonEventArgs e)
        {
            var comment = (Comment)(sender as ListBox).SelectedItem;
            Process.Start(new ProcessStartInfo { FileName = $"https://www.acfun.cn/u/{comment.UserId}", UseShellExecute = true });
        }
    }
}
