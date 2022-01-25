﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Interaction logic for TransportPanelControl.xaml
    /// </summary>
    public partial class TransportPanelControl : UserControl
    {
        #region DEPENDENCY PROPERTIES



        public double SliderValueAfterDrag
        {
            get { return (double)GetValue(SliderValueAfterDragProperty); }
            set { SetValue(SliderValueAfterDragProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SliderValueAfterDrag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderValueAfterDragProperty =
            DependencyProperty.Register("SliderValueAfterDrag", typeof(double), typeof(TransportPanelControl), new FrameworkPropertyMetadata(null));



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
        }

        private void VolumeSlider_MouseMove(object sender, MouseEventArgs e) => SliderMouseMove(sender as Slider, e);

        private void VolumeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => SliderMouseLeftButtonUp(sender as Slider);

        private void PositionSlider_MouseMove(object sender, MouseEventArgs e) => SliderMouseMove(sender as Slider, e);

        private void PositionSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) 
        {
            var slider = sender as Slider;
            SliderMouseLeftButtonUp(slider);
            //SliderValueAfterDrag = slider.Value;
            //slider.GetBindingExpression(Slider.ValueProperty).UpdateSource();
            //var expression = BindingOperations.GetBindingExpression(PositionSlider, Slider.ValueProperty);
        }

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
