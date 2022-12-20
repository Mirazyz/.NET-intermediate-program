using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for AdWindow.xaml
    /// </summary>
    public partial class AdWindow : Window
    {
        private static readonly Random _random = new Random();
        private static readonly string adLink = "http://example.com";
        private static readonly DispatcherTimer adTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(3)
        };

        private int imagesCount = _random.Next(1, 3);

        public AdWindow(Window owner)
        {
            InitializeComponent();

            Owner = owner;
            
            // Run the timer that changes the ad's image
            SetupTimer();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ChangeAds(this, new EventArgs());
        }

        protected override void OnClosed(EventArgs e)
        {
            Unsubscribe();
            base.OnClosed(e);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(adLink);
            Close();
        }

        private void SetupTimer()
        {
            adTimer.Tick += ChangeAds;
            adTimer.Start();
        }

        public void Unsubscribe()
        {
            adTimer.Tick -= ChangeAds;
            adTimer.Stop();
        }

        public void Subscribe()
        {
            adTimer.Tick += ChangeAds;
            adTimer.Start();
        }

        private void ChangeAds(object sender, EventArgs eventArgs)
        {
            ImageBrush myBrush = new ImageBrush();

            switch (imagesCount)
            {
                case 1:
                    myBrush.ImageSource =
                        new BitmapImage(new Uri("ad1.jpg", UriKind.Relative));
                    break;
                case 2:
                    myBrush.ImageSource =
                        new BitmapImage(new Uri("ad2.jpg", UriKind.Relative));
                    break;
                case 3:
                    myBrush.ImageSource =
                        new BitmapImage(new Uri("ad3.jpg", UriKind.Relative));
                    break;
            }

            // Reset image count
            imagesCount = imagesCount == 3 ? 1 : imagesCount + 1;
            Background = myBrush;
        }
    }
}
