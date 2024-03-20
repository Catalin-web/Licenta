from enum import Enum
import dataclasses
from models.FinishScheduledNotebookRequest import Status
from models.ModelType import ModelType

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
    
    def to_dict(self):
        return {
            "name": self.name,
            "value": self.value,
        }


@dataclasses.dataclass(frozen=True)
class NotebookParameterToGenerate:
    name_of_the_parameter: str
    description_of_theParameter: str
    model_type: ModelType

    @staticmethod
    def from_dict(notebook_parameter_dict: dict):
        return NotebookParameterToGenerate(
            name_of_the_parameter=notebook_parameter_dict["nameOfTheParameter"],
            description_of_theParameter=notebook_parameter_dict[
                "descriptionOfTheParameter"
            ],
            model_type=ModelType(notebook_parameter_dict["modelType"]),
        )

    def to_dict(self):
        return {
            "nameOfTheParameter": self.name_of_the_parameter,
            "descriptionOfTheParameter": self.description_of_theParameter,
            "modelType": self.model_type.value,
        }


@dataclasses.dataclass(frozen=True)
class ScheduledNotebook:
    id: str
    notebook_name: str
    progress: Progress
    status: Status
    input_parameters: list[NotebookParameter]
    input_parameters_to_generate: list[NotebookParameterToGenerate]
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
            input_parameters_to_generate=[
                NotebookParameterToGenerate.from_dict(notebook_parameter_dict)
                for notebook_parameter_dict in scheduled_notebook_dict[
                    "inputParametersToGenerate"
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
