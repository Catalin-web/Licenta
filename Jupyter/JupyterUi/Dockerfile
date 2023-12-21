FROM jupyterhub/k8s-hub:3.1.0

USER root

RUN apt-get update --yes && \
    apt-get install --yes --no-install-recommends procps && \
    # The following is for security patches
    apt-get install --yes --no-install-recommends --only-upgrade \
    # script-auto-update
    libjson-c5=0.15-2+deb11u1 \
    bind9-host=1:9.16.44-1~deb11u1 \
    bind9-libs=1:9.16.44-1~deb11u1 \
    bind9-dnsutils=1:9.16.44-1~deb11u1 \
    ncurses-base=6.2+20201114-2+deb11u2 \
    ncurses-bin=6.2+20201114-2+deb11u2 \
    dnsutils=1:9.16.44-1~deb11u1 \
    curl=7.74.0-1.3+deb11u10 \
    libcurl3-gnutls=7.74.0-1.3+deb11u10 \
    libcurl4=7.74.0-1.3+deb11u10 \
    libc6=2.31-13+deb11u7 \
    libc-bin=2.31-13+deb11u7 \
    libpq5=13.13-0+deb11u1
    # end script-auto-update

USER ${NB_UID}

COPY requirements.txt /tmp/requirements.txt

RUN pip install -r /tmp/requirements.txt
