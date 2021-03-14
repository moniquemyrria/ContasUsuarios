import Vue from 'vue';
import Vuetify from 'vuetify';
import 'vuetify/dist/vuetify.min.css';
import pt from 'vuetify/src/locale/pt';

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
      options: {
        customProperties: true,
      },
    themes: {
      light: {
        primary: '#093768',
        secondary: '#1697F6',
        accent: '#82B1FF',
        error: '#FF5252',
        info: '#2196F3',
        success: '#4CAF50',
        warning: '#FFC107',
        colorPrimaryText: 'black',
        colorMenuText: 'white',
        colorTableText: 'white'
      },
    },
  },
    lang: {
      locales: { pt },
      current: 'pt',
    },
});
