(($) ->
  $.Member = 
    notice: (msg)->
        $("<div class='alert fade in alert-success'></div>")
            .text(msg)
            .append("<button type='button' class='close' data-dismiss='alert'>��</button>")
            .prependTo("#notice");
)(jQuery)
    