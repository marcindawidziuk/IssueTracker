<template>
  <div>
    <!-- Remove this component to get started! -->
    <NuxtPage />
  </div>
</template>

<script setup>
import './assets/css/tailwind.css';
import {useCookie} from "#app"
import {apiHelper} from "./src/services/ApiAxios";
const authCookie = useCookie('auth', {path: '/'})
if (authCookie.value){
  console.log('authcookie', authCookie.value)
  apiHelper.token = authCookie.value
  console.log('apihelper', apiHelper)
}
</script>
