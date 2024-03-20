Build image:
docker build . -t catalibugnar/run_notebook

Run image:
docker run catalibugnar/run_notebook

Local usage:
./run_notebook.py --notebook_input_path ./inputs/notebook.ipynb --input_parameters_file_path ./inputs/input_parameters.json --input_parameters_to_generate_output ./inputs/input_parameters_to_generate.json --output_parameters_file_path ./inputs/output_parameters_names.txt --output_parameters_output_file_path ./outputs/output_parameters.json
