from enum import Enum
import dataclasses


class Status(Enum):
    NONE = 0
    SUCCEDED = 1
    FAILED = 2


@dataclasses.dataclass(frozen=True)
class FinishScheduledNotebookRequest:
    scheduled_notebook_id: str
    status: Status
    resulted_parameters: dict[str, str]

    def to_dict(self):
        return {
            "scheduledNotebookId": self.scheduled_notebook_id,
            "status": self.status,
            "resultedParameters": self.resulted_parameters,
        }
