using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseThink.NTCA.DataEntity
{
    public enum Status : int
    {
        RenewalForRecogntion = 1,
        PendingForEvaluation = 2,
        AssignedToEvaluator = 3,
        //AssignedForEvaluation = 3,
        EvaluationCompleted = 4,
        InTechnicalCommittee = 5,
        ShowCause = 6,
        Withheld = 7,
        Recognized = 8,
        ShowCauseReplied = 9,
        CancellationLetter = 10,
        AppealToSecretary = 11,
        Derecognized = 12,
        Rejected = 13
    }
}
