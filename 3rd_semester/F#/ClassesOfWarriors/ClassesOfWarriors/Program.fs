open System

type DirectionsEnum = Up = 'U' | Down = 'D' | Right = 'R' | Left = 'L'

let FIELD_HEIGHT = 5
let FIELD_WIDTH = 7
let MAX_HP = 100 

let mutable PoolOfNames = "Andrew" :: "Bobbie" :: "Charlie" :: "Donnie" :: "Erick" :: "Fernand" :: []

type FieldElement = WarriorOnField of Warrior | None
and Field private () = class
    static let instance = Field()
    let mutable _fieldArray = Array2D.create FIELD_WIDTH FIELD_HEIGHT FieldElement.None
    let mutable Field : Warrior List = []
    let mutable _printedCount = 0

    static member GetInstance = instance
    member this.GetListOfWarriors = Field
    member this.AddWarrior (newWarrior : Warrior) = Field <- List.append Field [newWarrior]
    member this.DeleteWarrior (delWarrior : Warrior) = Field <- List.filter (fun w -> w <> delWarrior) Field

    member this.PrintFieldElem (x : obj) =
        match x with
            | :? FieldElement as elem -> match elem with 
                                            | FieldElement.WarriorOnField(warrior) -> printf "%7s" warrior.GetWarriorName
                                            | FieldElement.None                    -> printf "___.___"
            | _ -> printf "___.___" 
        _printedCount <- _printedCount + 1
        if _printedCount = FIELD_WIDTH then 
            printf "\n"
            _printedCount <- 0

    member this.PrintField = 
        _printedCount <- 0
        for i in 0 .. FIELD_HEIGHT - 1 do
            for j in 0 .. FIELD_WIDTH - 1 do
                _fieldArray.SetValue(FieldElement.None, j, i)
        for k in Field do
            _fieldArray.SetValue(FieldElement.WarriorOnField(k), (k.GetXPos : int), (k.GetYPos : int))
        // !! test Array2D.iter (fun x -> this.PrintFieldElem x) _fieldArray
        for i in 0 .. FIELD_HEIGHT - 1 do
            for j in 0 .. FIELD_WIDTH - 1 do
                this.PrintFieldElem (_fieldArray.GetValue(j, i))
end
and [<AbstractClass>] Warrior (xPos : int, yPos : int, gunName : string, damagePoints : int, warriorName : string) = class
    let mutable _xPos = xPos
    let mutable _yPos = yPos
    let mutable _healthPoints = 100
    
    member this.GetXPos = _xPos
    member this.GetYPos = _yPos
    member this.GetGunName = gunName
    member this.GetWarriorName = warriorName
    member this.GetHealthPoints = _healthPoints
    abstract member SpecialAbility: unit -> unit

    member this.UpdateHP dh = 
        _healthPoints <- _healthPoints + dh
        if _healthPoints < 0 then
            Field.GetInstance.DeleteWarrior this
            printfn "%s is dead now" warriorName
        else
            printfn "Now %s has %d hp" warriorName _healthPoints
        

    member this.Move (direction : DirectionsEnum) =
        if (List.forall (fun (w : Warrior) -> (w.GetXPos <> _xPos || w.GetYPos <> _yPos)) Field.GetInstance.GetListOfWarriors.Tail) then
            match direction with
                | DirectionsEnum.Right -> if (_xPos + 1 < FIELD_WIDTH)  then _xPos <- _xPos + 1 else printfn "Not possible"
                | DirectionsEnum.Left  -> if (_xPos - 1 >= 0)           then _xPos <- _xPos - 1 else printfn "Not possible"
                | DirectionsEnum.Up    -> if (_yPos - 1 >= 0)           then _yPos <- _yPos - 1 else printfn "Not possible"
                | DirectionsEnum.Down  -> if (_yPos + 1 < FIELD_HEIGHT) then _yPos <- _yPos + 1 else printfn "Not possible"
                | _ -> ()
        else
            printfn "Imposible move"
                
    member this.GetInjuredWarrior (direction : DirectionsEnum) =
        let listOfWarriors = Field.GetInstance.GetListOfWarriors.Tail
        match direction with
            | DirectionsEnum.Right -> if (List.filter (fun (w : Warrior) -> w.GetYPos = _yPos && w.GetXPos > _xPos) listOfWarriors).IsEmpty then None else 
                                          WarriorOnField(listOfWarriors |> List.filter (fun (w : Warrior) -> w.GetYPos = _yPos && w.GetXPos > _xPos) |> List.minBy (fun (w : Warrior) -> w.GetXPos))
            | DirectionsEnum.Left  -> if (List.filter (fun (w : Warrior) -> w.GetYPos = _yPos && w.GetXPos < _xPos) listOfWarriors).IsEmpty then None else 
                                          WarriorOnField(listOfWarriors |> List.filter (fun (w : Warrior) -> w.GetYPos = _yPos && w.GetXPos < _xPos) |> List.maxBy (fun (w : Warrior) -> w.GetXPos))
            | DirectionsEnum.Up    -> if (List.filter (fun (w : Warrior) -> w.GetXPos = _xPos && w.GetYPos < _yPos) listOfWarriors).IsEmpty then None else
                                          WarriorOnField(listOfWarriors |> List.filter (fun (w : Warrior) -> w.GetXPos = _xPos && w.GetYPos < _yPos) |> List.maxBy (fun (w : Warrior) -> w.GetYPos))
            | DirectionsEnum.Down  -> if (List.filter (fun (w : Warrior) -> w.GetXPos = _xPos && w.GetYPos > _yPos) listOfWarriors).IsEmpty then None else 
                                          WarriorOnField(listOfWarriors |> List.filter (fun (w : Warrior) -> w.GetXPos = _xPos && w.GetYPos > _yPos) |> List.minBy (fun (w : Warrior) -> w.GetYPos))
            | _                    -> None

    member this.Shoot (direction : DirectionsEnum) =
        let injuredWarrior = this.GetInjuredWarrior direction
        printf "%s shoots using %s and " this.GetWarriorName this.GetGunName
        match injuredWarrior with
            | WarriorOnField(w) -> printfn "injures %s" w.GetWarriorName
                                   w.UpdateHP (-1 * damagePoints)
            | None ->              printfn "misses"
                            
