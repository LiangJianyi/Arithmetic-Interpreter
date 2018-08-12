using System;
using System.Collections.Generic;
//using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;

namespace Arithmetic_Interpreter_UWP {
	public static class Tokenizer {
		public static LinkedList<string> GetTokens(string text) {
			string[] tokens = text.Split(new char[] { ' ' });
			LinkedList<string> result = new LinkedList<string>();
			foreach (var token in tokens) {
				result.AddLast(token);
			}
			return result;
		}
	}
}
