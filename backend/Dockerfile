FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
COPY *.sln .
COPY ErrorlineSystem/*.csproj ErrorlineSystem/
COPY ErrorlineSystem.DataContext/*.csproj ErrorlineSystem.DataContext/
COPY ErrorlineSystem.Services/*.csproj ErrorlineSystem.Services/
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -r linux-x64 -o /app/out --self-contained false


FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app
COPY --from=build /app/out .
COPY --from=build /usr/share/dotnet /usr/share/dotnet
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5071
CMD ["dotnet", "ErrorlineSystem.dll", "--urls=http://0.0.0.0:5071"]