end

type Medic(medicXPos, medicYPos) = class
    inherit Warrior(medicXPos, medicYPos, "Scar-H", 30, PoolOfNames.Head)
    override this.SpecialAbility () = 
        let incValue = 10
        printf "%s cures themself" this.GetWarriorName
        if incValue + this.GetHealthPoints > MAX_HP then this.UpdateHP (MAX_HP - this.GetHealthPoints) else this.UpdateHP incValue
end

type Support(supportXPos, supportYPos) = class
    inherit Warrior(supportXPos, supportYPos, "RPK", 35, PoolOfNames.Head)
    override this.SpecialAbility () = 
        System.Console.ForegroundColor <- System.ConsoleColor.Yellow
        let colorsArray = [|System.ConsoleColor.Blue; System.ConsoleColor.Cyan; System.ConsoleColor.Gray; System.ConsoleColor.Green; System.ConsoleColor.Magenta; System.ConsoleColor.Red|]
        let rnd = Random()
        System.Console.ForegroundColor <- colorsArray.[rnd.Next(colorsArray.Length)]
end

type Sniper(sniperXPos, sniperYPos) = class
    inherit Warrior(sniperXPos, sniperYPos, "Barret", 80, PoolOfNames.Head)
    override this.SpecialAbility () = 
        System.Console.Beep()
end

let createWarrior() =
    printfn "Enter width position"
    let xPos = System.Int32.Parse(System.Console.ReadLine()) 
    printfn "Enter height position"
    let yPos = System.Int32.Parse(System.Console.ReadLine())                       
    let currentField = Field.GetInstance
    let currentWarriors = currentField.GetListOfWarriors
    if PoolOfNames.IsEmpty then printf "There are too many warriors on field"
    else if ((xPos < FIELD_WIDTH && yPos < FIELD_HEIGHT) && (xPos >= 0 && yPos >= 0)) then
        if (List.forall (fun (w : Warrior) -> w.GetXPos <> xPos || w.GetYPos <> yPos) currentWarriors) then
            printfn "Enter warrior class: (Sniper, Support, Medic)"
            match System.Console.ReadLine() with 
                | "Sniper" ->  let newWarrior = new Sniper(xPos, yPos)
                               currentField.AddWarrior newWarrior
                               PoolOfNames <- PoolOfNames.Tail
                | "Support" -> let newWarrior = new Support(xPos, yPos)
                               currentField.AddWarrior newWarrior
                               PoolOfNames <- PoolOfNames.Tail
                | "Medic" ->   let newWarrior = new Medic(xPos, yPos)
                               currentField.AddWarrior newWarrior
                               PoolOfNames <- PoolOfNames.Tail
                | _ ->         printfn "Incorrect input class"
        else printfn "There is a warrior in this place"
    else printfn "Incorrect positon"

let getDirection () = 
    printfn "Print direction: (Up, Down, Right, Left)"
    match System.Console.ReadLine() with 
        | "Up"    -> DirectionsEnum.Up
        | "Down"  -> DirectionsEnum.Down
        | "Left"  -> DirectionsEnum.Left
        | "Right" -> DirectionsEnum.Right
        | _       -> DirectionsEnum.Right

[<EntryPoint>]
let main args =
    let mutable gameIsActive = true
    let stepsCount = 3
    while gameIsActive do
        let listOfWarriors = Field.GetInstance.GetListOfWarriors
        if listOfWarriors.IsEmpty then 
            printfn "Field is empty, create new warrior"
            createWarrior()
        else
            let currentWarrior = listOfWarriors.Head
            printfn "Now is %s's turn:" currentWarrior.GetWarriorName
            for i in 1 .. stepsCount do
                printfn "Step %d of %d\nType command: (Create, Print, Shoot, Move, Special, Exit), he is %A" i stepsCount (currentWarrior.GetType().Name)
                match System.Console.ReadLine() with 
                    | "Exit"   -> gameIsActive <- false
                    | "Create" -> createWarrior()
                    | "Print"  -> Field.GetInstance.PrintField
                    | "Shoot"  -> currentWarrior.Shoot (getDirection())
                    | "Move"   -> currentWarrior.Move (getDirection())
                    | "Special"-> currentWarrior.SpecialAbility()
                    | _        -> printfn "Unknown command, try again"
            Field.GetInstance.DeleteWarrior currentWarrior
            Field.GetInstance.AddWarrior currentWarrior
    0