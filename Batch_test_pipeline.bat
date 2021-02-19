:: This workload is used for making a test of parallelism of MAREX architecture. 
:: PIPE is a model that generate rules without dependencies in their LHS rules. n is the number of rules in the model.
:: For each execution, the following data is gathered in the result file:
:: - Number of Cycles to reach the end of the computation.
:: - Execution Steps reached.
:: - Rule executions reached.

@ECHO OFF
mkdir Results_PIPELINE

SET modelo=PIPE
for %%e in (false true) do for %%n in (10 50 100 150 200 250 300 350 400) do MAREXSIM m=%modelo% n=%%n e=%%e v=-4 >> ./Results_PIPELINE/R_%modelo%_%%e.txt
ECHO ON






