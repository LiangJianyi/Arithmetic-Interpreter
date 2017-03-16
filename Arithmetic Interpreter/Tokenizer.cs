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
			Func<bool> isSqrt = ( ) => {
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
								return true;
							}
						}
					}
				}
				return false;
			};
			Func<bool> isPi = ( ) => {
				string word = String.Empty;
				Action next = ( ) => {
					word += express[ index ];
					index++;
				};
				if (express[ index ] == 'p' || express[ index ] == 'P') {
					next( );
					if (express[ index ] == 'i' || express[ index ] == 'I') {
						return true;
					}
				}
				return false;
			};
			Action leftRacketOfNext = ( ) => {

			};

			for (; index < express.Count( ) ; index++) {
				if (index < 1) {
					if (express[ index ] == '(') {

					}
					else if (isSqrt( )) {

					}
					else if (isPi( )) {

					}
					else if (isOperator( express[ index ] )) {

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
					else if (isSqrt( )) {

					}
					else if (isPi( )) {

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
