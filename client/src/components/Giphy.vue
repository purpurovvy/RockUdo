<template>
 <div>
   <!-- <v-img ref="giphy" max-width="500" max-height="300" contain :src="displayedGIF" v-if="checkLink"></v-img> -->
         <svg class="icon-svg"  ref="svgico" height="24" viewBox="0 0 24 24" width="24" xmlns="http://www.w3.org/2000/svg">
          <path d="M0 0h24v24H0z" fill="none"/>
          <path d="M11.99 2C6.47 2 2 6.48 2 12s4.47 10 9.99 10C17.52 22 22 17.52 22 12S17.52 2 11.99 2zM12 20c-4.42 0-8-3.58-8-8s3.58-8 8-8 8 3.58 8 8-3.58 8-8 8zm3.5-9c.83 0 1.5-.67 1.5-1.5S16.33 8 15.5 8 14 8.67 14 9.5s.67 1.5 1.5 1.5zm-7 0c.83 0 1.5-.67 1.5-1.5S9.33 8 8.5 8 7 8.67 7 9.5 7.67 11 8.5 11zm3.5 6.5c2.33 0 4.31-1.46 5.11-3.5H6.89c.8 2.04 2.78 3.5 5.11 3.5z"/>
        </svg>
<svg @click="dialogGIPHY = true" class="icon-gif" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="24px" height="24px" viewBox="0 0 45.057 45.057" style="enable-background:new 0 0 45.057 45.057;" xml:space="preserve">
<g>
	<g id="_x37_7_7_">
		<g>
			<path d="M17.193,25.24h1.164c-0.032,0.16-0.092,0.301-0.18,0.42c-0.089,0.121-0.196,0.219-0.324,0.295
				c-0.128,0.076-0.267,0.133-0.414,0.174c-0.148,0.039-0.298,0.061-0.45,0.061c-0.4,0-0.726-0.078-0.978-0.234
				c-0.252-0.155-0.448-0.346-0.588-0.569c-0.141-0.226-0.236-0.465-0.288-0.721c-0.053-0.257-0.078-0.481-0.078-0.685
				c0-0.096,0.002-0.227,0.006-0.391c0.004-0.164,0.02-0.338,0.048-0.521c0.028-0.183,0.076-0.369,0.144-0.559
				c0.068-0.188,0.166-0.357,0.295-0.51c0.127-0.15,0.294-0.275,0.497-0.371c0.204-0.096,0.458-0.145,0.763-0.145
				c0.312,0,0.586,0.1,0.822,0.301c0.235,0.199,0.378,0.455,0.426,0.768h2.531c-0.191-1.016-0.623-1.805-1.296-2.363
				c-0.672-0.561-1.556-0.842-2.651-0.842c-0.48,0-0.971,0.088-1.471,0.26s-0.953,0.439-1.361,0.803
				c-0.408,0.365-0.742,0.836-1.002,1.416c-0.261,0.58-0.391,1.279-0.391,2.094c0,0.608,0.095,1.183,0.282,1.717
				c0.188,0.535,0.46,1.002,0.816,1.397c0.355,0.396,0.786,0.709,1.29,0.941s1.071,0.348,1.704,0.348
				c0.472,0,0.922-0.084,1.35-0.252c0.428-0.168,0.79-0.442,1.086-0.828l0.084,0.877h1.668v-4.717h-3.504V25.24z"/>
			<rect x="22.005" y="19.552" width="2.64" height="8.567"/>
			<polygon points="26.001,28.121 28.642,28.121 28.642,24.857 32.062,24.857 32.062,22.816 28.642,22.816 28.642,21.748 
				32.626,21.748 32.626,19.552 26.001,19.552 			"/>
			<path d="M33.431,0H5.179v45.057h34.699V6.251L33.431,0z M36.878,42.056H8.179V3h23.707v4.76h4.992V42.056L36.878,42.056z"/>
		</g>
	</g>
</g>
</svg>
  <v-dialog
        v-model="dialogGIPHY"
        max-width="800"
        max-height="500"
      >
       <v-sheet
      class="mx-auto"
      elevation="8"
      max-width="800"
      
    >
    <v-container id="giphy">
       

        
            <v-text-field 
              v-model="searchValue"             
              label="Search GIPHY..."              
              @keyup.enter="search"
            >
            </v-text-field>
      
        <v-layout row wrap>
            <v-flex v-for="(g,n) in giphy.URL"
            :key="n"   
            class="px-4"
            height="150"
            width="200"  >

          <v-img 
              class="giphy"
             :src="g.Link" 
             height="150" 
             width="200"
             @click="passLink(g.Link)"
           ></v-img>

            </v-flex>
            
        </v-layout>
</v-container>
     </v-sheet> 
    </v-dialog> 
  </div>
</template>

<script>
import axios from 'axios'
export default {
  data(){
    return{
      dialogGIPHY: false,
      displayedGIF:'',
      searchValue:'',
      giphy:{
        URL:[]
      },
    }
  },
  computed:{
    checkLink(){
      if(this.displayedGIF!='') return true
      else{return false}
    }
  },
methods:{
  passLink(link){
    this.displayedGIF=link;
    this.dialogGIPHY=false;
    this.$emit('giphyAdded', link);
  },
  search(){
      axios.get(`https://api.giphy.com/v1/gifs/search?api_key=P6iiUdvPcjqTwbTbPaNxKaZDcaAIYdKf&q=${this.searchValue}&limit=18&offset=0&rating=G&lang=en`)
      .then(response=>{
        this.giphy.URL=[];
        let arr= response.data.data;
        arr.forEach(element => {
          this.giphy.URL.push({"Link":element.images.fixed_width.url});}
        );
      })
  }
}
}
</script>

<style>
.giphy{
  cursor: pointer;
}
.icon-gif{
  cursor: pointer;
}

::-webkit-scrollbar { 
    display: none; 
}
#giphy{
    background-color:aliceblue;
}
svg{
  fill:whitesmoke;
}
</style>