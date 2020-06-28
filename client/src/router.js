import Vue from 'vue'
import VueRouter from 'vue-router'
import KudoMain from './components/KudoMain.vue'
import Live from './components/LiveAuthorization.vue'
import Statistics from './components/Statistics.vue'
import stat2 from './components/Stat2.vue'

Vue.use(VueRouter)

export default new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name : "main",
      component : KudoMain,
      meta: {
        requireAuth: false
      }
    },
    {
      path: '/live',
      name : "live",
      component : Live,
      meta: {
        requireAuth: true
      }
    },
    {
      path: '/statistics',
      name : "statistics",
      component : Statistics,
      meta: {
        requireAuth: true
      }
    },
    {
      path: '/stat2',
      name : "stat2",
      component : stat2,
      meta: {
        requireAuth: true
      }
    },

  ]
});