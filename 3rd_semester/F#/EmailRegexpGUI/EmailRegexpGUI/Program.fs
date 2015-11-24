open System
open System.Text.RegularExpressions
open System.Drawing
open System.Windows.Forms

let checkEmail aString =
    let nickname = @"^[_A-z][-_A-z0-9]{0,50}([.][-_A-z0-9]{1,50}){0,10}"
    let domain = @"[A-z0-9][A-z0-9-]{0,50}([.][A-z0-9][-A-z0-9]{1,50}){0,10}"
    let domainZone = @"[A-z]{2,15}$"
    match aString with 
    | x when Regex.Match(x, nickname + @"[@]" + domain + @"[.]" + domainZone).Success-> 
        MessageBox.Show(aString + " is an email", "Success") |> ignore
    | _ -> 
        MessageBox.Show(aString + " is not an email", "Fail") |> ignore

let mainForm = new Form(Width = 500, Height = 128, Text = "e-mail valid checker") 

let txtBox = new RichTextBox(Width = 360, Height = 80, Location=new Point(5, 5), Text= "Enter a string to check")
mainForm.Controls.Add(txtBox)

let buttonCheck = new Button(Width = 100, Height = 80, Location=new Point(370, 5), Text="Check")

let myKeyPressedEventHandler (evArgs : System.Windows.Forms.KeyPressEventArgs) =
    if ((int)evArgs.KeyChar).Equals(13) then 
        txtBox.Text <- txtBox.Text.Substring(0, txtBox.Text.Length - 1)
        checkEmail <| txtBox.Text.ToString()

txtBox.KeyPress.Add(myKeyPressedEventHandler)

txtBox.Click.Add(fun _ -> if txtBox.Text.ToString().Equals "Enter a string to check" then txtBox.Clear())
mainForm.Controls.Add(buttonCheck)
buttonCheck.Click.Add(fun _ -> checkEmail <| txtBox.Text.ToString())

do 
    Application.Run(mainForm)
