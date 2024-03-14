import requests
from models.FinishScheduledNotebookRequest import FinishScheduledNotebookRequest


class NotebookServiceClient:
    def __init__(self, notebook_service_url: str):
        self.notebook_service_url = notebook_service_url

    def finish_scheduled_notebook(
        self, finish_scheduled_notebook_request: FinishScheduledNotebookRequest
    ):
        response = requests.post(
            f"{self.notebook_service_url}/notebookService/scheduleNotebook/finish",
            json=finish_scheduled_notebook_request.to_dict(),
        )
