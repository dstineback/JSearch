using System.ComponentModel;

namespace JSearch.Models
{
    public enum SubmissionStatus
    {
        [Description("New")]
        New = 0,
        [Description("Submitted")]
        Submitted = 1,
        [Description("Interviewing")]
        Interviewing = 2,
        [Description("Denied")]
        Denied = 3,
    }
}
