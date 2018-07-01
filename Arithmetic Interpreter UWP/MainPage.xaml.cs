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

		private static MenuFlyout CreateMenuFlyout() {
			MenuFlyout menuFlyout = new MenuFlyout();
			MenuFlyoutItem mfi1 = new MenuFlyoutItem();
			MenuFlyoutItem mfi2 = new MenuFlyoutItem();
			MenuFlyoutItem mfi3 = new MenuFlyoutItem();
			mfi1.Text = "valensiya";
			mfi2.Text = "janyee";
			mfi3.Text = "jupiter";
			menuFlyout.Items.Add(mfi1);
			menuFlyout.Items.Add(mfi2);
			menuFlyout.Items.Add(mfi3);
			return menuFlyout;
		}

		private void SetFocusButton_Click(object sender, RoutedEventArgs e) {
			CodeEditor.Focus(FocusState.Programmatic);
		}

		private void CodeEditor_PointerPressed(object sender, PointerRoutedEventArgs e) {
			//System.Diagnostics.Debug.WriteLine("fired pointer presse.");
		}

		private void CodeEditor_PointerEntered(object sender, PointerRoutedEventArgs e) {
			//System.Diagnostics.Debug.WriteLine("fired pointer enter.");

		}

		private void CodeEditor_PointerReleased(object sender, PointerRoutedEventArgs e) {
			//System.Diagnostics.Debug.WriteLine("fired pointer release.");
		}

		private void Root_Loading(FrameworkElement sender, object args) {
			CodeEditor.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(CodeEditor_PointerPressed), true);
		}

		private void CodeEditor_KeyUp(object sender, KeyRoutedEventArgs e) {
			CodeEditor.Document.Selection.GetPoint(
				horizontalAlign: Windows.UI.Text.HorizontalCharacterAlignment.Left,
				verticalAlign: Windows.UI.Text.VerticalCharacterAlignment.Baseline,
				options: Windows.UI.Text.PointOptions.ClientCoordinates,
				point: out Point point
			);
			textBlock1.Text = point.ToString();
			MenuFlyout menuFlyout = CreateMenuFlyout();
			switch (e.Key) {
				case Windows.System.VirtualKey.Q:
					menuFlyout.ShowAt(null, new Point(point.X + 10, point.Y + 10));
					break;
				case Windows.System.VirtualKey.E:
					menuFlyout.ShowAt(null, new Point(point.X + 10, point.Y + 10));
					break;
				default:
					break;
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
