FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY . ./
RUN dotnet build
RUN dotnet publish ./MyBank.Cashback.TransactionConsumer.Console/MyBank.Cashback.TransactionConsumer.Console.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "MyBank.Cashback.TransactionConsumer.Console.dll", "--prod", "1"]