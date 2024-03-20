FROM python:3.12
WORKDIR /app
COPY . /app

RUN python3 -m pip install -r /app/requirements.txt

CMD ["python3", "/app/run_notebook.py",\
    "--notebook_input_path", "/app/inputs/notebook.ipynb",\
    "--input_parameters_file_path", "/app/inputs/input_parameters.json",\
    "--input_parameters_to_generate_output", "/app/inputs/input_parameters_to_generate.json",\
    "--output_parameters_file_path", "/app/inputs/output_parameters_names.txt",\
    "--output_parameters_output_file_path", "/app/outputs/output_parameters.txt"]