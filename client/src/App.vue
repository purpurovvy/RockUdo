<template>
  <v-app class="white">
    <v-content id="mainContent">
        <section>
            <Toolbar />
        </section>
        <router-view/>
    </v-content>
  </v-app>
</template>

<script>
import Toolbar from './components/Toolbar'

export default {
  name: 'App',
  components: {
    Toolbar
  },
  data () {
    return { }
  }
}
</script>

<style>

#mainContent {
    background-image: url('assets/rockudo_background.jpg');
    background-attachment: fixed;
    background-size: cover;
}

#ie-content {
  height: 0%;
}
.v-content {
    margin-top: -64px;
}

.v-textarea .v-text-field__slot {
    height: 32px;
} 

.v-textarea textarea {
    padding-bottom: 0;
} 

#kudo {
    margin-top: 100px;
    margin-bottom: 180px;
}
</style>
