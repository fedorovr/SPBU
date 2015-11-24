open System.Threading
open System

let tolerance = 1e-9
let countOfTasks = Environment.ProcessorCount

let mutable answerList : float List = []
let mutable answerArray = Array.create countOfTasks 0.0

let mutable deg = 0.0
let mutable polynom = []
let mutable fromX = 0.0
let mutable toX = 0.0
let mutable dx = 0.0
let mutable taskLength = 0.0

let calc fromA toB = 
    let mutable i = fromA
    let mutable currentAnswer = 0.0
    while ((i < toB) && (abs(i - toB) > tolerance)) do
        let mutable currentDegree = deg
        for p in polynom do
            currentAnswer <- currentAnswer + p * (i ** currentDegree) * dx
            currentDegree <- currentDegree - 1.0
        i <- i + dx
    currentAnswer

let calcAsync step = 
    async {
        answerArray.[step] <- calc (fromX + (float)step * taskLength) (fromX + (float)(step + 1) * taskLength)
    }

[<EntryPoint>]
let main argv = 
    let filePath = "inp.txt"
    let numbers = System.IO.File.ReadAllLines(filePath)
    let numbers = numbers.[0].Split()
    let numArray : float [] = [| for n in numbers -> (System.Double.Parse(n)) |]
    deg <- numArray.[0]
    polynom <- [for i in 0 .. (int)deg do yield numArray.[i + 1]] 
    fromX <- numArray.[(int)deg + 2]
    toX <- numArray.[(int)deg + 3]
    dx <- numArray.[(int)deg + 4]
    taskLength <- (toX - fromX) / (float)countOfTasks

    let sw = System.Diagnostics.Stopwatch.StartNew()
    
    let tasks = [for i in 0 .. (countOfTasks - 1) -> calcAsync i]

    Async.RunSynchronously (Async.Parallel tasks) |> ignore 
    sw.Stop()   
    printf "Using parallel computation " 
    printfn "Answer is %.3f calculated in %.2f ms" (Array.sum answerArray) sw.Elapsed.TotalMilliseconds

    let sw' = System.Diagnostics.Stopwatch.StartNew()
    printf "Without using parallel computation "
    printfn "Answer is %.3f calculated in %.2f ms" (calc fromX toX) sw'.Elapsed.TotalMilliseconds
    sw'.Stop()
    0