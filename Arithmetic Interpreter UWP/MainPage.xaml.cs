using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Arithmetic_Interpreter_UWP {
	public sealed partial class MainPage : Page {
		public MainPage() {
			this.InitializeComponent();
			if (Resources["buttonStackCommonProperties"] is ButtonStackCommonProperties buttonStackCommonProperties) {
				buttonStackCommonProperties.Width = 100;
				buttonStackCommonProperties.Height = 50;
				buttonStackCommonProperties.Foreground = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
				buttonStackCommonProperties.FontSize = 18;
				buttonStackCommonProperties.Margin = new Thickness(0, 20, 0, 0);
			}
		}

		private void SetFocusButton_Click(object sender, RoutedEventArgs e) {
			CodeEditor.Focus(FocusState.Programmatic);
		}

		private void CodeEditor_PointerPressed(object sender, PointerRoutedEventArgs e) {
			System.Diagnostics.Debug.WriteLine("fired pointer presse.");
		}

		private void CodeEditor_PointerEntered(object sender, PointerRoutedEventArgs e) {
			System.Diagnostics.Debug.WriteLine("fired pointer enter.");

		}

		private void CodeEditor_PointerReleased(object sender, PointerRoutedEventArgs e) {
			System.Diagnostics.Debug.WriteLine("fired pointer release.");
		}

		private void Root_Loading(FrameworkElement sender, object args) {
			CodeEditor.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(CodeEditor_PointerPressed), true);
		}
	}
}
