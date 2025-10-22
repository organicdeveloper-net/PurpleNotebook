using System.Windows;
using System.Windows.Controls;

namespace PurpleNotebook
{
    public partial class NoteEditor : UserControl
    {
        public NoteEditor()
        {
            InitializeComponent();
        }

        private void ApplyTags_Click(object sender, RoutedEventArgs e)
        {
            string tags = TagsBox?.Text?.Trim();
            // TODO: Add logic to apply tags, log hash, etc.
        }
    }
}
