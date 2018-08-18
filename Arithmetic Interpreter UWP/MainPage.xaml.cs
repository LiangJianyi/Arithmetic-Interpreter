using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Arithmetic_Interpreter_UWP {
	using PointOptions = Windows.UI.Text.PointOptions;
	using Debug = System.Diagnostics.Debug;

	public sealed partial class MainPage : Page {
		private Window _window = Window.Current;
		/// <summary>
		/// 指示 ConsoleWindowSpring() 的行为
		/// </summary>
		private bool _consoleState = false;

		public MainPage() {
			this.InitializeComponent();

			/*
			 * 为了让用户单击面板类控件实现关闭 Console，需要捕获所有从非 Button 控件路由上来的 PointerPressed 事件，
			 * 这样用户单击非运行按钮和 Console 以外的控件时实现关闭 Console。但为了避免运行按钮的 PointerPressed
			 * 处理程序结束后也路由到其它能够关闭 Console 的事件处理程序，因此需要在负责打开 Console 的事件处理程序中
			 * 执行 e.Handler=true 并只对 RunAppBarButton 的 AddHandler 的 handledEventsToo 参数传递 true，
			 * 这样当打开 Console 时所触发的事件处理程序会立即标记完成并停止向上路由，从而避免了 Console 一打开就立即关闭的现象。
			 */
			RunAppBarButton.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(AppBarRunButton_PointerPressed), true);
			CommandBar.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(this.CloseConsole_PointerPressed), false);
			Root.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(this.CloseConsole_PointerPressed), false);

			if (Resources["buttonStackCommonProperties"] is ButtonStackCommonProperties buttonStackCommonProperties) {
				buttonStackCommonProperties.Width = 100;
				buttonStackCommonProperties.Height = 50;
				buttonStackCommonProperties.Foreground = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
				buttonStackCommonProperties.FontSize = 18;
				buttonStackCommonProperties.Margin = new Thickness(0, 20, 0, 0);
			}

			SetTitleBarTheme();
		}

		private void CloseConsole_PointerPressed(object sender, RoutedEventArgs e) {
			if (this._consoleState) {
				this._consoleState = !this._consoleState;
			}
			this.ConsoleWindowSpring();
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
			MenuFlyout menuFlyout = CreateMenuFlyout(new List<string>() { "Google", "Apple", "Microsoft" });
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
				Debug.WriteLine(argexp.Message);
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

		private void AppBarRunButton_PointerPressed(object sender, PointerRoutedEventArgs e) {
			this._consoleState = true;
			this.ConsoleWindowSpring();
			e.Handled = true;

			CodeEditor.Document.GetText(Windows.UI.Text.TextGetOptions.UseObjectText, out string result);
			if (Console.Blocks.Count > 0) {
				Console.Blocks.Clear();
			}
			LinkedList<string> tokens = Tokenizer.GetTokens(result);
			foreach (var token in tokens) {
				Paragraph paragraph = new Paragraph();
				paragraph.Inlines.Add(new Run() { Text = token });
				Debug.WriteLine($"{token}");
				Console.Blocks.Add(paragraph);
			}
			Debug.WriteLine("\n");
			Debug.WriteLine(Console.Blocks.Count);
		}

		/// <summary>
		/// 负责 Console 的开关，当字段 _consoleState 为 true 时打开 Console，否则关闭 Console
		/// </summary>
		private void ConsoleWindowSpring() {
			if (this._consoleState) {
				if (Root.RowDefinitions[2].Height.Value <= 0) {
					Root.RowDefinitions[2].Height = new GridLength(this._window.Bounds.Height * 0.2);
					Console.Width = this._window.Bounds.Width;
					Console.Height = this._window.Bounds.Height * 0.2;
				}
			}
			else {
				Root.RowDefinitions[2].Height = new GridLength(0);
				Console.Width = 0;
				Console.Height = 0;
			}
		}
	}

	public static class Tokenizer {
		public static LinkedList<string> GetTokens(string text) {
			string[] tokens = text.Split(new char[] { ' ', '(', ')', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
			LinkedList<string> result = new LinkedList<string>();
			foreach (var token in tokens) {
				result.AddLast(token);
			}
			return result;
		}
	}
}
