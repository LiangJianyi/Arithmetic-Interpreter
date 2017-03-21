using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Arithmetic_Interpreter {
	public partial class Form1 : Form {
		public Form1( ) {
			InitializeComponent( );
			this.lblResult.Text = string.Empty;
		}

		private void btnCalculating_Click( object sender , EventArgs e ) {
			if (this.txtInput.Text.Equals( string.Empty )) {
				MessageBox.Show( "表达式不能为空，重新输入" , "警告" , MessageBoxButtons.OK , MessageBoxIcon.Warning );
			}
			else {
				try {
					List<string> token = Tokenizer.Tokenization( this.txtInput.Text );
					foreach (var item in token) {
						this.lblResult.Text += item;
					}
				}
				catch (InvalidOperationException ex) {
					MessageBox.Show( ex.Message , "错误" , MessageBoxButtons.OK , MessageBoxIcon.Error );
				}
			}
		}

		private void Form1_Load( object sender , EventArgs e ) {

		}
	}
}
