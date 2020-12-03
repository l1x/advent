namespace Advent

// internal
open Logging

// external
open System
open System.Text.RegularExpressions
open System.IO


module Cli =

  let loggerCli =
    Logger.CreateLogger "Cli" "info" (fun _ -> DateTime.Now)

  let isValidDay (s: string) = Utils.parseInt s

  let isFolder (s: string) = Directory.Exists(s)

  [<StructuredFormatDisplay("Day: {Day} :: Benchmark: {Benchmark} :: Folder: {Folder} :: Exercise: {Exercise}")>]
  type CommandLineOptions =
    { Day: int
      Benchmark: bool
      Folder: string
      Exercise: int }


  // create the "helper" recursive function
  let rec private parseCommandLineRec args optionsSoFar =
    //loggerCli.LogInfo(args)
    match args with
    // empty list means we're done.
    | [] ->
        loggerCli.LogInfo(sprintf "optionsSoFar %A" optionsSoFar)
        optionsSoFar

    // match day
    | "--day" :: xs ->
        match xs with
        | day :: xss ->
            match Utils.parseInt day with
            | Some dayInt -> parseCommandLineRec xss { optionsSoFar with Day = dayInt }
            | None ->
                loggerCli.LogError(String.Format("Unsupported month: {0}", day))
                Environment.Exit 1
                parseCommandLineRec xss optionsSoFar // never reach

        | [] ->
            loggerCli.LogError(String.Format("Day cannot be empty"))
            Environment.Exit 1
            parseCommandLineRec xs optionsSoFar // never reach

    // match exercise
    | "--exercise" :: xs ->
        match xs with
        | exercise :: xss ->
            match Utils.parseInt exercise with
            | Some exerciseInt ->
                parseCommandLineRec
                  xss
                  { optionsSoFar with
                      Exercise = exerciseInt }
            | None ->
                loggerCli.LogError(String.Format("Unsupported exercise: {0}", exercise))
                Environment.Exit 1
                parseCommandLineRec xss optionsSoFar // never reach

        | [] ->
            loggerCli.LogError(String.Format("Day cannot be empty"))
            Environment.Exit 1
            parseCommandLineRec xs optionsSoFar // never reach

    // benchmark day
    | "--benchmark" :: xs ->
        match xs with
        | benchmark :: xss ->
            match Utils.parseBool benchmark with
            | Some benchmarkBool ->
                parseCommandLineRec
                  xss
                  { optionsSoFar with
                      Benchmark = benchmarkBool }
            | None ->
                loggerCli.LogError(String.Format("Unsupported benchmark: {0}", benchmark))
                Environment.Exit 1
                parseCommandLineRec xss optionsSoFar // never reach

        | [] ->
            loggerCli.LogError(String.Format("Day cannot be empty"))
            Environment.Exit 1
            parseCommandLineRec xs optionsSoFar // never reach

    // folder day
    | "--folder" :: xs ->
        match xs with
        | folder :: xss ->
            match isFolder folder with
            | true -> parseCommandLineRec xss { optionsSoFar with Folder = folder }
            | false ->
                loggerCli.LogError(String.Format("Unsupported folder: {0}", folder))
                Environment.Exit 1
                parseCommandLineRec xss optionsSoFar // never reach

        | [] ->
            loggerCli.LogError(String.Format("Folder cannot be empty"))
            Environment.Exit 1
            parseCommandLineRec xs optionsSoFar // never reach

    // handle unrecognized option and keep looping
    | x :: xs ->
        loggerCli.LogError(String.Format("Option {0} is unrecognized", x))
        parseCommandLineRec xs optionsSoFar

  // create the "public" parse function
  let parseCommandLine args =
    // create the defaults
    let defaultOptions =
      { Day = 1
        Benchmark = false
        Folder = "."
        Exercise = 1 }
    // call the recursive one with the initial options
    parseCommandLineRec args defaultOptions


// END
