using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArithmeticSexp;

namespace ConsoleApplication1 {
	class Parser {
		public static Node Parse( List<string> token ) {

			Predicate<string> isNumber = ( ch ) => ch == "0" || ch == "1" || ch == "2" || ch == "3" || ch == "4" || ch == "5" || ch == "6" || ch == "7" || ch == "8" || ch == "9";
			Predicate<string> isOperator = ( ch ) => ch == "+" || ch == "-" || ch == "*" || ch == "/" || ch == "%";

			Func<Node , Node> function = null;
			int index = token.Count - 1;
			#region 去除多余的外层括号，获得有效的 index 初始值和分析界限（range）
			//int range = 0;
			//int count = 1;
			//for (; count < 1 ; count++) {
			//	if (token[ token.Count - count ].Equals( ")" )) {
			//		if (token[ count - 1 ].Equals( "(" )) {
			//			continue;
			//		}
			//		else {
			//			index = token.Count - count;
			//		}
			//	}
			//	else {
			//		index = token.Count - count - 1;
			//	}
			//}
			//range = count - 1;
			#endregion
			function = ( node ) => {
				if (token[ index ].Equals( ")" )) {
					if (node.RightNode == null) {
						node.RightNode = new Node( );
						index--;
						function( node.RightNode );
						if (index > 0) {
							index--;
						}
						else {
							return node;
						}
					}
					else {
						node.LeftNode = new Node( );
						index--;
						function( node.LeftNode );
						if (index > 0) {
							index--;
						}
						else {
							return node;
						}
					}
				}
				else if (isNumber( token[ index ] )) {
					if (node.RightNode == null) {
						node.RightNode = new Node( Convert.ToDouble( token[ index ] ) );
						if (index > 0) {
							index--;
						}
						else {
							return node;
						}
						//return function( node );
					}
					else {
						node.LeftNode = new Node( Convert.ToDouble( token[ index ] ) );
						if (index > 0) {
							index--;
						}
						else {
							return node;
						}
						//return function( node );
					}
				}
				else if (isOperator( token[ index ] )) {
					node.Ops = Convert.ToChar( token[ index ] );
					if (index > 0) {
						index--;
					}
					else {
						return node;
					}
					//return function( node );
				}
				else {
					return node;
				}

				return function( node );

			};

			return function( new Node( ) );
		}
	}
}
