using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Arithmetic_Interpreter_UWP {
	class ButtonStackCommonProperties : INotifyPropertyChanged {
		private double _width;
		private double _height;
		private SolidColorBrush _foreground;
		private double _fontsize;
		private Thickness _margin;

		public double Width {
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
				this.OnPropertyChanged();
			}
		}
		public double Height {
			get
			{
				return this._height;
			}
			set
			{
				this._height = value;
				OnPropertyChanged();
			}
		}
		public SolidColorBrush Foreground { 
			get
			{
				return this._foreground;
			}
			set
			{
				this._foreground = value;
				OnPropertyChanged();
			}
		}
		public double FontSize {
			get
			{
				return this._fontsize;
			}
			set
			{
				this._fontsize = value;
				OnPropertyChanged();
			}
		}
		public Thickness Margin {
			get
			{
				return this._margin;
			}
			set
			{
				this._margin = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
