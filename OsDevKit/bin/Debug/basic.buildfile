{"Name":"Standert Build File","Steps":[{"Name":"Assembly","Description":"Asm - Compile all asm files","Exe":"{nasm}","OnceOfExecute":false,"Waitfor":true,"OPT":"","OutPutRegex":"","FileType":".asm","Path":".","Args":["-f elf","-o {build}{buildedname}","{filepath}"]},{"Name":"GCC","Description":"Gcc - Compile all C files","Exe":"{gcc}","OnceOfExecute":false,"Waitfor":true,"OPT":"-w -m32 -Wall -O -fstrength-reduce  -finline-functions -fomit-frame-pointer -nostdinc -fno-builtin -I {include} -c -fno-strict-aliasing -fno-common -fno-stack-protector","OutPutRegex":"","FileType":".c","Path":".","Args":["{opt}","-o {build}{buildedname}","{filepath}"]},{"Name":"G++","Description":"G++ - Compile all C++ files","Exe":"{g++}","OnceOfExecute":false,"Waitfor":true,"OPT":"-ffreestanding -O2 -Wall -Wextra -fno-exceptions -fno-rtti -I {include}","OutPutRegex":"","FileType":".cpp|.c\\+\\+","Path":".","Args":["{opt}","-o {build}{buildedname}","{filepath}"]},{"Name":"FreeBasic","Description":"Freebasic - Compile all bas files","Exe":"{fbc}","OnceOfExecute":false,"Waitfor":true,"OPT":"","OutPutRegex":"","FileType":".bas","Path":".","Args":["-c {filepath}","-o {build}{buildedname}"]},{"Name":"Linker","Description":"Linker - Link all files","Exe":"{ld}","OnceOfExecute":true,"Waitfor":true,"OPT":"","OutPutRegex":"","FileType":".o","Path":".","Args":["-melf_i386","-T {dls}","{build}*.o"]},{"Name":"BuildImageFile","Description":"Build Image File - Builds Image File","Exe":"{git}","OnceOfExecute":true,"Waitfor":true,"OPT":"","OutPutRegex":"","FileType":".*+","Path":".","Args":["-i Factory\\CD","-o {img}"]},{"Name":"Qemu","Description":"Qemu - Start the emulator","Exe":"{qemu}","OnceOfExecute":true,"Waitfor":false,"OPT":"","OutPutRegex":"","FileType":".*+","Path":".","Args":["-L .","-fda {img}","-serial tcp:127.0.0.1:8080,server,nowait"]}],"Description":"This is the build file used to compile the templates","LinkerScriptPath":null}