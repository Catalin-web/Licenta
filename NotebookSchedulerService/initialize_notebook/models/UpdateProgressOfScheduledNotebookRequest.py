from enum import Enum
import dataclasses


class Progress(Enum):
    CREATED = 0
    QUEUED = 1
    IN_PROGRESS = 2
    COMPLETED = 2


@dataclasses.dataclass(frozen=True)
class UpdateProgressOfScheduledNotebookRequest:
    scheduled_notebook_id: str
    progress: Progress

    def to_dict(self):
        return {
            "scheduledNotebookId": self.scheduled_notebook_id,
            "progress": self.progress.value,
        }
