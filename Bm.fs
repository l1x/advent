namespace Advent


open BenchmarkDotNet.Attributes
open System.IO


module Bm =


  module Advent1e1 =

    let numbers folder =
      File.ReadAllLines(sprintf "%s/%s" folder "input/1.input")
      |> Seq.map (Utils.parseInt)

    let miez numbers =
      for outterNumber in numbers do
        for innerNumber in numbers do
          if (outterNumber + innerNumber) = 2020
          then printfn "Found: %A :: %A :: %A" outterNumber innerNumber (outterNumber * innerNumber)
          else ()

    let run folder =
      let nums = numbers folder |> Seq.choose id
      miez nums

  module Advent1e2 =

    let numbers folder =
      File.ReadAllLines(sprintf "%s/%s" folder "input/1.input")
      |> Seq.map (Utils.parseInt)

    let miez numbers =
      for outterNumber in numbers do
        for inner1Number in numbers do
          for inner2Number in numbers do
            if (outterNumber + inner1Number + inner2Number) = 2020 then
              printfn
                "Found: %A :: %A :: %A :: %A"
                outterNumber
                inner1Number
                inner2Number
                (outterNumber * inner1Number * inner2Number)
            else
              ()

    let run folder =
      let nums = numbers folder |> Seq.choose id
      miez nums

  [<MemoryDiagnoser>]
  type Advent1() =

    [<Benchmark(Baseline = true)>]
    member _.BuiltIn() = Advent1e1.numbers ""

    [<Benchmark>]
    member _.Custom() = Advent1e2.numbers ""
