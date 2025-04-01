#!/bin/bash
echo -ne "\033]0;IndividualLabsService\007"

dotnet run --project Covid19.IndividualLabsService --urls=https://localhost:7068 &
wait
