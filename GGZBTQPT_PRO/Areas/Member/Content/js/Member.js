(function() {

  (function($) {
    return $.Member = {
      notice: function(msg) {
        return $("<div class='alert fade in alert-" + msg.type + "'></div>").text(msg.message).append("<button type='button' class='close' data-dismiss='alert'>×</button>").prependTo("#notice");
      },
      sendRandomPwd: function() {
        return $("#send_random_pwd").click(function() {
          alert("xx");
          $.get($(this).attr("href"), {
            "loginname": $("#find_loginname").attr("value")
          }, function(data) {
            return $(".modal-msg").html(data);
          });
          return false;
        });
      }
    };
  })(jQuery);

}).call(this);
