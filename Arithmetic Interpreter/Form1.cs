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
			string[ ] tokens = this.Parse( this.txtInput.Text );
			this.lblResult.Text = Evaluator.Calculating( tokens );
			
		}

		private string[ ] Parse( string express ) {
			string[ ] numbers = express.Split( '+' , '-' , '*' , '/' , '%' );

			#region 抽取运算符
			List<string> ops = new List<string>( );
			for (int index = 0 ; index < express.Length ; index++) {
				switch (express.Substring( index , 1 )) {
					case "+":
						ops.Add( express.Substring( index , 1 ) );
						break;
					case "-":
						ops.Add( express.Substring( index , 1 ) );
						break;
					case "*":
						ops.Add( express.Substring( index , 1 ) );
						break;
					case "/":
						ops.Add( express.Substring( index , 1 ) );
						break;
					case "%":
						ops.Add( express.Substring( index , 1 ) );
						break;
				}
			}
			string[ ] operators = ops.ToArray<string>( );
			#endregion

			#region 组装成 Token
			List<string> lib = new List<string>( );
			for (int numIndex = 0, opsIndex = 0 ; numIndex < numbers.Length ; numIndex++) {
				lib.Add( numbers[ numIndex ] );
				if (opsIndex < operators.Length) {
					lib.Add( operators[ opsIndex ] );
					opsIndex++;
				}
				else {
					continue;
				}
			}
			string[ ] tokens = lib.ToArray<string>( );
			#endregion

			return tokens;
		}
	}
}
