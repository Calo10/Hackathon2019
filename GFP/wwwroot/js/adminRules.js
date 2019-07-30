// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
       $('#tblAdminRules').dataTable();
       GetAllReceiveData();
    });

var jq = $(document);

function GetAllReceiveData ()
{
   
    var lstReceiveData;

    $.ajax({
        url: 'https://localhost:5001/ReceiveData/GetRules',
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

    var table = $('#tblAdminRules');

    table.find("tbody tr").remove();
    console.log(lstReceiveData);


    lstReceiveData.forEach(function (data) {

        if(data.active != 'N')
        {
            table.append("<tr><td>" + data.idRules + "</td><td>" + data.name + "</td><td>" + data.parameter + "</td><td>" + data.description + "</td><td> <img height='20' src='http://css-stars.com/wp-content/uploads/2014/06/checked-checkbox-xxl.png' /> </td></tr>");
        }
        else
        {
            table.append("<tr><td>" + data.idRules + "</td><td>" + data.name + "</td><td>" + data.parameter + "</td><td>" + data.description + "</td><td></td></tr>");
        }
        
    });


}