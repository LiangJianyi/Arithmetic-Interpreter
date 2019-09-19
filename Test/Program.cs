using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arithmetic_Interpreter_UWP;

namespace Test {
	class Program {
		static void Main(string[] args) {
            BaseCons.ConsIterator(Test3(),cons=>Console.WriteLine(cons));
			Console.ReadKey();
		}

		private static void Test1() {
			string text = "(((([[[[(define list-end-marks (list #\\) #\\]))";
			Tokenizer splitor = new Tokenizer(text, new char[] { ' ' }, new char[] { '(', ')', '[', ']' });
			var lik = splitor.GetTokens();
			foreach (var item in lik) {
				Console.WriteLine(item);
			}
		}

		private static AST Test3() {
			AST c1 = new AST("r");
			AST c2 = new AST("l", c1);
			AST c3 = new AST("+", c2);
			AST c4 = new AST("r");
			AST c5 = new AST("l", c4);
			AST c6 = new AST("add", c5);
			AST c7 = new AST(c3);
			AST c8 = new AST(c6, c7);
			AST c9 = new AST("define", c8);
			return c9;
		}

		private static void Print2(AST item) {
			Console.WriteLine(item.Car.Previous);
			Console.WriteLine(item.Car.Next);
			Console.WriteLine(item.Cdr.Previous);
			Console.WriteLine(item.Cdr.Next);
			Console.WriteLine(item.CarValue);
			Console.WriteLine(item.CdrValue);
		}

		private static void Print(object node) {
			if (node == null) {
				Console.WriteLine("null");
			}
			else if (node is ValueType vt) {
				Console.WriteLine(vt);
			}
			else if (node is LinkedListNode<int> liknode) {
				if (node == null) {
					Console.WriteLine("null");
				}
				else {
					Console.WriteLine(liknode.Value);
				}
			}
			else if (node is string text) {
				if (text == string.Empty) {
					Console.WriteLine("\"\"");
				}
				else {
					Console.WriteLine(text);
				}
			}
			else if (node is LinkedListNode<BaseCons> consLikNode) {
				if (consLikNode.Value == null) {
					Console.WriteLine("null");
				}
				else {
					Console.WriteLine(consLikNode.Value);
				}
			}
			else if (node is BaseCons baseCons) {
				if (baseCons == null) {
					Console.WriteLine("null");
				}
				else {
					Console.WriteLine(baseCons);
				}
			}
		}
	}
}
