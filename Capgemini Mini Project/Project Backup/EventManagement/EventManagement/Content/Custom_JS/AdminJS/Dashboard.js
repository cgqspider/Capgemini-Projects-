//Get the dashboard
function Dashboard() {

    if (localStorage.admid == "null") {
        alert("Please Login First then try again ...");
        window.location.href = "/Admin/Admlogin";

    }
    else {

        $('#loader').show();

        $.ajax({
            url: "/Admin/GetDashboard",
            type: "GET",
            contentType: "application/Json;charset=UTF-8",
            dataType: "Json",
            success: function (result) {
                console.log(result);
                document.getElementById('TotalStudents').innerHTML = result.TotalStudents;
                document.getElementById('TotalEvents').innerHTML = result.TotalEvents;
                document.getElementById('TotalParticipations').innerHTML = result.TotalParticipations;
                document.getElementById('NotParticipations').innerHTML = result.TotalNotParticipations;

                $('#loader').hide();

            },
            error: function (errormessage) {
                alert("Something went wrong please try again");
                $('#loader').hide();

            }

        });
    }

}