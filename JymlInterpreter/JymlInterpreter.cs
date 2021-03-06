﻿using System;
using Arithmetic_Interpreter_UWP;
using JymlTypeSystem;

/*
 * #lang racket
(require liangjianyi-racket/linkedlist)
(require "./jyml-parser.rkt")
(require "./exec-enviroment.rkt")

(provide eval)
(provide inferred-type)
(provide result-set)

(define (add node)
  (letrec ([f (lambda (i k)
                (if [= i 0]
                    (k [list-ref node i])
                    (f [- i 1] (lambda (x) (k (+ x [list-ref node i]))))))])
    (f (- (length node) 1) (lambda (x) x))))
(define (sub node)
  (letrec ([f (lambda (i k)
                (if [= i 0]
                    (k [list-ref node i])
                    (f [- i 1] (lambda (x) (k (- x [list-ref node i]))))))])
    (f (- (length node) 1) (lambda (x) x))))
(define (multi node)
  (letrec ([f (lambda (i k)
                (if [= i 0]
                    (k [list-ref node i])
                    (f [- i 1] (lambda (x) (k (* x [list-ref node i]))))))])
    (f (- (length node) 1) (lambda (x) x))))
(define (div node)
  (letrec ([f (lambda (i k)
                (if [= i 0]
                    (k [list-ref node i])
                    (f [- i 1] (lambda (x) (k (/ x [list-ref node i]))))))])
    (f (- (length node) 1) (lambda (x) x))))


;;; 判断一个数据是不是过程就看它是否在 procedures 表中存在
;;; 在表中登记的数据（关键字）视为 procedure，其类型为 symbol
;;; 当一个对象赋值为 lambda，那么该对象名就需要在表中登记
;;;
(define procedures null)
(set! procedures (append-linkedlist procedures [mcons (lambda (dispatch)
                                                        (cond [(eq? dispatch 'proc-name) '+]
                                                              [(eq? dispatch 'args-count) 'infinite]
                                                              [(eq? dispatch 'proc-body) add])) null]))
(set! procedures (append-linkedlist procedures [mcons (lambda (dispatch)
                                                        (cond [(eq? dispatch 'proc-name) '-]
                                                              [(eq? dispatch 'args-count) 'infinite]
                                                              [(eq? dispatch 'proc-body) sub])) null]))
(set! procedures (append-linkedlist procedures [mcons (lambda (dispatch)
                                                        (cond [(eq? dispatch 'proc-name) '*]
                                                              [(eq? dispatch 'args-count) 'infinite]
                                                              [(eq? dispatch 'proc-body) multi])) null]))
(set! procedures (append-linkedlist procedures [mcons (lambda (dispatch)
                                                        (cond [(eq? dispatch 'proc-name) '/]
                                                              [(eq? dispatch 'args-count) 'infinite]
                                                              [(eq? dispatch 'proc-body) div])) null]))

(define (get-proc-by-symbol key)
  (with-handlers ([exn:fail? (lambda (e) (error "未绑定的 procedure: " key))])
    [mcar (get-element-by-value procedures key (lambda (x) (x 'proc-name)))]))

;(define (calc node)
;  (define left [mcar node])
;  (define right [mcdr node])
;  (cond [(symbol? left) ([get-proc-by-symbol procedures left] right)]
;        [(number? left) left]
;        [(mpair? left) (calc [mcar left]) (calc [mcdr left])]))


;;; name: 对象名字
;;; obj: 对象实体
;;; table: 环境对象表
(define (mydefine name obj table)
  ((table 'add) name obj))

; node 必须是 ast 的原子节点
(define (inferred-type node)
  (define number-collection (string->linkedlist "0123456789"))
  (define point #\.)
  (define minus-mark #\-)
  (define quote #\')
  (define double-quote #\")
  (define backslash #\\)
  (define boolean-collection (linkedlist "true" "false" "#t" "#f"))
  
  (define (build-number node)
    (define len (string-length node))
    ;;; 下面的 cond 完全可以通过 string->number 和异常处理来达到同样的目的，只不过我自己重新造了轮子罢了
    (cond [;;; 负号开头的分析过程
           (equal? (string-ref node 0) minus-mark)
           (cond [(= len 1) (error "类型无法识别: " node)]
                 [(or (= len 2) (= len 3)) (for ([e (substring node 1 len)])
                                             (when (or [not (find-node? number-collection e)]
                                                       [equal? point e])
                                               (error "类型无法识别: " node)))
                                           (string->number node)]
                 [(>= len 4) (if [or (equal? point (string-ref node 1)) (equal? point (string-ref node (- len 1)))]
                                 (error "类型无法识别: " node)
                                 (let ([point-count 0]
                                       [sub (substring node 1 len)])
                                   (for ([e sub])
                                     (if [> point-count 1]
                                         (error "类型无法识别: " node)
                                         (cond [(equal? point e) (set! point-count (+ point-count 1))]
                                               [(not (find-node? number-collection e)) (error "类型无法识别: " node)])))
                                   (string->number node)))])]
          [;;; 数字开头的分析过程
           (find-node? number-collection (string-ref node 0))
           (cond [(<= len 2)
                  (for ([e node])
                    (when [not (find-node? number-collection e)]
                      (error "类型无法识别: " node)))
                  (string->number node)]
                 [(>= len 3)
                  (let ([point-count 0]
                        [sub (substring node 1 (- len 1))])
                    (for ([e sub])
                      (if [> point-count 1]
                          (error "类型无法识别" node)
                          (cond [(equal? point e) (set! point-count (+ point-count 1))]
                                [(not (find-node? number-collection e)) (error "类型无法识别: " node)])))
                    (string->number node))])]))
  
  (define (lexica->boolean lex)
    (cond [(equal? lex "true") true]
          [(equal? lex "false") false]
          [(equal? lex "#t") #t]
          [(equal? lex "#f") #f]
          (error "类型无法识别: " lex)))

  (define (lexica->string lex)
    (substring lex 1 (- (string-length lex) 1)))
  
  [cond [(find-node? boolean-collection node) (lexica->boolean node)]
        [(and (equal? double-quote [string-ref node 0])
              (equal? double-quote [string-ref node (- [string-length node] 1)])) (lexica->string node)]
        [(find-node? procedures (string->symbol node)) (string->symbol "procedure")]
        [(equal? node "null") null]
        [else (cond [(equal? (string-ref node 0) quote) (string->symbol node)]
                    [(or (equal? (string-ref node 0) minus-mark)
                         (find-node? number-collection (string-ref node 0)))
                     (build-number node)]
                    [else (error "类型无法识别: " node)])]])


(define (eval ast)
  (cond [(pair? ast)
         (eval [car ast])
         (eval [cdr ast])]
        [(or [number? ast]
             [symbol? ast]
             [char? ast]
             [string? ast]
             [boolean? ast]
             [null? ast])
         ast]
        [else
         (((get-proc-by-symbol [string->symbol [car ast]]) 'proc-body) [cdr ast])]))

; 存放由 eval 返回 top-level 的值
(define result-set null)
; ast 必须是一个经过类型推导的 ast
(define (eval-2 ast)
  (cond [(or [number? ast]
             [symbol? ast]
             [char? ast]
             [string? ast]
             [boolean? ast]
             [null? ast])
         (set! result-set (list (append result-set ast)))]
        [(pair? ast)
         (if [pair? [car ast]]
             (eval-2 [car ast])
             (set! result-set
                   (list (append ast (((get-proc-by-symbol [string->symbol [car ast]]) 'proc-body) [cdr ast])))))
         ;(eval-2 [car ast])
         ;(eval-2 [cdr ast])
         ]))

(define ast (list (list "+" 1 2 3 4 5)))
(eval-2 ast)
result-set
 */

namespace JymlInterpreter {
	public static class JymlInterpreter {
		public static JymlType Eval(AST ast) {
            JymlRuntime.Environment.AddTable(new JymlRuntime.ObjectTable(0));
            if (ast is Cons c) {
                if (c.CarValue != null) {
                    if (c.CarValue is Atom atom) {
                        InferredType(atom);
                        if (c.CdrValue != null) {
                            Eval(c.CdrValue);
                        }
                    }
                    else if (c.CarValue is Cons) {
                        Eval(c.CarValue);
                        if (c.CdrValue != null) {
                            Eval(c.CdrValue);
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
            else if (ast is Atom atom) {
                InferredType(atom);
            }
            else {
                throw new InvalidCastException();
            }
        }
        public static void InferredType(Atom atom) {
            string lexical = atom.ToString();
            
        }
        public static void InferredType(AST ast) {
            
        }
		public static void ResultSet() {

		}
	}
}
