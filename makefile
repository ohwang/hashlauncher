# Makefile for HashLaucnher

main: hashlauncher.hs Icon.o
			ghc hashlauncher.hs icon.o

Icon.o: Icon.rc hash.ico
			windres -i icon.rc icon.o

clean:
			rm *.o *.hi
