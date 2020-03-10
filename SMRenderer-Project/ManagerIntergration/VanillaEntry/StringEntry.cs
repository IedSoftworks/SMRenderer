using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SMRenderer.ManagerIntergration.Attributes;
using SMRenderer.ManagerIntergration.Base;

namespace SMRenderer.ManagerIntergration.VanillaEntry
{
    [FieldEntry(typeof(string))]
    internal class StringEntry : VisualDataEntry
    {
        private TextBox _entry;

        public override UIElement CreatePanel()
        {
            _entry = new TextBox
            {
                FontSize = 15,
                MaxLines = 1,
                MinLines = 1
            };

            return _entry;
        }

        public override object GetValue()
        {
            if (string.IsNullOrWhiteSpace(_entry.Text))
                return DataEntryReport.StopLoading(() => _entry.BorderBrush = Brushes.Red);

            return _entry.Text;
        }
    }
}