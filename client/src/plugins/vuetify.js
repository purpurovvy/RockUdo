import Vue from 'vue'
import Vuetify from 'vuetify/lib'
import 'vuetify/src/stylus/app.styl'

Vue.prototype.$eventBus = new Vue()

Vue.use(Vuetify, {
  iconfont: 'md',
})
