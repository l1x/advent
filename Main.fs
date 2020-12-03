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

    let adventDay = commandLineArgumentsParsed.Day
    let adventExercise = commandLineArgumentsParsed.Exercise
    let benchmark = commandLineArgumentsParsed.Benchmark
    let folder = commandLineArgumentsParsed.Folder

    match adventDay, adventExercise, benchmark with
    | 1, 1, true -> ()
    | 1, 1, false ->
        loggerMain.LogInfo
        <| (sprintf "%A" (Bm.Advent1e1.run folder))
    | 1, 2, false ->
        loggerMain.LogInfo
        <| (sprintf "%A" (Bm.Advent1e2.run folder))
    | _, _, _ -> Environment.Exit 1

    0
