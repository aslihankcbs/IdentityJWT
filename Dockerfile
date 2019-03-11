FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["IdentityJWT/IdentityJWT.csproj", "IdentityJWT/"]
RUN dotnet restore "IdentityJWT/IdentityJWT.csproj"
COPY . .
WORKDIR "/src/IdentityJWT"
RUN dotnet build "IdentityJWT.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IdentityJWT.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityJWT.dll"]