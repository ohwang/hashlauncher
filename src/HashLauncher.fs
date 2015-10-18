namespace HashLauncher

open System
open System.Collections.Generic
open System.IO

module Utility = 
    let join sep (xs : IEnumerable<string>) = String.Join(sep, xs)
    let joinWs = join " "
    
    let getFirstLine filePath = 
        use fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        use reader = new StreamReader(fs)
        reader.ReadLine()
    
    let getExt = Path.GetExtension
    
    let trimCmdWith prefix (cmd : string) = 
        if cmd.StartsWith(prefix) then cmd.Substring(prefix.Length)
        else cmd
    
    let trimCmd = trimCmdWith "/usr/bin/" >> trimCmdWith "/usr/local/bin/"
    
    let getCmdAndArgs (cmdline : string) = 
        let chunks = cmdline.Split [| ' ' |] |> Array.toList
        let cmd = List.head chunks |> trimCmd
        let args = List.tail chunks
        Some(cmd, args)
    
    let getCmdAndArgsFromConfig ext = 
        match Config.getCmd ext with
        | Some cmdline -> getCmdAndArgs cmdline
        | None -> None


module Main = 
    let run cmd args = 
        use proc = new Diagnostics.Process()
        proc.StartInfo.FileName <- cmd
        proc.StartInfo.Arguments <- args
        // proc.StartInfo.RedirectStandardInput <- true
        // proc.StartInfo.RedirectStandardOutput <- true
        // proc.StartInfo.RedirectStandardError <- true
        proc.StartInfo.UseShellExecute <- false
        try 
            proc.Start() |> ignore
        with ex -> printfn "%A" ex
        proc.WaitForExit()
        proc.ExitCode
    
    let printUsageAndExit() = 
        printfn "Usage: hashlauncher.exe script-path [args]"
        Environment.Exit(0)
    
    [<EntryPoint>]
    let main hlArgs = 
        printfn "CommandLine: %s" Environment.CommandLine
        printfn "Launcher args: %A" hlArgs

        if hlArgs.Length = 0 then
            printUsageAndExit()

        let scriptPath = hlArgs.[0]
        let shabangLine = Utility.getFirstLine scriptPath
        
        let cmdArgs = 
            if shabangLine.StartsWith "#!" then Utility.getCmdAndArgs <| shabangLine.Substring(2)
            else Utility.getCmdAndArgsFromConfig (Utility.getExt scriptPath)

        if Option.isNone cmdArgs then Environment.Exit(1)
        else printfn "Run with: %A" cmdArgs

        let cmd, shabangArgs = Option.get cmdArgs
        let arguments = 
            sprintf "%s %s" (Utility.joinWs shabangArgs) (Utility.joinWs <| Array.toList hlArgs)

        run cmd arguments
