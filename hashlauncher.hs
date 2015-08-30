-- #!/usr/bin/env runhs

import System.Environment
import System.IO
import System.Exit (die, exitWith)
import System.FilePath.Windows
import System.Process (runProcess, waitForProcess)

runPs cmd args =
  runProcess cmd args Nothing Nothing Nothing Nothing Nothing

getProgFromExt ext =
  case ext of
    ".py" -> "python"
    ".hs" -> "runhaskell"
    ".fsx" -> "fsi"

main = do
  putStrLn "Hello SheLauncher"
  args <- getArgs

  let scriptPath = head args

  putStrLn scriptPath

  psHandle <- withFile scriptPath ReadMode (\handle -> do
    firstLine <- hGetLine handle
    putStrLn firstLine
    let shebang = take 2 firstLine
        progPath = drop 2 firstLine
    if shebang == "#!"
      then runPs progPath args
      else case takeExtension scriptPath
        of "" -> die "Cannot determine a program to execute this file"
           ext -> runPs (getProgFromExt ext) args
    )

  exitCode <- waitForProcess psHandle
  exitWith exitCode



