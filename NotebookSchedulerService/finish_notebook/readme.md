Build image:
docker build . -t catalibugnar/finish_notebook

Run image:
docker run catalibugnar/finish_notebook

Local usage:
./finish_notebook.py --notebook_service_url http://localhost:12700 --scheduled_notebook_id 65f07de2923c4626bdfaafb1 --output_parameters_file_path ./inputs/output_parameters.json --has_errors_output_file_path ./outputs/has_errors.txt --error_message_output_file_path ./outputs/error_message.txt
