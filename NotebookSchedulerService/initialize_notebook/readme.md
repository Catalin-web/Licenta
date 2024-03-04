Build image:
docker build . -t catalibugnar/initialize_notebook

Run image:
docker run catalibugnar/initialize_notebook

Local usage:
./init_notebook.py --notebook_service_url http://localhost:12700 --file_service_url http://localhost:12600 --scheduled_notebook_id 65e4e145176e0f4f04518e13 --notebook_output_file_path ./outputs/notebook_output.ipynb --input_parameters_output_file_path ./outputs/input_parameters.txt --output_parameters_names_output_file_path ./outputs/output_parameters_names.txt