using System.ComponentModel;

namespace JSearch.Models
{
    public enum ReferallStatus
    {
        [Description("Internet")]
        Internet= 0,
        [Description("Personal")]
        Personal = 1,
        [Description("Friend Referral")]
        Friend = 2,
        [Description("Recruiter")]
        Recruiter=3
    }
}
