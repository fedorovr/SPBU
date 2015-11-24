printfn "Print a string"
let strn = System.Console.ReadLine()
printfn "Print a pattern"
let ptrn = System.Console.ReadLine()  

let cache = new System.Collections.Generic.Dictionary<(string*string), int> ()

let rec occurs pattern string1 = 
    if cache.ContainsKey (pattern, string1) then 
        cache.[(pattern, string1)]
    else
        let count = 
            match (pattern, string1) with
            | ("", _) -> 1
            | (_, "") -> 0
            | (_, _)  -> if (pattern.[0] = string1.[0]) then
                            (occurs pattern.[1..] string1.[1..] + occurs pattern.[0..] string1.[1..])
                         else
                            occurs pattern.[0..] string1.[1..]
        cache.Add((pattern, string1), count)  
        count             

printfn "%A" (occurs ptrn strn)