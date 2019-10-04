using System;
using MusicAppXamarinForms.Themes;
using Xamarin.Forms;

namespace MusicAppXamarinForms.Views
{
    public partial class MainPage : ContentPage
    {
        private bool isPaused;
        private double bandImageStartPosition;
        private double moreIndicatorStartPosition;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            this.bandImageStartPosition = this.BandImageView.Y;
            this.moreIndicatorStartPosition = this.MoreIndicator.Y;
        }

        private void AnimationView_OnOnClick(object sender, EventArgs e)
        {
            this.PlayPause.PlayProgressSegment(
                this.isPaused ? 0.5f : 0.0f,
                this.isPaused ? 1.0f : 0.5f);
            this.isPaused = !this.isPaused;
        }

        private void ScrollView_OnScrolled(object sender, ScrolledEventArgs e)
        {
            this.BandImageView.TranslationY = e.ScrollY * 0.6;
            this.MoreIndicator.TranslationY = e.ScrollY * 0.6;
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            ThemeSwitcher.ToggleTheme();
        }
    }
}