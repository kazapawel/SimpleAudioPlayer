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
    /// Logika interakcji dla klasy PlaylistItem.xaml
    /// </summary>
    public partial class PlaylistItem : UserControl
    {
        public PlaylistItem()
        {
            InitializeComponent();
        }

        private void PlaylistItem_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton==MouseButtonState.Pressed && sender is FrameworkElement frameworkElement)
            {
                DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, frameworkElement.DataContext), DragDropEffects.Move);
            }
        }


        private void PlaylistItem_DragOver(object sender, DragEventArgs e)
        {
            //Gets playlist data context
            var x = sender as PlaylistItem;
            var audioFileVm = x.DataContext as AudioFileVM;


            //Gets index of moved item
        }
    }
}
