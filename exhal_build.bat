pushd exhal
gcc -c -std=c99 compress.c
gcc -shared -o compress.dll compress.o
copy compress.dll ..\EbPsiAnimationEditor\bin\Debug\compress.dll
copy compress.dll ..\EbPsiAnimationEditor\bin\Release\compress.dll
popd