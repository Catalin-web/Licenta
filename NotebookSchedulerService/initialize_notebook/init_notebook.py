import json
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
    "--input_parameters_to_generate_output_file_path",
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
    input_parameters_to_generate_output_file_path: str,
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
    input_parameters_list = [parameter.to_dict() for parameter in scheduled_notebook.input_parameters]

    with open(input_parameters_output_file_path, "w") as file:
        json.dump({"parameters": input_parameters_list}, file)

    with open(output_parameters_names_output_file_path, "w") as file:
        for output_parameter_name in scheduled_notebook.output_parameters_names:
            file.write(f"{output_parameter_name}\n")

    list_of_input_parameters_to_generate = []
    for input_parameter_to_generate in scheduled_notebook.input_parameters_to_generate:
        list_of_input_parameters_to_generate.append(
            input_parameter_to_generate.to_dict()
        )
    json_input_parameters_to_generate = {
        "inputParametersToGenerate": list_of_input_parameters_to_generate
    }
    with open(input_parameters_to_generate_output_file_path, "w") as file:
        string_to_write = json.dumps(json_input_parameters_to_generate)
        file.write(string_to_write)


if __name__ == "__main__":
    init_scheduled_notebook()
