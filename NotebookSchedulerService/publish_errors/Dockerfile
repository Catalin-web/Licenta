FROM python:3.12
WORKDIR /app
COPY . /app

RUN python3 -m pip install -r /app/requirements.txt

CMD ["python3", "/app/publish_notebook_errors.py",\
    "--notebook_service_url", "http://host.docker.internal:12700",\
    "--scheduled_notebook_id", "65f08460892df1f2c422790f", \
    "--error_message_file_path", "/app/inputs/error_message.txt"]
