Build image:
docker build . -t catalibugnar/finish_notebook

Run image:
docker run catalibugnar/finish_notebook

Local usage:
./finish_notebook.py --notebook_service_url http://localhost:12700 --scheduled_notebook_id 65e35f610c03b39e958f8c6f --output_parameters_file_path ./inputs/output_parameters.txt
