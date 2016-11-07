using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualCalculator2 {
	public partial class Calculator : Form {

		public Calculator( ) {
			InitializeComponent( );
		}

		private void btnZero_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "0";
		}

		private void btnOne_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "1";
		}

		private void btnTwo_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "2";
		}

		private void btnThree_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "3";
		}

		private void btnFour_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "4";
		}

		private void btnFive_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "5";
		}

		private void btnSix_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "6";
		}

		private void btnSeven_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "7";
		}

		private void btnEight_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "8";
		}

		private void btnNine_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text += "9";
		}

		private void btnDot_Click( object sender , EventArgs e ) {
			if (this.txtDisplay.Text.IndexOf( "." ) == -1) {
				this.txtDisplay.Text += ".";
			}
		}

		private void btnClean_Click( object sender , EventArgs e ) {
			this.txtDisplay.Text = this.btnZero.Text;
			this.btnDivision.BackColor = Color.LightGray;
			this.btnRemian.BackColor = Color.LightGray;
			this.btnMultiplication.BackColor = Color.LightGray;
			this.btnSubtraction.BackColor = Color.LightGray;
			this.btnAddition.BackColor = Color.LightGray;
		}

		private void btnDivision_Click( object sender , EventArgs e ) {
			this.btnDivision.BackColor = Color.DarkGray;
			this.btnRemian.BackColor = Color.LightGray;
			this.btnMultiplication.BackColor = Color.LightGray;
			this.btnSubtraction.BackColor = Color.LightGray;
			this.btnAddition.BackColor = Color.LightGray;
		}

		private void btnMultiplication_Click( object sender , EventArgs e ) {
			this.btnMultiplication.BackColor = Color.DarkGray;
			this.btnDivision.BackColor = Color.LightGray;
			this.btnRemian.BackColor = Color.LightGray;
			this.btnSubtraction.BackColor = Color.LightGray;
			this.btnAddition.BackColor = Color.LightGray;
		}

		private void btnSubtraction_Click( object sender , EventArgs e ) {
			this.btnSubtraction.BackColor = Color.DarkGray;
			this.btnMultiplication.BackColor = Color.LightGray;
			this.btnDivision.BackColor = Color.LightGray;
			this.btnRemian.BackColor = Color.LightGray;
			this.btnAddition.BackColor = Color.LightGray;
		}

		private void btnAddition_Click( object sender , EventArgs e ) {
			this.btnSubtraction.BackColor = Color.LightGray;
			this.btnMultiplication.BackColor = Color.LightGray;
			this.btnDivision.BackColor = Color.LightGray;
			this.btnRemian.BackColor = Color.LightGray;
			this.btnAddition.BackColor = Color.DarkGray;
		}

		private void btnRemian_Click( object sender , EventArgs e ) {
			this.btnDivision.BackColor = Color.LightGray;
			this.btnRemian.BackColor = Color.DarkGray;
			this.btnMultiplication.BackColor = Color.LightGray;
			this.btnSubtraction.BackColor = Color.LightGray;
			this.btnAddition.BackColor = Color.LightGray;
		}

		private void btnEqual_MouseDown( object sender , MouseEventArgs e ) {
			this.btnSubtraction.BackColor = Color.LightGray;
			this.btnMultiplication.BackColor = Color.LightGray;
			this.btnDivision.BackColor = Color.LightGray;
			this.btnRemian.BackColor = Color.LightGray;
			this.btnAddition.BackColor = Color.LightGray;
			this.btnEqual.BackColor = Color.DarkGray;
		}

		private void btnEqual_MouseUp( object sender , MouseEventArgs e ) {
			this.btnEqual.BackColor = Color.LightGray;
		}

		private void Calculator_Load( object sender , EventArgs e ) {
			this.txtDisplay.Text = this.btnZero.Text;
		}
	}
}
