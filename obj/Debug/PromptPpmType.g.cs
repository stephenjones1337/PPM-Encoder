﻿#pragma checksum "..\..\PromptPpmType.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3F5C8AE971FB868285F71E4104FF6E593DBBEF236F8B9A7707B606FEEDBD7950"
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
    /// PromptPpmType
    /// </summary>
    public partial class PromptPpmType : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\PromptPpmType.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton togAscii;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\PromptPpmType.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton togRaw;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\PromptPpmType.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSubmit;
        
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
            System.Uri resourceLocater = new System.Uri("/Project Encode;component/promptppmtype.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PromptPpmType.xaml"
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
            this.togAscii = ((System.Windows.Controls.RadioButton)(target));
            
            #line 12 "..\..\PromptPpmType.xaml"
            this.togAscii.Checked += new System.Windows.RoutedEventHandler(this.TogAscii_Checked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.togRaw = ((System.Windows.Controls.RadioButton)(target));
            
            #line 13 "..\..\PromptPpmType.xaml"
            this.togRaw.Checked += new System.Windows.RoutedEventHandler(this.TogRaw_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnSubmit = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\PromptPpmType.xaml"
            this.btnSubmit.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

