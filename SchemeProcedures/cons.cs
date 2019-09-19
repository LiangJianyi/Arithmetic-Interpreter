using System;
using System.Collections.Generic;

namespace Arithmetic_Interpreter_UWP {
	public abstract class AST {
		protected string _lexical;

		public static void ConsIterator(AST cons, Action<AST> f) {
			if (cons is Cons c) {
				if (c.CarValue != null) {
					if (c.CarValue is Atom atom) {
						f(atom);
						if (c.CdrValue != null) {
							ConsIterator(c.CdrValue, f);
						}
					}
					else if (c.CarValue is Cons) {
						ConsIterator(c.CarValue, f);
						if (c.CdrValue != null) {
							ConsIterator(c.CdrValue, f);
						}
					}
					else {
						throw new InvalidCastException();
					}
				}
				else {
					throw new NullReferenceException();
				}
			}
			else if (cons is Atom atom) {
				f(atom);
			}
			else {
				throw new InvalidCastException();
			}
		}
	}

	public class Cons : AST {
		private LinkedList<AST> _lik = new LinkedList<AST>();

		public Cons(string car) {
			this._lik.AddFirst(new Atom(car));
			this._lik.AddAfter(this._lik.First, new LinkedListNode<AST>(null));
		}

		public Cons(AST car) {
			this._lik.AddFirst(car);
			this._lik.AddAfter(this._lik.First, new LinkedListNode<AST>(null));
		}

		public Cons(string car, AST cdr) {
			this._lik.AddFirst(new Atom(car));
			this._lik.AddAfter(this._lik.First, cdr);
		}

		public Cons(AST car, AST cdr) {
			this._lik.AddFirst(new LinkedListNode<AST>(car));
			this._lik.AddAfter(this._lik.First, cdr);
		}

		public LinkedListNode<AST> Car => this._lik.First;
		public LinkedListNode<AST> Cdr => this._lik.Last;
		public AST CarValue => this._lik.First.Value;
		public AST CdrValue => this._lik.Last.Value;
		public void SetCar(AST car) => this._lik.First.Value = car;
		public void SetCdr(AST cdr) => this._lik.Last.Value = cdr;
	}

	public class Atom : AST {
		public Atom() {
			base._lexical = string.Empty;
		}

		public Atom(string lex) {
			base._lexical = lex;
		}

		public override string ToString() {
			return base._lexical;
		}
	}
}
