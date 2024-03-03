from enum import Enum
import dataclasses


@dataclasses.dataclass(frozen=True)
class NotebookParameter:
    name: str
    value: str

    def to_dict(self):
        return {
            "name": self.name,
            "value": self.value,
        }


class Status(Enum):
    NONE = 0
    SUCCEDED = 1
    FAILED = 2


@dataclasses.dataclass(frozen=True)
class FinishScheduledNotebookRequest:
    scheduled_notebook_id: str
    status: Status
    output_parameters: list[NotebookParameter]

    def to_dict(self):
        return {
            "scheduledNotebookId": self.scheduled_notebook_id,
            "status": self.status.value,
            "outputParameters": [param.to_dict() for param in self.output_parameters],
        }
