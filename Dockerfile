FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /home/site/wwwroot
COPY dist /home/site/wwwroot
EXPOSE 5001
WORKDIR /home/site/wwwroot
ENTRYPOINT [ "dotnet", "Main.dll" ]