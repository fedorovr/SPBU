namespace MedvedFS

type MedvedFS() = 
    inherit MedvedVBasic.MedvedVB()
    override x.MeetMedved() = 
        printfn "Hello from F#"
        base.MeetMedved()