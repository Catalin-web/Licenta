FROM quay.io/jupyterhub/k8s-hub:3.3.7


USER ${NB_USER}

RUN python3 -m pip install --no-cache-dir \
    dockerspawner \
    jupyterhub-nativeauthenticator

COPY ./authenticator.py /custom/authenticator.py

ENV PYTHONPATH "${PYTHONPATH}:/custom"

# EXPOSE 8081