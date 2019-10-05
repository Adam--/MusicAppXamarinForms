using System;
using System.Threading.Tasks;
using MusicAppXamarinForms.Themes;
using Xamarin.Forms;

namespace MusicAppXamarinForms.Views
{
    public partial class MainPage : ContentPage
    {
        private bool isPaused;
        private bool isShowingPlayingView;
        private readonly double nowPlayingStartHeight;

        public MainPage()
        {
            InitializeComponent();
            this.nowPlayingStartHeight = this.NowPlayingGrid.HeightRequest;
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
            this.AlbumImageView.TranslationY = e.ScrollY * 0.6;
            this.MoreIndicator.TranslationY = e.ScrollY * 0.3;
        }

        private void MoreIndicator_OnClicked(object sender, EventArgs e)
        {
            this.ScrollView.ScrollToAsync(this.TracksCollectionView, ScrollToPosition.Start, true);
        }

        private void Shuffle_OnClicked(object sender, EventArgs e)
        {
            ThemeSwitcher.ToggleTheme();
        }

        private async void NowPlaying_OnTapped(object sender, EventArgs e)
        {
            if (this.isShowingPlayingView)
            {
                this.ShowAlbumView();
            }
            else
            {
                if (this.ScrollView.ScrollY > 0)
                {
                    await this.ScrollView.ScrollToAsync(this.NowPlayingGrid, ScrollToPosition.Start, true);
                }

                this.ShowPlayingView();
            }
        }

        private void ShowPlayingView()
        {
            this.AbortAnimation(nameof(ShowAlbumView));

            var albumControlsFadeOutAnimation = new Animation(
                d =>
                {
                    this.TracksCollectionView.Opacity = d;
                    this.ShowMoreIndicator.Opacity = d;
                    this.TrackProgressBar.Opacity = d;
                },
                1.0,
                0.0,
                Easing.Linear,
                () =>
                {
                    this.TracksCollectionView.IsVisible = false;
                    this.ShowMoreIndicator.IsVisible = false;
                    this.TrackProgressBar.IsVisible = false;
                });

            this.TrackTitleLabel.Opacity = 0.0;
            this.TrackTitleLabel.IsVisible = true;
            var titleSwitchAnimation = new Animation(
                d =>
                {
                    this.TrackTitleLabel.Opacity = d;
                    this.AlbumTitleLabel.Opacity = 1.0 - d;
                    this.AlbumImageView.Opacity = 1.0 - d;
                },
                0.0,
                1.0,
                Easing.CubicOut,
                () => this.AlbumTitleLabel.IsVisible = false);

            this.TrackProgressSlider.Opacity = 0;
            var trackProgressSliderFadeInAnimation = new Animation(
                d =>
                {
                    this.TrackProgressSlider.IsVisible = true;
                    this.TrackProgressSlider.Opacity = d;
                },
                0.0,
                1.0,
                Easing.Linear);

            var expandNowPlayingAnimation = new Animation(
                d => this.NowPlayingGrid.HeightRequest = d,
                nowPlayingStartHeight,
                nowPlayingStartHeight + 100,
                Easing.CubicOut);

            new Animation
            {
                {0.0, 0.3, albumControlsFadeOutAnimation},
                {0.0, 0.8, expandNowPlayingAnimation},
                {0.0, 0.8, titleSwitchAnimation},
                {0.3, 1.0, trackProgressSliderFadeInAnimation},
            }.Commit(
                this,
                nameof(ShowPlayingView),
                16U,
                750,
                finished: (d, b) => this.isShowingPlayingView = true);
        }

        private void ShowAlbumView()
        {
            this.AbortAnimation(nameof(ShowPlayingView));

            var trackProgressSliderFadeOutAnimation = new Animation(
                d => { this.TrackProgressSlider.Opacity = d; },
                1.0,
                0.0,
                Easing.Linear,
                () => { this.TrackProgressSlider.IsVisible = false; });

            var albumControlsFadeInAnimation = new Animation(
                d =>
                {
                    this.TracksCollectionView.IsVisible = true;
                    this.ShowMoreIndicator.IsVisible = true;
                    this.TrackProgressBar.IsVisible = true;

                    this.TracksCollectionView.Opacity = d;
                    this.ShowMoreIndicator.Opacity = d;
                    this.TrackProgressBar.Opacity = d;
                },
                0.0,
                1.0,
                Easing.Linear);

            var collapseNowPlayingAnimation = new Animation(
                d => this.NowPlayingGrid.HeightRequest = d,
                this.NowPlayingGrid.Height,
                nowPlayingStartHeight,
                Easing.CubicOut);

            this.AlbumTitleLabel.Opacity = 0.0;
            this.AlbumTitleLabel.IsVisible = true;
            var titleSwitchAnimation = new Animation(
                d =>
                {
                    this.TrackTitleLabel.Opacity = 1.0 - d;
                    this.AlbumTitleLabel.Opacity = d;
                    this.AlbumImageView.Opacity = d;
                },
                0.0,
                1.0,
                Easing.CubicOut,
                () => this.TrackTitleLabel.IsVisible = false);

            new Animation
            {
                {0.0, 0.3, trackProgressSliderFadeOutAnimation},
                {0.0, 0.8, collapseNowPlayingAnimation},
                {0.0, 0.8, titleSwitchAnimation},
                {0.3, 1.0, albumControlsFadeInAnimation},
            }.Commit(
                this,
                nameof(ShowPlayingView),
                16U,
                750,
                finished: (d, b) => this.isShowingPlayingView = false);
        }


    }
}