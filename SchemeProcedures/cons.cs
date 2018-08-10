using System;
using System.Collections.Generic;

namespace Arithmetic_Interpreter_UWP {
	public class Cons<T> : LinkedList<T> {
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
	}
}
