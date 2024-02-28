ARG JUPYTERHUB_VERSION
FROM quay.io/jupyterhub/jupyterhub:$JUPYTERHUB_VERSION

RUN python3 -m pip install --no-cache-dir \
    dockerspawner \
    jupyterhub-nativeauthenticator

CMD ["jupyterhub", "-f", "/srv/jupyterhub/jupyterhub_config.py"]