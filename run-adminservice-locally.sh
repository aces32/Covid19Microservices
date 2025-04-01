#!/bin/bash
echo -ne "\033]0;AdminService\007"

dotnet run --project Covid19.AdministratorService --urls=https://localhost:7061 &
wait
