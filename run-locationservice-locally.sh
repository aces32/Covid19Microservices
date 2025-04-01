#!/bin/bash
echo -ne "\033]0;LocationService\007"

dotnet run --project Covid19.LocationService --urls=https://localhost:7201 &
wait
