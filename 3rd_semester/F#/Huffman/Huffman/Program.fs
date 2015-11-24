open NUnit.Framework
open FsUnit

type binaryTree =
    | Node of int * char * binaryTree * binaryTree
    | Empty

let input = System.IO.File.ReadAllText(@"input.txt")
let (a : int array) = Array.zeroCreate 128

let allChars = [for c in input -> c]

allChars |> List.iter (fun x -> a.[(int)x] <- a.[(int)x] + 1)

//charList is a List of binaryTree's leaves, every element contains a char and a number of chars in the text
let charList = [for x in 0..127 do 
                       if a.[x] > 0 then
                           yield  Node(a.[x], (char)x, Empty, Empty)]   

let merge (a, b) =
    let getInt = function
        | Node(i, c, b1, b2) -> i
        | Empty -> failwith "Error"
    Node(((getInt a) + (getInt b)), '0', a, b)

//buildTree recurrently merges 2 currently rarest chars into one, the result is a binary Tree
let rec buildTree treeList =
    match (List.length treeList) with
        | 1 ->  treeList.Head
        | _ ->  let sortedTreeList = List.sort treeList
                buildTree (merge (sortedTreeList.Head, sortedTreeList.Tail.Head) :: sortedTreeList.Tail.Tail)

let myTree = buildTree charList

let rec getCodes accList tree code =
    match tree with
       | Node (i, c, left, right) -> 
           match c with
           | '0' -> let accList = getCodes accList left (code + "0")
                    let accList = getCodes accList right (code + "1")
                    accList
           | _ ->   let accList = (c, code) :: accList
                    accList
       | Empty
           -> accList

let charsList = getCodes [] myTree ""
let charsMap = Map.ofList charsList
let charsArray = Array.ofList charsList
printfn "%A" charsList

let rec encode (str: string) = 
    match str.Length with
    | 0 -> ""
    | _ -> ((charsMap.TryFind str.[0]).Value) + encode(str.[1..])

let output = encode input
printfn "%A" output

let codesList =
    [for i in charsArray do
        yield (snd(i), fst(i))]

let codesMap = Map.ofList codesList

let rec decode (str: string) i (accStr: string) (codesMap: Map<string, char>)= 
    match str.Length with
    | 0 -> accStr
    | _ -> 
            match (codesMap.TryFind str.[0..i]) with
            | None -> decode str (i+1) accStr codesMap
            | Some(x) -> decode str.[(i+1)..] 0 (accStr + (string)x) codesMap

let input2 = decode output 0 "" codesMap
printfn "%A" input2

[<TestFixture>]
module test =
    [<Test>]
    let ``createCodes`` () =
        let testInput = "aabbbcdeeeeeeee"
        let (testA : int array) = Array.zeroCreate 128
        let testAllChars = [for c in testInput -> c]
        testAllChars |> List.iter (fun x -> testA.[(int)x] <- testA.[(int)x] + 1)
        let testCharList = [for x in 0..127 do 
                                if testA.[x] > 0 then
                                    yield  Node(testA.[x], (char)x, Empty, Empty)]   
        let testMyTree = buildTree testCharList
        let res = getCodes [] testMyTree ""
        res |> should equal [('e', "1"); ('a', "011"); ('d', "0101"); ('c', "0100"); ('b', "00")]
    [<Test>]
    let ``decode`` () =
        let testOutput = "0110110000000100010111111111"
        let testCharsArray = Array.ofList [('e', "1"); ('a', "011"); ('d', "0101"); ('c', "0100"); ('b', "00")]
        let testCodesList =
            [for i in testCharsArray do
                yield (snd(i), fst(i))]
        let testCodesMap = Map.ofList testCodesList
        let res = decode testOutput 0 "" testCodesMap
        res |> should equal "aabbbcdeeeeeeee"