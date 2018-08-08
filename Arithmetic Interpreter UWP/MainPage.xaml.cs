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
	using PointOptions = Windows.UI.Text.PointOptions;
	using Debug = System.Diagnostics.Debug;

	public sealed partial class MainPage : Page {
		private Window _window = Window.Current;

		public MainPage() {
			this.InitializeComponent();
			if (Resources["buttonStackCommonProperties"] is ButtonStackCommonProperties buttonStackCommonProperties) {
				buttonStackCommonProperties.Width = 100;
				buttonStackCommonProperties.Height = 50;
				buttonStackCommonProperties.Foreground = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
				buttonStackCommonProperties.FontSize = 18;
				buttonStackCommonProperties.Margin = new Thickness(0, 20, 0, 0);
			}
			SetTitleBarTheme();
		}

		private static void SetTitleBarTheme() {
			var titlebar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
			titlebar.BackgroundColor = (Application.Current.Resources["TitleBarDarkTheme"] as AcrylicBrush).TintColor;
			titlebar.ButtonBackgroundColor = (Application.Current.Resources["TitleBarDarkTheme"] as AcrylicBrush).TintColor;
		}

		private static MenuFlyout CreateMenuFlyout(List<string> list) {
			MenuFlyout menuFlyout = new MenuFlyout();
			foreach (var item in list) {
				var menuFlyoutItem = new MenuFlyoutItem();
				menuFlyoutItem.Text = item;
				menuFlyout.Items.Add(menuFlyoutItem);
			}
			return menuFlyout;
		}

		private void CodeEditor_KeyUp(object sender, KeyRoutedEventArgs e) {
			RichEditBox richEditBox = sender as RichEditBox;
			richEditBox.Document.Selection.ScrollIntoView(PointOptions.Start);
			richEditBox.Document.Selection.GetPoint(
				horizontalAlign: Windows.UI.Text.HorizontalCharacterAlignment.Left,
				verticalAlign: Windows.UI.Text.VerticalCharacterAlignment.Baseline,
				options: Windows.UI.Text.PointOptions.ClientCoordinates,
				point: out Point point
			);
			Debug.WriteLine($"point: {point.ToString()}");
			MenuFlyout menuFlyout = CreateMenuFlyout(Class1.IntellisenseList);
			switch (e.Key) {
				case Windows.System.VirtualKey.Q:
					this.ShowIntellisense(point.X + 10, point.Y + 10, (x, y) => {
						menuFlyout.ShowAt(richEditBox, new Point(x, y));
					}); 
					break;
				case Windows.System.VirtualKey.E:
					this.ShowIntellisense(point.X + 10, point.Y + 10, (x, y) => {
						menuFlyout.ShowAt(richEditBox, new Point(x, y));
					}); 
					break;
				default:
					break;
			}
		}

		private void ShowIntellisense(double x, double y, Action<double, double> action) {
			if (x < 0) {
				x = 0;
			}
			else if (x > this._window.Bounds.Width) {
				x = this._window.Bounds.Width;
			}
			else {
				if (x > CodeEditor.ActualWidth) {
					x = CodeEditor.ActualWidth;
				}
			}
			if (y < 0) {
				y = 0;
			}
			else if (y > this._window.Bounds.Height) {
				y = this._window.Bounds.Height;
			}
			else {
				if (y > CodeEditor.ActualHeight) {
					y = CodeEditor.ActualHeight;
				}
			}
			System.Diagnostics.Debug.WriteLine($"{x},{y}");
			try {
				action(x, y);
			}
			catch (ArgumentException argexp) {
				// Coordinates out of range
				System.Diagnostics.Debug.WriteLine(argexp.Message);
			}
		}

		private static void PutCursorFollowEndPositionBeforeSetText(RichEditBox editor) {
			editor.Document.Selection.StartPosition += 1;
			editor.Document.Selection.EndPosition += 1;
		}

		private static void PutCursorFollowEndPositionAfterSetText(RichEditBox editor, int wordLength) {
			editor.Document.Selection.StartPosition += wordLength;
			editor.Document.Selection.EndPosition += wordLength;
		}

		private static bool IsShiftKeyPressed() {
			var shiftState = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetKeyState(Windows.System.VirtualKey.Shift);
			return (shiftState & Windows.UI.Core.CoreVirtualKeyStates.Down) == Windows.UI.Core.CoreVirtualKeyStates.Down;
		}
	}
}
