using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for VolumeSliderControl.xaml
    /// </summary>
    public partial class VolumeSliderControl : UserControl
    {
        public VolumeSliderControl()
        {
            InitializeComponent();
        }

        #region SLIDERS 

        private void VolumeSlider_MouseMove(object sender, MouseEventArgs e) => SliderMouseMove(sender as Slider, e);

        private void VolumeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => SliderMouseLeftButtonUp(sender as Slider);

        //private void PositionSlider_MouseMove(object sender, MouseEventArgs e) => SliderMouseMove(sender as Slider, e);

        //private void PositionSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    var slider = sender as Slider;
        //    SliderMouseLeftButtonUp(slider);
        //    //SliderValueAfterDrag = slider.Value;
        //    //slider.GetBindingExpression(Slider.ValueProperty).UpdateSource();
        //    //var expression = BindingOperations.GetBindingExpression(PositionSlider, Slider.ValueProperty);
        //}

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

        #endregion

        public void OnLeftArrowPressed(object sender, EventArgs e)
        {
            if(volumeSlider.Value>0)
                volumeSlider.Value--;
        }

        public void OnRightArrowPressed(object sender, EventArgs e)
        {
            if(volumeSlider.Value<100)
                volumeSlider.Value++;
        }
    }
}
