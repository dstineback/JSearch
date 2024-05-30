using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using JSearch.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JSearch.Helpers;

public class DashboardModel : PageModel
{
    private readonly IConfiguration _connectionString;

    public DashboardModel(IConfiguration configuration)
    {
        _connectionString = configuration;
    }

    public int TotalSubmissions { get; set; }
    public int PendingSubmissions { get; set; }
    public int DeniedSubmissions { get; set; }
    public List<Submissions> Submissions { get; set; }

    public Submissions SelectedSubmission { get; set; }

    public void OnGet()
    {
        GetSubmissions();
        SelectedSubmission = new Submissions();
    }

    
    public JsonResult OnGetUpdate(long id)
    {
        string connectionString = _connectionString.GetConnectionString("Connection");
        SelectedSubmission = new Submissions(id, connectionString);
        GetSubmissions();

        var jSon = JsonConvert.SerializeObject(SelectedSubmission);

        return new JsonResult(jSon);
    }

    private void GetSubmissions()
    {
        Submissions = new List<Submissions>();

        using (SqlConnection connection = new SqlConnection(_connectionString.GetConnectionString("Connection")))
        {
            using (SqlCommand command = new SqlCommand("Get_Submissions", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Submissions.Add(new Submissions
                        {
                            SubmissionId = reader.GetInt64(reader.GetOrdinal("ID")),
                            Position = reader.GetString(reader.GetOrdinal("Position")),
                            Company = reader.GetString(reader.GetOrdinal("Company")),
                            ContactName = reader.GetString(reader.GetOrdinal("Contact_Name")),
                            ContactEmail = reader.GetString(reader.GetOrdinal("Contact_Email")),
                            ContactPhone = reader.GetString(reader.GetOrdinal("Contact_Phone")),
                            Status = reader.GetInt32(reader.GetOrdinal("Status")),
                            JobDescription = reader.GetString(reader.GetOrdinal("Job_Description")),
                            ReferralType = reader.GetInt32(reader.GetOrdinal("Referral_Type")),
                            ReferralContactName = reader.GetString(reader.GetOrdinal("Referral_Contact_Name")),
                            ReferralContactEmail = reader.GetString(reader.GetOrdinal("Referral_Contact_Email")),
                            ReferralPhone = reader.GetString(reader.GetOrdinal("Referral_Phone")),
                            FirstContact = reader.GetDateTime(reader.GetOrdinal("First_Contact")),
                            FollowUpContact = reader.GetDateTime(reader.GetOrdinal("Follow_Up_Contact")),
                            DeniedDate = reader.IsDBNull(reader.GetOrdinal("Denied_Date")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("Denied_Date")),
                            Website = reader.IsDBNull(reader.GetOrdinal("WEB_SITE")) ? null : reader.GetString(reader.GetOrdinal("WEB_SITE"))
                            
                        });

                        switch (reader.GetInt32(reader.GetOrdinal("Status")))
                        {
                            case 0:
                            case 1:
                            case 2:
                                PendingSubmissions++;
                                break;
                            case 3:DeniedSubmissions++; break;
                        }
                        TotalSubmissions++;
                    }
                }
            }
        }

        Submissions = Submissions.OrderByDescending(s => s.FirstContact).ToList();
    }

    public JsonResult OnGetSave(Submissions data)
    {
        string connectionString = _connectionString.GetConnectionString("Connection");
        if(data.Status == (int)SubmissionStatus.Denied) data.DeniedDate = DateTime.Now;
        JSearch.Models.Submissions.Update(data, connectionString);
        GetSubmissions();
        SelectedSubmission = new Submissions() { };
        var jSon = JsonConvert.SerializeObject(Submissions);
        return new JsonResult(jSon);    
    }
}



