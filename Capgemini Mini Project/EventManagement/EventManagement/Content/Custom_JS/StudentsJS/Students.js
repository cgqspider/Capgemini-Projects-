//function for adding the form
function add() {

    var stuobj = { 
        stuname: $('#name').val().trim(),
        stufname: $('#fname').val().trim(),
        stuemail: $('#email').val().trim().toLowerCase(),
        stupwd: $('#pwd').val().trim(),
    };
    
    //Validation for the form with regular expression and Required Field
    //-------------------------------------------------------------------
    var status = true;
    $(".error").remove();
    //Valid email
    //Must contains @ and .com inside a valid email
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var validEmail = re.test($('#email').val());

    //Valid password 
    //Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters
    var rePass = /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}/;
    var validPass = rePass.test(stuobj.stupwd);
    
    //Required Field Validation
    if (stuobj.stuname.length < 1)
    {
        $('#name').after('<span class="error">** Name is required **</span>');
        status = false;
    }


    //Required Field Validation
    if (stuobj.stufname.length < 1) {
        $('#fname').after('<span class="error">** Father name is required **</span>');
        status = false;
    }


    //Required Field Validation
    if (!validPass || stuobj.stupwd.length < 1) {
        
        $('#pwd').after('<span class="error">** Password must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters **</span>');
        status = false;
    }


    //Required Field Validation
    if (!validEmail || stuobj.stuemail.length < 1) {
        $('#email').after('<span class="error">** Enter a valid email **</span>');
        status = false;
    }


    //Validation for the form with regular expression and Required Field
    //-------------------------------------------------------------------
    //If Everything is Ok then send the Ajax Request to the Server

    if(status!=false)
    {

        $('#loader').show();
        $.ajax({
            url: "/Students/RegisterStudent",
            data: JSON.stringify(stuobj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result != -2)
                {
                    $('#loader').hide();
                    $('#msg').show();
                    Clear_Form();
                }
                else{
                    alert("Something went wrong please try again");
                    $('#loader').hide();
                }
            },
            error: function (errormessage) {
              
                alert("Something went wrong please try again");
                $('#loader').hide();
            }

        });

    }

    


}


//function for clearing the form
function Clear_Form() {

    $('#name').val("");
    $('#fname').val("");
    $('#email').val("");
    $('#pwd').val("");

}


//function for display the profile
function disp_profile()
{
    
    //Check for the user weather user is logged in or not
    if (localStorage.stuid == "null") {
        alert("Please Login First...");
        window.location.href = "/Students/Signin";

    }

    //If logged in then Send the request to the server for getting profile details
    else {
        $('#loader').show();
        $.ajax({
            url: "/Students/FindStudent/" + parseInt(localStorage.stuid),
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
              
                if (result != -2) {
                   
                    document.getElementById('stuid').innerHTML = "Entry Id :" + "AdViK" + result.stuid + "TYa19";
                    document.getElementById('name').innerHTML = result.stuname;
                    document.getElementById('fname').innerHTML = result.stufname;
                    document.getElementById('email').innerHTML = result.stuemail;
                    document.getElementById('pwd').innerHTML = "***********"; //result.stupwd

                    $('#loader').hide();
                }
                else {
                    alert("Something Went Wrong please try again");
                }
            },
            error: function (errormessage) {
                alert("Something Went Wrong please try again");
            }

        });
    }
}


//function for display the event timing to jquery timeline
function LoadEventData() {

    document.getElementById("btnregevt").disabled = false;
    // Check weather user logged in or not
    if (localStorage.stuid == "null") {
        alert("Please Login First...");
        window.location.href = "/Students/Signin";

    }

    //If logged In then Display the events
    else {
        $('#loader').show();

        $.ajax({
            url: "/Admin/dispallevents",
            type: "GET",
            contentType: "application/Json;charset=UTF-8",
            dataType: "Json",
            success: function (result) {
                console.log(result);
                $('#ddevt').html('');
                $.each(result, function (i, item) {
                    $("#ddevt").append($("<option></option>").val(result[i].eid).html(result[i].etitle));
                });
               
                $('#loader').hide();

            },
            error: function (errormessage) {
                alert("Something went wrong Please try again...");
            }

        });

    }
}


