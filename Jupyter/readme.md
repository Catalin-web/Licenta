# Build notebook image
docker build .\JupyterNotebookImage\ -t jupyter-notebook-image

# Build proxy
docker build .\JupyterProxy\ -t jupyter-proxy

# Build ui
docker build .\JupyterUi\ -t jupyter-ui

# Release

helm repo add jupyterhub https://hub.jupyter.org/helm-chart/
helm repo update

helm upgrade --cleanup-on-fail --install jupyterhub/jupyterhub --namespace default --values values.yml

helm show values jupyterhub/jupyterhub > tmp.yml

# Cmd:
helm install jupyterhub jupyterhub/jupyterhub --values .\values.yml

# Port forward by public proxy:
App running on http://localhost://8000