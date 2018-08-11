using System;
using System.Collections.Generic;

/*
 (define (parse tokens)
  (define list-begin-marks (list #\( #\[))
  (define list-end-marks (list #\) #\]))
  (define (f)
    (if [null? tokens]
        null
        [cond [(list? (member [car tokens] list-begin-marks))
               (set! tokens [cdr tokens])
               (cons (f) (f))]
              [(list? (member [car tokens] list-end-marks))
               (set! tokens [cdr tokens])
               null]
              [else
               (cons [car tokens]
                     (begin
                       (set! tokens [cdr tokens])
                       (f)))]]))
  (f))
	 */

namespace Arithmetic_Interpreter_UWP {
	public class Parser {
		private readonly List<string> _beginMarks = new List<string>() { "(", "[" };
		private readonly List<string> _endMarks = new List<string>() { ")", "]" };
		private readonly LinkedList<string> _tokens;

		public Parser(LinkedList<string> tokens) {
			this._tokens = tokens;
		}

		public Cons<dynamic> GenerateAst() {
			Cons<dynamic> f(Func<Cons<dynamic>> x) {
				_tokens.RemoveFirst();
				return x();
			}

			if (_tokens == null) {
				return null;
			}
			else {
				if (_beginMarks.Contains(_tokens.First.Value)) {
					return f(() => new Cons<dynamic>(this.GenerateAst(), this.GenerateAst()));
				}
				else if (_endMarks.Contains(_tokens.First.Value)) {
					return f(() => null);
				}
				else {
					return new Cons<dynamic>(_tokens.First.Value, f(this.GenerateAst));
				}
			}
		}
	}
}
