<template>
  <div class="bg-white shadow rounded px-3 pt-3 pb-5 border border-white mt-3 cursor-move" draggable="false" style="max-width: 300px">
    <div class="flex justify-between">
      <p class="text-gray-700 font-semibold font-sans tracking-wide text-sm">
        <router-link :to="`/issues/${id}`">
          {{ title }}
        </router-link>
      </p>
      <img :src="avatar" alt="Avatar" class="w-6 h-6 rounded-full ml-2" draggable="false">
    </div>
    <div class="flex mt-4 justify-between items-center">
      <span class="text-sm text-gray-600">{{ reference }}</span>
      <div class="px-3 h-6 rounded-full text-xs font-semibold flex items-center bg-red-100 text-teal-700">
        <span class="w-2 h-2 rounded-full mr-1 bg-red-400"></span>
        <span>{{ tagName ?? "Bug" }}</span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
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
  }
})
</script>

<style scoped>

</style>