using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator {
	class Arithmetic {

		private static bool _called = false;    // 判断 AfterAddition 方法是否为递归之前的初次调用
		private static double _digitCache = 0;   // 暂存器
		private static int _benchmark = 0;    //  基准位置
		private static int probe = _benchmark + 1;   //  当前扫描位置（探针）

		public static double ExecuteCalculate( String express ) {
			double result = 0;

			//  该字符串数组用来记录运算符在表达式中的索引
			int[ ] allOperatorIndexArr;
			int[ ] highOperatorIndexArr;
			int[ ] lowOperatorIndexArr;

			String[ ] atom = new string[ express.Length ];

			//  抽取出字符串的每个字符放到与该字符串等长的一个数组里
			for (int index = 0 ; index < express.Length ; index += 1) {
				atom[ index ] = express.Substring( index , 1 );
			}

			//  获取所有运算符的索引
			allOperatorIndexArr = CaptureAllPriorityOperatorIndex( atom );

			//  获取高优先级运算符的索引
			highOperatorIndexArr = CaptureHighPriorityOperatorIndex( atom );

			//  获取低优先级运算符的索引
			lowOperatorIndexArr = CaptureLowPriorityOperatorIndex( atom );

			if (highOperatorIndexArr[ 0 ] == -1) {
				//  没有找到高优先级的运算符，执行低优先级运算
				result = LowEgalityCalculator( atom , lowOperatorIndexArr );
			}
			else if (lowOperatorIndexArr[ 0 ] == -1) {
				//  没有找到低优先级的运算符，执行高优先级运算
				result = HighEgalityCalculator( atom , highOperatorIndexArr );
			}
			else {
				//  两者都找到了，执行混合运算
			}

			//  对表达式执行算术解释
			//result = Calculator( atom , allOperatorIndexArr );

			return result;
		}

		/*
		public static double Calculator( String[ ] atom , int[ ] operatorIndexArr ) {
			double result;
			double digit;

			if (_benchmark < operatorIndexArr.Length - 2) {
				switch (atom[ operatorIndexArr[ _benchmark ] ]) {
					case "+":
						After( atom , operatorIndexArr , probe );
						break;
					case "-":
						//  Subtraction( );
						break;
					case "*":
						//  Multiplication( );
						break;
					case "/":
						//  Division( );
						break;
					case "%":
						//  Remainder( );
						break;
				}
			}
			else {
				if (_called == true) {
					_called = false;

				}
			}
		}
		*/

		/// <summary>
		/// 该方法会修改成员变量 digitCache 的值
		/// </summary>
		/// <param name="atom"></param>
		/// <param name="operatorIndexArr"></param>
		/// <param name="nextOperator">记录下一个运算符的索引</param>
		/*
		private static void After( string[ ] atom , int[ ] operatorIndexArr , int nextOperator ) {

			_called = false;

			if (nextOperator.Equals( "*" ) || nextOperator.Equals( "/" ) || nextOperator.Equals( "%" )) {
				HighPriority( atom , operatorIndexArr , nextOperator );
			}
			else if (nextOperator.Equals( "+" ) || nextOperator.Equals( "-" )) {

				String previousToken = null;
				//  用于反转字符串
				String reversalToken = null;

				for (int previous = _benchmark - 1 ; CheckBound( atom , previous ) ; previous -= 1) {
					previousToken += atom[ previous ];
				}
				//  对 previousToken 的内容进行反转
				for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
					reversalToken += previousToken.Substring( pincers , 1 );
				}
			}
		}
		*/

		/// <summary>
		/// 抽取出所有的运算符索引
		/// </summary>
		/// <param name="atom"></param>
		/// <returns></returns>
		public static int[ ] CaptureAllPriorityOperatorIndex( String[ ] atom ) {
			//  该字符串用来记录运算符在表达式中的索引
			StringBuilder operators = new StringBuilder( );
			String[ ] operatorRow;
			//  返回值
			int[ ] operatorIndex;

			//  记录运算符的索引
			for (int index = 0 ; index < atom.Length ; index += 1) {
				if (atom[ index ] == "+" || atom[ index ] == "-" || atom[ index ] == "*" || atom[ index ] == "/" || atom[ index ] == "%") {
					operators.Append( index );
					operators.Append( '@' );      //  每个索引转换成字符串后，它们之间要用字符'@'分隔
				}
			}
			//  移除最后一个字符，原因是上面的分隔操作会导致该字符串的最后一个字符是一个'@'
			//  而这个'@'在接下来的 Split 操作中毫无意义（也许会有副作用？）  
			operators.Remove( operators.Length - 1 , 1 );

			//  切割之后的每一个字符串元素代表一个运算符索引
			operatorRow = operators.ToString( ).Split( '@' );
			operatorIndex = new int[ operatorRow.Length ];
			for (int index = 0 ; index < operatorRow.Length ; index += 1) {
				operatorIndex[ index ] = int.Parse( operatorRow[ index ] );
			}

			return operatorIndex;
		}

		/// <summary>
		/// 抽取高优先级运算符，没有找到高优先级运算符返回 -1
		/// </summary>
		/// <param name="atom"></param>
		/// <returns></returns>
		private static int[ ] CaptureHighPriorityOperatorIndex( String[ ] atom ) {
			//  该字符串用来记录运算符在表达式中的索引
			StringBuilder operators = new StringBuilder( );
			String[ ] operatorRow;
			//  返回值
			int[ ] operatorIndex;
			//  检测是否找到了高优先级运算符
			bool isFind = false;

			//  记录高优先级运算符的索引
			for (int index = 0 ; index < atom.Length ; index += 1) {
				if (atom[ index ] == "*" || atom[ index ] == "/" || atom[ index ] == "%") {
					isFind = true;
					operators.Append( index );
					operators.Append( '@' );      //  每个索引转换成字符串后，它们之间要用字符'@'分隔
				}
			}
			if (isFind) {
				//  移除最后一个字符，原因是上面的分隔操作会导致该字符串的最后一个字符是一个'@'
				//  而这个'@'在接下来的 Split 操作中毫无意义（也许会有副作用？）  
				operators.Remove( operators.Length - 1 , 1 );

				//  切割之后的每一个字符串元素代表一个运算符索引
				operatorRow = operators.ToString( ).Split( '@' );
				operatorIndex = new int[ operatorRow.Length ];
				for (int index = 0 ; index < operatorRow.Length ; index += 1) {
					operatorIndex[ index ] = int.Parse( operatorRow[ index ] );
				}
			}
			else {
				//  如果没有发现高优先级的运算符，则返回一个数值为 -1 的数组
				operatorIndex = new int[ ] { -1 };
			}

			return operatorIndex;
		}

		/// <summary>
		/// 抽取低优先级运算符，没有找到低优先级运算符返回 -1
		/// </summary>
		/// <param name="atom"></param>
		/// <returns></returns>
		private static int[ ] CaptureLowPriorityOperatorIndex( String[ ] atom ) {
			//  该字符串用来记录运算符在表达式中的索引
			StringBuilder operators = new StringBuilder( );
			String[ ] operatorRow;
			//  返回值
			int[ ] operatorIndex;
			//  检测是否找到了高优先级运算符
			bool isFind = false;

			//  记录低优先级运算符的索引
			for (int index = 0 ; index < atom.Length ; index += 1) {
				if (atom[ index ] == "+" || atom[ index ] == "-") {
					isFind = true;
					operators.Append( index );
					operators.Append( '@' );      //  每个索引转换成字符串后，它们之间要用字符'@'分隔
				}
			}
			if (isFind) {
				//  移除最后一个字符，原因是上面的分隔操作会导致该字符串的最后一个字符是一个'@'
				//  而这个'@'在接下来的 Split 操作中毫无意义（也许会有副作用？）  
				operators.Remove( operators.Length - 1 , 1 );

				//  切割之后的每一个字符串元素代表一个运算符索引
				operatorRow = operators.ToString( ).Split( '@' );
				operatorIndex = new int[ operatorRow.Length ];
				for (int index = 0 ; index < operatorRow.Length ; index += 1) {
					operatorIndex[ index ] = int.Parse( operatorRow[ index ] );
				}
			}
			else {
				//  如果没有发现低优先级的运算符，则返回一个数值为 -1 的数组
				operatorIndex = new int[ ] { -1 };
			}

			return operatorIndex;
		}

		/// <summary>
		/// 高优先级运算
		/// </summary>
		/// <param name="atom"></param>
		/// <param name="operatorIndexArr"></param>
		/// <param name="nextOperator"></param>
		/*
		private static void HighPriority( string[ ] atom , int[ ] operatorIndexArr , int nextOperator ) {

			String previousToken = null;
			String nextToken = null;
			//  用于反转字符串
			String reversalToken = null;

			//  这里不能使用 switch 语句，因为不能够从一个 case 贯穿到另一个 case
			if (atom[ operatorIndexArr[ nextOperator ] ].Equals( "*" )) {

				for (int previous = nextOperator - 1, next = nextOperator + 1 ; CheckBound( atom , previous , next ) ; previous -= 1, next += 1) {
					previousToken += atom[ previous ];
					nextToken += atom[ next ];
				}
				//  对 previousToken 的内容进行反转
				for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
					reversalToken += previousToken.Substring( pincers , 1 );
				}
				//  转换成数字进行计算
				if (_called) {
					_digitCache = double.Parse( reversalToken ) * double.Parse( nextToken );
				}
				else {
					_digitCache *= double.Parse( nextToken );
				}

				After( atom , operatorIndexArr , probe + 1 );
			}
			else if (atom[ operatorIndexArr[ nextOperator ] ].Equals( "/" )) {

				for (int previous = nextOperator - 1, next = nextOperator + 1 ; CheckBound( atom , previous , next ) ; previous -= 1, next += 1) {
					previousToken += atom[ previous ];
					nextToken += atom[ next ];
				}
				//  对 previousToken 的内容进行反转
				for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
					reversalToken += previousToken.Substring( pincers , 1 );
				}
				//  转换成数字进行计算
				if (_called) {
					_digitCache = double.Parse( reversalToken ) / double.Parse( nextToken );
				}
				else {
					_digitCache /= double.Parse( nextToken );
				}

				After( atom , operatorIndexArr , probe + 1 );
			}
			else if (atom[ operatorIndexArr[ nextOperator ] ].Equals( "%" )) {

				for (int previous = nextOperator - 1, next = nextOperator + 1 ; CheckBound( atom , previous , next ) ; previous -= 1, next += 1) {
					previousToken += atom[ previous ];
					nextToken += atom[ next ];
				}
				//  对 previousToken 的内容进行反转
				for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
					reversalToken += previousToken.Substring( pincers , 1 );
				}
				//  转换成数字进行计算
				if (_called) {
					_digitCache = double.Parse( reversalToken ) % double.Parse( nextToken );
				}
				else {
					_digitCache %= double.Parse( nextToken );
				}

				After( atom , operatorIndexArr , probe + 1 );
			}
			else {
				//  如果本次递归没有遇见高优先级的运算符，终止递归
			}
		}
		*/

		/// <summary>
		/// 同级运算（低优先级）
		/// </summary>
		/// <param name="atom"></param>
		/// <param name="lowOperatorIndex"></param>
		private static double LowEgalityCalculator( String[ ] atom , int[ ] lowOperatorIndex ) {
			//  返回值
			double result = 0;

			while (_benchmark < lowOperatorIndex.Length) {
				String previousToken = null;
				String nextToken = null;
				//  字符串反转器
				String reversal = null;
				int previousDigitIndex;
				int nextDigitIndex;


				if (atom[ lowOperatorIndex[ _benchmark ] ].Equals( "+" )) {
					#region
					if (_called == false) {
						_called = true;
						for (previousDigitIndex = lowOperatorIndex[ _benchmark ] - 1, nextDigitIndex = lowOperatorIndex[ _benchmark ] + 1 ;
							 CheckBound( atom , previousDigitIndex , nextDigitIndex ) ; previousDigitIndex -= 1, nextDigitIndex += 1) {
							previousToken += atom[ previousDigitIndex ];
							nextToken += atom[ nextDigitIndex ];
						}
						//  对 previousToken 的内容进行反转
						for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
							reversal += previousToken.Substring( pincers , 1 );
						}
						previousToken = reversal;

						//  转换成数字进行计算
						_digitCache = double.Parse( previousToken ) + double.Parse( nextToken );
					}
					else {
						//  如果该循环之前就被执行过（called 等于 true ），那接下来的运算就不需要 previousToken，
						//  表达式左边的结果已经缓存到 digitCache 里面了，直接用它参与运算即可

						for (nextDigitIndex = lowOperatorIndex[ _benchmark ] + 1 ; CheckBound( atom , nextDigitIndex ) ; nextDigitIndex += 1) {
							nextToken += atom[ nextDigitIndex ];
						}

						//  转换成数字进行计算
						_digitCache += double.Parse( nextToken );
					}
					#endregion
				}
				else if (atom[ lowOperatorIndex[ _benchmark ] ].Equals( "-" )) {
					#region
					if (_called == false) {
						_called = true;
						for (previousDigitIndex = lowOperatorIndex[ _benchmark ] - 1, nextDigitIndex = lowOperatorIndex[ _benchmark ] + 1 ;
							 CheckBound( atom , previousDigitIndex , nextDigitIndex ) ; previousDigitIndex -= 1, nextDigitIndex += 1) {
							previousToken += atom[ previousDigitIndex ];
							nextToken += atom[ nextDigitIndex ];
						}
						//  对 previousToken 的内容进行反转
						for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
							reversal += previousToken.Substring( pincers , 1 );
						}
						previousToken = reversal;

						//  转换成数字进行计算
						_digitCache = double.Parse( previousToken ) - double.Parse( nextToken );
					}
					else {
						//  如果该循环之前就被执行过（called 等于 true ），那接下来的运算就不需要 previousToken，
						//  表达式左边的结果已经缓存到 digitCache 里面了，直接用它参与运算即可

						for (nextDigitIndex = lowOperatorIndex[ _benchmark ] + 1 ; CheckBound( atom , nextDigitIndex ) ; nextDigitIndex += 1) {
							nextToken += atom[ nextDigitIndex ];
						}

						//  转换成数字进行计算
						_digitCache -= double.Parse( nextToken );
					}
					#endregion
				}
				//  开始新的循环之前，移动基准位置
				_benchmark += 1;
			}

			result = _digitCache;
			return result;
		}

		/// <summary>
		/// 同级运算（高优先级）
		/// </summary>
		/// <param name="atom"></param>
		/// <param name="highOperatorIndex"></param>
		/// <returns></returns>
		private static double HighEgalityCalculator( String[ ] atom , int[ ] highOperatorIndex ) {
			//  返回值
			double result = 0;

			while (_benchmark < highOperatorIndex.Length) {
				String previousToken = null;
				String nextToken = null;
				//  字符串反转器
				String reversal = null;
				int previousDigitIndex;
				int nextDigitIndex;


				if (atom[ highOperatorIndex[ _benchmark ] ].Equals( "*" )) {
					#region
					if (_called == false) {
						_called = true;
						for (previousDigitIndex = highOperatorIndex[ _benchmark ] - 1, nextDigitIndex = highOperatorIndex[ _benchmark ] + 1 ;
							 CheckBound( atom , previousDigitIndex , nextDigitIndex ) ; previousDigitIndex -= 1, nextDigitIndex += 1) {
							previousToken += atom[ previousDigitIndex ];
							nextToken += atom[ nextDigitIndex ];
						}
						//  对 previousToken 的内容进行反转
						for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
							reversal += previousToken.Substring( pincers , 1 );
						}
						previousToken = reversal;

						//  转换成数字进行计算
						_digitCache = double.Parse( previousToken ) * double.Parse( nextToken );
					}
					else {
						//  如果该循环之前就被执行过（called 等于 true ），那接下来的运算就不需要 previousToken，
						//  表达式左边的结果已经缓存到 digitCache 里面了，直接用它参与运算即可

						for (nextDigitIndex = highOperatorIndex[ _benchmark ] + 1 ; CheckBound( atom , nextDigitIndex ) ; nextDigitIndex += 1) {
							nextToken += atom[ nextDigitIndex ];
						}

						//  转换成数字进行计算
						_digitCache *= double.Parse( nextToken );
					}
					#endregion
				}
				else if (atom[ highOperatorIndex[ _benchmark ] ].Equals( "/" )) {
					#region
					if (_called == false) {
						_called = true;
						for (previousDigitIndex = highOperatorIndex[ _benchmark ] - 1, nextDigitIndex = highOperatorIndex[ _benchmark ] + 1 ;
							 CheckBound( atom , previousDigitIndex , nextDigitIndex ) ; previousDigitIndex -= 1, nextDigitIndex += 1) {
							previousToken += atom[ previousDigitIndex ];
							nextToken += atom[ nextDigitIndex ];
						}
						//  对 previousToken 的内容进行反转
						for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
							reversal += previousToken.Substring( pincers , 1 );
						}
						previousToken = reversal;

						//  转换成数字进行计算
						_digitCache = double.Parse( previousToken ) / double.Parse( nextToken );
					}
					else {
						//  如果该循环之前就被执行过（called 等于 true ），那接下来的运算就不需要 previousToken，
						//  表达式左边的结果已经缓存到 digitCache 里面了，直接用它参与运算即可

						for (nextDigitIndex = highOperatorIndex[ _benchmark ] + 1 ; CheckBound( atom , nextDigitIndex ) ; nextDigitIndex += 1) {
							nextToken += atom[ nextDigitIndex ];
						}

						//  转换成数字进行计算
						_digitCache /= double.Parse( nextToken );
					}
					#endregion
				}
				//  开始新的循环之前，移动基准位置
				_benchmark += 1;
			}

			result = _digitCache;
			return result;
		}

		/// <summary>
		/// 高优先级运算
		/// </summary>
		/// <param name="atom"></param>
		/// <param name="allOperatorIndex"></param>
		/// <param name="probe">接收一个探针，用于判断基准值之后的运算符</param>
		/// <returns></returns>
		private static double HighPriorityCalculate( String[ ] atom , int[ ] allOperatorIndex , int probe ) {
			//  返回值
			double result;

			while (probe < allOperatorIndex.Length) {
				String previousToken = null;
				String nextToken = null;
				//  字符串反转器
				String reversal = null;
				int previousDigitIndex;
				int nextDigitIndex;

				if (atom[ allOperatorIndex[ probe ] ].Equals( "*" )) {
					#region
					if (_called == false) {
						_called = true;

						for (previousDigitIndex = allOperatorIndex[ probe ] - 1, nextDigitIndex = allOperatorIndex[ probe ] + 1 ;
							 CheckBound( atom , previousDigitIndex , nextDigitIndex ) ; previousDigitIndex -= 1, nextDigitIndex += 1) {
							previousToken += atom[ previousDigitIndex ];
							nextToken += atom[ nextDigitIndex ];
						}
						//  对 previousToken 的内容进行反转
						for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
							reversal += previousToken.Substring( pincers , 1 );
						}
						previousToken = reversal;

						//  转换成数字进行计算
						_digitCache = double.Parse( previousToken ) * double.Parse( nextToken );
					}
					else {
						//  如果该循环之前就被执行过（called 等于 true ），那接下来的运算就不需要 previousToken，
						//  表达式左边的结果已经缓存到 digitCache 里面了，直接用它参与运算即可

						for (nextDigitIndex = allOperatorIndex[ probe ] + 1 ; CheckBound( atom , nextDigitIndex ) ; nextDigitIndex += 1) {
							nextToken += atom[ nextDigitIndex ];
						}

						//  转换成数字进行计算
						_digitCache *= double.Parse( nextToken );
					}
					#endregion
				}
				else if (atom[ allOperatorIndex[ probe ] ].Equals( "/" )) {
					#region
					if (_called == false) {
						_called = true;

						for (previousDigitIndex = allOperatorIndex[ probe ] - 1, nextDigitIndex = allOperatorIndex[ probe ] + 1 ;
							 CheckBound( atom , previousDigitIndex , nextDigitIndex ) ; previousDigitIndex -= 1, nextDigitIndex += 1) {
							previousToken += atom[ previousDigitIndex ];
							nextToken += atom[ nextDigitIndex ];
						}
						//  对 previousToken 的内容进行反转
						for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
							reversal += previousToken.Substring( pincers , 1 );
						}
						previousToken = reversal;

						//  转换成数字进行计算
						_digitCache = double.Parse( previousToken ) / double.Parse( nextToken );
					}
					else {
						//  如果该循环之前就被执行过（called 等于 true ），那接下来的运算就不需要 previousToken，
						//  表达式左边的结果已经缓存到 digitCache 里面了，直接用它参与运算即可

						for (nextDigitIndex = allOperatorIndex[ probe ] + 1 ; CheckBound( atom , nextDigitIndex ) ; nextDigitIndex += 1) {
							nextToken += atom[ nextDigitIndex ];
						}

						//  转换成数字进行计算
						_digitCache /= double.Parse( nextToken );
					}
					#endregion
				}
				else if (atom[ allOperatorIndex[ probe ] ].Equals( "%" )) {
					#region
					if (_called == false) {
						_called = true;

						for (previousDigitIndex = allOperatorIndex[ probe ] - 1, nextDigitIndex = allOperatorIndex[ probe ] + 1 ;
							 CheckBound( atom , previousDigitIndex , nextDigitIndex ) ; previousDigitIndex -= 1, nextDigitIndex += 1) {
							previousToken += atom[ previousDigitIndex ];
							nextToken += atom[ nextDigitIndex ];
						}
						//  对 previousToken 的内容进行反转
						for (int pincers = previousToken.Length - 1 ; pincers >= 0 ; pincers -= 1) {
							reversal += previousToken.Substring( pincers , 1 );
						}
						previousToken = reversal;

						//  转换成数字进行计算
						_digitCache = double.Parse( previousToken ) % double.Parse( nextToken );
					}
					else {
						//  如果该循环之前就被执行过（called 等于 true ），那接下来的运算就不需要 previousToken，
						//  表达式左边的结果已经缓存到 digitCache 里面了，直接用它参与运算即可

						for (nextDigitIndex = allOperatorIndex[ probe ] + 1 ; CheckBound( atom , nextDigitIndex ) ; nextDigitIndex += 1) {
							nextToken += atom[ nextDigitIndex ];
						}

						//  转换成数字进行计算
						_digitCache %= double.Parse( nextToken );
					}
					#endregion
				}
				else if (atom[ allOperatorIndex[ probe ] ].Equals( "+" ) || atom[ allOperatorIndex[ probe ] ].Equals( "-" )) {
					break;
				}
				//  开始新的循环之前，移动探针指向下一个运算符索引
				probe += 1;
			}

			result = _digitCache;
			return result;
		}

		private static bool CheckBound( String[ ] atom , int next ) {
			bool nextStates;

			if (next < atom.Length) {
				switch (atom[ next ]) {
					case "+":
						nextStates = false;
						break;
					case "-":
						nextStates = false;
						break;
					case "*":
						nextStates = false;
						break;
					case "/":
						nextStates = false;
						break;
					case "%":
						nextStates = false;
						break;
					default:
						nextStates = true;
						break;
				}
			}
			else {
				return false;
			}

			if (nextStates) {
				return true;
			}
			else {
				return false;
			}
		}

		private static bool CheckBound( String[ ] atom , int previous , int next ) {
			bool previousStatus;
			bool nextStatus;

			if (previous >= 0 && next < atom.Length) {
				switch (atom[ previous ]) {
					case "+":
						previousStatus = false;
						break;
					case "-":
						previousStatus = false;
						break;
					case "*":
						previousStatus = false;
						break;
					case "/":
						previousStatus = false;
						break;
					case "%":
						previousStatus = false;
						break;
					default:
						previousStatus = true;
						break;
				}
				switch (atom[ next ]) {
					case "+":
						nextStatus = false;
						break;
					case "-":
						nextStatus = false;
						break;
					case "*":
						nextStatus = false;
						break;
					case "/":
						nextStatus = false;
						break;
					case "%":
						nextStatus = false;
						break;
					default:
						nextStatus = true;
						break;
				}
			}
			else {
				return false;
			}

			if (previousStatus && nextStatus) {
				return true;
			}
			else {
				return false;
			}
		}
	}
}
