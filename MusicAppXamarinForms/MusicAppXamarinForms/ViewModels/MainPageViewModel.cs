using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicAppXamarinForms.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public string Artist => "Matt & Kim";

        public string AlbumTitle => "Grand";

        public string NowPlayingImageSource => "band";

        public IList<TrackInfo> AlbumTracks =>
            new List<TrackInfo>
            {
                new TrackInfo{Title = "Daylight", Duration = "2:51"},
                new TrackInfo{Title = "Cutdown", Duration = "2:51"},
                new TrackInfo{Title = "Good Ol' Fashion Nightmare", Duration = "3:29"},
                new TrackInfo{Title = "Spare Change", Duration = "1:13"},
                new TrackInfo{Title = "I Wanna", Duration = "1:38"},
                new TrackInfo{Title = "Lessons Learned", Duration = "3:36"},
                new TrackInfo{Title = "Don't Slow Down", Duration = "3:07"},
                new TrackInfo{Title = "Turn This Boat Around", Duration = "2:10"},
                new TrackInfo{Title = "Cinders", Duration = "1:46"},
                new TrackInfo{Title = "I'll Take Us Home", Duration = "3:26"},
            };

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