//function to register a student to the event
function StuRegEvt()
{
    regevtobj = {
        
        regstuid: parseInt(localStorage.stuid),
        regevtid:parseInt($('#ddevt').val())
    }

    $('#loader').show();
    document.getElementById("btnregevt").disabled = true;
    $.ajax({
        url: "/Students/StuRegEvt",
        data: JSON.stringify(regevtobj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#loader').hide();
          
            $('#msg').show();
            
            window.setTimeout(function () {
                window.location.href = "/Students/DispRegEvt";
            }, 3000);
          
           
        },
        error: function (errormessage) {
            
            alert("Something went wrong please try again...");
        }

    });
   


}


//function for display the students's registered event
function DispRegsEvt()
{
    //Check weather user logged in or not
    if (localStorage.stuid == "null") {
        alert("Please Login First...");
        window.location.href = "/Students/Signin";

    }
       //if logged in then Display the registered events
    else {

        $('#loader').show();

        $.ajax({
            url: "/Students/DispStuRegs/" + parseInt(localStorage.stuid),
            type: "GET",
            contentType: "application/Json;charset=UTF-8",
            dataType: "Json",
            success: function (result) {
                if (result.length == 0)
                {
                    alert("You Havent registered in any event...");
                }
                $('#tblregs tbody').html('');

                var trHtml = '';

                $.each(result, function (i, item) {
                 
                    trHtml += '<tr><td>' + result[i].stuname + '</td>' + '<td>' + result[i].stufname + '</td>' + '<td>' + result[i].stuemail + '</td>' + '<td>' + result[i].etitle + '</td>' + ' <td> <a class="btn btn-danger" onclick="DeRegister(' + result[i].eid + ')">Delete</a></td>';

                });
                $("#tblregs").append(trHtml);
                $('#loader').hide();

            },
            error: function (errormessage) {
                alert("Something went wrong please try again");
                $('#loader').hide();

            }

        });
    }

}


//function to deregister from the event
function DeRegister(evtid)
{
    //Check weather user logged in or not
    if (localStorage.stuid == "null") {
        alert("Please Login First...");
        window.location.href = "/Students/Signin";

    }
    //if logged in then Deregister the event on click
    else {
        var URLL = "/Students/Deregister/?stuid=" + localStorage.stuid + "&evtid=" + evtid;
        $('#loader').show();
        var ans = confirm("Are you sure you want to delete this record ? ");
        if (ans) {
            $.ajax({
                url: URLL,
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {

                    $('#msg').show();
                    DispRegsEvt();
                    $('#loader').hide();
                },
                error: function (errormessage) {
                    alert("Something went wrong please try again...");
                    $('#loader').hide();
                }

            });

        }

        else {
            $('#loader').hide();
        }

    }

}


//student funtion to check the login
function StuLoginChk()
{

    var stuobj = {
        stuemail: $('#email').val().trim().toLowerCase(),
        stupwd: $('#pwd').val().trim()
    };

    //Validation
    var status = true;
    $(".error").remove();
    //Valid email
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var validEmail = re.test($('#email').val());
    //Valid password
    var rePass = /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}/;
    var validPass = rePass.test(stuobj.stupwd);

    if (!validEmail || stuobj.stuemail.length < 1) {
        $('#email').after('<span class="error">** Enter a valid email **</span>');
        status = false;
    }
    if (!validPass || stuobj.stupwd.length < 1) {
        $('#pwd').after('<span class="error">** Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters **</span>');
        status = false;
    }

    //If everything is ok then send the request to the server for LOGIN Check
    if (status != false) {

        $('#loader').show();
        $.ajax({
            url: "/Students/stu_login_check",
            data: JSON.stringify(stuobj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result == -1) {
                    $('#loader').hide();
                    $('#msg').show();
                    return;
                }
                else if (result == -2) {
                    alert("Something Went wrong... Please try again");
                    return;
                }

                else {

                    $('#msg').hide();
                    $('#loader').hide();
                    localStorage.stuid = parseInt(result);
                    $('#msgscs').show();
                    window.setTimeout(function () {
                        window.location.href = "/Students/Profile";
                    }, 3000);

                }

                var trHtml = '';
                Clear_Form();

            },
            error: function (errormessage) {
                alert("Something went wrong Please Try again...");
                $('#loader').hide();
            }

        });
    }
}


//student function to logout from the panel
function StuLogout()
{
    localStorage.stuid = null;
    window.location.href = "/Students/Index";
}

