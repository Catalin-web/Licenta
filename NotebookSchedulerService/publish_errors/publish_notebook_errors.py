import click

from clients.NotebookServiceClient import NotebookServiceClient
from models.FinishScheduledNotebookRequest import (
    FinishScheduledNotebookRequest,
    Status,
)

@click.command
# ENV variables
@click.option("--notebook_service_url", type=click.STRING)
# Input arguments
@click.option(
    "--scheduled_notebook_id",
    type=click.STRING,
)
@click.option(
    "--error_message_file_path",
    type=click.STRING,
)
def publish_notebook_errors(
    notebook_service_url: str,
    scheduled_notebook_id: str,
    error_message_file_path: str,
):
    with open(error_message_file_path, "r") as error_message_file:
        error_message = error_message_file.read()
    finish_scheduled_notebook_request = FinishScheduledNotebookRequest(
        output_parameters=[],
        scheduled_notebook_id=scheduled_notebook_id,
        status=Status.FAILED,
        error_message=error_message,
    )
    notebook_service_client = NotebookServiceClient(notebook_service_url)
    notebook_service_client.finish_scheduled_notebook(finish_scheduled_notebook_request)

if __name__ == "__main__":
    publish_notebook_errors()