Build image:
docker build . -t catalibugnar/initialize_notebook

Run image:
docker run catalibugnar/initialize_notebook

Local usage:
./init_notebook.py --notebook_service_url http://localhost:12700 --file_service_url http://localhost:12600 --scheduled_notebook_id 65f9d81c9d347315dd3969c7 --notebook_output_file_path ./outputs/notebook_output.ipynb --input_parameters_output_file_path ./outputs/input_parameters.json --input_parameters_to_generate_output_file_path ./outputs/input_parameters_to_generate.json --output_parameters_names_output_file_path ./outputs/output_parameters_names.txt