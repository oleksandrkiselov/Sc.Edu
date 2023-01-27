# escape=`

ARG BASE_IMAGE
ARG SPE_IMAGE
ARG TOOLING_IMAGE
ARG SOLUTION_IMAGE

FROM ${SOLUTION_IMAGE} as solution
FROM ${TOOLING_IMAGE} as tooling
FROM ${SPE_IMAGE} as spe
FROM ${BASE_IMAGE}

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Copy development tools and entrypoint
COPY --from=tooling \tools\ \tools\

WORKDIR C:\inetpub\wwwroot

# Add SPE module
COPY --from=spe \module\cm\content .\
# RUN Rename-Item -Path "c:\inetpub\wwwroot\App_Config\Include\Spe\Spe.IdentityServer.config.disabled" -NewName "Spe.IdentityServer.config"

# Disable Identity Server
RUN Copy-Item -Path 'C:\\inetpub\\wwwroot\\App_Config\\Include\\Examples\\Sitecore.Owin.Authentication.IdentityServer.Disabler.config.example' `
              -Destination 'C:\\inetpub\\wwwroot\\App_Config\\Include\\Sitecore.Owin.Authentication.IdentityServer.Disabler.config';

# Copy solution website files
COPY --from=solution \artifacts\website\ .\

# Copy solution transforms
COPY --from=solution \artifacts\transforms\ \transforms\solution\

# Copy role transforms
COPY .\transforms\ \transforms\role\

# Perform solution transforms
RUN C:\tools\scripts\Invoke-XdtTransform.ps1 -Path .\ -XdtPath C:\transforms\solution\Project\WebSite\code\GettingStarted.Project.WebSite

# Perform role transforms
RUN C:\tools\scripts\Invoke-XdtTransform.ps1 -Path .\ -XdtPath C:\transforms\role