# Makefile for HashLaucnher

main: hashlauncher.hs Icon.o
			ghc hashlauncher.hs Icon.o

Icon.o: Icon.rc hash.ico
			windres -i Icon.rc Icon.o

clean:
			rm *.o *.hi
