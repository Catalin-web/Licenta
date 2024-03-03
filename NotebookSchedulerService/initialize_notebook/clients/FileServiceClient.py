import requests


class FileServiceClient:
    def __init__(self, file_service_url: str):
        self.file_service_url = file_service_url

    def download_notebook(self, notebook_name: str, file_path: str):
        response = requests.get(
            f"{self.file_service_url}/fileService/notebook/download/{notebook_name}",
        )
        with open(file_path, "wb") as file:
            file.write(response.content)
