FROM jupyterhub/configurable-http-proxy:4.6.1 as builder

# TODO: Remove workaround and use jupyterhub/configurable-http-proxy after the alpine version is upgraded upstream
FROM node:lts-alpine3.18 as final

USER root

RUN mkdir -p /srv/configurable-http-proxy
COPY --from=builder /srv/configurable-http-proxy /srv/configurable-http-proxy
WORKDIR /srv/configurable-http-proxy

# Security patches
RUN npm install -g npm@9.8.1

# Switch from the root user to the nobody user
USER 65534

# Expose the proxy for traffic to be proxied (8000) and the
# REST API where it can be configured (8001)
EXPOSE 8000
EXPOSE 8001

# Put configurable-http-proxy on path for chp-docker-entrypoint
ENV PATH=/srv/configurable-http-proxy/bin:$PATH
ENTRYPOINT ["/srv/configurable-http-proxy/chp-docker-entrypoint"]