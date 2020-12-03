namespace Advent

// internal
open Logging

// external
open System
open System.Text.RegularExpressions


module Cli =

  let loggerCli =
    Logger.CreateLogger "Cli" "info" (fun _ -> DateTime.Now)

  let isValidDay (s: string) = Utils.parseInt s

  [<StructuredFormatDisplay("Day: {Day}")>]
  type CommandLineOptions = { Day: int }


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

    // handle unrecognized option and keep looping
    | x :: xs ->
        loggerCli.LogError(String.Format("Option {0} is unrecognized", x))
        parseCommandLineRec xs optionsSoFar

  // create the "public" parse function
  let parseCommandLine args =
    // create the defaults
    let defaultOptions = { Day = 1 }
    // call the recursive one with the initial options
    parseCommandLineRec args defaultOptions


// END
