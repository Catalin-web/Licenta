FROM python:3.12
WORKDIR /app
COPY . /app

RUN python3 -m pip install -r /app/requirements.txt

CMD ["python3", "/app/init_notebook.py",\
    "--notebook_service_url", "http://host.docker.internal:12700",\
    "--file_service_url", "http://host.docker.internal:12600",\
    "--scheduled_notebook_id", "65f08460892df1f2c422790f", \
    "--notebook_output_file_path", "./outputs/notebook_output.ipynb", \ 
    "--input_parameters_output_file_path", "./outputs/input_parameters.json", \
    "--input_parameters_to_generate_output_file_path", "./outputs/input_parameters_to_generate.json", \
    "--output_parameters_names_output_file_path", "./outputs/output_parameters_names.txt"]