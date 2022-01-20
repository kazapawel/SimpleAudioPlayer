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
        #region PRIVATE FIELDS

        private Track positionTrack;
        private Slider positionSlider;

        private Track volumeTrack;
        private Slider volumeSlider;

        #endregion

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
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            positionSlider = this.FindName("PositionSlider") as Slider;
            volumeSlider = this.FindName("VolumeSlider") as Slider;
        }

        #region VOLUME SLIDER DRAG

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolumeSlider_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets track from slider template
            volumeTrack = volumeSlider.Template.FindName("PART_Track", volumeSlider) as Track;

            if (e.LeftButton == MouseButtonState.Pressed && volumeTrack != null)
            {
                //Captures mouse to prevent mouse movement overlaping
                volumeTrack.CaptureMouse();

                //Sets track's value
                volumeSlider.Value = volumeTrack.ValueFromPoint(e.GetPosition(volumeTrack));
            }
        }

        /// <summary>
        /// Releases mouse capture 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolumeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(volumeTrack!=null)
                volumeTrack.ReleaseMouseCapture();
        }

        #endregion

        #region POSITION SLIDER DRAG 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionSlider_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets track from slider template
            positionTrack = positionSlider.Template.FindName("PART_Track", positionSlider) as Track;

            if (e.LeftButton == MouseButtonState.Pressed && positionTrack != null)
            {
                //Captures mouse to prevent mouse movement overlaping
                positionTrack.CaptureMouse();

                //Sets track's value
                positionSlider.Value = positionTrack.ValueFromPoint(e.GetPosition(positionTrack));
            }
        }

        /// <summary>
        /// Releases mouse capture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (positionTrack != null)
                positionTrack.ReleaseMouseCapture();
        }

        #endregion
    }
}
