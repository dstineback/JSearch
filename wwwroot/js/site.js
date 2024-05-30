// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function openSubmissionModalFromRow(rowData) {
    // Populate modal content with data from the clicked row
    $('#submissionModal .modal-body').html('<p><strong>Company:</strong> ' + rowData[1] + '</p>' +
        '<p><strong>Position:</strong> ' + rowData[0] + '</p>' +
        '<p><strong>Date Applied:</strong> ' + rowData[12] + '</p>' +
        '<p><string>Description: </strong>  ' + rowData[7] + '<p>' +
        '<p style="display:none;">' + rowData[13] + '</p>' +
        '<input type="hidden" id="hiddenInputField" name="hiddenInputField" value="' + rowData[15] + '" />' // Set value for hidden input field
    );

    // Show the modal
    $('#submissionModal').modal('show');
}

function openUpdateModal(rowData) {
    var submission = JSON.parse(rowData);
    $('#companyName').val(submission.Company);
    $('#referralType').val(parseInt(submission.ReferralType));
    $('#position').val(submission.Position);
    $('#status').val(parseInt(submission.Status));
    $('#contactName').val(submission.ContactName);
    $('#contactEmail').val(submission.ContactEmail);
    $('#contactPhone').val(submission.ContactPhone);
    $('#referralPhone').val(submission.ReferralPhone);
    $('#referralContactEmail').val(submission.ReferralContactEmail);
    $('#referralContactName').val(submission.ReferralContactName);
    $('#jobDescription').val(submission.JobDescription);
    $('#website').val(submission.Website);
     
    
      $('#updateModal').modal('show');
}
