module HashMap =

    open System
    open System.Collections.Generic
    open Microsoft.FSharp.Collections

    let tableSize = 1047

    let add (myMap : List<'K * 'V> []) ((key' : 'K), (value' : 'V)) =
        let hashcode = key'.GetHashCode() % Array.length myMap
        myMap.[hashcode] <- (key', value') :: myMap.[hashcode]
        myMap
        
    let find (myMap : List<'K * 'V> []) (key' : 'K) =
        let mutable answer = None
        let hashcode = key'.GetHashCode() % Array.length myMap
        for p in myMap.[hashcode] do
            if (fst p = key') then answer <- Some(snd p)
        answer

    let delete (myMap : List<'K * 'V> []) (key' : 'K) =
        let hashcode = key'.GetHashCode() % Array.length myMap
        myMap.[hashcode] <- myMap.[hashcode] |> List.filter (fun (p : 'K * 'V) -> not((fst p).Equals(key')))
        myMap

    type Map<'K, 'V when 'K: comparison and 'V: equality> (myMap: List<'K * 'V> [])= 
        new (source:seq<'K * 'V>) =
            let myMap = Array.create tableSize List.empty
            Seq.iter (fun x -> add myMap x |> ignore) source
            new Map<_, _>(myMap)

        new () =
            let myMap = Array.create tableSize []
            new Map<_, _>(myMap)

        member this.Add (k : 'K, v : 'V) = new Map<_, _>(add myMap (k, v))
        member this.Remove k = new Map<_, _>(delete myMap k)
        member this.TryFind k = find myMap k
        member this.ContainsKey k = this.TryFind k |> Option.isSome
        member this.Item k = (this.TryFind k).Value
        member this.Count = 
            let mutable count = 0
            for i in myMap do
                count <- count + i.Length
            count
            
        member this.IsEmpty() = 
            let mutable isEmpty = true
            for i in myMap do
                if not(i.IsEmpty) then
                    isEmpty <- false
            isEmpty

        override this.ToString() = 
            let mutable output = ""
            for list in myMap do
                for e in list do
                    output <- e.ToString() + "; " + output
            output
    
        override this.Equals anotherMap =
            match anotherMap with
            | :? Map<'K, 'V> as anotherMap -> (anotherMap.Count = this.Count) && (Seq.forall2 (=) this anotherMap)
            | _ -> false

        override this.GetHashCode() =
            let mutable hash = 0
            for list in myMap do
                for e in list do
                    hash <- e.GetHashCode() + hash
            hash

        member private this.getEnumerator() = 
            let allElements  = ref [for list in myMap do
                                    for e in list do
                                            yield e]

            let currentList = allElements

            let current() = 
                match (!currentList).Length with
                | 0 -> failwith "No more elemnts"
                | _ -> (!currentList).Head
            
            { new IEnumerator<'K * 'V> with
                    member enum.Current = current()
                interface System.Collections.IEnumerator with
                    member enum.Current = current() |> box
                    member enum.MoveNext() = 
                        currentList := (!currentList).Tail
                        not (!currentList).IsEmpty            
                    member enum.Reset() = currentList := !allElements
                interface System.IDisposable with 
                    member enum.Dispose() = () 
            }       


        interface IEnumerable<'K * 'V> with
            member this.GetEnumerator() =  this.getEnumerator()
        interface System.Collections.IEnumerable with
            member this.GetEnumerator() = this.getEnumerator() :> System.Collections.IEnumerator

    
//
let a = (1, "1") :: (2, "02") :: (3, "003") :: []
let s = List.toSeq a
let m = Map(s)
let m1 = m.Add (10, "10") 
printfn "%A" (m1.TryFind(1))