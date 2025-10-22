using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PurpleNotebook
{
    public partial class MainWindow : Window
    {
        private string currentTheme = "Lilac"; // default fallback

        public MainWindow()
        {
            InitializeComponent();
            BootSequence();
        }

        #region Window Events

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double aspectRatio = 0.75;
            this.Width = this.Height * aspectRatio;
        }

        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                currentTheme = selectedItem.Content.ToString();
                Brush backgroundBrush = GetThemeBrush(currentTheme);
                this.Background = backgroundBrush;

                // Style other controls
                MyButton.Background = backgroundBrush;
                MyButton.Foreground = Brushes.White;
                MyLabel.Foreground = Brushes.White;

                SaveThemePreference(currentTheme);
                Console.WriteLine($"[THEME] Applied theme: {currentTheme} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }
        }

        #endregion

        #region Boot Sequence

        private void BootSequence()
        {
            PlayBootSound();
            LogBootTimestamp();

            // Load and apply saved theme
            currentTheme = LoadThemePreference();
            Brush backgroundBrush = GetThemeBrush(currentTheme);
            this.Background = backgroundBrush;

            // Style other controls
            MyButton.Background = backgroundBrush;
            MyButton.Foreground = Brushes.White;
            MyLabel.Foreground = Brushes.White;

            // Update ComboBox selection
            foreach (ComboBoxItem item in ThemeSelector.Items)
            {
                if (item.Content.ToString() == currentTheme)
                {
                    ThemeSelector.SelectedItem = item;
                    break;
                }
            }

            Console.WriteLine($"[BOOT] Theme loaded: {currentTheme}");
        }

        private void PlayBootSound()
        {
            string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "boot.wav");

            if (File.Exists(soundPath))
            {
                try
                {
                    SoundPlayer player = new SoundPlayer(soundPath);
                    player.Play();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Boot sound playback failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("[WARN] Boot sound file not found.");
            }
        }

        private void LogBootTimestamp()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[BOOT] Notebook launched at {timestamp}");
        }

        #endregion

        #region Theme Logic

        private Brush GetThemeBrush(string themeName)
        {
            switch (themeName)
            {
                case "Lilac":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7C33CB"));
                case "Lavender":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A383E8"));
                case "Smoke":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#62679C"));
                case "Grape":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8648FF"));
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        private void SaveThemePreference(string themeName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "theme.txt");
            File.WriteAllText(path, themeName);
        }

        private string LoadThemePreference()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "theme.txt");
            return File.Exists(path) ? File.ReadAllText(path) : "Lilac";
        }

        #endregion
    }
}
