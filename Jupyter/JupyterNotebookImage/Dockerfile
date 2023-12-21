# See Jupyter Docker image versioning:
# https://jupyter-docker-stacks.readthedocs.io/en/latest/using/selecting.html#versioning-via-image-tags

FROM jupyter/minimal-notebook:lab-4.0.7

USER root

RUN apt-get update --yes && \
    apt-get install --yes --no-install-recommends \
    mdbtools \
    gcc python3-dev \
    # Security patches
    # script-auto-update
    libc-bin=2.35-0ubuntu3.5 \
    locales=2.35-0ubuntu3.5 \
    curl=7.81.0-1ubuntu1.15 \
    libcurl3-gnutls=7.81.0-1ubuntu1.15 \
    libcurl4=7.81.0-1ubuntu1.15 \
    # The following dependecies are for dotnet run-time
    aspnetcore-runtime-6.0=6.0.125-0ubuntu1~22.04.1 \
    libgssapi-krb5-2=1.19.2-2ubuntu0.3 \
    libicu70=70.1-2 \
    liblttng-ust1=2.13.1-1ubuntu1 \
    libunwind8=1.3.2-2build2.1 \
    zlib1g=1:1.2.11.dfsg-2ubuntu9.2 \
    # Dependencies for chromium
    gconf-service=3.2.6-7ubuntu2 \
    libasound2=1.2.6.1-1ubuntu1 \
    libatk1.0-0=2.36.0-3build1 \
    libc6=2.35-0ubuntu3.5 \
    libcairo2=1.16.0-5ubuntu2 \
    libcups2=2.4.1op1-1ubuntu4.7 \
    libdbus-1-3=1.12.20-2ubuntu4.1 \
    libexpat1=2.4.7-1ubuntu0.2 \
    libfontconfig1=2.13.1-4.2ubuntu5 \
    libgcc-s1=12.3.0-1ubuntu1~22.04 \
    libgconf-2-4=3.2.6-7ubuntu2 \
    libgdk-pixbuf2.0-0=2.40.2-2build4 \
    libglib2.0-0=2.72.4-0ubuntu2.2 \
    libgtk-3-0=3.24.33-1ubuntu2 \
    libnspr4=2:4.32-3build1 \
    libpango-1.0-0=1.50.6+ds-2ubuntu1 \
    libpangocairo-1.0-0=1.50.6+ds-2ubuntu1 \
    libstdc++6=12.3.0-1ubuntu1~22.04 \
    libx11-6=2:1.7.5-1ubuntu0.3 \
    libx11-xcb1=2:1.7.5-1ubuntu0.3 \
    libxcb1=1.14-3ubuntu3 \
    libxcomposite1=1:0.4.5-1build2 \
    libxcursor1=1:1.2.0-2build4 \
    libxdamage1=1:1.1.5-2build2 \
    libxext6=2:1.3.4-1build1 \
    libxfixes3=1:6.0.0-1 \
    libxi6=2:1.8-1build1 \
    libxrandr2=2:1.5.2-1build1 \
    libxrender1=1:0.9.10-1build4 \
    libxss1=1:1.2.3-1build2 \
    libxtst6=2:1.2.3-1build4 \
    ca-certificates=20230311ubuntu0.22.04.1 \
    fonts-liberation=1:1.07.4-11 \
    libappindicator1=12.10.1+20.10.20200706.1-0ubuntu1 \
    libnss3=2:3.68.2-0ubuntu1.2 \
    lsb-release=11.1.0ubuntu4 \
    xdg-utils=1.1.3-4.1ubuntu3~22.04.1 && \
    # end script-auto-update
    apt-get clean && rm -rf /var/lib/apt/lists/*

RUN dpkg -r --force-depends libpdfbox-java && \
    dpkg -r --force-depends libfontbox-java

USER ${NB_UID}

COPY requirements.txt /tmp/jupyter-kernel-requirements.txt

RUN pip install -r /tmp/jupyter-kernel-requirements.txt

RUN wget https://storage.googleapis.com/chromium-browser-snapshots/Linux_x64/588429/chrome-linux.zip && \
        mkdir -p /home/jovyan/.local/share/pyppeteer/local-chromium/588429/chrome-linux/ && \
        unzip chrome-linux.zip -d /home/jovyan/.local/share/pyppeteer/local-chromium/588429/ && \
        rm -f chrome-linux.zip
