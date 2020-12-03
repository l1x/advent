namespace Advent

// internal
open Cli
open Logging

//external
open System
open BenchmarkDotNet.Running


module Main =


  let loggerMain =
    Logger.CreateLogger "Main" "info" (fun () -> DateTime.Now)


  [<EntryPoint>]
  let main argv =
    let commandLineArgumentsParsed = parseCommandLine (Array.toList argv)

    loggerMain.LogInfo
    <| sprintf "%A" commandLineArgumentsParsed

    let advent = commandLineArgumentsParsed.Day

    match advent with
    | 1 -> BenchmarkRunner.Run<Bm.Advent1>() |> ignore
    | _ -> Environment.Exit 1

    0
