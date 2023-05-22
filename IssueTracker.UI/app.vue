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
const router = useRouter()
if (authCookie.value){
  apiHelper.token = authCookie.value
}else{
  router.push({path: '/account/login'})
}
</script>
