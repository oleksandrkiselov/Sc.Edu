# escape=`

ARG BASE_IMAGE
ARG TOOLING_IMAGE

FROM ${TOOLING_IMAGE} as tooling
FROM ${BASE_IMAGE}

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Copy development tools and entrypoint
COPY --from=tooling \tools\ \tools\

# Grant full access to IIS_IUSRS so it can install packages
RUN icacls 'C:\inetpub\wwwroot' /grant 'IIS_IUSRS:(F)' /t