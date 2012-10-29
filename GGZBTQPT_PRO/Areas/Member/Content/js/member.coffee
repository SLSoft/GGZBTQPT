(($) ->
  $.Member = 
    notice: (msg)->
        $("<div class='alert fade in alert-success'></div>")
            .text(msg)
            .append("<button type='button' class='close' data-dismiss='alert'>¡Á</button>")
            .prependTo("#notice")

    sendRandomPwd: ->
        $("#send_random_pwd").click ->
            alert("xx")
            $.get(
                $(this).attr("href"),
                {"loginname": $("#find_loginname").attr("value")},
                (data) -> 
                   $(".modal-msg").html(data) 
            )
            return false
)(jQuery)
    