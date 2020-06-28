
<template>
    <v-content>
        <v-dialog v-model="dialog" width="800">
            <v-card>
                <v-container fill-height fluid>
                    <v-layout fill-height>
                    <v-flex xs12 align-end flexbox>
                        <span class="headline font-weight-bold">{{ whom }}</span>
                    </v-flex>
                    </v-layout>
                </v-container>
                <v-card-title>
                    <v-layout column >
                <div>
                    <span class="title font-weight-light">{{title}}</span><br>
                </div>
                    <v-flex align-self-center v-if="this.giphyLink !='' ? true:false" style="margin-top:3%">
                <v-img :src="giphyLink" max-height="250" width="250" ></v-img>
                    </v-flex>
                    </v-layout>
                </v-card-title>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn flat @click="dialog = false"> OK</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>

        <v-layout row id="kudo-bord" >
            <v-flex xs12 sm8 offset-sm2 mb-5 >
                <v-card>
                    <v-toolbar color="#d20014" dark>
                        <v-layout>
                            <v-flex xs8>
                                <v-layout class="v-text-field">
                                    <v-flex xs1>
                                        <img src="../assets/header_kudoboard.png"/>
                                    </v-flex>
                                    <v-flex xs11>
                                        <v-toolbar-title>KUDO BOARD</v-toolbar-title>
                                    </v-flex>
                                </v-layout>
                            </v-flex>
                            <v-flex xs4>
                                <v-combobox
                                    v-model="selectedSort"
                                    :items="items"
                                    @change="refreshBoard"
                                ></v-combobox>
                            </v-flex>
                        </v-layout>
                     </v-toolbar>
                     <v-list two-line>
                        <div class="ma-8 d-flex align-center loader" v-if="listOfKudosIsLoading">
                            <v-progress-circular
                                :value="40"
                                :indeterminate="true"
                                :size="100"
                                :width="10"
                                color="#d20014"
                            >
                            </v-progress-circular>
                        </div>
                        <div v-if="!listOfKudosIsLoading">
                        <template v-for="(kudos, index) in kudoses">
                            <v-subheader v-if="kudos.header" :key="kudos.header"> {{ kudos.header }} </v-subheader>
                            <v-divider v-else-if="kudos.divider" :inset="kudos.inset" :key="index"> </v-divider>
                            <v-list-tile v-else :key="kudos.title + index" class="mainTile" avatar>
                                <v-list-tile-avatar class="avatar">
                                    <img src="../assets/avatar_female.png"/>
                                </v-list-tile-avatar>
                                <v-list-tile-content class="tile">
                                    <v-list-tile-title>{{kudos.who}}</v-list-tile-title>
                                    <v-list-tile-sub-title>{{kudos.title}} </v-list-tile-sub-title>
                                    <v-list-tile-sub-title v-if="kudos.giphy">*GIF*</v-list-tile-sub-title>
                                </v-list-tile-content>
                                <v-icon large class="tileIcon">arrow_back</v-icon>
                                <v-list-tile-avatar class="avatar">
                                    <img src="../assets/avatar_male.png"/>
                                </v-list-tile-avatar>
                                <v-list-tile-content class="tile">
                                    <v-list-tile-title>{{kudos.whoFrom}}</v-list-tile-title>
                                    <v-list-tile-sub-title>{{kudos.when}}</v-list-tile-sub-title>
                                </v-list-tile-content>
                                <v-spacer></v-spacer>
                                <div class="text-xs-center">
                                    <v-btn color="#d20014" dark large @click="selectKudos(index)">Show Kudo</v-btn>
                                </div>
                            </v-list-tile>
                        </template>
                        </div>
                     </v-list>
                     <div>
                        <div class="text-xs-right">
                            <v-btn color="#d20014" dark medium @click="prevPage" :disabled="this.page <= 0">Prev</v-btn>
                            <v-btn color="#d20014" dark medium @click="nextPage" :disabled="this.kudoses.length < this.take">Next</v-btn>
                        </div>
                     </div>                     
                </v-card>
            </v-flex>
            
        </v-layout>
    </v-content>
</template>

<script>
/* eslint-disable no-console */
import axios from 'axios';

