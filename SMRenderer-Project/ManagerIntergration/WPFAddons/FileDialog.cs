using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace SMRenderer.ManagerIntergration.WPFAddons
{
    public class FileDialogWPF : Grid
    {
        FileDialog fd;
        public TextBox entry;
        public string Text { get => entry.Text; set => entry.Text = value; }
        public bool Empty => entry.Text == "";

        public FileDialogWPF(FileDialog fd)
        {
            this.fd = fd;
            CreateVisual();
        }

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
