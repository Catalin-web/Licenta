import click
from clients.FileServiceClient import FileServiceClient

from clients.NotebookServiceClient import NotebookServiceClient
from models.UpdateProgressOfScheduledNotebookRequest import (
    Progress,
    UpdateProgressOfScheduledNotebookRequest,
)


@click.command()
# ENV variables
@click.option("--notebook_service_url", type=click.STRING)
@click.option("--file_service_url", type=click.STRING)
# Input arguments
@click.option(
    "--scheduled_notebook_id",
    type=click.STRING,
)
# Output arguments
@click.option(
    "--notebook_output_file_path",
    type=click.STRING,
)
@click.option(
    "--input_parameters_output_file_path",
    type=click.STRING,
)
@click.option(
    "--output_parameters_names_output_file_path",
    type=click.STRING,
)
def init_scheduled_notebook(
    notebook_service_url: str,
    file_service_url: str,
    scheduled_notebook_id: str,
    notebook_output_file_path: str,
    input_parameters_output_file_path: str,
    output_parameters_names_output_file_path: str,
):
    notebook_service_client = NotebookServiceClient(notebook_service_url)
    file_service_client = FileServiceClient(file_service_url)
    update_progress_of_scheduled_notebook_request = (
        UpdateProgressOfScheduledNotebookRequest(
            scheduled_notebook_id=scheduled_notebook_id, progress=Progress.IN_PROGRESS
        )
    )
    scheduled_notebook = (
        notebook_service_client.update_progress_of_a_scheduled_notebook(
            update_progress_of_scheduled_notebook_request
        )
    )
    file_service_client.download_notebook(
        scheduled_notebook.notebook_name, notebook_output_file_path
    )
    with open(input_parameters_output_file_path, "w") as file:
        for input_parameter in scheduled_notebook.input_parameters:
            file.write(f"{input_parameter.name}={input_parameter.value}\n")
    with open(output_parameters_names_output_file_path, "w") as file:
        for output_parameter_name in scheduled_notebook.output_parameters_names:
            file.write(f"{output_parameter_name}\n")


if __name__ == "__main__":
    init_scheduled_notebook()
