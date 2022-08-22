﻿using System;
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

namespace LofiGirlPlayer
{
	/// <summary>
	/// SettingsPage.xaml Logic
	/// </summary>
	public partial class SettingsPage : Page
	{
		public MainWindow mainWindow = null;
		public SettingsPage()
		{
			InitializeComponent();
			VersionTB.Text = String.Format("Version {0} ({1})", App.VERSION_NAME, App.VERSION_CODE);
		}

		private void CloseSettingsButton_Click(object sender, RoutedEventArgs e)
		{
			mainWindow.HideSettingsWindow();
		}
	}
}
