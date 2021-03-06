#pragma checksum "..\..\..\..\Controls\AudioPlayer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "67179A3573A3E2DC5D5D1FBBCBB3A094AF603D3F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AudioPlayerMVVMandNAudio;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AudioPlayerMVVMandNAudio {
    
    
    /// <summary>
    /// TransportPanelControl
    /// </summary>
    public partial class AudioPlayerControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\..\..\Controls\AudioPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider VolumeSlider;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\Controls\AudioPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider PositionSlider;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\..\Controls\AudioPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup TimeValuePopup;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AudioPlayerMVVMandNAudio;V1.0.0.0;component/controls/audioplayer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Controls\AudioPlayer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.VolumeSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 58 "..\..\..\..\Controls\AudioPlayer.xaml"
            this.VolumeSlider.MouseMove += new System.Windows.Input.MouseEventHandler(this.VolumeSlider_MouseMove);
            
            #line default
            #line hidden
            
            #line 59 "..\..\..\..\Controls\AudioPlayer.xaml"
            this.VolumeSlider.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.VolumeSlider_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PositionSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 110 "..\..\..\..\Controls\AudioPlayer.xaml"
            this.PositionSlider.MouseMove += new System.Windows.Input.MouseEventHandler(this.PositionSlider_MouseMove);
            
            #line default
            #line hidden
            
            #line 111 "..\..\..\..\Controls\AudioPlayer.xaml"
            this.PositionSlider.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.PositionSlider_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TimeValuePopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

