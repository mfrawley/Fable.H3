namespace fable.test

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open H3

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestNumHexagons() =
        let num = h3.numHexagons 6.0
        Assert.Equals(5.0, num) |> ignore
        // let r = (h3.geoToH3 0.0 0.0 7.0)
        ()