type Expr = 
    | Const of int
    | Var of string
    | Add of Expr * Expr
    | Sub of Expr * Expr
    | Mul of Expr * Expr
    | Div of Expr * Expr

let rec simple expr =
    match expr with
    | Const x -> Const x

    | Var str -> Var str

    | Add (Const a, Const b) -> Const (a+b)
    | Add (Const 0, x) | Add (x, Const 0) -> x
    | Add (x, y) -> let x' = simple x
                    let y' = simple y
                    simple (Add (x', y'))

    | Sub (Const a, Const b) -> Const (a-b)
    | Sub (Var str, Var str') -> Const (System.Convert.ToInt32(str <> str'))  
    | Sub (x, y) -> let x' = simple x
                    let y' = simple y
                    simple (Sub (x', y'))

    | Mul (Const a, Const b) -> Const (a*b)
    | Mul (Const 0, x) | Add (x, Const 0) -> Const 0
    | Mul (Const 1, x) | Add (x, Const 1) -> x
    | Mul (x, y) -> let x' = simple x
                    let y' = simple y
                    simple (Mul (x', y'))

    | Div (Const a, Const b) -> Const (a/b)
    | Div (x, Const 1) -> x 
    | Div (x, y) -> let x' = simple x
                    let y' = simple y
                    simple (Mul (x', y'))

printfn "%A" (simple (Add (Mul (Const 1, Var "x"), (Sub (Var "zs", Var "zs")))))
