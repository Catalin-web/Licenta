from enum import Enum
import dataclasses
from models.FinishScheduledNotebookRequest import Status

from models.UpdateProgressOfScheduledNotebookRequest import Progress


@dataclasses.dataclass(frozen=True)
class NotebookParameter:
    name: str
    value: str

    @staticmethod
    def from_dict(notebook_parameter_dict: dict):
        return NotebookParameter(
            name=notebook_parameter_dict["name"], value=notebook_parameter_dict["value"]
        )


@dataclasses.dataclass(frozen=True)
class ScheduledNotebook:
    id: str
    notebook_name: str
    progress: Progress
    status: Status
    input_parameters: list[NotebookParameter]
    output_parameters: list[NotebookParameter]
    output_parameters_names: list[str]

    @staticmethod
    def from_dict(scheduled_notebook_dict: dict):
        return ScheduledNotebook(
            id=scheduled_notebook_dict["id"],
            notebook_name=scheduled_notebook_dict["notebookName"],
            progress=Progress.IN_PROGRESS,
            status=Status.NONE,
            input_parameters=[
                NotebookParameter.from_dict(notebook_parameter_dict)
                for notebook_parameter_dict in scheduled_notebook_dict[
                    "inputParameters"
                ]
            ],
            output_parameters=[
                NotebookParameter.from_dict(notebook_parameter_dict)
                for notebook_parameter_dict in scheduled_notebook_dict[
                    "outputParameters"
                ]
            ],
            output_parameters_names=scheduled_notebook_dict["outputParametersNames"],
        )
