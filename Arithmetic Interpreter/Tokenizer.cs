using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arithmetic_Interpreter {
	internal static class Tokenizer {
		private class FSMachine {
			private FSMachine _next;

			public char Key { get; set; }
			public Func<int , string , List<string> , dynamic> Function { get; set; }

			public Func<int , string , List<string> , dynamic> this[ char key ]
			{
				get
				{
					FSMachine current = this._next;
					while (current != null) {
						if (key == current.Key) {
							return current.Function;
						}
						else {
							current = current._next;
						}
					}
					return null;
				}
			}

			public FSMachine( ) {
				FSMachine.Initial( this );
			}

			private FSMachine( char key , Func<int , string , List<string> , dynamic> function ) {
				this.Key = key;
				this.Function = function;
			}

			private void Add( char key , Func<int , string , List<string> , dynamic> function ) {
				FSMachine current = new FSMachine( key , function );
				if (this._next == null) {
					this._next = current;
				}
				else {
					current._next = this._next;
					this._next = current;
				}
			}


			private static void Initial( FSMachine fsm ) {
				Predicate<char> isNumber = ch => ch == '0' || ch == '1' || ch == '2' || ch == '3' || ch == '4' || ch == '5' || ch == '6' || ch == '7' || ch == '8' || ch == '9';
				Predicate<char> isOperator = ch => ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '%';
				/*
				 * 判断以 express[ index ] 为首的关键字是否为 sqrt，并返回一个匿名类型表示的复合状态；
				 * 此复合状态封装三个属性，其中：
				 * index 表示关键字所在的字符串的下一个字符索引；
				 * state 表示关键字是否为 “sqrt”，如果是为“true”，否则为“false”；
				 * word 表示返回当前检查的关键字；
				 */
				Func<int , string , dynamic> isSqrt = ( index , express ) => {
					string word = String.Empty;
					Action next = ( ) => {
						word += express[ index ];
						index++;
					};
					if (express[ index ] == 's' || express[ index ] == 'S') {
						next( );
						if (express[ index ] == 'q' || express[ index ] == 'Q') {
							next( );
							if (express[ index ] == 'r' || express[ index ] == 'R') {
								next( );
								if (express[ index ] == 't' || express[ index ] == 'T') {
									next( );
									return new { index = index , state = true , word = word };
								}
								else {
									return new { index = index , state = false , word = word };
								}
							}
							else {
								return new { index = index , state = false , word = word };
							}
						}
						else {
							return new { index = index , state = false , word = word };
						}
					}
					else {
						return new { index = index , state = false , word = word };
					}

				};
				/*
				 * 判断以 express[ index ] 为首的关键字是否为 PI，并返回一个匿名类型表示的复合状态；
				 * 此复合状态封装三个属性，其中：
				 * index 表示关键字所在的字符串的下一个字符索引；
				 * state 表示关键字是否为 “PI”，如果是为“true”，否则为“false”；
				 * word 表示返回当前检查的关键字；
				 */
				Func<int , string , dynamic> isPi = ( index , express ) => {
					string word = String.Empty;
					Action next = ( ) => {
						word += express[ index ];
						index++;
					};
					if (express[ index ] == 'p' || express[ index ] == 'P') {
						next( );
						if (express[ index ] == 'i' || express[ index ] == 'I') {
							next( );
							return new { index = index , state = true , word = word };
						}
						else {
							return new { index = index , state = false , word = word };
						}
					}
					else {
						return new { index = index , state = false , word = word };
					}
				};
				Func<int , string , List<string> , dynamic> leftRacketOfNext = ( index , express , token ) => {
					if (index < express.Length - 1) {
						int next = index + 1;
						if (isNumber( express[ next ] ) || express[ next ] == '-' || isSqrt( next , express ).state || express[ next ] == '(') {
							token.Add( express[ index ].ToString( ) );
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							if (express[ next ] == ')') {
								throw new InvalidOperationException( "圆括号之间的内容不能为空" );
							}
							else {
								throw new InvalidOperationException( "左圆括号右边只能是数字、负号、sqrt、左圆括号" );
							}
						}
					}
					else {
						throw new InvalidOperationException( "左圆括号没有匹配的右圆括号。" );
					}
				};
				Func<int , string , List<string> , dynamic> numberOfNext = ( index , express , token ) => {
					if (index < express.Length - 1) {
						int next = index + 1;
						if (express[ next ] == ')' || express[ next ] == '^' || express[ next ] == '.' || isOperator( express[ next ] ) || express[ next ] == '!') {
							return new { index = index , state = true , word = express[ index ] };
						}
						else if (isNumber( express[ next ] )) {
							string word = express[ index ].ToString( ) + express[ next ].ToString( );
							next++;
							while (isNumber( express[ next ] )) {
								word += express[ next ];
								next++;
							}
							return new { index = next - 1 , state = true , word = word };
						}
						else {
							throw new InvalidOperationException( "数字右边只能是\")\", \"^\", \"!\", 小数点, 算术运算符" );
						}
					}
					else {
						return new { index = index , state = true , word = express[ index ] };    // 数字可以作为最后一个字符
					}
				};
				Func<int , string , List<string> , dynamic> plusOfNext = ( index , express , token ) => {
					if (index < express.Length - 1) {
						int next = index + 1;
						if (isNumber( express[ next ] ) || express[ next ] == '+' || express[ next ] == '-' || isSqrt( index , express ).state || express[ next ] == '(' || isPi( index , express ).state) {
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							throw new InvalidOperationException( "算术运算符的右边只能是加号、减号、数字、sqrt、左圆括号" );
						}
					}
					else {
						throw new InvalidOperationException( "算术运算符不能用作表达式的结尾。" );
					}
				};
				Func<int , string , List<string> , dynamic> minusOfNext = ( index , express , token ) => {
					if (index < express.Length - 1) {
						int next = index + 1;
						if (isNumber( express[ next ] ) || express[ next ] == '+' || express[ next ] == '-' || isSqrt( index , express ).state || express[ next ] == '(' || isPi( index , express ).state) {
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							throw new InvalidOperationException( "算术运算符的右边只能是加号、减号、数字、sqrt、左圆括号" );
						}
					}
					else {
						throw new InvalidOperationException( "算术运算符不能用作表达式的结尾。" );
					}
				};
				Func<int , string , List<string> , dynamic> decimalpointOfNext = ( index , express , token ) => {
					if (index < express.Length - 1) {
						int next = index + 1;
						if (isNumber( express[ next ] )) {
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							throw new InvalidOperationException( "小数点右边只能是数字。" );
						}
					}
					else {
						throw new InvalidOperationException( "小数点不能用作表达式的结尾。" );
					}
				};
				Func<int , string , List<string> , dynamic> piOfNext = ( index , express , token ) => {
					if (index < express.Length - 1) {
						int next = index + 1;
						if (express[ next ] == '(') {
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							throw new InvalidOperationException( "PI 是函数表达式，必须遵循\"PI( )\"的形式。" );
						}
					}
					else {
						throw new InvalidOperationException( "PI 是函数表达式，必须遵循\"PI( )\"的形式。" );
					}
				};
				Func<int , string , List<string> , dynamic> multiOfNext = ( index , express , token ) => {
					if (index < express.Length) {
						int next = index + 1;
						if (isNumber( express[ next ] ) || isSqrt( index , express ).state || express[ next ] == '+' || express[ next ] == '-' || isPi( index , express ).state) {
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							throw new InvalidOperationException( "乘号右边只能是数字, \"sqrt\", 加号, 减号, \"PI\"" );
						}
					}
					else {
						throw new InvalidOperationException( "乘号不能作为表达式的结尾。" );
					}
				};
				Func<int , string , List<string> , dynamic> divOfNext = ( index , express , token ) => {
					if (index < express.Length) {
						int next = index + 1;
						if (isNumber( express[ next ] ) || isSqrt( next , express ).state || express[ next ] == '+' || express[ next ] == '-' || isPi( next , express ).state) {
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							throw new InvalidOperationException( "除号右边只能是数字, \"sqrt\", 加号, 减号, \"PI\"" );
						}
					}
					else {
						throw new InvalidOperationException( "除号不能作为表达式的结尾。" );
					}
				};
				Func<int , string , List<string> , dynamic> remaOfNext = ( index , express , token ) => {
					if (index < express.Length) {
						int next = index + 1;
						if (isNumber( express[ next ] ) || isSqrt( next , express ).state || express[ next ] == '+' || express[ next ] == '-' || isPi( next , express ).state) {
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							throw new InvalidOperationException( "取余号右边只能是数字, \"sqrt\", 加号, 减号, \"PI\"" );
						}
					}
					else {
						throw new InvalidOperationException( "取余号不能作为表达式的结尾。" );
					}
				};
				Func<int , string , List<string> , dynamic> sqrtOfNext = ( index , express , token ) => {
					if (index < express.Length - 1) {
						int next = index + 1;
						if (express[ next ] == '(') {
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							throw new InvalidOperationException( "sqrt 是函数表达式，必须遵循\"sqrt( )\"的形式。" );
						}
					}
					else {
						throw new InvalidOperationException( "sqrt 是函数表达式，必须遵循\"sqrt( )\"的形式。" );
					}
				};
				Func<int , string , List<string> , dynamic> capOfNext = ( index , express , token ) => {
					if (index < express.Length) {
						int next = index + 1;
						if (isNumber( express[ next ] )) {
							return new { index = index , state = true , word = express[ index ] };
						}
						else {
							throw new InvalidOperationException( "\"^\"右边只能是数字, \"sqrt\", 加号, 减号, \"PI\"" );
						}
					}
					else {
						throw new InvalidOperationException( "\"^\"不能作为表达式的结尾。" );
					}
				};

				fsm.Add( '(' , leftRacketOfNext );
				fsm.Add( '0' , numberOfNext );
				fsm.Add( '1' , numberOfNext );
				fsm.Add( '2' , numberOfNext );
				fsm.Add( '3' , numberOfNext );
				fsm.Add( '4' , numberOfNext );
				fsm.Add( '5' , numberOfNext );
				fsm.Add( '6' , numberOfNext );
				fsm.Add( '7' , numberOfNext );
				fsm.Add( '8' , numberOfNext );
				fsm.Add( '9' , numberOfNext );
				fsm.Add( '+' , plusOfNext );
				fsm.Add( '-' , minusOfNext );
				fsm.Add( '.' , decimalpointOfNext );
				fsm.Add( 'p' , piOfNext );
				fsm.Add( '*' , multiOfNext );
				fsm.Add( '/' , divOfNext );
				fsm.Add( '%' , remaOfNext );
				fsm.Add( 's' , sqrtOfNext );
				fsm.Add( '^' , capOfNext );
			}
		}

		internal static List<string> Tokenization( string express ) {
			List<string> token = new List<string>( );
			FSMachine fsm = new FSMachine( );
			for (int index = 0 ; index < express.Count( ) ; index++) {
				if (fsm[ express[ index ] ] != null) {
					dynamic component = fsm[ express[ index ] ]( index , express , token );
					if (component.state) {
						token.Add( component.word.GetType( ).ToString( ) == "System.String" ? component.word : component.word.ToString( ) );
					}
				}
			}
			return token;
		}
	}
}
