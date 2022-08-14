using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Bakugo
{
    public sealed partial class MainPage : Page
    {
        MediaElement mediaElement = new MediaElement();
        Windows.Media.SpeechSynthesis.SpeechSynthesizer synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
        Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream;

        public MainPage()
        {
            this.InitializeComponent();
            SetStream("");
            if(App.Current.RequestedTheme is ApplicationTheme.Dark)
            {
                themeToggleSwitch.IsOn = true;
            }
        }

        private void Speak_Click(object sender, RoutedEventArgs e)
        {
            if(!(mediaElement.CurrentState is MediaElementState.Playing))
            {
                SetStream(Text1.Text);
                mediaElement.Play();
            }
        }

        private async void SetStream(string s)
        {
            stream = await synth.SynthesizeTextToStreamAsync(s);
            mediaElement.SetSource(stream, stream.ContentType);
        }

        private void TextKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key is Windows.System.VirtualKey.Enter)
            {
                Speak_Click(sender, e);
                Text1.Text = "";
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                if(themeToggleSwitch.IsOn)
                    frameworkElement.RequestedTheme = ElementTheme.Dark;
                else
                    frameworkElement.RequestedTheme = ElementTheme.Light;
            }
        }
    }
}
