namespace MusicAppXamarinForms.Themes
{
    using System.Linq;
    using Xamarin.Forms;

    public static class ThemeSwitcher
    {
        public static void ToggleTheme()
        {
            ResourceDictionary newThemeResourceDictionary;
            if (Application.Current.Resources.MergedDictionaries.Any(d => d.GetType().Name == typeof(LightTheme).Name))
            {
                newThemeResourceDictionary = new DarkTheme();
            }
            else
            {
                newThemeResourceDictionary = new LightTheme();
            }

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(newThemeResourceDictionary);
        }
    }
}
