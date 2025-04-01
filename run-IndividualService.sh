#!/bin/bash
echo -ne "\033]0;IndividualService\007"

dotnet run --project Covid19.IndividualService --urls=https://localhost:7117 &
wait
