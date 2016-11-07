using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualCalculator {
	public partial class Standard : Form {

		StringBuilder DisplayCharacter = new StringBuilder( );

		public Standard( ) {
			InitializeComponent( );
		}

		private void Standard_Load( object sender , EventArgs e ) {

		}

		private void btnZero_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "0" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnOne_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "1" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnTwo_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "2" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnThree_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "3" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnFour_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "4" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnFive_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "5" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnSix_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "6" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnSeven_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "7" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnEight_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "8" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnNine_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "9" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnDot_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "." );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnClean_Click( object sender , EventArgs e ) {
			DisplayCharacter.Remove( 0 , DisplayCharacter.Length );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnDelete_Click( object sender , EventArgs e ) {
			DisplayCharacter.Remove( DisplayCharacter.Length - 1 , 1 );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnAddition_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "+" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnSubtraction_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "-" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnMultiplication_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "*" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnDivision_Click( object sender , EventArgs e ) {
			DisplayCharacter.Append( "/" );
			txtDisplay.Text = DisplayCharacter.ToString( );
		}

		private void btnEqual_Click( object sender , EventArgs e ) {
			txtDisplay.Text = Arithmetic.ExecuteCalculate( DisplayCharacter.ToString( ) ).ToString( );
		}
	}
}
