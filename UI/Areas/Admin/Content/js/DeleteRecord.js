var URL = "";
var ID = "";
function AskQuestion(url, id) {
    $("#modalmessage").modal();
    URL = url;
    ID = id;    
}
function Delete() {
    $.ajax(
        {
            url: URL + ID,
            type: "POST",
            success: function (resutl) {
                $("#a_" + ID).fadeOut();
                $("#modalmessage").modal('hide');

            }
        })
}