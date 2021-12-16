using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Interaction logic for TransportPanelControl.xaml
    /// </summary>
    public partial class TransportPanelControl : UserControl
    {
        /// <summary>
        /// Occurs when files are drop into control.
        /// </summary>
        public EventHandler<DropFilesEventArgs> FilesDropEvent;

        /// <summary>
        /// Dedfault constructor
        /// </summary>
        public TransportPanelControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                FilesDropEvent?.Invoke(this, new DropFilesEventArgs((string[])e.Data.GetData(DataFormats.FileDrop)));
            }
        }
    }
}
