import requests
from models.Notebook import Notebook
from models.ScheduledNotebook import ScheduledNotebook

from models.UpdateProgressOfScheduledNotebookRequest import (
    UpdateProgressOfScheduledNotebookRequest,
)


class NotebookServiceClient:
    def __init__(self, notebook_service_url: str):
        self.notebook_service_url = notebook_service_url

    def update_progress_of_a_scheduled_notebook(
        self,
        update_progress_of_scheduled_notebook_request: UpdateProgressOfScheduledNotebookRequest,
    ) -> ScheduledNotebook:
        response = requests.post(
            f"{self.notebook_service_url}/notebookService/scheduleNotebook/updateStatus",
            json=update_progress_of_scheduled_notebook_request.to_dict(),
        )
        response.raise_for_status()
        return ScheduledNotebook.from_dict(response.json())
