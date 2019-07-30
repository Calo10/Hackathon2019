
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#tblConsolidatePayments').dataTable();
    GetAllReceiveData();
 });

var jq = $(document);

function GetAllReceiveData ()
{

 var lstReceiveData;

 $.ajax({
     url: 'https://localhost:5001/ReceiveData/GetConsolidatePayments',
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

 var table = $('#tblConsolidatePayments');

 table.find("tbody tr").remove();
 console.log(lstReceiveData);


 lstReceiveData.forEach(function (data) {

    
    table.append("<tr><td>" + data.id + "</td><td>" + data.firstName + "</td><td>" + data.iban + "</td><td>" + data.total_amount + "</td></tr>");
     
     
 });

}

