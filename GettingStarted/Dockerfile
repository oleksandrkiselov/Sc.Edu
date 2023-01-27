# escape=`

ARG BASE_IMAGE
ARG BUILD_IMAGE

FROM ${BUILD_IMAGE} AS prep
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Gather only artifacts necessary for NuGet restore, retaining directory structure
COPY *.sln .\_Build\nuget.config .\_Build\Directory.Build.targets .\GettingStarted\src\Foundation\Configuration\code\GettingStarted.Foundation.Configuration\packages.config .\GettingStarted\src\Feature\PageContent\code\GettingStarted.Feature.PageContent\packages.config .\GettingStarted\src\Project\WebSite\code\GettingStarted.Project.WebSite\packages.config \nuget\ 
COPY GettingStarted\src\ \temp\
RUN Invoke-Expression 'robocopy C:\temp C:\nuget\GettingStarted\src /s /ndl /njh /njs *.csproj *.scproj packages.config'

FROM ${BUILD_IMAGE} AS builder

ARG BUILD_CONFIGURATION

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Create an empty working directory
WORKDIR C:\build

# Copy prepped NuGet artifacts, and restore as distinct layer to take better advantage of caching
COPY --from=prep .\nuget .\
RUN nuget restore

# Copy remaining source code
COPY GettingStarted\src\ .\GettingStarted\src\

# Copy transforms, retaining directory structure
RUN Invoke-Expression 'robocopy C:\build\GettingStarted\src C:\out\transforms /s /ndl /njh /njs *.xdt'

# Build website with file publish
RUN Get-ChildItem *.sln | %{  msbuild $_.FullName /p:Configuration=$env:BUILD_CONFIGURATION /m /p:DeployOnBuild=true /p:IsLocalDockerDeploy=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:PublishUrl=C:\out\website }
FROM ${BASE_IMAGE}

WORKDIR C:\artifacts

# Copy final build artifacts
COPY --from=builder C:\out\website .\website\
COPY --from=builder C:\out\transforms .\transforms\