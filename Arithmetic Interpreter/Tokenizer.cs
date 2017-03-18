using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arithmetic_Interpreter {
	internal static class Tokenizer {
		private static void Tokenization( string express ) {
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
			Func<bool> leftRacketOfNext = ( ) => {
				if (index < express.Length - 1) {
					int next = index - 1;
					if (isNumber( express[ next ] ) || express[ next ] == '-' || isSqrt( express[ next ] ) || express[ next ] == '(') {
						return true;
					}
					else {
						throw new InvalidOperationException( "左圆括号右边只能是数字、负号、sqrt、左圆括号" );
					}
				}
				else {
					throw new InvalidOperationException( "左圆括号没有匹配的右圆括号。" );
				}
			};
			Func<bool> numberOfNext = ( ) => {
				if (index < express.Length - 1) {
					int next = index - 1;
					if (express[ next ] == ')' || express[ next ] == '^' || express[ next ] == '.' || isOperator( express[ next ] ) || express[ next ] == '!') {
						return true;
					}
					else {
						throw new InvalidOperationException( "数字右边只能是\")\", \"^\", \"!\", 小数点, 算术运算符" );
					}
				}
				else {
					return true;	// 数字可以作为最后一个字符
				}
			};
			Func<bool> plusOfNext = () =>{
				if (index < express.Length - 1) {
					int next = index - 1;
					if (isNumber( express[ next ] ) || express[ next ] == '+' || express[ next ] == '-' || isSqrt( express[ next ] ) || express[ next ] == '(') {
						return true;
					}
					else {
						throw new InvalidOperationException( "算术运算符的右边只能是加号、减号、数字、sqrt、左圆括号" );
					}
				}
				else {
					throw new InvalidOperationException( "算术运算符不能用作表达式的结尾。" );
				}
			};
			Func<bool> minusOfNext = ( ) => {
				if (index < express.Length - 1) {
					int next = index - 1;
					if (isNumber( express[ next ] ) || express[ next ] == '+' || express[ next ] == '-' || isSqrt( express[ next ] ) || express[ next ] == '(') {
						return true;
					}
					else {
						throw new InvalidOperationException( "算术运算符的右边只能是加号、减号、数字、sqrt、左圆括号" );
					}
				}
				else {
					throw new InvalidOperationException( "算术运算符不能用作表达式的结尾。" );
				}
			};
			Func<bool> decimalpointOfNext = ( ) => {
				if (index < express.Length - 1) {
					int next = index - 1;
					if (isNumber( express[ next ] )) {
						return true;
					}
					else {
						throw new InvalidOperationException( "小数点的右边只能是数字" );
					}
				}
				else {
					throw new InvalidOperationException( "小数点不能用作表达式的结尾。" );
				}
			};


			for (; index < express.Count( ) ; index++) {
				if (index < 1) {
					if (express[ index ] == '(') {

					}
					else if (isSqrt( index )) {

					}
					else if (isPi( index )) {

					}
					else if (express[index]=='+') {

					}
					else if (express[index]=='-') {

					}
					else if (isNumber( express[ index ] )) {

					}
					else {
						throw new InvalidOperationException( "表达式无效。" );
					}
				}
				else {
					if (express[ index ] == '(') {

					}
					else if (isSqrt( index )) {

					}
					else if (isPi( index )) {

					}
					else if (isOperator( express[ index ] )) {

					}
					else if (isNumber( express[ index ] )) {

					}
					else {
						throw new InvalidOperationException( "表达式无效。" );
					}
				}
			}
		}
	}
}
