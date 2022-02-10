// function changeTrColor(trObj, oldColor, newColor) {
//   trObj.style.backgroundColor = newColor;
//   trObj.onmouseout = function () {
//     trObj.style.backgroundColor = oldColor;
//   }
// }

// function clickTrEvent(trObj) {
//   alert(trObj.id);
// }




const app = new Vue({
  el: '#app',
  data: {
    list: [],
  },
  created: function() {
    axios.get('/api/get_contact')
    .then(res => {
      if (res.data.code || !res.data.rows.length) return alert('There is no contact data!');
      var list = res.data.rows
      var len = list.length
      for (var i = 0; i < len; i++) {
        var d = new Date(list[i].contact_date * 1000)
        var year = d.getFullYear()
        var mon = d.getMonth() + 1 < 10 ? '0' + (d.getMonth() + 1) : d.getMonth() + 1
        var day = d.getDate() < 10 ? '0' + d.getDate() : d.getDate()
        var hour = d.getHours() < 10 ? '0' + d.getHours() : d.getHours()
        var min = d.getMinutes() < 10 ? '0' + d.getMinutes() : d.getMinutes()
        list[i].date = ('' + year + '/' + mon + '/' + day + ' ' + hour + ':' + min).substr(2)
      }
      this.list = list
      // context.commit('getData', res.data)
    })
    .catch(err => {
      console.log('err: ', err);
      // context.commit('popupAlert', CBM.lg.server_err);
    })
  }
})