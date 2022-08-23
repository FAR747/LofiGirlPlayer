using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;

namespace LofiGirlPlayer
{
	/// <summary>
	/// MainWindow.xaml logic
	/// </summary>
	//document.getElementsByClassName('video-stream html5-main-video')[0].pause()
	//document.getElementsByClassName('video-stream html5-main-video')[0].play();
	//document.getElementsByClassName('video-stream html5-main-video')[0].volume = 1
	//document.getElementsByClassName('video-stream html5-main-video')[0].paused
	public partial class MainWindow : Window
	{
		public Config config = null;
		public SourceList sourcelist = null;
		public bool isplayed = true;
		bool stopnav = false;
		public MainWindow()
		{
			config = ConfigManager.LoadConfig();
			string browserargs = "--autoplay-policy=no-user-gesture-required";
			if (config.DisableGPU)
			{
				browserargs += " --disable-gpu";
			}
			Environment.SetEnvironmentVariable("WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS", browserargs);
			System.Diagnostics.Trace.WriteLine(browserargs);
			InitializeComponent();
			System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
			provider.NumberDecimalSeparator = ".";
			VolumeSlider.Value = Convert.ToDouble(config.YTVolume, provider);
			sourcelist = LoadSourceList();
			StreamComboBox.Items.Clear();
			foreach (SourceListElement sl in sourcelist.Sources)
			{
				ComboBoxItem item = new ComboBoxItem();
				item.Content = sl.Name;
				if (StreamComboBox.Items.Count == 0)
				{
					item.IsSelected = true;
				}
				StreamComboBox.Items.Add(item);
			}
			
		}

		public SourceList LoadSourceList(string file = "./Sources.json")
		{
			if (!File.Exists(file))
			{
				return null;
			}
			string json = File.ReadAllText(file);
			try
			{
				SourceList sourcelist = JsonConvert.DeserializeObject<SourceList>(json);
				return sourcelist;
			}
			catch
			{
				return null;
			}
		}

		public void PlaySource(int id)
		{
			string ytid = sourcelist.Sources[id].Ytid;
			Browser.Source = new Uri(String.Format("https://www.youtube-nocookie.com/embed/{0}?autoplay=1&vq={1}", ytid, config.YTQuality));
			System.Diagnostics.Trace.WriteLine("Loading Source: " + Browser.Source);
		}

		public async void SetVolume(string volume = "1.0")
		{
			if (Browser.CoreWebView2 != null) {
				config.YTVolume = volume;
				await Browser.CoreWebView2.ExecuteScriptAsync(String.Format("document.getElementsByClassName('video-stream html5-main-video')[0].volume = {0}", volume));

				if (volume == "0")
				{
					VolumeMuteIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeOff;
				}
				else
				{
					VolumeMuteIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;
				}
			}
		}

		public async void SetPlay(bool play = true)
		{
			if (play)
			{
				await Browser.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('video-stream html5-main-video')[0].play();");
				SetPlayIcon(true);
			}
			else
			{
				await Browser.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('video-stream html5-main-video')[0].pause();");
				SetPlayIcon(false);
			}
			isplayed = play;
		} 

		public void SetPlayIcon(bool isplayed)
		{
			if (isplayed)
			{
				PlayIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
			}
			else
			{
				PlayIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
			}
		}

		private void Browser_Initialized(object sender, EventArgs e)
		{
			Browser.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
		}

		private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e) //We catch the opening of a new window and prevent it.
		{
			e.NewWindow = (CoreWebView2)sender;
			e.Handled = false;
			stopnav = true;
		}

