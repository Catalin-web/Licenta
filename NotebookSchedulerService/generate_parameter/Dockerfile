FROM python:3.12
WORKDIR /app
COPY . /app

RUN python3 -m pip install -r /app/requirements.txt

CMD ["python3", "/app/generate_parameter.py",\
    "--generator_service_url", "http://host.docker.internal:12800",\
    "--input_parameters_to_generate_file_path", "/app/inputs/input_parameters_to_generate.json",\ 
    "--input_parameters_to_generate_output_file_path", "./outputs/generated_parameters.json"]