$(document).ready(function () {
   
});
$("#ddlEmails").change(function () {
    var selectedid = $('#ddlEmails').val();
    $("#spanid").text("");
    debugger;
    if (selectedid ==0) {
        $("#divuserdetails").css("display", "none");
        $("#spanid").text("Please select Email id");
    }
    else {
         CallAPI(selectedid);
        
    }

    // showhide();
})
function CallAPI(id) {
    var serviceURL = '/User/Details';
    $.ajax({
        type: "POST",
        url: serviceURL,
        data: "{'id':" + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

}
function successFunc(data, status) {
    debugger;
    if (data == "") {
        $("#spanid").text("User data not found");
    }
    if (status == "success" & data != "") {
        debugger;
        $("#tdFirstname").text(data.FirstName);
        $("#tdlastname").text(data.LastName);
        $("#tdbirthdate").text(data.Birthdate);
        $("#tdmobile").text(data.MobileNumber);
        $("#tdemail").text(data.Email);
        $("#tdcity").text(data.City);
        $("#tdcountry").text(data.Country);
        $("#tdtemp").text(data.temperature);
        $("#tdweathersummary").text(data.weathersummary);
        $("#divuserdetails").css("display", "block");
    }
}

function errorFunc() {
    alert('error');
}
