#!.\packages\FAKE\tools\Fake.exe

#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.FscHelper
open Fake.FileUtils

let buildDir = "./build/"

Target "Clean" (fun _ -> CleanDir buildDir)

Target "Build" (fun _ ->
    !!"./src/HashLauncher.fsproj"
    |> MSBuildDebug buildDir "Build"
    |> Log "Build: ")

Target "Release" (fun _ ->
    !! "./src/HashLauncher.fsproj"
    |> MSBuild buildDir "Build" [ ("ApplicationIcon", "./res/Icon.ico")
                                  ("Configuration", "Release") ]
    |> Log "Release: ")


let Libraries =
    !! "./packages/FSharp.Configuration/lib/net40/*.dll"
    |> Seq.toList

let buildStandalone () =
    pushd "./src/" 
    ["AssemblyInfo.fs"; "Config.fs"; "HashLauncher.fs"]
    |> Fsc (fun p -> { p with Output = "../HashLauncher.exe"
                              FscTarget = Exe
                              References = Libraries
                              OtherParams = [ "--standalone" ] })
    popd()

Target "Standalone" buildStandalone


// Specify dependencies
"Clean" ==> "Build"
"Clean" ==> "Release"
"Clean" ==> "Standalone"


RunTargetOrDefault "Build"
