open NUnit.Framework
open FsUnit

type LineElement =
    | Word of string
    | Space of int

let lines = System.IO.File.ReadAllText(@"lines.txt")

let makeList (text : string) =
    text.Split [|' '|] |> Array.toList

let l = makeList lines

//printfn "Enter width of a line"
//let width = System.Int32.Parse(System.Console.ReadLine()) 
let width = 11

let rec makeLine acc (list: string List) currentList thisLineWasFinished =
    if ((list.Length > 0) && (not(thisLineWasFinished)) && ((list.Head.Length + acc) <= width)) then
        if ((list.Length > 1) && ((acc + list.Head.Length + 1 + list.Tail.Head.Length) <= width)) then  
            let acc = (acc + list.Head.Length + 1)
            let currentList = Word(list.Head + " ") :: currentList
            makeLine acc list.Tail currentList false
        else    
            let acc = (acc + list.Head.Length)
            let currentList = Word(list.Head) :: currentList
            makeLine acc list.Tail currentList true
    else
        (Space(width - acc) :: currentList, list)

let rec makeListOfLines acc l2=
    match l2 with
        | [] -> acc
        | _ 
             -> let acc = (List.rev(fst(makeLine 0 l2 [] false)) :: acc)
                makeListOfLines acc (snd(makeLine 0 l2 [] false))

let linesList = List.rev(makeListOfLines ([(List.rev(fst(makeLine 0 l [] false)))]) (snd(makeLine 0 l [] false)))

printfn "Enter 'left', 'center', 'right' or 'width' to organize words in a line"
let lineType = System.Console.ReadLine()

let getSpaces (ll : LineElement) =
    match ll with 
    | Space(q) -> q
    | Word(a) -> 0

let rec createList acc len count extra = 
    match len with
    | 0 -> acc
    | _ when extra > 0
        -> let acc = Space(count + 1) :: acc 
           createList acc (len - 1) count (extra - 1)
    | _ when extra <= 0
        -> let acc = Space(count) :: acc 
           createList acc (len - 1) count extra

let rec mergeTwoLists l1 l2 =
    match l1, l2 with
    | [],l | l,[] -> l
    | l1h::l1', l2h::l2' -> l1h :: l2h :: (mergeTwoLists l1' l2')
        

let alignment lType (lList: LineElement List List) =
    match lType with 
    | "width" 
        -> [ for i in lList do
                let i = List.rev(i)
                let t = i.Head
                let h = getSpaces t
                let i = List.rev(i.Tail)
                let q = createList [] (i.Length - 1) (h / (i.Length - 1)) (h % (i.Length - 1))
                yield (mergeTwoLists i q)
           ]
    | "center"
        -> [ for i in lList do
                let i = List.rev(i)
                let t = i.Head
                let h = getSpaces t
                let i = i.Tail
                let i = Space(h/2 + h%2) :: i
                yield Space(h/2) :: List.rev(i) 
           ]
    | "right"
        -> [ for i in lList do
                let i = List.rev(i)
                let t = i.Head
                let i = List.rev(i.Tail)
                yield t :: i 
           ]
    | _ 
        -> [ for i in lList do
                yield i 
           ]

let printText (myList: LineElement List List) =
    for i in myList do
        for j in i do
            match j with
            | Space(q)
                -> for k in 1..q do
                       printf " "
            | Word(s) -> printf "%s" s
        printfn ""
    
printfn "%A" linesList
let q = alignment lineType linesList
printfn "%A" q

printfn "%A" (printText q)

[<TestFixture>]
module test =
    [<Test>]
    let ``Make list of lines`` () =
        let input = ["Hello,"; "how"; "are"; "you?"; "I'm"; "fine!"]
        let res = List.rev(makeListOfLines ([(List.rev(fst(makeLine 0 input [] false)))]) (snd(makeLine 0 input [] false)))
        printfn "___%A___" res
        res |> should equal [[Word "Hello, "; Word "how"; Space 1];
                             [Word "are "; Word "you?"; Space 3];
                             [Word "I'm "; Word "fine!"; Space 2]]
    [<Test>]
    let ``AlignRight`` () =
        let input2 = [[Word "Hello, "; Word "how"; Space 1];
                      [Word "are "; Word "you?"; Space 3];
                      [Word "I'm "; Word "fine!"; Space 2]]
        let res = alignment "right" input2
        res |> should equal [[Space 1; Word "Hello, "; Word "how"];
                             [Space 3; Word "are "; Word "you?"];
                             [Space 2; Word "I'm "; Word "fine!"]]
    [<Test>]
    let ``AlignCenter`` () =
        let input2 = [[Word "Hello, "; Word "how"; Space 1];
                      [Word "are "; Word "you?"; Space 3];
                      [Word "I'm "; Word "fine!"; Space 2]]
        let res = alignment "center" input2
        res |> should equal [[Space 0; Word "Hello, "; Word "how"; Space 1];
                             [Space 1; Word "are "; Word "you?"; Space 2];
                             [Space 1; Word "I'm "; Word "fine!"; Space 1]] 
    [<Test>]
    let ``AlignWidth`` () =
        let input2 = [[Word "Hello, "; Word "how"; Space 1];
                      [Word "are "; Word "you?"; Space 3];
                      [Word "I'm "; Word "fine!"; Space 2]]
        let res = alignment "width" input2
        res |> should equal [[Word "Hello, "; Space 1; Word "how"];
                             [Word "are "; Space 3; Word "you?"];
                             [Word "I'm "; Space 2; Word "fine!"]] 