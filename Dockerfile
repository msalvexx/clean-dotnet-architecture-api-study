FROM mcr.microsoft.com/dotnet/sdk:5.0
ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_HTTPS_PORT=5001
ENV ASPNETCORE_HTTP_PORT=5000
ENV ASPNETCORE_ENVIRONMENT=Development
RUN apt-get update && \
    apt-get install unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg
WORKDIR /home/site/wwwroot
COPY dist /home/site/wwwroot
EXPOSE 80
WORKDIR /home/site/wwwroot
ENTRYPOINT [ "dotnet", "Main.dll" ]