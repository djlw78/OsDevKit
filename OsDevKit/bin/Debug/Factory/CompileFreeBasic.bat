setlocal
cd Factory
cd ..
cd freebasic
fbc.exe -c -o ..\..\Factory\Build\%2.o %1 
cd ..
cd Factory