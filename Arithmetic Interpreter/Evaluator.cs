using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic_Interpreter {
	internal class Evaluator {
		#region Calculator
		static double Addition( double leftValue , double rightValue ) {
			return leftValue + rightValue;
		}

		static double Subtraction( double leftValue , double rightValue ) {
			return leftValue - rightValue;
		}

		static double Multiplication( double leftValue , double rightValue ) {
			return leftValue * rightValue;
		}

		static double Division( double leftValue , double rightValue ) {
			return leftValue / rightValue;
		}

		static double Remaination( double leftValue , double rightValue ) {
			return leftValue % rightValue;
		}

		static string Cure( Func<double , double , double> calcultor , double leftValue , double rightValue ) {
			return calcultor( leftValue , rightValue ).ToString( );
		}
		#endregion

		/// <summary>
		/// 归约 -> 去除 null
		/// 返回去除掉 null 的重量
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns>返回去除掉 null 的重量</returns>
		private static int Cleanull( ref string[ ] tokens ) {
			int nullCount = 0;
			string[ ] temp = new string[ tokens.Length - 2 ];
			for (int tokensIndex = 0, tempIndex = 0 ; tokensIndex < tokens.Length ; tokensIndex++) {
				if (tokens[ tokensIndex ] == null) {
					nullCount++;
					continue;
				}
				else {
					// 之所以把 temp 的边界检查放在这里而不是放在 for 表达式2 中，
					// 是因为这样做能避免 null 出现在 tokens 数组的最后导致 nullCount 无法 increment，
					// nullCount 无法正确 increment 会导致该方法返回错误的值，导致整个程序产生错误的结果
					if (tempIndex < temp.Length) {
						temp[ tempIndex ] = tokens[ tokensIndex ];
						tempIndex++;
					}
					else {
						continue;
					}
				}
			}
			tokens = temp;
			return nullCount;
		}

		/// <summary>
		/// 求值
		/// </summary>
		/// <param name="calcultor"> 算术计算函数 </param>
		/// <param name="tokens"> 进行归约的 token 数组 </param>
		/// <param name="leftValue"> 左操作数 </param>
		/// <param name="rightValue"> 右操作数 </param>
		/// <param name="index"> 当前 token 的索引 </param>
		private static void Eval( Func<double , double , double> calcultor , string[ ] tokens , out double leftValue , out double rightValue , int index ) {
			leftValue = Convert.ToDouble( tokens[ index - 1 ] );
			rightValue = Convert.ToDouble( tokens[ index + 1 ] );
			tokens[ index - 1 ] = Cure( calcultor , leftValue , rightValue );
			tokens[ index ] = null;
			tokens[ index + 1 ] = null;
		}

		private static string[ ] Foo( string[ ] tokens ) {
			double leftValue;
			double rightValue;
			int nullCount = 0;
			Predicate<string> isHighOps = ( ops ) => ops.Equals( "*" ) || ops.Equals( "/" ) || ops.Equals( "%" );
			Predicate<int> nextOperatorIsHighOperator = ( param_index ) => {
				try {
					return isHighOps( tokens[ param_index + 2 ] );
				}
				catch (IndexOutOfRangeException) {
					// 捕获了该异常意味着表达式只存在一个运算符
					return false;
				}
			};

			for (int index = 0 ; index < tokens.Length ; index++) {
				try {
					if (tokens[ index - 1 ] != null) {
						switch (tokens[ index ]) {
							case "+":
								if (nextOperatorIsHighOperator( index )) {
									break;
								}
								else {
									Eval( Addition , tokens , out leftValue , out rightValue , index );
									nullCount = Cleanull( ref tokens );
									index = 0;
									break;
								}
							case "-":
								if (nextOperatorIsHighOperator( index )) {
									break;
								}
								else {
									Eval( Subtraction , tokens , out leftValue , out rightValue , index );
									nullCount = Cleanull( ref tokens );
									index = 0;
									break;
								}
							case "*":
								Eval( Multiplication , tokens , out leftValue , out rightValue , index );
								nullCount = Cleanull( ref tokens );
								index = 0;
								break;
							case "/":
								Eval( Division , tokens , out leftValue , out rightValue , index );
								nullCount = Cleanull( ref tokens );
								index = 0;
								break;
							case "%":
								Eval( Remaination , tokens , out leftValue , out rightValue , index );
								nullCount = Cleanull( ref tokens );
								index = 0;
								break;
						}
					}
					else {
						// 终止循环 -> 归约
						break;
					}
				}
				catch (IndexOutOfRangeException) {
					// 抛出 IndexOutOfRangeException 意味当前 tokens[index] 为第一个元素
					continue;
				}
			}
			// 如果存在 null 元素，则继续递归
			if (nullCount > 0) {
				return Foo( tokens );
			}
			else {
				return tokens;
			}
		}

		/// <summary>
		/// 获得计算结果
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns></returns>
		public static string Calculating( string[ ] tokens ) {
			//return Evaluator.LowEval( Evaluator.HighEval( tokens ) )[ 0 ];
			return Evaluator.Foo( tokens )[ 0 ];
		}
	}
}