		private async void Browser_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			string adblock = "function C(d,o){v=d.createElement('div');o.parentNode.replaceChild(v,o);}function A(d){for(j=0;t=[\", Interaction.IIf(browser.Address.Contains(\"youtube-nocookie.com\"), \"'iframe','marquee'\", \"'iframe','embed','marquee'\")), \"][j];++j){o=d.getElementsByTagName(t);for(i=o.length-1;i>=0;i--)C(d,o[i]);}g=d.images;for(k=g.length-1;k>=0;k--)if({'21x21':1,'48x48':1,'60x468':1,'88x31':1,'88x33':1,'88x62':1,'90x30':1,'90x32':1,'90x90':1,'100x30':1,'100x37':1,'100x45':1,'100x50':1,'100x70':1,'100x100':1,'100x275':1,'110x50':1,'110x55':1,'110x60':1,'110x110':1,'120x30':1,'120x60':1,'120x80':1,'120x90':1,'120x120':1,'120x163':1,'120x181':1,'120x234':1,'120x240':1,'120x300':1,'120x400':1,'120x410':1,'120x500':1,'120x600':1,'120x800':1,'125x40':1,'125x60':1,'125x65':1,'125x72':1,'125x80':1,'125x125':1,'125x170':1,'125x250':1,'125x255':1,'125x300':1,'125x350':1,'125x400':1,'125x600':1,'125x800':1,'126x110':1,'130x60':1,'130x65':1,'130x158':1,'130x200':1,'132x70':1,'140x55':1,'140x350':1,'145x145':1,'146x60':1,'150x26':1,'150x60':1,'150x90':1,'150x100':1,'150x150':1,'155x275':1,'155x470':1,'160x80':1,'160x126':1,'160x600':1,'180x30':1,'180x66':1,'180x132':1,'180x150':1,'194x165':1,'200x60':1,'220x100':1,'225x70':1,'230x30':1,'230x33':1,'230x60':1,'234x60':1,'234x68':1,'240x80':1,'240x300':1,'250x250':1,'275x60':1,'280x280':1,'300x60':1,'300x100':1,'300x250':1,'320x50':1,'320x70':1,'336x280':1,'350x300':1,'350x850':1,'360x300':1,'380x112':1,'380x250':1,'392x72':1,'400x40':1,'400x50':1,'425x600':1,'430x225':1,'440x40':1,'464x62':1,'468x16':1,'468x60':1,'468x76':1,'468x120':1,'468x248':1,'470x60':1,'480x400':1,'486x60':1,'545x90':1,'550x5':1,'600x30':1,'720x90':1,'720x300':1,'725x90':1,'728x90':1,'734x96':1,'745x90':1,'750x25':1,'750x100':1,'750x150':1,'850x120':1}[g[k].width+'x'+g[k].height])C(d,g[k]);}A(document);for(f=0;z=frames[f];++f)A(z.document)"; // https://github.com/cefsharp/CefSharp/issues/1413#issuecomment-157375374
			System.Threading.Thread.Sleep(600); // workaround. Without this, the volume is not set because the player does not have time to initialize.
			SetVolume(VolumeSlider.Value.ToString().Replace(",", "."));
			await Browser.CoreWebView2.ExecuteScriptAsync(adblock);
			// This is necessary to hide the profile picture and the button to go to YouTube, as this could cause bugs.
			await Browser.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('ytp-title-channel')[0].remove()");
			await Browser.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('ytp-youtube-button ytp-button yt-uix-sessionlink')[0].remove()");
			//
			Browser.CoreWebView2.IsMuted = false;
			LoadingCircle.Visibility = Visibility.Collapsed;
			PlayButton.IsEnabled = true;
			VolumeMuteButton.IsEnabled = true;
			VolumeSlider.IsEnabled = true;
			WebTitleNameTB.Text = Browser.CoreWebView2.DocumentTitle;
			SetPlay(!config.DisableAutoPlay);
			
			
			if (stopnav)
			{
				PlaySource(StreamComboBox.SelectedIndex);
				stopnav = false;
			}
		}

		private void Browser_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
		{
			LoadingCircle.Visibility = Visibility.Visible;
			PlayButton.IsEnabled = false;
			VolumeMuteButton.IsEnabled = false;
			VolumeSlider.IsEnabled = false;
			Browser.CoreWebView2.IsMuted = true;
		}

		private void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			Browser.CoreWebView2.Reload();
		}

		private void StreamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			PlaySource(StreamComboBox.SelectedIndex);
		}

		private void VolumeSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
		{
			SetVolume(VolumeSlider.Value.ToString().Replace(",", "."));
		}
		private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			SetVolume(VolumeSlider.Value.ToString().Replace(",", "."));
		}
		private void PlayButton_Click(object sender, RoutedEventArgs e)
		{
			SetPlay(!isplayed);
		}

		private async void VolumeMuteButton_Click(object sender, RoutedEventArgs e)
		{
			string currentvolume = await Browser.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('video-stream html5-main-video')[0].volume");
			if (currentvolume == "0")
			{
				SetVolume(VolumeSlider.Value.ToString().Replace(",", "."));
			}
			else
			{
				SetVolume("0");
			}
		}

		private void OpenBrowserViewButton_Click(object sender, RoutedEventArgs e)
		{
			WebGrid.Visibility = Visibility.Visible;
			UIGrid.Visibility = Visibility.Hidden;
		}

		private void HideBrowserViewButton_Click(object sender, RoutedEventArgs e)
		{
			WebGrid.Visibility = Visibility.Hidden;
			UIGrid.Visibility = Visibility.Visible;
		}

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			SettingsPage sp = (SettingsPage)SettingsFrame.Content;
			sp.mainWindow = this;
			sp.LoadUISettings();
			SettingsUIGrid.Visibility = Visibility.Visible;
		}
		public void HideSettingsWindow()
		{
			SettingsUIGrid.Visibility = Visibility.Collapsed;
		}
	}
}