export default {
    data: () => ({
        dialog: false,
        listOfKudosIsLoading: true,
        kudoses: [],
        selected: 0,
        title: '',
        whom: '',
        giphyLink:'',
        page: 0,
        selectedSort: { text: 'Newest', value: 'date-descending' },
        items: [
            { text: 'Newest', value: 'date-descending' },
            { text: 'Oldest', value: 'date-ascending' }
        ]
    }),
    computed: {
        skip: function() { return this.page * 5; },
        take: function() { return 5; }
    },
     mounted () {
         var url = '';
            if(location.protocol != 'https:'){
                url = `https://${this.$store.getters.apiUrlBase}/${this.skip}/${this.take}/`;
            } else {
                url = `https://${this.$store.getters.apiUrlBase}/${this.skip}/${this.take}/`;
            }
        axios
            .get(url, this.$store.getters.apiTokenHeader)
            .then(response => {
                // eslint-disable-next-line no-console
                console.log(response);
                this.listOfKudosIsLoading = false;
                this.kudoses = this.parseDate(response.data);
                this.kudoses = this.parseEmail(this.kudoses);
                this.kudoses = this.parseEmoji(this.kudoses);
            });

        this.$eventBus.$on('refresh-kudo-board', () => {
            this.refreshBoard();
        });
        var str = '&#128533;&#128528;test&#128563;&#128547;x&#128512;'

        str = str.replace(/(&#+\d+;)|(;&#+\d+;)/g, (match)=>{
            match = match.replace(/;|&|#/g,'');
            match = String.fromCodePoint(parseInt (match, 10));
            return match;

       
});
 console.log(str);
    },
    methods: {
        selectKudos(kudosIndex) {
           
            this.dialog = true;
            this.selected = kudosIndex;
            this.title = this.kudoses[this.selected].title;
            this.whom = this.kudoses[this.selected].who;
            this.giphyLink = (this.kudoses[this.selected].giphy == null) ? '' : this.kudoses[this.selected].giphy;
             console.log(this.kudoses[this.selected].giphy);
            

        },
        parseDate(response) {
            return response
                .map(
                    (kudos) => {
                        var options = { year: 'numeric', month: 'long', day: 'numeric' };
                        kudos.when = new Date(kudos.when).toLocaleDateString("en-US", options);
                        return kudos;
                    }            
                );
        },
          parseEmail(response){
            return response
                .map(
                    (kudos) => {
                            kudos.who = kudos.who.replace(/@[^@]+$/, '').replace(/\./g, ' ').replace(/(?:^|\s|-)+\S/g, match => match.toUpperCase());
                            kudos.whoFrom = kudos.whoFrom.replace(/@[^@]+$/, '').replace(/\./g, ' ').replace(/(?:^|\s|-)+\S/g, match => match.toUpperCase());
                        return kudos;
                    }            
                );
        },
         parseEmoji(response){
          function emojiParser(message){
                   message = message.replace(/(&#+\d+;)|(;&#+\d+;)/g, (match)=>{
                        match = match.replace(/;|&|#/g,'');
                        match = String.fromCodePoint(parseInt (match, 10));
                        console.log(`Moj match: ${match}`)
                        return match;
                    });
                    // eslint-disable-next-line no-console
                    console.log(`Mój message ${message}`);
                    return message
                }

            return response
                .map(
                    (kudos) => {
                        // eslint-disable-next-line no-console
                        // console.log(`moj kudos title ${kudos.title}`);
                        kudos.title = emojiParser(kudos.title);
                        return kudos;
                    }            
                );
         },     
        refreshBoard() {
            // eslint-disable-next-line no-console
            console.log('próbuje odswiezyć')
            this.giphyLink='';
            var direct = this.selectedSort.value === 'date-descending' ? 'Desc' : 'Asc';
            var sort = "When";
            var url = `https://${this.$store.getters.apiUrlBase}/${this.skip}/${this.take}/${sort}/${direct}`;
            // if(location.protocol != 'https:'){
            //     url = `http://${this.$store.getters.apiUrlBase}/${this.skip}/${this.take}/${sort}/${direct}`;
            // } else {
            //     url = `https://${this.$store.getters.apiUrlBase}/${this.skip}/${this.take}/${sort}/${direct}`;
            // }
            axios
                .get(url, this.$store.getters.apiTokenHeader)
                .then(response => { 
                    this.kudoses = this.parseDate(response.data)
                    this.kudoses = this.parseEmail(this.kudoses);
                    this.kudoses = this.parseEmoji(this.kudoses);
                    // eslint-disable-next-line no-console
                    // console.log(this.kudoses);
                });

        },
        nextPage(){
            this.page = this.page + 1;
            this.refreshBoard();
        },
        prevPage(){
            this.page = this.page > 0 ? this.page - 1 : 0;
            this.refreshBoard();
        }
    }
};
</script>

<style>
    .tile {
        max-width: 200px;
    }
    
    .tileIcon {
        margin-right: 50px;
        margin-left: 50px;
    }

    .avatar {
        margin-right: 20px;
        min-width: 0px;
    }

    .loader {
        width:100px;
        margin:auto;
        height:300px;
    }

</style>
