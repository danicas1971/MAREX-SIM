#!/bin/bash

# This script must be executed under Ubuntu. 
# You can install "Ubuntu 18.04 LTS" application in your windows machine
# and run a command console to execute this script.

# Sorry but this was the quickest way to do it that I could think of :

#Condense all result files from RNDG with n=8 y n=9
cat R_RNDG_9*.txt >> R_RNDG_false_9.txt
cat R_RNDG_8*.txt >> R_RNDG_false_8.txt

rm R_RNDG_9_*.txt 
rm R_RNDG_8_*.txt


#Condense all the results in one file (RESULT.txt), at directory ./condensated

mkdir condensated
for VAR in $( ls R_*.txt )
do
	echo "$VAR"
	tr '\r\n' ' ' < "$VAR" > ./condensated/"$VAR"
done
cd condensated

for VAR in $( ls R_*.txt )
do
	echo "$VAR $(cat $VAR)" >> RESULT.txt
	
done
cd ..
