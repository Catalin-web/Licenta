Build image:
docker build . -t catalibugnar/generate_parameter

Run image:
docker run catalibugnar/generate_parameter

Local usage:
./generate_parameter.py --generator_service_url http://localhost:12800 --input_parameters_to_generate_file_path ./inputs/input_parameters_to_generate.json --input_parameters_to_generate_output_file_path ./outputs/generated_parameters.json --has_errors_output_file_path ./outputs/has_errors.txt --error_message_output_file_path ./outputs/error_message.txt
