<template>
    <v-layout justify-center id="kudo">
        <v-flex xs12 sm8 md6 mt-5>
            <v-card dark class="transparent" flat>
                <v-card-title class="display-2 font-weight-bold" >
                    <v-card-text>
                        <p class="text-xs-center"><u class="red-underline">GIVE KUDOS</u> TO YOUR COLLEAGUE</p>
                    </v-card-text>
                </v-card-title>
                <v-card-text>
                    <v-form>
                        <v-container>
                        <v-text-field prepend-icon="person" name="name" label="to Whom?" type="text" ref="whom" v-model="name">
                        </v-text-field>
                        <v-text-field prepend-icon="person" name="whoFrom" label="from Who?" type="text" ref="whoFrom" readonly :value="whoFrom">
                        </v-text-field>
                        
                             <v-layout row wrap>
                                 <v-flex xs11>
                            
                            <v-textarea rows="1" prepend-icon="subject" name="description" label="What do you want to say?" v-model="description" >
                            </v-textarea>
                              </v-flex>
                            <v-flex xs1>
                            <emoji :msg="description" @emojiAdded="description = $event"></emoji>
                             <giphy @giphyAdded="giphyLink = $event"></giphy>
                            
                              </v-flex>
                             </v-layout>
                        </v-container>
                       

                    </v-form>
                    
                </v-card-text>
           
                <v-flex align-center justify-center style="display:flex" v-if="checkGiphyLink" >
                    
                    <v-img max-width="500" max-height="300" contain :src="giphyLink"  @click="removeGiphy" class="giphyContainer">
                        <p class="giphyDescription">REMOVE GIPHY</p>
                    </v-img>
                </v-flex>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <div class="text-xs-center">
                        <v-btn color="#d20014" v-if="!addingKudoLoader" dark large @click="submitEntry" :disabled="name==='' || description === '' || whoFrom === ''">Add</v-btn>
                        <v-progress-circular v-if="addingKudoLoader" indeterminate color="red"></v-progress-circular>
                    </div>
                </v-card-actions>
                <!-- <emoji :msg="description" @emojiAdded="description = $event"></emoji> -->
            </v-card>
        </v-flex>
    </v-layout>
</template>

<script>
import axios from 'axios';
import emoji from './Emoji.vue';
import giphy from './Giphy.vue'

// https://alligator.io/vuejs/component-communication/
// https://alligator.io/vuejs/global-event-bus/

export default {
    data: () => ({
        name: '',
        description: '',
        addingKudoLoader: false,
        giphyLink: ''
    }),
    computed: {
        whoFrom: function() { return this.$store.getters.user.unique_name; },
        checkGiphyLink : function() { return this.giphyLink!='' ?  true : false}
    },
    methods: {
        submitEntry() {
            this.addingKudoLoader = true;

            var url = '';
            if(location.protocol != 'https:'){
                url = `https://${this.$store.getters.apiUrlBase}`;
            } else {
                url = `https://${this.$store.getters.apiUrlBase}`;
            }
            let x = axios.post(url, { Whom: this.name, Description: this.description, WhoFrom: this.whoFrom, Giphy: this.giphyLink },this.$store.getters.apiTokenHeader)
                .then(response => {
                    this.responseData = response.data;
                    this.name = '';
                    this.description = '';
                    this.giphyLink = '';                    
                    this.$eventBus.$emit('refresh-kudo-board');
                    this.addingKudoLoader = false;
                })
                .catch(error => { error });
                // eslint-disable-next-line no-console
                console.log(x);
                // eslint-disable-next-line no-console
                console.dir(x);
        },

        test2(){
            let s = String.fromCharCode(parseInt(this.description,16))
            // eslint-disable-next-line no-console
            console.log(s);
        },
        removeGiphy(){
            this.giphyLink = '';
        }
       
    },

    components:{
        emoji,
        giphy
      
    },
    mounted: function () {
        this.$eventBus.$on('set-focus-in-form-on-input-whom', () => {
            this.$nextTick(() => this.$refs.whom.focus())
        });
    }
};
</script>

<style>
    .transparent {
        background-color: transparent!important;
        border-color: transparent!important;
    }

    .red-underline {
        text-decoration-color: red; 
    }
    .giphyContainer{
        cursor: pointer;
        position:relative;
        padding:0;
    }
    .giphyDescription{
        position:absolute;
        z-index: 1;
        visibility: hidden;
        opacity:0;
        color:white;
        background-color:red;
        top:50%;
        left:40%;
        opacity:1;
    }
    .giphyContainer:hover{
        opacity:0.3;
        background-color:black;
    }

    .giphyContainer:hover .giphyDescription{
        
        visibility: visible;
        
    }
</style>