using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arithmetic_Interpreter_UWP;

namespace Test {
	class Program {
		static void Main(string[] args) {
            AST.ConsIterator(Test3(),cons=>Console.WriteLine(cons));
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

		private static Cons Test3() {
			Cons c1 = new Cons("r");
			Cons c2 = new Cons("l", c1);
			Cons c3 = new Cons("+", c2);
			Cons c4 = new Cons("r");
			Cons c5 = new Cons("l", c4);
			Cons c6 = new Cons("add", c5);
			Cons c7 = new Cons(c3);
			Cons c8 = new Cons(c6, c7);
			Cons c9 = new Cons("define", c8);
			return c9;
		}

		private static void Print2(Cons item) {
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
			else if (node is LinkedListNode<AST> consLikNode) {
				if (consLikNode.Value == null) {
					Console.WriteLine("null");
				}
				else {
					Console.WriteLine(consLikNode.Value);
				}
			}
			else if (node is AST baseCons) {
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
