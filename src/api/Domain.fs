namespace NRK.Dotnetskolen

module Domain = 
    open System
    open System.Text.RegularExpressions
    type Sending = {
        Tittel: string
        Kanal: string
        StartTidspunkt: DateTimeOffset
        SluttTidspunkt: DateTimeOffset
    }
    type Epg = Sending list

    let isTittelValid (tittel: string) : bool = 
        let tittelRegex = Regex(@"^[\p{L}0-9\.,-:!]{5,100}$")
        tittelRegex.IsMatch(tittel)

    let isKanalValid (kanal: string) : bool =
    //     Regex("NRK1").IsMatch(kanal) || Regex("NRK2").IsMatch(kanal)
        List.contains kanal ["NRK1"; "NRK2"]
        
    let areStartAndSluttidspunktValid (startTidspunkt: DateTimeOffset) (sluttTidspunkt: DateTimeOffset) =
        startTidspunkt < sluttTidspunkt 
 
    let isSendingValid (sending: Sending) = 
        (isTittelValid sending.Tittel) && (isKanalValid sending.Kanal) && (areStartAndSluttidspunktValid sending.StartTidspunkt sending.SluttTidspunkt)