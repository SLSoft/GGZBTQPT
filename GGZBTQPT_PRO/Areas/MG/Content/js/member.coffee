(($) ->
    $.Member = 
        notice: (msg)->
            $("<div class='alert fade in alert-" + msg.type + "'></div>")
                .text(msg.message)
                .append("<button type='button' class='close' data-dismiss='alert'>¡Á</button>")
                .prependTo("#notice")

        sendRandomPwd: ->
            $("#send_random_pwd").click ->

                $.get(
                    $(this).attr("href"),
                    {"loginname": $("#find_loginname").attr("value")},
                    (data) -> 
                       $(".modal-msg").html(data) 
                )
                return false
        replaceBlankForMVCPager: -> 
            $(".pagination").html($(".pagination").html().replace(/&nbsp;/ig, ""))
        rounded: -> 
            if (window.PIE) 
                $('.rounded').each -> 
                    PIE.attach(this);

    $.Member.rounded()

)(jQuery)
