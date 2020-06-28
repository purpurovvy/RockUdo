<template>
<div id="kudo">
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
                                        <v-toolbar-title>KUDO STATISTICS</v-toolbar-title>
                                    </v-flex>
                                </v-layout>
                            </v-flex>
                        </v-layout>
                     </v-toolbar>

                     <v-list two-line>
                        <div class="ma-8 d-flex align-center loader" v-if="loading">
                            <v-progress-circular
                                :value="40"
                                :indeterminate="true"
                                :size="100"
                                :width="10"
                                color="#d20014"
                            >
                            </v-progress-circular>
                        </div>
                     </v-list>
                      <div v-if="!loading">


    <v-data-table
      :headers="headers"
      :items="desserts"
      hide-actions
      class="elevation-1"
    >
      <template slot="items" slot-scope="props">
        <td>{{ props.item.name }}</td>
        <td class="text-xs-center">{{ props.item.calories }}</td>
        <td class="text-xs-center">{{ props.item.fat }}</td>
        <!-- <td class="text-xs-right">{{ props.item.carbs }}</td>
        <td class="text-xs-right">{{ props.item.protein }}</td>
        <td class="text-xs-right">{{ props.item.iron }}</td> -->
      </template>
    </v-data-table>


                      </div>
              </v-card>
         </v-flex>


</div>

    
</template>
<script>
export default {
    data () {
    return {
    loading:false,
      headers: [
        {
          text: 'Dessert (100g serving)',
          align: 'left',
          sortable: false,
          value: 'name'
        },
        { text: 'Calories', value: 'calories', align:'center' },
        { text: 'Fat (g)', value: 'fat', align:'center' },

      ],
      desserts: [
        {
          value: false,
          name: 'Frozen Yogurtoooo',
          calories: 159,
          fat: 6.0,
          carbs: 24,
          protein: 4.0,
          iron: '1%'
        },
        {
          value: false,
          name: 'Ice cream sandwich',
          calories: 237,
          fat: 9.0,
          carbs: 37,
          protein: 4.3,
          iron: '1%'
        },
        {
          value: false,
          name: 'Eclair',
          calories: 262,
          fat: 16.0,
          carbs: 23,
          protein: 6.0,
          iron: '7%'
        },
        {
          value: false,
          name: 'Cupcake',
          calories: 305,
          fat: 3.7,
          carbs: 67,
          protein: 4.3,
          iron: '8%'
        },
        {
          value: false,
          name: 'Gingerbread',
          calories: 356,
          fat: 16.0,
          carbs: 49,
          protein: 3.9,
          iron: '16%'
        },
        {
          value: false,
          name: 'Jelly bean',
          calories: 375,
          fat: 0.0,
          carbs: 94,
          protein: 0.0,
          iron: '0%'
        },
        {
          value: false,
          name: 'Lollipop',
          calories: 392,
          fat: 0.2,
          carbs: 98,
          protein: 0,
          iron: '2%'
        },
        {
          value: false,
          name: 'Honeycomb',
          calories: 408,
          fat: 3.2,
          carbs: 87,
          protein: 6.5,
          iron: '45%'
        },
        {
          value: false,
          name: 'Donut',
          calories: 452,
          fat: 25.0,
          carbs: 51,
          protein: 4.9,
          iron: '22%'
        },
        {
          value: false,
          name: 'KitKat',
          calories: 518,
          fat: 26.0,
          carbs: 65,
          protein: 7,
          iron: '6%'
        }
      ]
    }
  }
}
</script>