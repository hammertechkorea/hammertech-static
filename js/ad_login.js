
$(function () {
  $('#pw').on('keypress', function (e) {
    if (e.keyCode === 13) $('.btn_login').trigger('click');
  });
  $('.btn_login').on('click', function () {
    $.post('/api/admin_login', {
      id: $.trim($('#id').val()),
      pw: $.trim($('#pw').val()),
      save_id: $('.check_box').hasClass('up') ? true : false
    }, function (res) {
      if (res.code == 1)
        return alert('Your ID or Password is wrong!')
      location.reload()
    })
  })
})