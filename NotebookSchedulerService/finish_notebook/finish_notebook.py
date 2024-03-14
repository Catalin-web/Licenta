import json
import click

from clients.NotebookServiceClient import NotebookServiceClient
from models.FinishScheduledNotebookRequest import (
    FinishScheduledNotebookRequest,
    NotebookParameter,
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
    "--output_parameters_file_path",
    type=click.STRING,
)
def finish_notebook(
    notebook_service_url: str,
    scheduled_notebook_id: str,
    output_parameters_file_path: str,
):
    notebook_service_client = NotebookServiceClient(notebook_service_url)
    output_parameters: list[NotebookParameter] = []
    with open(output_parameters_file_path, "r") as file:
        dict = json.load(file)
        output_parameters_dict = dict["output_parameters"]
        for parameter in output_parameters_dict:
            output_parameters.append(NotebookParameter.from_dict(parameter))
    finish_scheduled_notebook_request = FinishScheduledNotebookRequest(
        scheduled_notebook_id, Status.SUCCEDED, output_parameters
    )
    notebook_service_client.finish_scheduled_notebook(finish_scheduled_notebook_request)


if __name__ == "__main__":
    finish_notebook()
