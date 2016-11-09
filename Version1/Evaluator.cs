using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version1 {
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
		/// 高优先级运算
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns></returns>
		private static string[ ] HighEval( string[ ] tokens ) {
			double leftValue;
			double rightValue;
			int nullCount = 0;
			for (int index = 0 ; index < tokens.Length ; index++) {
				if (tokens[ index ] != null) {
					try {
						if (tokens[ index - 1 ] != null) {
							switch (tokens[ index ]) {
								case "*":
									Eval( Multiplication , tokens , out leftValue , out rightValue , index );
									nullCount = Cleanull( ref tokens );
									break;
								case "/":
									Eval( Division , tokens , out leftValue , out rightValue , index );
									nullCount = Cleanull( ref tokens );
									break;
								case "%":
									Eval( Remaination , tokens , out leftValue , out rightValue , index );
									nullCount = Cleanull( ref tokens );
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
				else {
					continue;
				}
			}
			// 如果存在 null 元素，则继续递归
			if (nullCount > 0) {
				return HighEval( tokens );
			}
			else {
				return tokens;
			}
		}

		/// <summary>
		/// 低优先级运算
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns></returns>
		private static string[ ] LowEval( string[ ] tokens ) {
			double leftValue;
			double rightValue;
			int nullCount = 0;
			for (int index = 0 ; index < tokens.Length ; index++) {
				if (tokens[ index ] != null) {
					try {
						if (tokens[ index - 1 ] != null) {
							switch (tokens[ index ]) {
								case "+":
									Eval( Addition , tokens , out leftValue , out rightValue , index );
									nullCount = Cleanull( ref tokens );
									break;
								case "-":
									Eval( Subtraction , tokens , out leftValue , out rightValue , index );
									nullCount = Cleanull( ref tokens );
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
				else {
					continue;
				}
			}
			// 如果存在 null 元素，则继续递归
			if (nullCount > 0) {
				return LowEval( tokens );
			}
			else {
				return tokens;
			}
		}

		/// <summary>
		/// 归约 -> 去除 null
		/// 返回去除掉 null 的重量
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns>返回去除掉 null 的重量</returns>
		private static int Cleanull( ref string[ ] tokens ) {
			int nullCount = 0;
			string[ ] temp = new string[ tokens.Length - 2 ];
			for (int tokensIndex = 0, tempIndex = 0 ;
				tokensIndex < tokens.Length && tempIndex < temp.Length ;
				tokensIndex++) {
				if (tokens[ tokensIndex ] == null) {
					nullCount++;
					continue;
				}
				else {
					temp[ tempIndex ] = tokens[ tokensIndex ];
					tempIndex++;
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

		/// <summary>
		/// 获得计算结果
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns></returns>
		public static string Calculating( string[ ] tokens ) {
			return Evaluator.LowEval( Evaluator.HighEval( tokens ) )[ 0 ];
		}
	}
}
