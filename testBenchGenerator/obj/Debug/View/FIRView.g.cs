﻿#pragma checksum "..\..\..\View\FIRView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "147C5B886B18DDA84914987E3D5AAF19C26504BA84DB4122AEED3DE188A4E62D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using OxyPlot.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using testBenchGenerator.View;


namespace testBenchGenerator.View {
    
    
    /// <summary>
    /// FIRView
    /// </summary>
    public partial class FIRView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal OxyPlot.Wpf.Plot firResp;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal OxyPlot.Wpf.Plot firChart;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal OxyPlot.Wpf.Plot winResp;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal OxyPlot.Wpf.Plot winChart;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox fs;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox len;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ss;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lf;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox hf;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox wt;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ft;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox pt;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox coeffs;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button design;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\..\View\FIRView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button export;
        
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
            System.Uri resourceLocater = new System.Uri("/testBenchGenerator;component/view/firview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\FIRView.xaml"
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
            this.firResp = ((OxyPlot.Wpf.Plot)(target));
            return;
            case 2:
            this.firChart = ((OxyPlot.Wpf.Plot)(target));
            return;
            case 3:
            this.winResp = ((OxyPlot.Wpf.Plot)(target));
            return;
            case 4:
            this.winChart = ((OxyPlot.Wpf.Plot)(target));
            return;
            case 5:
            this.fs = ((System.Windows.Controls.TextBox)(target));
            
            #line 100 "..\..\..\View\FIRView.xaml"
            this.fs.GotFocus += new System.Windows.RoutedEventHandler(this.fs_GotFocus);
            
            #line default
            #line hidden
            
            #line 100 "..\..\..\View\FIRView.xaml"
            this.fs.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.fs_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 6:
            this.len = ((System.Windows.Controls.TextBox)(target));
            
            #line 101 "..\..\..\View\FIRView.xaml"
            this.len.GotFocus += new System.Windows.RoutedEventHandler(this.len_GotFocus);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\View\FIRView.xaml"
            this.len.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.len_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ss = ((System.Windows.Controls.TextBox)(target));
            
            #line 102 "..\..\..\View\FIRView.xaml"
            this.ss.GotFocus += new System.Windows.RoutedEventHandler(this.ss_GotFocus);
            
            #line default
            #line hidden
            
            #line 102 "..\..\..\View\FIRView.xaml"
            this.ss.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ss_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lf = ((System.Windows.Controls.TextBox)(target));
            
            #line 103 "..\..\..\View\FIRView.xaml"
            this.lf.GotFocus += new System.Windows.RoutedEventHandler(this.lf_GotFocus);
            
            #line default
            #line hidden
            
            #line 103 "..\..\..\View\FIRView.xaml"
            this.lf.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.lf_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 9:
            this.hf = ((System.Windows.Controls.TextBox)(target));
            
            #line 104 "..\..\..\View\FIRView.xaml"
            this.hf.GotFocus += new System.Windows.RoutedEventHandler(this.hf_GotFocus);
            
            #line default
            #line hidden
            
            #line 104 "..\..\..\View\FIRView.xaml"
            this.hf.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.hf_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 10:
            this.wt = ((System.Windows.Controls.ComboBox)(target));
            
            #line 105 "..\..\..\View\FIRView.xaml"
            this.wt.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.wt_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ft = ((System.Windows.Controls.ComboBox)(target));
            
            #line 118 "..\..\..\View\FIRView.xaml"
            this.ft.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ft_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.pt = ((System.Windows.Controls.ComboBox)(target));
            
            #line 124 "..\..\..\View\FIRView.xaml"
            this.pt.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.pt_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            this.coeffs = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            this.design = ((System.Windows.Controls.Button)(target));
            
            #line 145 "..\..\..\View\FIRView.xaml"
            this.design.Click += new System.Windows.RoutedEventHandler(this.design_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.export = ((System.Windows.Controls.Button)(target));
            
            #line 146 "..\..\..\View\FIRView.xaml"
            this.export.Click += new System.Windows.RoutedEventHandler(this.export_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
