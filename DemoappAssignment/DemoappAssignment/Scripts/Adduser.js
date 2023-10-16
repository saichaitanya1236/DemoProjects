$(document).ready(function () {
  //  alert("add  user js page");
    setdefaults();
    setdefaultcity();

   
})
function setdefaultcity() {
    var serviceURL = '/User/Getcitylist?countryid=1';
    var apitype = "GET"
    AjaxCallApi(serviceURL, apitype, "", successdata, errordata);
}
function setdefaults() {
    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();

    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var maxDate = year + '-' + month + '-' + day;
    $('#txtdate').attr('max', maxDate);
}
function AjaxCallApi(serviceURL, Apitype, data, successfun, errorfun) {
    $.ajax({
        type: Apitype,
        url: serviceURL,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successfun,
        error: errorfun
    });
}



$("#ddlCountries").change(function () {
    var countryid = $("#ddlCountries").val();
    if (countryid == 0) {

    }
    else {
        var serviceURL = '/User/Getcitylist?countryid=' + countryid;
        var apitype = "GET"
        AjaxCallApi(serviceURL, apitype, "", successdata, errordata);
    }

})

function successdata(data, status) {
    debugger;
    if (status == "success") {
        binddropdown(data);
    }
   
   
}
function errordata() {
    debugger;
    alert('error while loading page');
}

function binddropdown(data) {
    debugger;
    var arrayrec = data
    $("#ddlcity").find('option').remove().end();
    for (var i = 0; i < arrayrec.length; i++) {
        $("#ddlcity").append('<option value="' + arrayrec[i].Id + '">' + arrayrec[i].Text + '</option>');
    
    }
}

$("#btnsubmit").click(function () {
    var countryid = $("#ddlCountries").val();
    if (countryid == 0) {
        $("#spanerrormsg").Text("please specify country");
    }
})