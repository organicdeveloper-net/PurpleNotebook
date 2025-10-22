using System.Windows;

namespace PurpleNotebook
{
    public partial class TagInputDialog : Window
    {
        #region Properties

        public string EnteredTag { get; private set; }

        #endregion

        #region Constructor

        public TagInputDialog()
        {
            InitializeComponent(); // <-- call, not just the identifier
        }

        #endregion

        #region Event Handlers

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string rawTag = TagTextBox?.Text?.Trim();

            if (!string.IsNullOrEmpty(rawTag))
            {
                EnteredTag = rawTag;
                DialogResult = true;
            }
            else
            {
                DialogResult = false; // Optional: disable empty tags
            }

            Close();
        }

        private void SplashVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.Close(); // Or transition to MainWindow
        }

        #endregion
    }
}
