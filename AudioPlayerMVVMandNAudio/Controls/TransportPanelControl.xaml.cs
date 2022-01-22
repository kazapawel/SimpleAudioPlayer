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

        private void VolumeSlider_MouseMove(object sender, MouseEventArgs e) => SliderMouseMove(sender as Slider, e);

        private void VolumeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => SliderMouseLeftButtonUp(sender as Slider);

        private void PositionSlider_MouseMove(object sender, MouseEventArgs e) => SliderMouseMove(sender as Slider, e);

        private void PositionSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => SliderMouseLeftButtonUp(sender as Slider);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="slider"></param>
        /// <param name="e"></param>
        private void SliderMouseMove(Slider slider, MouseEventArgs e)
        {
            //Gets track from slider template
            var track = slider.Template.FindName("PART_Track", slider) as Track;

            if (e.LeftButton == MouseButtonState.Pressed && slider != null)
            {
                //Captures mouse to prevent mouse movement overlaping
                slider.CaptureMouse();

                //Sets slider's value based on mouse/track position
                slider.Value = track.ValueFromPoint(e.GetPosition(track));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slider"></param>
        private void SliderMouseLeftButtonUp(Slider slider)
        {
            if (slider != null)
                slider.ReleaseMouseCapture();
        }
    }
}
