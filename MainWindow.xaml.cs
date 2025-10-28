using Microsoft.Win32;
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
        private string currentTheme = "Lilac";
        private string currentBackground = "notebook";

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Console.WriteLine("[INFO] MainWindow constructor triggered.");
                BootSequence();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] MainWindow failed to initialize:\n{ex}");
                MessageBox.Show("Something went wrong during startup:\n" + ex.Message, "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Window Events

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double aspectRatio = 0.75;
            this.Width = this.Height * aspectRatio;
        }

        #endregion

        #region Theme Logic

        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                currentTheme = selectedItem.Content.ToString();
                Brush backgroundBrush = GetThemeBrush(currentTheme);
                this.Background = backgroundBrush;

                MyButton.Background = backgroundBrush;
                MyButton.Foreground = Brushes.White;
                MyLabel.Foreground = Brushes.White;

                SaveThemePreference(currentTheme);
                Console.WriteLine($"[THEME] Applied theme: {currentTheme} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }
        }

        private Brush GetThemeBrush(string themeName)
        {
            return themeName switch
            {
                "Lilac" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7C33CB")),
                "Lavender" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A383E8")),
                "Smoke" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#62679C")),
                "Grape" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8648FF")),
                _ => new SolidColorBrush(Colors.White),
            };
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

        #region Background Logic

        private void BackgroundSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (BackgroundSelector.SelectedItem is ComboBoxItem selectedItem)
                {
                    currentBackground = selectedItem.Content.ToString().Replace(" ", "").ToLower();
                    string brushKey = $"bg_{currentBackground}";
                    Console.WriteLine($"[BG] User selected: {brushKey}");

                    if (Application.Current.Resources[brushKey] is ImageBrush brush)
                    {
                        MainGrid.Background = brush;
                        SaveBackgroundPreference(currentBackground);
                        Console.WriteLine($"[BG] Background switched to {brushKey} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    }
                    else
                    {
                        Console.WriteLine($"[WARN] Background brush '{brushKey}' not found. Using fallback.");
                        MainGrid.Background = Brushes.White;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Background selection failed:\n{ex}");
                MessageBox.Show("Background selection failed:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveBackgroundPreference(string backgroundKey)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "background.txt");
            File.WriteAllText(path, backgroundKey);
        }

        private string LoadBackgroundPreference()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "background.txt");
            return File.Exists(path) ? File.ReadAllText(path) : "notebook";
        }

        #endregion

        #region Boot Sequence

        private void BootSequence()
        {
            try
            {
                PlayBootSound();
                LogBootTimestamp();

                currentTheme = LoadThemePreference();
                Brush themeBrush = GetThemeBrush(currentTheme);
                this.Background = themeBrush;

                MyButton.Background = themeBrush;
                MyButton.Foreground = Brushes.White;
                MyLabel.Foreground = Brushes.White;

                foreach (ComboBoxItem item in ThemeSelector.Items)
                {
                    if (item.Content.ToString() == currentTheme)
                    {
                        ThemeSelector.SelectedItem = item;
                        break;
                    }
                }

                currentBackground = LoadBackgroundPreference();
                string brushKey = $"bg_{currentBackground}";
                Console.WriteLine($"[BOOT] Attempting to load background: {brushKey}");

                if (Application.Current.Resources[brushKey] is ImageBrush brush)
                {
                    MainGrid.Background = brush;
                    Console.WriteLine($"[BOOT] Background applied: {brushKey}");
                }
                else
                {
                    Console.WriteLine($"[WARN] Background brush '{brushKey}' not found. Using fallback.");
                    MainGrid.Background = Brushes.White;
                }

                foreach (ComboBoxItem item in BackgroundSelector.Items)
                {
                    if (item.Content.ToString().Replace(" ", "").ToLower() == currentBackground)
                    {
                        BackgroundSelector.SelectedItem = item;
                        break;
                    }
                }

                Console.WriteLine($"[BOOT] Theme loaded: {currentTheme}, Background: {currentBackground}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] BootSequence failed:\n{ex}");
                MessageBox.Show("Boot sequence failed:\n" + ex.Message, "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        #region Save Logic

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Note.txt");
            File.WriteAllText(path, NoteEditor.Text);
            Console.WriteLine($"[SAVE] Note saved to {path} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        private void SaveNoteAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                FileName = "Note",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            bool? result = saveDialog.ShowDialog();

            if (result == true)
            {
                File.WriteAllText(saveDialog.FileName, NoteEditor.Text);
                Console.WriteLine($"[SAVE AS] Note saved to {saveDialog.FileName} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }
        }

        #endregion
    }
}
