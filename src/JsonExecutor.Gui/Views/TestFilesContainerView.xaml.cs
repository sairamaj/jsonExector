﻿using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Util.Core.Views;

namespace JsonExecutor.Gui.Views
{
    /// <summary>
    /// Interaction logic for TestFilesContainerView.xaml
    /// </summary>
    public partial class TestFilesContainerView : UserControl
    {
        public TestFilesContainerView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Task selection change event.
        /// </summary>
        public event EventHandler<CommandChangeEventArgs> SelectionChangedEvent;

        /// <summary>
        /// Command selected item changed method.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectionChangedEvent?.Invoke(this, new CommandChangeEventArgs(e.NewValue));
        }

    }
}
