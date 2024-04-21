import os
import requests
import time
import logging


class FileService:
    def __init__(self):
        self.base_url = "http://fileservice.default.svc.cluster.local:12600"
        self.upload_file_route = "/fileService/notebook/upload/"  # +file_name

    def upload_file(self, file_name, file_path):
        url = self.base_url + self.upload_file_route + file_name
        files = {"file": open(file_path, "rb")}
        response = requests.post(url, files=files)
        return response.status_code


# Upload all files from /home/jobyan to file service
def upload_files():
    try:
        logging.info("Uploading files....")
        print("Uploading files....")
        file_service = FileService()
        for file_name in os.listdir("/home/jovyan"):
            if "ipynb" not in file_name or ".ipynb_checkpoints" in file_name:
                continue
            file_path = os.path.join("/home/jovyan", file_name)
            file_service.upload_file(file_name, file_path)
    except Exception as e:
        print(e)


while True:
    upload_files()
    time.sleep(5)
