﻿#pragma checksum "..\..\..\Views\Joystick.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "36DE461BFC330953F1F8039AC060C81DE38948D68D9CE7E7D4E32AD07BF2D4C2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AdvancedProgrammingProject1.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace AdvancedProgrammingProject1.Views {
    
    
    /// <summary>
    /// Joystick
    /// </summary>
    public partial class Joystick : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\Views\Joystick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AdvancedProgrammingProject1.Views.Joystick Joystick1;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Views\Joystick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Base;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Views\Joystick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Knob;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\Views\Joystick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.TranslateTransform knobPosition;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\Views\Joystick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse KnobBase;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AdvancedProgrammingProject1;component/views/joystick.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\Joystick.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Joystick1 = ((AdvancedProgrammingProject1.Views.Joystick)(target));
            return;
            case 2:
            this.Base = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.Knob = ((System.Windows.Controls.Canvas)(target));
            
            #line 62 "..\..\..\Views\Joystick.xaml"
            this.Knob.MouseMove += new System.Windows.Input.MouseEventHandler(this.Knob_MouseMove);
            
            #line default
            #line hidden
            
            #line 62 "..\..\..\Views\Joystick.xaml"
            this.Knob.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Knob_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 62 "..\..\..\Views\Joystick.xaml"
            this.Knob.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Knob_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 65 "..\..\..\Views\Joystick.xaml"
            ((System.Windows.Media.Animation.Storyboard)(target)).Completed += new System.EventHandler(this.CenterKnob_Completed);
            
            #line default
            #line hidden
            return;
            case 5:
            this.knobPosition = ((System.Windows.Media.TranslateTransform)(target));
            return;
            case 6:
            this.KnobBase = ((System.Windows.Shapes.Ellipse)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

