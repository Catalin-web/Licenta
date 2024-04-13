Build image:
docker build . -t catalibugnar/publish_notebook_errors

Run image:
docker run catalibugnar/publish_notebook_errors

Local usage:
./publish_notebook_errors.py --notebook_service_url http://localhost:12700 --scheduled_notebook_id 66123702c923beba0edf31b6 --error_message_file_path ./inputs/error_message.txt
