﻿#pragma checksum "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "817B93611FDEE1B5769C0DB203540F533B37FBA7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
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


namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms {
    
    
    /// <summary>
    /// AddProfessorForm
    /// </summary>
    public partial class AddProfessorForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseButton;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Gmail;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Age;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Address;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/STUDENT-VERIFICATION-SYSTEM-THIRD-YEAR-PROJECT;component/view/popupforms/addprof" +
                    "essorform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            this.CloseButton.Click += new System.Windows.RoutedEventHandler(this.CloseBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Name = ((System.Windows.Controls.TextBox)(target));
            
            #line 83 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            this.Name.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Name_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 84 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            this.Name.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ProfessorName_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Gmail = ((System.Windows.Controls.TextBox)(target));
            
            #line 112 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            this.Gmail.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Gmail_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 113 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            this.Gmail.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Gmail_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 114 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            this.Gmail.LostFocus += new System.Windows.RoutedEventHandler(this.Gmail_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Age = ((System.Windows.Controls.TextBox)(target));
            
            #line 143 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            this.Age.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Age_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 144 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            this.Age.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Age_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Address = ((System.Windows.Controls.TextBox)(target));
            
            #line 170 "..\..\..\..\..\View\PopUpForms\AddProfessorForm.xaml"
            this.Address.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Address_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

