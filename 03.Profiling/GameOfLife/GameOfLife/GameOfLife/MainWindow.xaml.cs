using System;
using System.Windows;
using System.Windows.Threading;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        private readonly Grid mainGrid;
        private readonly DispatcherTimer timer;   //  Generation timer
        private readonly AdWindow[] adWindows = new AdWindow[2];
        private int generationsCounter = 0;

        public MainWindow()
        {
            InitializeComponent();

            mainGrid = new Grid(MainCanvas);
            timer = new DispatcherTimer();
            timer.Tick += OnTimer;
            timer.Interval = TimeSpan.FromMilliseconds(200);
        }

        private void StopAds()
        {
            for(int i = 0; i < adWindows.Length; i++)
            {
                adWindows[i]?.Unsubscribe();
            }
        }

        private void StartAds()
        {
            for (int i = 0; i < adWindows.Length; i++)
            {
                if (adWindows[i] == null)
                {
                    adWindows[i] = new AdWindow(this);
                    adWindows[i].Closed += OnAdWindow_Closed;
                    adWindows[i].Top = Top + (330 * i) + 70;
                    adWindows[i].Left = Left + 240;
                    adWindows[i].Show();
                }
                else
                {
                    adWindows[i].Subscribe();
                }
            }
        }

        private void OnAdWindow_Closed(object sender, EventArgs eventArgs)
        {
            for (int i = 0; i < 2; i++)
            {
                adWindows[i].Closed -= OnAdWindow_Closed;
                adWindows[i] = null;
            }
        }

        private void OnStartButton_Clicked(object sender, EventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                ButtonStart.Content = "Stop";
                StartAds();
            }
            else
            {
                timer.Stop();
                ButtonStart.Content = "Start";
                StopAds();
            }
        }

        private void OnClearButton_Clicked(object sender, RoutedEventArgs e)
        {
            mainGrid.Clear();
        }

        private void OnTimer(object sender, EventArgs e)
        {
            mainGrid.Update();
            generationsCounter++;
            lblGenCount.Content = "Generations: " + generationsCounter;
        }
    }
}
