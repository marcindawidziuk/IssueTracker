<template>
  <div class="bg-white shadow rounded px-3 pt-3 pb-5 border border-white mt-3 cursor-move relative" draggable="false" style="max-width: 300px" 
       @contextmenu.prevent="isMenuOpened = !isMenuOpened">
    <div v-if="isMenuOpened" class="origin-top-right absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none" role="menu" aria-orientation="vertical" aria-labelledby="menu-button" tabindex="-1">
      <div class="py-1" role="none">
        <a href="#" class="text-gray-700 block px-4 py-2 text-sm hover:bg-gray-100" role="menuitem" tabindex="-1" id="menu-item-0"
           @click="menuClicked('self-assign')"
        >Assign to me</a>
        <a href="#" class="text-red-700 block px-4 py-2 text-sm hover:bg-gray-100" role="menuitem" tabindex="-1" id="menu-item-1"
           @click="menuClicked('delete')"
        >Delete</a>
      </div>
    </div>
    <div class="flex justify-between">
      <p class="text-gray-700 font-semibold font-sans tracking-wide text-sm">
        <router-link :to="`/issues/${id}`">
          {{ title }}
        </router-link>
      </p>
      <img v-if="avatar" :src="avatar" alt="Avatar" class="w-6 h-6 rounded-full ml-2" draggable="false">
    </div>
    <div class="flex mt-4 justify-between items-center">
      <span class="text-sm text-gray-600">{{ reference }}</span>
      <div v-for="label in labels" class="px-3 h-6 rounded-full text-xs font-semibold flex items-center bg-red-100 text-red-800">
        <span class="w-2 h-2 rounded-full mr-1 bg-red-400"></span>
        <span>{{ label.name }}</span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import {PropType, ref} from "vue";
import {LabelDto} from "~/src/services/api.generated.clients";

defineProps({
  id: {
    type: Number
  },
  title: {
    type: String,
    required: true
  },
  avatar: {
    type: String,
    required: true
  },
  date: {
    type: Date,
    required: true
  },
  tagName: {
    type: String,
    required: true
  },
  reference: {
    type: String,
    required: true
  },
  labels: {
    type: Array as PropType<LabelDto[]>,
    default: []
  }
})

const emit = defineEmits(['self-assign', 'delete'])
const menuClicked = function(name: 'self-assign' | 'delete'){
  emit(name);
  isMenuOpened.value = false
}

const isMenuOpened = ref(false)
</script>

<style scoped>

</style>