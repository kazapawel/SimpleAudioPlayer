using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Interaction logic for TransportPanelControl.xaml
    /// </summary>
    public partial class TransportPanelControl : UserControl
    {
        private Track track;
        private Slider positionSlider;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            positionSlider = this.FindName("PositionSlider") as Slider;
            //track = positionSlider.Template.FindName("PART_Track", positionSlider) as Track ;
        }

        /// <summary>
        /// Dedfault constructor
        /// </summary>
        public TransportPanelControl()
        {
            InitializeComponent();
        }

        private void PositionSlider_DragStarted(object sender, DragStartedEventArgs e)
        {

        }

        private void PositionSlider_MouseMove(object sender, MouseEventArgs e)
        {
            track = positionSlider.Template.FindName("PART_Track", positionSlider) as Track;

            if (e.LeftButton == MouseButtonState.Pressed && track != null)
            {
                positionSlider.Value = track.ValueFromPoint(e.GetPosition(track));
            }
        }
    }
}
