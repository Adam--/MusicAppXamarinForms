using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicAppXamarinForms.Views
{
    public partial class MainPage : ContentPage
    {
        private bool isPaused;

        public MainPage()
        {
            InitializeComponent();
        }

        private void AnimationView_OnOnClick(object sender, EventArgs e)
        {
            this.PlayPause.PlayProgressSegment(
                this.isPaused ? 0.5f : 0.0f,
                this.isPaused ? 1.0f : 0.5f);
            this.isPaused = !this.isPaused;
        }
    }
}