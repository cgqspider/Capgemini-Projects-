//function for adding the event to the server
//add or update event

function addorupdevt()
{
   
    var stuobj = {
        eid:parseInt(document.getElementById('eid').innerHTML),
        etime: $('#time').val(),
        etitle: $('#title').val(),
        edesc: $('#desc').val()
    };

    //Validation
    var status = true;
    //Regular Expression for time HH.MM FORMAT
    var re = /^([01]\d|2[0-3]).?([0-5]\d)$/;
    var validtime=re.test($('#time').val());
    
    
    $(".error").remove();
    //Required
    if (validtime==false || stuobj.etime.length < 1) {
        $('#time').after('<span class="error">** Time is required with HH.MM format**</span>');
        status = false;

    }
    //Required
    if (stuobj.etitle.length < 1) {
        $('#title').after('<span class="error">** Title is required and should contain less than 40 Characters **</span>');
        status = false;

    }
    //Required
    if (stuobj.edesc.length < 1) {
        $('#desc').after('<span class="error">** Description is required **</span>');
        status = false;

    }
    //If everything is ok then register the event
   if(status!=false) {

        if (document.getElementById('addorupdevt').innerHTML == "Add Event") { addevent(stuobj); }
        else { PostEditEvent(stuobj); }
    }
}

//Function to add the event
function addevent(stuobj) {

  
        $('#loader').show();
        $.ajax({
            url: "/Admin/addevent",
            data: JSON.stringify(stuobj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                $('#loader').hide();
                $('#msg').show();
                var trHtml = '';
                Clear_Event_Form();
                LoadEventData();
              

            },
            error: function (errormessage) {
                alert("Something Went wrong please try again");
                $('#loader').hide();
            }

        });

    
}


//function for clearing the whole form
function Clear_Event_Form() {

    $('#time').val("");
    $('#title').val("");
    $('#desc').val("");
   
}


//Display all the data  to the table
function LoadEventData() {
    
    //Check weather admin is loggedin or not
    if (localStorage.admid == "null") {
        alert("Please Login First...");
        window.location.href = "/Admin/Admlogin";

    }   
        //If logged in then display all the event data
    else {
        $('#loader').show();
        $.ajax({
            url: "/Admin/dispallevents",
            type: "GET",
            contentType: "application/Json;charset=UTF-8",
            dataType: "Json",
            success: function (result) {
                console.log(result);
                $('#tbleventdisp tbody').html('');

                var trHtml = '';

                $.each(result, function (i, item) {

                    trHtml += '<tr><td>' + result[i].etime + '</td>' + '<td>' + result[i].etitle + '</td>' + '<td>' + result[i].edesc + '</td>' + ' <td><a class="btn btn-success" onclick="GetEditEvent(' + item.eid + ')">edit</a>  <a class="btn btn-danger" onclick="DeleteEvent(' + item.eid + ')">Delete</a></td></tr>'

                });
                $("#tbleventdisp").append(trHtml);
                $('#loader').hide();

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }

        });

    }
}


//delete the event from sql server
function DeleteEvent(id)
{
    $('#loader').show();
    var ans = confirm("Are you sure you want to delete this record ? ");
    if (ans) {
        $.ajax({
            url: "/Admin/delevent/" + id,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#msg').show();
                LoadEventData();
                $('#loader').hide();
            },
            error: function (errormessage) {
                alert("Something went wrong please try again");
                $('#loader').hide();
            }

        });

    }

    else {
        $('#loader').hide();
    }

}


//Get edit method 
function GetEditEvent(id)
{
    $('#loader').show();
    $.ajax({
        url: "/Admin/findevent/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            document.getElementById('eid').innerHTML = result.eid;
            $('#time').val(result.etime);
            $('#title').val(result.etitle);
            $('#desc').val(result.edesc);
            document.getElementById('addorupdevt').innerHTML = "Update";
            $('#loader').hide();
            $('#cancel').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    });
}


//Post Edit Method
function PostEditEvent(stuobj)
{
    
    $('#loader').show();
    $.ajax({
        url: "/Admin/updevent",
        data: JSON.stringify(stuobj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            $('#loader').hide();
            $('#msg').show();
            var trHtml = '';
            Clear_Event_Form();
            LoadEventData();
            document.getElementById('addorupdevt').innerHTML = "Add Event";
            cancelupd();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    });

   
}


//cancel update
function cancelupd() {
    Clear_Event_Form();
    $('#cancel').hide();
    document.getElementById('addorupdevt').innerHTML = "Add Event";
}






