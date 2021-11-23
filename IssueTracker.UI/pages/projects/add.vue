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
      <button class="bg-blue-600 text-white p-2 rounded" @click="add()">Add project</button>
    </div>
  </div>
</template>

<script setup lang="ts">

import {
  AddProjectDto, CreateIssueStatusDto,
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

const router = useRouter();
const add = async function(){
  try {
    const client = new ProjectsClient();
    const s = statuses.value.map(x => new CreateIssueStatusDto(x))
    const dto = new AddProjectDto({
      name: name.value,
      abbreviation: abbreviation.value,
      statuses: s
    });
    const projectId = await client.add(dto);
    await router.push({path: `/issues/${projectId}`})
  } catch (e) {
    console.log("Failed adding a project", e)
    alert("Failed adding a project")
  }
}

</script>

<style scoped>

</style>