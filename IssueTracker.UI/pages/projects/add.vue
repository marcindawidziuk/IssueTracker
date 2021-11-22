<template>
  <div class="m-4">
    <div class="inline">
      <span>Name</span>
      <input v-model="name" class="bg-indigo-100 rounded p-1 m-2" />
    </div>

    <div class="inline">
      <span>Abbreviation</span>
      <input v-model="abbreviation" class="bg-indigo-100 rounded p-1 m-2" />
    </div>

    <div class="inline">
      <button class="bg-blue-600 text-white p-2 rounded">Add project</button>
    </div>
  </div>
</template>

<script setup lang="ts">

import {
  AddProjectDto,
  CreateIssueStatusDto,
  ICreateIssueStatusDto,
  ProjectsClient
} from "~/src/services/api.generated.clients";
import {ref} from "vue";
const name = ref("")
const abbreviation = ref("")

const defaultStatuses: ICreateIssueStatusDto[] = [
  {name: "Todo", priority: 0},
  {name: "In progress", priority: 1},
  {name: "Done", priority: 2}
]

const statuses = ref(defaultStatuses)

const add = function(){
  try {
    const client = new ProjectsClient();
    const dto = new AddProjectDto({
      name: name.value,
      abbreviation: abbreviation.value,
      statuses: statuses.value
    });
    client.add(dto);
  } catch (e) {
    console.log("Failed adding a project", e)
    alert("Failed adding a project")
  }
}

</script>

<style scoped>

</style>