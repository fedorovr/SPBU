open System.Text.RegularExpressions
open NUnit.Framework
open FsUnit



let checkEmail aString =
    let nickname = @"^[_A-z][-_A-z0-9]{0,50}([.][-_A-z0-9]{1,50}){0,10}"
    let domain = @"[A-z0-9][A-z0-9-]{0,50}([.][A-z0-9][-A-z0-9]{1,50}){0,10}"
    let domainZone = @"[A-z]{2,15}$"
    match aString with 
    | x when Regex.Match(x, nickname + @"[@]" + domain + @"[.]" + domainZone).Success-> 
        printfn "%s is an email" aString
        true
    | _ -> 
        printfn "%s is not an email" aString
        false

//visualisation: regexper.com

//  Rules:
// nickname:
//first character is A..z or _
//least characters are A..z or 0..9 or - or _ or .
//nick can contains dots, but not 2 (or more) together
//nick should ends with A..z or 0..9 or - or _
//@
// domain:
//domain starts wirh A..z or 0..9, another chars are A..z or 0..9 or - , length is >= 1
//domain can inludes subdomains, their rules are similar to a domain name
//.
// domain zone:
//domain zone contains only A..z, length is >= 2 and length is <= 15
    
[<TestFixture>]
module test =
    [<Test>]
    let ``onlyDigits`` () =
        let res = checkEmail "1@1"
        res |> should equal false

    [<Test>]
    let ``noAt`` () =
        let res = checkEmail "sadDDsa_example.com"
        res |> should equal false

    [<Test>]
    let ``domainName`` () =
        let res = checkEmail "google.com"
        res |> should equal false

    [<Test>]
    let ``shortNickAndDomain`` () =
        let res = checkEmail "a@b.cc"
        res |> should equal true

    [<Test>]
    let ``VictorPolozov`` () =
        let res = checkEmail "victor.polozov@mail.ru.su"
        res |> should equal true

    [<Test>]
    let ``usualDomain`` () =
        let res = checkEmail "my@domain.info"
        res |> should equal true

    [<Test>]
    let ``uderscopeDotDigit_rareNickname`` () =
        let res = checkEmail "_.1@mail.com"
        res |> should equal true

    [<Test>]
    let ``hermitage`` () =
        let res = checkEmail "paints_department@hermitage.museum"
        res |> should equal true

    [<Test>]
    let ``lotsOfSubdomains`` () =
        let res = checkEmail "yo@domain.somedomain.onemore.andonemoredomain.indomainzone"
        res |> should equal true

    [<Test>]
    let ``incorrectDomainZone`` () =
        let res = checkEmail "a@b.c"
        res |> should equal false

    [<Test>]
    let ``twoDotsInARowInNickName`` () =
        let res = checkEmail "a..b@mail.ru"
        res |> should equal false

    [<Test>]
    let ``nickStartsWithDot`` () =
        let res = checkEmail ".a@mail.ru"
        res |> should equal false

    [<Test>]
    let ``nickStartsWithDigit`` () =
        let res = checkEmail "1@mail.ru"
        res |> should equal false
        
    [<Test>]
    let ``containsSpace`` () =
        let res = checkEmail "kover98@ mail.ru"
        res |> should equal false 

   [<Test>]
    let ``containsEnter`` () =
        let res = checkEmail ("kover98@" + '\n'.ToString() + "mail.ru")
        res |> should equal false