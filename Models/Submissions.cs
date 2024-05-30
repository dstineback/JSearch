using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using JSearch.Helpers;
using JSearch.Services;

namespace JSearch.Models
{
    public class Submissions
    {
        public long SubmissionId { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 0)]
        public string Position { get; set; }

        [Required(ErrorMessage = "Company is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 0)]
        public string Company { get; set; }

        
        [StringLength(maximumLength: 50, MinimumLength = 0)]
        public string? ContactName { get; set; } = string.Empty;


        [EmailAddress]
        [StringLength(maximumLength: 50, MinimumLength = 0)]
        public string? ContactEmail { get; set; } = string.Empty;


        [Phone(ErrorMessage = "Not valid phone")]
        [StringLength(maximumLength: 50, MinimumLength = 0)]
        public string? ContactPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        public int Status { get; set; }

        [Required(ErrorMessage = "Job Description is required.")]
        public string JobDescription { get; set; }

        [Required(ErrorMessage = "Referral Type is required.")]
        public int ReferralType { get; set; }


        [StringLength(maximumLength: 50, MinimumLength = 0)]
        public string? ReferralContactName { get; set; } = string.Empty;


        [EmailAddress(ErrorMessage =  "not valid email")]
        [StringLength(maximumLength:50, MinimumLength = 0)]
        public string? ReferralContactEmail { get; set; } = string.Empty ;

        
        [Phone(ErrorMessage = "Not valid phone")]
        [StringLength(maximumLength: 50, MinimumLength = 0)]
        public string? ReferralPhone { get; set; } = string.Empty;

        public DateTime FirstContact { get; set; } = DateTime.Now;
        public DateTime FollowUpContact { get; set; } = DateTime.Now.AddDays(7);
        public DateTime? DeniedDate { get; internal set; }

        public string? FormattedStatus
        {
            get
            {
                return EnumHelpers.GetDescription((SubmissionStatus)Status);
            }
        }

        public string? FormattedRefType
        {
            get
            {
                return EnumHelpers.GetDescription((ReferallStatus)Status);
            }
        }

        public string? FormattedFirstContact
        {
            get
            {

                return FirstContact.ToString("MM/dd/yyyy");
            }
        }

        public string? FormattedFollowupContact
        {
            get
            {
                return FollowUpContact.ToString("MM/dd/yyyy");
            }
        }

        public string? FormattedDeniedDate
        {
            get
            {
                if (DeniedDate.HasValue)
                {

                    return DeniedDate.Value.ToString("MM/dd/yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string? Website { get; set; }
        public Submissions() { }

        public Submissions(long id, string conn) 
        {
            GetData(id, conn);
        }

        public void  GetData(long id, string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {

                    using (SqlCommand command = new SqlCommand("GET_SUBMISSION_DATA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters if needed
                        command.Parameters.AddWithValue("@id", id);
                      

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                this.Company = reader.GetString("COMPANY");
                                this.ContactEmail = reader.GetString("CONTACT_EMAIL");
                                this.Position = reader.GetString("POSITION");
                                this.ContactName = reader.GetString("CONTACT_NAME");
                                this.ContactPhone = reader.GetString("CONTACT_PHONE");
                                this.Status = reader.GetInt32("Status");
                                this.JobDescription = reader.GetString("JOB_DESCRIPTION");
                                this.Website = reader.GetString("WEB_SITE");
                                this.ReferralType = reader.GetInt32("REFERRAL_TYPE");
                                
                            }

                        };

                        this.SubmissionId = id;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
               
            }
        }

        internal static void Add(Submissions submissions, string conn)
        {
                using (SqlConnection connection = new SqlConnection(conn))
                {
            try
            {

                    using (SqlCommand command = new SqlCommand("INSERT_SUBMISSIONS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters if needed
                        command.Parameters.AddWithValue("@POSITION", submissions.Position ?? string.Empty);
                        command.Parameters.AddWithValue("@COMPANY", submissions.Company ?? string.Empty);
                        command.Parameters.AddWithValue("@CONTACT_NAME", submissions.ContactName ?? string.Empty);
                        command.Parameters.AddWithValue("@CONTACT_EMAIL", submissions.ContactEmail ?? string.Empty);
                        command.Parameters.AddWithValue("@CONTACT_PHONE", submissions.ContactPhone ?? string.Empty );
                        command.Parameters.AddWithValue("@STATUS",submissions.Status );
                        command.Parameters.AddWithValue("@JOB_DESCRIPTION", submissions.JobDescription ?? string.Empty);
                        command.Parameters.AddWithValue("@REFERRAL_TYPE", submissions.ReferralType);
                        command.Parameters.AddWithValue("@REFERRAL_CONTACT_NAME", submissions.ReferralContactName ?? string.Empty );
                        command.Parameters.AddWithValue("@REFERRAL_CONTACT_EMAIL", submissions.ReferralContactEmail ?? string.Empty );
                        command.Parameters.AddWithValue("@REFERRAL_PHONE", submissions.ReferralPhone ?? string.Empty );
                        command.Parameters.AddWithValue("@WEBSITE", submissions.Website ?? string.Empty);
                        // Add more parameters as necessary

                        connection.Open();
                        command.ExecuteNonQuery();
                        
                    }
                }catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }
        }

        internal static void Update(Submissions submissions, string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {

                    using (SqlCommand command = new SqlCommand("UPDATE_SUBMISSIONS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters if needed
                        command.Parameters.AddWithValue("@SUBMISSION_ID", submissions.SubmissionId);
                        command.Parameters.AddWithValue("@POSITION", string.IsNullOrEmpty( submissions.Position) ? null : submissions.Position );
                        command.Parameters.AddWithValue("@COMPANY", string.IsNullOrEmpty( submissions.Company) ? null : submissions.Company );
                        command.Parameters.AddWithValue("@CONTACT_NAME", string.IsNullOrEmpty( submissions.ContactName) ? null : submissions.ContactName );
                        command.Parameters.AddWithValue("@CONTACT_EMAIL", string.IsNullOrEmpty( submissions.ContactEmail) ? null : submissions.ContactEmail );
                        command.Parameters.AddWithValue("@CONTACT_PHONE", string.IsNullOrEmpty( submissions.ContactPhone) ? null : submissions.ContactPhone );
                        command.Parameters.AddWithValue("@STATUS", submissions.Status);
                        command.Parameters.AddWithValue("@JOB_DESCRIPTION",string.IsNullOrEmpty( submissions.JobDescription) ? null : submissions.JobDescription );
                        command.Parameters.AddWithValue("@REFERRAL_TYPE", submissions.ReferralType);
                        command.Parameters.AddWithValue("@REFERRAL_CONTACT_NAME", string.IsNullOrEmpty( submissions.ReferralContactName) ? null : submissions.ReferralContactName );
                        command.Parameters.AddWithValue("@REFERRAL_CONTACT_EMAIL", string.IsNullOrEmpty( submissions.ReferralContactEmail) ? null : submissions.ReferralContactEmail );
                        command.Parameters.AddWithValue("@REFERRAL_PHONE", string.IsNullOrEmpty( submissions.ReferralPhone) ? null : submissions.ReferralPhone);
                        command.Parameters.AddWithValue("@DENIED_DATE", submissions.DeniedDate);
                        command.Parameters.AddWithValue("@WEBSITE", submissions.Website ?? string.Empty);

                        connection.Open();
                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
