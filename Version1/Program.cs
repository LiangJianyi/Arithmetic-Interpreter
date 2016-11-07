using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Version1 {
	class Program {
		static void Main( string[ ] args ) {
			string express = Console.ReadLine( );
			string result = Evaluator.Calculating( Parse( express ) );

			Console.WriteLine( result );
			Console.ReadKey( );
		}

		static string[] Parse( string express ) {
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
