using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arithmetic_Interpreter {
	internal static class Tokenizer {
		private static List<string> Tokenization( string express ) {
			List<string> token = new List<string>( );
			int index = 0;
			Predicate<char> isNumber = ch => ch == '0' || ch == '1' || ch == '2' || ch == '3' || ch == '4' || ch == '5' || ch == '6' || ch == '7' || ch == '8' || ch == '9';
			Predicate<char> isOperator = ch => ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '%';
			Predicate<int> isSqrt = ( i ) => {
				string word = String.Empty;
				Action next = ( ) => {
					word += express[ i ];
					i++;
				};
				if (express[ i ] == 's' || express[ i ] == 'S') {
					next( );
					if (express[ i ] == 'q' || express[ i ] == 'Q') {
						next( );
						if (express[ i ] == 'r' || express[ i ] == 'R') {
							next( );
							if (express[ i ] == 't' || express[ i ] == 'T') {
								index = i;
								return true;
							}
						}
					}
				}
				return false;
			};
			Predicate<int> isPi = ( i ) => {
				string word = String.Empty;
				Action next = ( ) => {
					word += express[ i ];
					i++;
				};
				if (express[ i ] == 'p' || express[ i ] == 'P') {
					next( );
					if (express[ i ] == 'i' || express[ i ] == 'I') {
						index = i;
						return true;
					}
				}
				return false;
			};
			Predicate<int> leftRacketOfNext = ( next ) => isNumber( express[ next ] ) || express[ next ] == '-' || isSqrt( express[ next ] ) || express[ next ] == '(';
			Predicate<int> numberOfNext = ( next ) => express[ next ] == ')' || express[ next ] == '^' || express[ next ] == '.' || isOperator( express[ next ] ) || express[ next ] == '!';
			Predicate<int> plusOfNext = ( next ) => isNumber( express[ next ] ) || express[ next ] == '+' || express[ next ] == '-' || isSqrt( express[ next ] ) || express[ next ] == '(' || isPi( express[ next ] );
			Predicate<int> minusOfNext = ( next ) => isNumber( express[ next ] ) || express[ next ] == '+' || express[ next ] == '-' || isSqrt( express[ next ] ) || express[ next ] == '(' || isPi( express[ next ] );
			Predicate<int> decimalpointOfNext = ( next ) => isNumber( express[ next ] );
			Predicate<int> piOfNext = ( next ) => express[ next ] == '(';
			Predicate<int> multiOfNext = ( next ) => isNumber( express[ next ] ) || isSqrt( express[ next ] ) || express[ next ] == '+' || express[ next ] == '-' || isPi( express[ next ] );
			Predicate<int> divOfNext = ( next ) => isNumber( express[ next ] ) || isSqrt( express[ next ] ) || express[ next ] == '+' || express[ next ] == '-' || isPi( express[ next ] );
			Predicate<int> remaOfNext = ( next ) => isNumber( express[ next ] ) || isSqrt( express[ next ] ) || express[ next ] == '+' || express[ next ] == '-' || isPi( express[ next ] );
			Predicate<int> sqrtOfNext = ( next ) => express[ next ] == '(';
			Predicate<int> capOfNext = ( next ) => isNumber( express[ next ] );


			for (int next = 0 ; index < express.Count( ) ; index++) {
				next = index + 1;
				if (index < 1) {
					if (express[ index ] == '(') {
						if (index < express.Length - 1) {
							if (leftRacketOfNext( next )) {
								//token.Add( express[ index ].ToString( ) );
								continue;
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
					}
					else if (isNumber( express[ index ] )) {
						if (index < express.Length - 1) {
							if (numberOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "数字右边只能是\")\", \"^\", \"!\", 小数点, 算术运算符" );
							}
						}
						else {
							continue;    // 数字可以作为最后一个字符
						}
					}
					else if (express[ index ] == '+') {
						if (index < express.Length - 1) {
							if (plusOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "算术运算符的右边只能是加号、减号、数字、sqrt、左圆括号" );
							}
						}
						else {
							throw new InvalidOperationException( "算术运算符不能用作表达式的结尾。" );
						}
					}
					else if (express[ index ] == '-') {
						if (index < express.Length - 1) {
							if (minusOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "算术运算符的右边只能是加号、减号、数字、sqrt、左圆括号" );
							}
						}
						else {
							throw new InvalidOperationException( "算术运算符不能用作表达式的结尾。" );
						}
					}
					else if (isPi( index )) {
						if (index < express.Length - 1) {
							if (piOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "PI 是函数表达式，必须遵循\"PI( )\"的形式。" );
							}
						}
						else {
							throw new InvalidOperationException( "PI 是函数表达式，必须遵循\"PI( )\"的形式。" );
						}
					}
					else if (isSqrt( index )) {
						if (index < express.Length - 1) {
							if (sqrtOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "sqrt 是函数表达式，必须遵循\"sqrt( )\"的形式。" );
							}
						}
						else {
							throw new InvalidOperationException( "sqrt 是函数表达式，必须遵循\"sqrt( )\"的形式。" );
						}
					}
					else {
						throw new InvalidOperationException( "表达式无效。" );
					}
				}
				else {
					if (express[ index ] == '(') {
						if (index < express.Length - 1) {
							if (leftRacketOfNext( next )) {
								continue;
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
					}
					else if (isNumber( express[ index ] )) {
						if (index < express.Length - 1) {
							if (numberOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "数字右边只能是\")\", \"^\", \"!\", 小数点, 算术运算符" );
							}
						}
						else {
							continue;    // 数字可以作为最后一个字符
						}
					}
					else if (express[ index ] == '+' || express[ index ] == '-') {
						if (express[ index ] == '+') {
							if (index < express.Length - 1) {
								if (plusOfNext( next )) {
									continue;
								}
								else {
									throw new InvalidOperationException( "算术运算符的右边只能是加号、减号、数字、sqrt、左圆括号" );
								}
							}
							else {
								throw new InvalidOperationException( "算术运算符不能用作表达式的结尾。" );
							}
						}
						else if (express[ index ] == '-') {
							if (index < express.Length - 1) {
								if (minusOfNext( next )) {
									continue;
								}
								else {
									throw new InvalidOperationException( "算术运算符的右边只能是加号、减号、数字、sqrt、左圆括号" );
								}
							}
							else {
								throw new InvalidOperationException( "算术运算符不能用作表达式的结尾。" );
							}
						}
					}
					else if (express[ index ] == '.') {
						if (index < express.Length - 1) {
							if (decimalpointOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "小数点右边只能是数字。" );
							}
						}
						else {
							throw new InvalidOperationException( "小数点不能用作表达式的结尾。" );
						}
					}
					else if (isPi( index )) {
						if (index < express.Length - 1) {
							if (piOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "PI 是函数表达式，必须遵循\"PI( )\"的形式。" );
							}
						}
						else {
							throw new InvalidOperationException( "PI 是函数表达式，必须遵循\"PI( )\"的形式。" );
						}
					}
					else if (express[ index ] == '*' || express[ index ] == '/' || express[ index ] == '%') {
						if (express[ index ] == '*') {
							if (index < express.Length) {
								if (multiOfNext( next )) {
									continue;
								}
								else {
									throw new InvalidOperationException( "乘号右边只能是数字, \"sqrt\", 加号, 减号, \"PI\"" );
								}
							}
							else {
								throw new InvalidOperationException( "乘号不能作为表达式的结尾。" );
							}
						}
						else if (express[ index ] == '/') {
							if (index < express.Length) {
								if (divOfNext( next )) {
									continue;
								}
								else {
									throw new InvalidOperationException( "除号右边只能是数字, \"sqrt\", 加号, 减号, \"PI\"" );
								}
							}
							else {
								throw new InvalidOperationException( "除号不能作为表达式的结尾。" );
							}
						}
						else if (express[ index ] == '%') {
							if (index < express.Length) {
								if (remaOfNext( next )) {
									continue;
								}
								else {
									throw new InvalidOperationException( "取余号右边只能是数字, \"sqrt\", 加号, 减号, \"PI\"" );
								}
							}
							else {
								throw new InvalidOperationException( "取余号不能作为表达式的结尾。" );
							}
						}
					}
					else if (isSqrt( index )) {
						if (index < express.Length - 1) {
							if (sqrtOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "sqrt 是函数表达式，必须遵循\"sqrt( )\"的形式。" );
							}
						}
						else {
							throw new InvalidOperationException( "sqrt 是函数表达式，必须遵循\"sqrt( )\"的形式。" );
						}
					}
					else if (express[ index ] == '^') {
						if (index < express.Length) {
							if (capOfNext( next )) {
								continue;
							}
							else {
								throw new InvalidOperationException( "\"^\"右边只能是数字, \"sqrt\", 加号, 减号, \"PI\"" );
							}
						}
						else {
							throw new InvalidOperationException( "\"^\"不能作为表达式的结尾。" );
						}
					}
					else if (express[ index ] == ' ') { // 忽略空格
						continue;
					}
					else {
						throw new InvalidOperationException( "存在无效的运算对象。" );
					}
				}
			}
			return token;
		}
	}
}
