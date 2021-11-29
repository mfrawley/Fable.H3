module H3Test
open H3
open System.Diagnostics

let assertEquals v1 v2 =
    if v1 <> v2 then
        failwith $"{v1} != {v2}"


let tests = [
    "numHexagons", (fun _ -> 
        let numHex = h3.numHexagons 5.0
        assertEquals numHex 2016842.0
    )
    "testGeoToH3", (fun _ -> 
        let nullIsland = h3.geoToH3(0.0, 0.0, 6.0)
        assertEquals nullIsland "86754e64fffffff"
    )
    "edgeLength", (fun _ -> 
        let len = h3.edgeLength(5.0, units.m)
        assertEquals len 8544.408276
        
        let len = h3.edgeLength(7.0, units.m)
        assertEquals len 1220.629759
    )
]

[<EntryPoint>]
let main args =
    for name, test in tests do
        try
            test()
        with
            | Failure(msg) -> printfn "%s: %s" name msg; 
            
        
    0