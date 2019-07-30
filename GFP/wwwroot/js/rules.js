// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
       $('#tblSocialProgramRules').dataTable();
       GetAllReceiveData();
    });

var jq = $(document);

function GetAllReceiveData ()
{
   
    var lstReceiveData;

    $.ajax({
        url: 'https://localhost:5001/ReceiveData/GetRuleSocialPrograms',
        type: 'Get',
        traditional: true,
        async: false,
        error: function (xhr, ajaxOptions, thrownError) {

        },
        beforeSend: function (jqXHR, settings) {

        },
        success: function (data) {
            lstReceiveData = data;
        },
        complete: function () {

        }
    });

    var table = $('#tblSocialProgramRules');

    table.find("tbody tr").remove();
    console.log(lstReceiveData);


    lstReceiveData.forEach(function (data) {

        if(data.rules_break != null)
        {
            table.append("<tr><td>" + data.id + "</td><td>" + data.first_name + "</td><td>" + data.last_name + "</td><td>" + data.email + "</td><td>" + data.program + "</td><td>" + data.date + "</td><td>" + data.value + "</td><td>" + data.rules_break + "</td></tr>");
        }
        else
        {
            table.append("<tr><td>" + data.id + "</td><td>" + data.first_name + "</td><td>" + data.last_name + "</td><td>" + data.email + "</td><td>" + data.program + "</td><td>" + data.date + "</td><td>" + data.value + "</td><td><img height='20' src='https://www.lupusuk.org.uk/wp-content/uploads/2018/03/Green-Tick-PNG-Picture.png'/></td></tr>");
        }
        
    });


}
