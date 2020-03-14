using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace SMRenderer.ManagerIntergration.WPFAddons
{
    /// <summary>
    /// Extension from iedExt; It creates a selection for a file.
    /// </summary>
    public class FileDialogWPF : Grid
    {
        /// <summary>
        /// The file dialog, that will be used
        /// </summary>
        FileDialog fd;
        /// <summary>
        /// The entryBox
        /// </summary>
        public TextBox entry;
        /// <summary>
        /// Returns / Sets the Text inside the entry.
        /// </summary>
        public string Text { get => entry.Text; set => entry.Text = value; }
        /// <summary>
        /// Is true, if the entry is empty.
        /// </summary>
        public bool Empty => entry.Text == "";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fd">The FileDialog that it should use</param>
        public FileDialogWPF(FileDialog fd)
        {
            this.fd = fd;
            CreateVisual();
        }
        /// <summary>
        /// Create the visual.
        /// </summary>
        private void CreateVisual()
        {
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
            RowDefinitions.Add(new RowDefinition());

            ColumnDefinitions[1].Width = GridLength.Auto;

            entry = new TextBox
            {
                MinLines = 1,
                MaxLines = 1
            };
            Children.Add(entry);

            Button save = new Button
            {
                Content = "Search..."
            };
            save.Click += Save_Click;
            Grid.SetColumn(save, 1);
            Children.Add(save);

        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (fd.ShowDialog() == true)
                entry.Text = fd.FileName;
        }
        /// <summary>
        /// Sets the error state.
        /// <para>0 = Nothing happend</para>
        /// <para>1 = Error</para>
        /// <para>2 = Success</para>
        /// </summary>
        /// <param name="state"></param>
        public void ErrorState(int state)
        {
            switch(state)
            {
                case 0:
                    entry.BorderBrush = Brushes.LightGray;
                    break;

                case 1:
                    entry.BorderBrush = Brushes.Red;
                    break;

                case 2:
                    entry.BorderBrush = Brushes.Green;
                    break;
            }
        }
    }
}
