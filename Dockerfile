FROM node:18.10-alpine AS frontend
WORKDIR /usr/src/app
COPY /ClientApp/package.json ./
COPY /ClientApp/package-lock.json ./
RUN npm install
COPY /ClientApp .
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS backend
WORKDIR /app

COPY /src ./Courses/
WORKDIR /app/Courses/Courses
RUN dotnet publish -c Release -o out
COPY --from=frontend /usr/src/app/dist ./out/wwwroot
WORKDIR /app/Courses/Courses/out
ENTRYPOINT ["dotnet", "Courses.dll"]

#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime
#WORKDIR /app
#COPY --from=build /app/Courses/Courses/out ./
#ENTRYPOINT ["dotnet", "Courses.dll"]
