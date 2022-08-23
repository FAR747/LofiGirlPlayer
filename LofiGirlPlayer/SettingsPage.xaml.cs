﻿using MaterialDesignThemes.Wpf;
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

namespace LofiGirlPlayer
{
	/// <summary>
	/// SettingsPage.xaml Logic
	/// </summary>
	public partial class SettingsPage : Page
	{
		public MainWindow mainWindow = null;
		bool showwarningatclosing = false;
		bool loaded = false;
		public SettingsPage()
		{
			InitializeComponent();
			VersionTB.Text = String.Format("Version {0} ({1})", App.VERSION_NAME, App.VERSION_CODE);
			
		}

		public void LoadUISettings()
		{
			loaded = false;
			DisableGPUTB.IsChecked = mainWindow.config.DisableGPU;
			DisableAutoPlayTB.IsChecked = mainWindow.config.DisableAutoPlay;
			showwarningatclosing = false;
			loaded = true;
		}

		private void CloseSettingsButton_Click(object sender, RoutedEventArgs e)
		{ 
			ConfigManager.SaveConfig(mainWindow.config);
			if (!showwarningatclosing)
			{
				mainWindow.HideSettingsWindow();
			}
			else
			{
				DialogHost.Show(WarningNeedRestartDH.DialogContent);
			}
		}

		private void DisableGPUTB_Checked(object sender, RoutedEventArgs e)
		{
			if (loaded)
			{
				mainWindow.config.DisableGPU = (bool)DisableGPUTB.IsChecked;
				ConfigManager.SaveConfig(mainWindow.config);
				showwarningatclosing = true;
			}
		}

		private void DisableAutoPlayTB_Checked(object sender, RoutedEventArgs e)
		{
			if (loaded)
			{
				mainWindow.config.DisableAutoPlay = (bool)DisableAutoPlayTB.IsChecked;
				ConfigManager.SaveConfig(mainWindow.config);
			}
		}

		private void WarningNeedRestartCloseButton_Click(object sender, RoutedEventArgs e)
		{
			showwarningatclosing = false;
			mainWindow.HideSettingsWindow();
		}

		private void SocialButton_Click(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			System.Diagnostics.Process.Start(btn.Tag.ToString());
		}
	}
}
