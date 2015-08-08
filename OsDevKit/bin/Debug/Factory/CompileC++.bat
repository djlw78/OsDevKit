setlocal
set opt=-ffreestanding -O2 -Wall -Wextra -fno-exceptions -fno-rtti -I %3
cd Factory
cd ..
cd Tools
cd Bin
g++.exe %opt% -o ..\..\Factory\Build\%2.o -c %1 
cd ..
cd ..
cd Factory