﻿#pragma checksum "..\..\..\..\Controls\TransportPanelControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AFE1D592F9B37FCA8F225E03E57C47F6EC1B5922"
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
    public partial class TransportPanelControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 55 "..\..\..\..\Controls\TransportPanelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button playbutton;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Controls\TransportPanelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pausebutton;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\..\Controls\TransportPanelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider PositionSlider;
        
        #line default
        #line hidden
        
        
        #line 193 "..\..\..\..\Controls\TransportPanelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup TimeValuePopup;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AudioPlayerMVVMandNAudio;component/controls/transportpanelcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Controls\TransportPanelControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.playbutton = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.pausebutton = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.PositionSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 188 "..\..\..\..\Controls\TransportPanelControl.xaml"
            this.PositionSlider.AddHandler(System.Windows.Controls.Primitives.Thumb.DragStartedEvent, new System.Windows.Controls.Primitives.DragStartedEventHandler(this.PositionSlider_DragStarted));
            
            #line default
            #line hidden
            
            #line 189 "..\..\..\..\Controls\TransportPanelControl.xaml"
            this.PositionSlider.MouseMove += new System.Windows.Input.MouseEventHandler(this.PositionSlider_MouseMove);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TimeValuePopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

