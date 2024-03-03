FROM python:3.12
WORKDIR /app
COPY . /app

RUN python3 -m pip install -r /app/requirements.txt

CMD ["python3", "/app/finish_notebook.py",\
    "--notebook_service_url", "http://host.docker.internal:12700",\
    "--scheduled_notebook_id", "65e360080c03b39e958f8c70", \
    "--output_parameters_file_path", "/app/inputs/output_parameters.txt"]
