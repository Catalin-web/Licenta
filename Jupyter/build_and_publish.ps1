docker build . -t catalibugnar/jupyterhub --no-cache
docker push catalibugnar/jupyterhub

docker build ./Notebook -t catalibugnar/jupyterhub-notebook --no-cache
docker push catalibugnar/jupyterhub-notebook