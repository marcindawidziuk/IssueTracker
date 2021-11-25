<template>
  <div class="m-4">
    <h3 class="text-xl">Projects</h3>
    <div v-for="project in projects">
      <router-link :to="`/issues/?projectId=${project.id}`" class="m-3">{{ project.name }}</router-link>
    </div>
    
    <div v-if="!projects?.length">
      No projects
    </div>
    
    <div class="my-3">
      <router-link to="/projects/add" class="bg-indigo-700 rounded p-2 text-white">
        New project
      </router-link>
    </div>
    
  </div>
</template>

<script setup lang="ts">
import {ProjectDto, ProjectsClient} from "~/src/services/api.generated.clients";
import {ref} from 'vue'

const projects = ref<ProjectDto[]>()
async function init(){
  try {
    const projectsClient = new ProjectsClient();
    projects.value = await projectsClient.getAll()
  } catch (e) {
    console.log("Failed loading projects", e)
    alert("Failed loading projects")
  }

}

onMounted(() => {
  init()
})
</script>
