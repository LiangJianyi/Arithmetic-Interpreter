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
				if (this.CheckLawlessness( this.txtInput.Text )) {
					if (this.CheckSynax( this.txtInput.Text )) {
						string[ ] tokens = this.Parse( this.txtInput.Text );
						this.lblResult.Text = Evaluator.Calculating( tokens );
					}
					else {
						MessageBox.Show( "运算符之间要包含数字或有效的表达式，重新输入" , "错误" , MessageBoxButtons.OK , MessageBoxIcon.Error );
					}
				}
				else {
					MessageBox.Show( "表达式含有非法字符，重新输入" , "错误" , MessageBoxButtons.OK , MessageBoxIcon.Error );
				}
			}
		}

		private bool CheckLawlessness( string express ) {
			Predicate<int> containLegalCharacter = ( index ) => {
				if (express.Substring( index , 1 ).Equals( "0" ) ||
					express.Substring( index , 1 ).Equals( "1" ) ||
					express.Substring( index , 1 ).Equals( "2" ) ||
					express.Substring( index , 1 ).Equals( "3" ) ||
					express.Substring( index , 1 ).Equals( "4" ) ||
					express.Substring( index , 1 ).Equals( "5" ) ||
					express.Substring( index , 1 ).Equals( "6" ) ||
					express.Substring( index , 1 ).Equals( "7" ) ||
					express.Substring( index , 1 ).Equals( "8" ) ||
					express.Substring( index , 1 ).Equals( "9" )) {
					return true;
				}
				else {
					if (express.Substring( index , 1 ).Equals( "+" ) ||
						express.Substring( index , 1 ).Equals( "-" ) ||
						express.Substring( index , 1 ).Equals( "*" ) ||
						express.Substring( index , 1 ).Equals( "/" ) ||
						express.Substring( index , 1 ).Equals( "%" )) {
						return true;
					}
					else {
						return false;
					}
				}
			};

			const bool LAWLESSNESS_CHARACTER = false;
			const bool LEGAL_CHARACTER = true;
			bool result = true;
			for (int index = 0 ; index < express.Length ; index++) {
				if (containLegalCharacter( index )) {
					result = LEGAL_CHARACTER;
				}
				else {
					if (containLegalCharacter( index )) {
						result = LEGAL_CHARACTER;
					}
					else {
						result = LAWLESSNESS_CHARACTER;
						break;
					}
				}
			}

			return result;
		}

		private bool CheckSynax( string express ) {
			Predicate<int> isNumberCharacter = ( index ) => {
				if (express.Substring( index , 1 ).Equals( "0" ) ||
					express.Substring( index , 1 ).Equals( "1" ) ||
					express.Substring( index , 1 ).Equals( "2" ) ||
					express.Substring( index , 1 ).Equals( "3" ) ||
					express.Substring( index , 1 ).Equals( "4" ) ||
					express.Substring( index , 1 ).Equals( "5" ) ||
					express.Substring( index , 1 ).Equals( "6" ) ||
					express.Substring( index , 1 ).Equals( "7" ) ||
					express.Substring( index , 1 ).Equals( "8" ) ||
					express.Substring( index , 1 ).Equals( "9" )) {
					return true;
				}
				else {
					return false;
				}
			};
			Predicate<int> isOperatorCharacter = ( index ) => {
				if (express.Substring( index , 1 ).Equals( "+" ) ||
					express.Substring( index , 1 ).Equals( "-" ) ||
					express.Substring( index , 1 ).Equals( "*" ) ||
					express.Substring( index , 1 ).Equals( "/" ) ||
					express.Substring( index , 1 ).Equals( "%" )) {
					return true;
				}
				else {
					return false;
				}
			};

			bool lastCharacterIsNumber = true;
			if (isNumberCharacter( 0 ) && isNumberCharacter( express.Length - 1 )) {
				for (int index = 1 ; index < express.Length - 1 ; index++) {
					if (isNumberCharacter( index )) {
						if (lastCharacterIsNumber) {
							continue;
						}
						else {
							lastCharacterIsNumber = !lastCharacterIsNumber;
						}
					}
					else if (isOperatorCharacter( index )) {
						if (lastCharacterIsNumber) {
							lastCharacterIsNumber = !lastCharacterIsNumber;
							continue;
						}
						else {
							return false;
						}
					}
				}
			}
			else {
				return false;
			}

			return true;
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
