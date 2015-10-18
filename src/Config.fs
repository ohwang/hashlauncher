namespace HashLauncher

open FSharp.Configuration

type HLConfig = YamlConfig<FilePath = "HashLauncher.yaml">

module Config =
  let config = HLConfig()

  let getCmd (ext:string) =
    let ext' = if ext.StartsWith(".") then ext else "." + ext

    config.filetypes
     |> Seq.tryPick (fun ftc ->
      if ftc.ext.Contains(ext')
       then Some ftc.prog
       else None)