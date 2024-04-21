#! /bin/bash

nohup python -u /app/background_process.py &

jupyterhub-singleuser --NotebookApp.tornado_settings "{\"headers\":{\"Content-Security-Policy\": \"frame-ancestors * self localhost:3005\"}}"