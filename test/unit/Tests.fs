module Tests

open NRK.Dotnetskolen.Domain
open Xunit
open System

[<Theory>]
[<InlineData("abc12")>]
[<InlineData(".,-:!")>]
[<InlineData("ABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJ")>]
let ``isTittelValid valid tittel returns true`` (tittel: string) =
    let isTittelValid = isTittelValid tittel
    Assert.True isTittelValid

[<Theory>]
[<InlineData("abcd")>]
[<InlineData("@$%&/")>]
[<InlineData("abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghija")>]
let ``isTittelValid invalid tittel returns false`` (tittel: string) =
    let isTittelValid = isTittelValid tittel
    Assert.False isTittelValid

[<Theory>]
[<InlineData("NRK1")>]
[<InlineData("NRK2")>]
let ``isKanalValid valid kanal returns true`` (kanal: string) =
    let isKanalValid = isKanalValid kanal
    Assert.True isKanalValid

[<Theory>]
[<InlineData("nrk1")>]
[<InlineData("NRK3")>]
let ``isKanalValid invalid kanal returns false`` (kanal: string) =
    let isKanalValid = isKanalValid kanal
    Assert.False isKanalValid


[<Fact>]
let ``areStartAndSluttidspunktValid start before end returns true`` () =
    let starttidspunkt = DateTimeOffset.Now
    let sluttidspunkt = starttidspunkt.AddMinutes 30.

    let areStartAndSluttidspunktValid = areStartAndSluttidspunktValid starttidspunkt sluttidspunkt
    Assert.True areStartAndSluttidspunktValid

[<Fact>]
let ``areStartAndSluttidspunktValid start after end returns false`` () =
    let startTime = DateTimeOffset.Now
    let endTime = startTime.AddMinutes -30.

    let areStartAndSluttidspunktValid = areStartAndSluttidspunktValid startTime endTime
    Assert.False areStartAndSluttidspunktValid

[<Fact>]
let ``areStartAndSluttidspunktValid start equals end returns false`` () =
    let startTime = DateTimeOffset.Now
    let endTime = startTime

    let areStartAndSluttidspunktValid = areStartAndSluttidspunktValid startTime endTime
    Assert.False areStartAndSluttidspunktValid

[<Fact>]
let ``isSendingValid valid sending returns true`` () = 
    let now = DateTimeOffset.Now
    let sending = {
        Tittel = "Sporten"
        Kanal = "NRK1"
        StartTidspunkt = now
        SluttTidspunkt = now.AddMinutes 30.
    }
    let isSendingValid = isSendingValid sending
    Assert.True isSendingValid

[<Fact>]
let ``isSendingValid invalid sending returns false`` () = 
    let now = DateTimeOffset.Now
    let sending = {
        Tittel = "@$£{sfa"
        Kanal = "nrk3"
        StartTidspunkt = now
        SluttTidspunkt = now.AddMinutes -30.
    }
    let isSendingValid = isSendingValid sending
    Assert.False isSendingValid