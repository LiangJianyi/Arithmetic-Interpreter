using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arithmetic_Interpreter_UWP;

namespace Test {
	class Program {
		static void Main(string[] args) {
			string text = "(((([[[[(define list-end-marks (list #\\) #\\]))";
			Splitor splitor = new Splitor(text, new char[] { ' ' }, new char[] { '(', ')', '[', ']' });
			var lik = splitor.Split();
			foreach (var item in lik) {
				Console.WriteLine(item);
			}
			Console.ReadKey();
		}
	}
}
