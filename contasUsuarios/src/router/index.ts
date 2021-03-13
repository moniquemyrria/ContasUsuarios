import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

const routes = [
  {
    path: "/Home",
    name: "Home",
    component: () => import("../views/Home.vue"),
    children: [
      {
        path: "/usuarios",
        name: "Usuario",
        component: () => import("../views/Cadastro/UsuarioLista.vue"),
      },
      {
        path: "/usuario/:id/:tipo",
        name: "UsuarioEdit",
        component: () => import("../views/Cadastro/UsuarioForm.vue"),
      },
      {
        path: "/usuario",
        name: "Usuario",
        component: () => import("../views/Cadastro/UsuarioForm.vue"),
      },
    ],
  },
  // {
  //   path: "/",
  //   name: "Home",
  //   component: () => import("../views/Home.vue"),
  // },



];

const router = new VueRouter({
  mode: "history",
  // base: "/contasusuarios/",
  base: "/",
  routes,
});

export default router;
