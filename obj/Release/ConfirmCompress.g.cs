﻿#pragma checksum "..\..\ConfirmCompress.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "407C9B510FC370711AD3BB0DEFAAF99DA97FA6848DB59F030CD23690931A5214"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project_Encode;
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


namespace Project_Encode {
    
    
    /// <summary>
    /// ConfirmCompress
    /// </summary>
    public partial class ConfirmCompress : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\ConfirmCompress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOK;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\ConfirmCompress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\ConfirmCompress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRleCompress;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\ConfirmCompress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDecompress;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\ConfirmCompress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtLzwCompress;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ConfirmCompress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton togNONE;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ConfirmCompress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton togLZW;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\ConfirmCompress.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton togRLE;
        
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
            System.Uri resourceLocater = new System.Uri("/Project Encode;component/confirmcompress.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ConfirmCompress.xaml"
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
            this.btnOK = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\ConfirmCompress.xaml"
            this.btnOK.Click += new System.Windows.RoutedEventHandler(this.BtnOK_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\ConfirmCompress.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.BtnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtRleCompress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtDecompress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtLzwCompress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.togNONE = ((System.Windows.Controls.RadioButton)(target));
            
            #line 19 "..\..\ConfirmCompress.xaml"
            this.togNONE.Checked += new System.Windows.RoutedEventHandler(this.TogNONE_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.togLZW = ((System.Windows.Controls.RadioButton)(target));
            
            #line 20 "..\..\ConfirmCompress.xaml"
            this.togLZW.Checked += new System.Windows.RoutedEventHandler(this.TogLZW_Checked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.togRLE = ((System.Windows.Controls.RadioButton)(target));
            
            #line 21 "..\..\ConfirmCompress.xaml"
            this.togRLE.Checked += new System.Windows.RoutedEventHandler(this.TogRLE_Checked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

