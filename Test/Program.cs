using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arithmetic_Interpreter_UWP;

namespace Test {
	class Program {
		static void Main(string[] args) {
			Test3();
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


		private static void Test2() {
			LinkedList<int> lik = new LinkedList<int>();
			Print("LinkedList<int> lik = new LinkedList<int>()");
			Print(lik.First);
			Print(lik.Last);
			lik.AddFirst(new LinkedListNode<int>(1));
			Print("lik.AddFirst(new LinkedListNode<int>(1))");
			Print(lik.First);
			Print(lik.Last);
			lik.AddAfter(lik.First, 2);
			Print("lik.AddAfter(lik.First, 2)");
			Print(lik.First.Next);
			Print("lik.AddBefore(lik.First, 0");
			lik.AddBefore(lik.First, 0);
			Print(lik.First);
			Print(lik.Last);
		}

		private static void Test3() {
			Cons2 c1 = new Cons2("r");
			Cons2 c2 = new Cons2("l", c1);
			Cons2 c3 = new Cons2("+", c2);
			Cons2 c4 = new Cons2("r");
			Cons2 c5 = new Cons2("1", c4);
			Cons2 c6 = new Cons2("add", c5);
			Cons2 c7 = new Cons2(c3);
			Cons2 c8 = new Cons2(c6, c7);
			Cons2 c9 = new Cons2("define", c8);
			List<Cons2> ccc = new List<Cons2>() { c1, c2, c3, c4, c5, c6, c7, c8, c9 };
			//foreach (var item in ccc) {
			//	Print(item.Car().Previous);
			//	Print(item.Car().Next);
			//	Print(item.Cdr().Previous);
			//	Print(item.Cdr().Next);
			//	Print(item.CarValue());
			//	Print(item.CdrValue());
			//}
			//Print2(c9);
			LinkedList<Cons2> fuck = new LinkedList<Cons2>();
			fuck.AddFirst(c9);
			Console.WriteLine(fuck.First.Value.CarValue());
			Console.WriteLine(fuck.Last.Value.CarValue());
			Console.WriteLine(fuck.First.Previous);
			Console.WriteLine(fuck.First.Next);
			Console.WriteLine(fuck.Last.Previous);
			Console.WriteLine(fuck.Last.Next);
		}

		private static void Print2(Cons2 item) {
			Print(item.Car().Previous);
			Print(item.Car().Next);
			Print(item.Cdr().Previous);
			Print(item.Cdr().Next);
			Print(item.CarValue());
			Print(item.CdrValue());
		}

		private static void Print(object node) {
			if (node == null) {
				Console.WriteLine("null");
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
