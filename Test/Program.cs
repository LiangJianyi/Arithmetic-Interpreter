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
			print("LinkedList<int> lik = new LinkedList<int>()");
			print(lik.First);
			print(lik.Last);
			lik.AddFirst(new LinkedListNode<int>(1));
			print("lik.AddFirst(new LinkedListNode<int>(1))");
			print(lik.First);
			print(lik.Last);
			lik.AddAfter(lik.First, 2);
			print("lik.AddAfter(lik.First, 2)");
			print(lik.First.Next);
			print("lik.AddBefore(lik.First, 0");
			lik.AddBefore(lik.First, 0);
			print(lik.First);
			print(lik.Last);
		}

		private static void Test3() {
			Cons2<int> c1 = new Cons2<int>(1, 2);
			print(c1.Car());
			print(c1.Cdr());
		}

		private static void print(object node) {
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
			else if (node is LinkedListNode<int> intLikNode) {
				if (intLikNode==null) {
					Console.WriteLine("null");
				}
				else {
					Console.WriteLine(intLikNode.Value);
				}
			}
		}
	}
}
