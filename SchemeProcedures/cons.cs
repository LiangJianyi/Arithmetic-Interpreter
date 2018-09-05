﻿using System;
using System.Collections.Generic;

namespace Arithmetic_Interpreter_UWP {
	public class Cons<T> {
		private T _car;
		private T _cdr;

		public Cons(T car, T cdr) {
			this._car = car;
			this._cdr = cdr;
		}

		public T Car {
			get => this._car;
			set
			{
				this._car = value;
			}
		}
		public T Cdr {
			get => this._cdr;
			set
			{
				this._cdr = value;
			}
		}

		public static T GetCar(Cons<T> cons) => cons._car;
		public static T GetCdr(Cons<T> cons) => cons._cdr;

		public override bool Equals(object obj) {
			if (obj is Cons<T> target) {
				return EqualityComparer<T>.Default.Equals(this.Car, target.Car) && EqualityComparer<T>.Default.Equals(this.Cdr, target.Cdr);
			}
			else {
				return false;
			}
		}
		public override int GetHashCode() {
			return this._car.GetHashCode() ^ this._cdr.GetHashCode();
		}
		public override string ToString() {
			return $"cons({this._car}, {this._cdr})";
		}
		public static bool operator ==(Cons<T> consLeft, Cons<T> consRight) =>
			EqualityComparer<T>.Default.Equals(consLeft.Car, consRight.Car) && EqualityComparer<T>.Default.Equals(consLeft.Cdr, consRight.Cdr);
		public static bool operator !=(Cons<T> consLeft, Cons<T> consRight) =>
			!(EqualityComparer<T>.Default.Equals(consLeft.Car, consRight.Car) && EqualityComparer<T>.Default.Equals(consLeft.Cdr, consRight.Cdr));
	}

	public class Cons<Tcar, Tcdr> {
		private Tcar _car;
		private Tcdr _cdr;

		public Cons(Tcar car, Tcdr cdr) {
			this._car = car;
			this._cdr = cdr;
		}

		public Tcar Car {
			get => this._car;
			set
			{
				this._car = value;
			}
		}
		public Tcdr Cdr {
			get => this._cdr;
			set
			{
				this._cdr = value;
			}
		}

		public static Tcar GetCar(Cons<Tcar, Tcdr> cons) => cons._car;
		public static Tcdr GetCdr(Cons<Tcdr, Tcdr> cons) => cons._cdr;

		public override bool Equals(object obj) {
			if (obj is Cons<Tcar, Tcdr> target) {
				return EqualityComparer<Tcar>.Default.Equals(this.Car, target.Car) && EqualityComparer<Tcdr>.Default.Equals(this.Cdr, target.Cdr);
			}
			else {
				return false;
			}
		}
		public override int GetHashCode() {
			return this._car.GetHashCode() ^ this._cdr.GetHashCode();
		}
		public override string ToString() {
			return $"cons({this._car}, {this._cdr})";
		}
		public static bool operator ==(Cons<Tcar, Tcdr> consLeft, Cons<Tcar, Tcdr> consRight) =>
			EqualityComparer<Tcar>.Default.Equals(consLeft.Car, consRight.Car) && EqualityComparer<Tcdr>.Default.Equals(consLeft.Cdr, consRight.Cdr);
		public static bool operator !=(Cons<Tcar, Tcdr> consLeft, Cons<Tcar, Tcdr> consRight) =>
			!(EqualityComparer<Tcar>.Default.Equals(consLeft.Car, consRight.Car) && EqualityComparer<Tcdr>.Default.Equals(consLeft.Cdr, consRight.Cdr));
	}

	public abstract class BaseCons {
		protected string _lexical;
	}

	public class Cons2 : BaseCons {
		private LinkedList<BaseCons> _lik = new LinkedList<BaseCons>();

		public Cons2(string car) {
			this._lik.AddFirst(new LinkedListNode<BaseCons>(new Atom(car)));
			this._lik.AddAfter(this._lik.First, null);
		}

		public Cons2(Cons2 car, string cdr = null) {
			this._lik.AddFirst(new LinkedListNode<Cons2>(car));
			this._lik.AddAfter(this._lik.First, new Cons2(cdr));
		}

		public Cons2(string car, Cons2 cdr) {
			this._lik.AddFirst(new LinkedListNode<Cons2>(new Cons2(car)));
			this._lik.AddAfter(this._lik.First, cdr);
		}

		public Cons2(Cons2 car, Cons2 cdr = null) {
			this._lik.AddFirst(new LinkedListNode<Cons2>(car));
			this._lik.AddAfter(this._lik.First, cdr);
		}

		public LinkedListNode<Cons2> Car() => this._lik.First;
		public LinkedListNode<Cons2> Cdr() => this._lik.Last;
		public void SetCar(Cons2 car) => this._lik.AddFirst(car);
		public void SetCdr(Cons2 cdr) => this._lik.AddLast(cdr);
	}

	public class Atom : BaseCons {
		public Atom() {
			base._lexical = string.Empty;
		}

		public Atom(string lex) {
			base._lexical = lex;
		}
	}

	public class Procedure : BaseCons {
		public Procedure(string lex) {
			base._lexical = lex;
		}
	}
}
