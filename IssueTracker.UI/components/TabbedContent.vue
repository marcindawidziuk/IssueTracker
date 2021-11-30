<template>
  <div>

    <div class="bg-white">
      <!--      <nav >-->
      <div class="flex flex-col sm:flex-row">

      <nav v-for="(tab, index) in tabs" 
           :key="index" >
        <button 
            @click="activeTab = index + 1"
            class="text-gray-600 py-4 px-6 block hover:text-blue-500 focus:outline-none text-blue-500 border-b-2 font-medium"
            :class="activeTab === (index + 1) ? 'border-blue-500' : ''">
          {{ tab }}
        </button>
      </nav>
      </div>
    </div>

    <template v-for="(tab, index) in tabs">
      <div v-if="index + 1 === activeTab" :key="index">
        <slot :name="`tab-${index + 1}`"/>
      </div>
    </template>
  </div>
</template>

<script setup lang="ts">
defineProps({
  tabs: {
    type: Array,
    required: true,
  },
  variant: {
    type: String,
    required: false,
  },
})
const activeTab = ref(1)
</script>

<style>
.flex {
  display: flex;
}
</style>