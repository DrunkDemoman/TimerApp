using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TimerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        private TimeSpan timeLeft;
        private int hoursInput = 0;
        private int minutesInput = 0;
        private int secondsInput = 0;

        public MainPage()
        {
            this.InitializeComponent();
            InitializeTimer();
        }



        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void Timer_Tick(object sender, object e)
        {
            if (timeLeft.TotalSeconds > 0)
            {
                timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
                UpdateTimerDisplay();
            }
            else
            {
                timer.Stop();
                StartButton.IsEnabled = true;
                StopButton.IsEnabled = false;
            }
        }

        private void UpdateTimerDisplay()
        {
            TimerDisplay.Text = timeLeft.ToString(@"hh\:mm\:ss");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Parse input values
            int.TryParse(HoursInput.Text, out hoursInput);
            int.TryParse(MinutesInput.Text, out minutesInput);
            int.TryParse(SecondsInput.Text, out secondsInput);

            // Calculate the total time in seconds
            int totalSeconds = (hoursInput * 3600) + (minutesInput * 60) + secondsInput;

            if (totalSeconds > 0)
            {
                StartButton.IsEnabled = false;
                StopButton.IsEnabled = true;

                timeLeft = TimeSpan.FromSeconds(totalSeconds);

                UpdateTimerDisplay();
                timer.Start();
            }
            else
            {

            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            timer.Stop();
        }
    }
}