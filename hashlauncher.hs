-- #!/usr/bin/env runhaskell

import System.Environment
import System.IO
import Data.Maybe (isJust, fromJust)
import System.Exit (die, exitWith)
import System.FilePath.Windows
import System.Process (runProcess, waitForProcess)

-- Utilities

startsWith str prefix = prefix == prefix'
  where prefix' = take (length prefix) str

headTail (x:xs) = (Just x, xs)
headTail [] = (Nothing, [])

--

runPs cmd args =
  runProcess cmd args Nothing Nothing Nothing Nothing Nothing

getProgFromExt ext =
  case ext of
    ".py" -> "python"
    ".hs" -> "runhaskell"
    ".fsx" -> "fsi"

getProgFromShabangPath path =
  if path `startsWith` "/usr/bin/"
    then drop 9 path
    else path

main = do
  launcherArgs <- getArgs

  let scriptPath = head launcherArgs

  print scriptPath
  print $ takeExtension scriptPath

  psHandle <- withFile scriptPath ReadMode (\handle -> do
    firstLine <- hGetLine handle

    let shebang = take 2 firstLine
        cmdWithArgs = words $ drop 2 firstLine
        (cmdPath, cmdArgs) = headTail cmdWithArgs
        args = cmdArgs ++ launcherArgs

    if shebang == "#!" && isJust cmdPath
      then runPs (getProgFromShabangPath $ fromJust cmdPath) args
      else case takeExtension scriptPath
        of "" -> die "Cannot determine a program to execute this file"
           ext -> runPs (getProgFromExt ext) args
    )

  exitCode <- waitForProcess psHandle
  exitWith exitCode



