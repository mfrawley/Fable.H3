module H3Test
open H3
open System.Diagnostics

let assertEquals v1 v2 =
    if v1 <> v2 then
        failwith $"{v1} != {v2}"

let h3ToGeoLocations (h3Index: string) : GeoLocation[] = 
    let b = h3.h3ToGeoBoundary(h3Index, formatAsGeoJson=true)
    let res =  
        b 
        |> Array.ofSeq
        |> Array.map (fun (pair: float ResizeArray) -> 
            let pairArr = Array.ofSeq pair
            GeoLocation(pairArr.[0], pairArr.[1])
        )
    res

let tests = [
    "numHexagons", (fun _ -> 
        let numHex = h3.numHexagons 5.0
        assertEquals numHex 2016842.0
    )
    "testGeoToH3", (fun _ -> 
        let nullIsland = h3.geoToH3(53.0, 9.8, 6.0)
        assertEquals nullIsland "861f156c7ffffff"
    )
    "testh3ToGeoLocations", (fun _ -> 
        let hhBoundary = h3ToGeoLocations("861f156c7ffffff")
        assertEquals hhBoundary.[0] (9.722437682823871,53.00350943424079)
    )
    "edgeLength", (fun _ -> 
        let len = h3.edgeLength(5.0, units.m)
        assertEquals len 8544.408276
        
        let len = h3.edgeLength(7.0, units.m)
        assertEquals len 1220.629759

        let len = h3.edgeLength(8.0, units.m)
        assertEquals len 461.3546837
    )
]

[<EntryPoint>]
let main args =
    for name, test in tests do
        try
            test()
            printfn "%s passed" name
        with
            | Failure(msg) -> printfn "%s: %s" name msg; 
            
        
    0