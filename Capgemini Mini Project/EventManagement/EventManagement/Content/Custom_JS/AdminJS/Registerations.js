//Display All Registerations
function DispRegs() {
    //Check weather admin is logged in or not
    if (localStorage.admid == "null") {
        alert("Please Login First...");
        window.location.href = "/Admin/Admlogin";

    }
        //if logged in then display all registerations of student
    else {
        $('#loader').show();
        $.ajax({
            url: "/Admin/dispregisterations",
            type: "GET",
            contentType: "application/Json;charset=UTF-8",
            dataType: "Json",
            success: function (result) {
                console.log(result);
                $('#tblregs tbody').html('');
                var trHtml = '';
                $.each(result, function (i, item) {
                    trHtml += '<tr><td>' + result[i].stuname + '</td>' + '<td>' + result[i].stufname + '</td>' + '<td>' + result[i].stuemail + '</td>' + '<td>' + result[i].etitle + '</td>';
                });
                $("#tblregs").append(trHtml);
                $('#loader').hide();

            },
            error: function (errormessage) {
                alert("Something Went wrong Please try again...");
            }

        });

    }
}

//admin login function
function AdmLoginChk() {

    var admobj = {
        admemail: $('#aemail').val(),
        admpwd: $('#apwd').val()
    };

   //Validations
    var status = true;
    $(".error").remove();
    //Valid email
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var validEmail = re.test($('#aemail').val());
    
    //Valid password
    var rePass = /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}/;
    var validPass = rePass.test(admobj.admpwd);
   
    //Required
    if (!validEmail || admobj.admemail.length < 1) {
        $('#aemail').after('<span class="error">** Enter a valid email **</span>');
        status = false;
    }
    //Required
    if (!validPass || admobj.admpwd.length < 1) {
        $('#apwd').after('<span class="error">** Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters **</span>');
        status = false;
    }
    //If everything is ok then go for login check of admin
    if (status != false) {
        $('#loader').show();
        $.ajax({
            url: "/Admin/adm_login_check",
            data: JSON.stringify(admobj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
               
                if (result == -1) {
                    $('#loader').hide();
                    $('#msg').show();
                }
                else {
                    $('#msg').hide();
                    $('#loader').hide();
                    localStorage.admid = parseInt(result);
                    $('#msgscs').show();
                    window.setTimeout(function () {
                        window.location.href = "/Admin/Index";
                    }, 3000);
                
                }

                var trHtml = '';


            },
            error: function (errormessage) {
                alert("Something went wrong please try again");
            }

        });
    }
}


//student function to logout from the panel
function AdmLogout() {
    localStorage.admid = null;
    window.location.href = "/Admin/Admlogin";
}

//Display all the events of students for fast scanner
function DispRegsEvtFS() {
    //Check weather admin is logged in or not
    if (localStorage.admid == "null") {
        alert("Please Login First then try again ...");
        window.location.href = "/Admin/Admlogin";

    }
    //If logged in then Give access of fast scanner to the admin
    else {
        $('#loader').show();
        $.ajax({
            url: "/Students/DispStuRegs/" + parseInt(localStorage.FSstuid),
            type: "GET",
            contentType: "application/Json;charset=UTF-8",
            dataType: "Json",
            success: function (result) {
                if (result.length == 0) {
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