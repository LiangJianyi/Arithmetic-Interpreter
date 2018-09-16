using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arithmetic_Interpreter_UWP;

namespace Test {
	class Program {
		static void Main(string[] args) {
			//Test3();
			Atom atom1 = new Atom("1");
			Atom atom2 = new Atom("2");
			Procedure plus1 = new Procedure("+");
			BaseCons expr1 = new Cons2(plus1, new Cons2(atom1, atom2));

			Atom atom3 = new Atom("1");
			Atom atom4 = new Atom("2");
			Procedure plus2 = new Procedure("+");
			BaseCons expr2 = new Cons2(plus2, new Cons2(atom3, atom4));

			Console.WriteLine(expr1 == expr2);
			Console.WriteLine(expr1 != expr2);
			Console.WriteLine(expr1.Equals(expr2));

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
			Cons2 c5 = new Cons2("l", c4);
			Cons2 c6 = new Cons2("add", c5);
			Cons2 c7 = new Cons2(c3);
			Cons2 c8 = new Cons2(c6, c7);
			Cons2 c9 = new Cons2("define", c8);
			Dictionary<string, Cons2> ccc = new Dictionary<string, Cons2>() {
				{ "c1", c1 },
				{ "c2", c2 },
				{ "c3", c3 },
				{ "c4", c4 },
				{ "c5", c5 },
				{ "c6", c6 },
				{ "c7", c7 },
				{ "c8", c8 },
				{ "c9", c9 }
			};
			foreach (var item in ccc) {
				Console.WriteLine(item.Key);
				Console.WriteLine($"cons.Car.Previous: {item.Value.Car.Previous}");
				Console.WriteLine($"cons.Car.Next: {item.Value.Car.Next}");
				Console.WriteLine($"cons.Cdr.Previous: {item.Value.Cdr.Previous}");
				Console.WriteLine($"cons.Cdr.Next: {item.Value.Cdr.Next}");
				Console.WriteLine($"cons.CarValue: {item.Value.CarValue}");
				Console.WriteLine($"cons.CdrValue: {item.Value.CdrValue}");
			}
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

		private static void ConsIterator(BaseCons cons, Action<BaseCons> f) {
			void iterator(BaseCons c) {
				if (c is Cons2 cons2) {
					if (cons2.CarValue!=null) {
						if (cons2.CarValue is Atom atom) {
							f(atom);
						}
						else if (cons2.CarValue is Cons2 subcons) {
							iterator(subcons);
						}
						else {
							throw new InvalidCastException();
						}
					}
					else {
						throw new NullReferenceException();
					}
				}
				else if (c is Atom atom) {
					f(atom);
				}
				else {

				}
			}
		}
	}
}
