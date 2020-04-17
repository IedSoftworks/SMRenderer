using System.Drawing;
using SMRenderer.ManagerIntergration.Attributes;
using SMRenderer.ManagerIntergration.Base;
using SMRenderer.ManagerIntergration.WPFAddons;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Brushes = System.Windows.Media.Brushes;

namespace SMRenderer.ManagerIntergration.VanillaEntry
{
    [FieldEntry(typeof(System.Drawing.Bitmap))]
    class BitmapEntry : VisualDataEntry
    {
        private FileDialogWPF dialog;

        public override UIElement CreatePanel()
        {
            dialog = new FileDialogWPF(new OpenFileDialog() {Filter = "Picture Files|*.bmp;*.jpg;*.png"});

            return dialog;
        }

        public override object GetValue()
        {
            if (dialog.Empty)
                return DataEntryReport.StopLoading(() => dialog.entry.BorderBrush = Brushes.Red);
            return new Bitmap(dialog.Text);
        }
    }
}
