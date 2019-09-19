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

		private static Cons2 Test3() {
			Cons2 c1 = new Cons2("r");
			Cons2 c2 = new Cons2("l", c1);
			Cons2 c3 = new Cons2("+", c2);
			Cons2 c4 = new Cons2("r");
			Cons2 c5 = new Cons2("l", c4);
			Cons2 c6 = new Cons2("add", c5);
			Cons2 c7 = new Cons2(c3);
			Cons2 c8 = new Cons2(c6, c7);
			Cons2 c9 = new Cons2("define", c8);
			return c9;
		}

		private static void Print2(Cons2 item) {
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
