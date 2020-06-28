<template>
    <v-toolbar app flat class="white" height="105" >
        <a href="#kudo">
            <img src="../assets/rockudo_logo_black.svg" height="75"/>
        </a>
        <v-spacer></v-spacer>
        <v-toolbar-items class="hidden-sm-and-down">
            <v-btn flat v-if="isLoggedUser">
                <div class="img-with-text">
                    <img src="../assets/avatar_male.png"/>
                    <p class="font-weight-black">{{username}}</p>
                </div>
            </v-btn>
            <v-btn flat href="#kudo"  @click="setFocusOnForm" v-if="isLoggedUser">
                <div class="img-with-text">
                    <img src="../assets/mainmenu_1.png"/>
                    <p class="font-weight-black">Give Kudos</p>
                </div>
            </v-btn>
            <v-btn flat href="#kudo-bord" v-if="isLoggedUser">
                <div class="img-with-text">
                    <img src="../assets/mainmenu_2.png"/>
                    <p class="font-weight-black">Kudo Board</p>
                </div>
            </v-btn>
            <v-btn flat v-if="isLoggedUser" v-on:click="logout">
                <div class="img-with-text">
                    <img src="../assets/logout.png"/>
                    <p class="font-weight-black">Logout</p>
                </div>
            </v-btn>
            <v-dialog
                full-width
                transition="slide-y-transition"
                origin="'top center 0'"
                leave-absolute
                v-model="dialog"
            >   
                <v-btn flat href="#kudo-info" slot="activator">
                    <div class="img-with-text">
                        <img src="../assets/mainmenu_3.png"/>
                        <p class="font-weight-black">About Kudo</p>
                    </div>                
                </v-btn>
                
                <v-card>
                    <v-card-title primary-title>
                        <v-layout column>
                            <v-flex>
                                <v-layout justify-end>
                                    <v-btn 
                                        offset-xs11 
                                        xs1 
                                        color="#FFFFFF" 
                                        flat 
                                        @click="dialog=false">
                                        <img src="../assets/exit.png" />
                                    </v-btn>
                                </v-layout>
                            </v-flex>
                        </v-layout>
                    </v-card-title>
                    <v-card-text>
                        <v-layout column align-center justify-center>
                            <v-flex class="about-title">
                                ABOUT KUDOS
                            </v-flex>
                            <v-flex pb-3>
                                <v-layout row wrap>
                                    <v-flex xs8 offset-xs2>
                                        Kudos it is a <b><u>written and public recognition of a colleague</u></b> for something he/she has contributed to the team. Across departments and organizations, anyone can recognize and appreciate someone else’s work. It’s a great and constructive way to say thanks and encourage everyone to offer instant positive feedback
                                    </v-flex>
                                </v-layout>
                            </v-flex>
                            <v-flex class="about-divider">
                                 <v-layout row wrap>
                                    <v-flex xs8 offset-xs2>
                                        <v-divider></v-divider>
                                    </v-flex>
                                </v-layout>
                            </v-flex>
                            <v-flex xs8 pt-3 pb-4>
                                <v-layout justify-center align-center row>
                                    <v-flex class="about-tutorial">
                                        <v-layout column justify-center align-center>
                                            <v-flex>
                                                <img src="../assets/about1.png" />
                                            </v-flex>
                                            <v-flex>
                                                <p class="font-weight-black">PICK YOUR COLLEAGUE</p>
                                            </v-flex>
                                        </v-layout>
                                    </v-flex>
                                    <v-flex pl-3 pr-3 class="about-img-text">
                                        <img src="../assets/arrow.png" />
                                    </v-flex>
                                    <v-flex>
                                        <v-layout column justify-center align-center>
                                            <v-flex>
                                                <img  src="../assets/about2.png" />
                                            </v-flex>
                                            <v-flex>
                                                <p class="font-weight-black">DESCRIBE YOUR APPRECIATION</p>
                                            </v-flex>
                                        </v-layout>
                                    </v-flex>
                                    <v-flex pl-3 pr-3 class="about-img-text">
                                        <img src="../assets/arrow.png" />
                                    </v-flex>
                                    <v-flex>
                                        <v-layout column justify-center align-center>
                                            <v-flex>
                                                <img  src="../assets/about3.png" />
                                            </v-flex>
                                            <v-flex>
                                                <p class="font-weight-black">SEND KUDOS TO THIS PERSON</p>
                                            </v-flex>
                                        </v-layout>
                                    </v-flex>
                                </v-layout>
                            </v-flex>
                        </v-layout>
                    </v-card-text>
                </v-card>
            </v-dialog>
        </v-toolbar-items>
    </v-toolbar>
</template>

<script>
import logger from '../providers/logProvider';

export default {
    created: function() {
        logger.Information("Created toolbar");
    },
    data() {
        return {
            dialog: false
        }
    },
    computed: {
        isLoggedUser: function() { return this.$store.getters.isAuthenticated; },
        username: function() { return this.$store.getters.user.name; },
    },
    methods: {
        setFocusOnForm() {
            this.$eventBus.$emit('set-focus-in-form-on-input-whom');
        },
        login() {
            this.$store.dispatch('login');
        },
        logout() {
            this.$store.dispatch('logout');
        }
    }
};
</script>

<style>
    .img-with-text {
        text-align: justify;
        font-size: 15px;
        font-weight: bold;
    }

    .img-with-text img {
        display: block;
        margin: 15px auto;
    }

    .about-title {
        color: #d20014;
        font-size: 38px;
        font-weight: 700;
    }

    .about-divider {
        height: 10px;
        width: 100%;
    }

    .about-tutorial {
        display: flex;
        align-content: center;
        flex-direction: column;
    }

    .about-img-text {
        margin-top:-20px;
    }
</style>