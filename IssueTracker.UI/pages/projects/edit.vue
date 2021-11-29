<template>
  <div v-if="details">
    <div class="md:grid md:grid-cols-3 md:gap-6">
      <div class="mt-5 md:mt-0 md:col-span-2">
        <div class="shadow sm:rounded-md sm:overflow-hidden">
          <div class="px-4 py-5 bg-white space-y-6 sm:p-6">
            <div class="grid grid-cols-3 gap-6">
              <div class="col-span-3 sm:col-span-2">
                <label for="company-website" class="block text-sm font-medium text-gray-700">
                  Title
                </label>
                <div class="mt-1 flex rounded-md shadow-sm">
                  <input type="text" name="company-website" class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2"
                         placeholder="..." v-model="details.name">
                </div>
              </div>
            </div>

            <div class="grid grid-cols-3 gap-6">
              <div class="col-span-3 sm:col-span-2">
                <label for="company-website" class="block text-sm font-medium text-gray-700">
                  Abbreviation
                </label>
                <div class="mt-1 flex rounded-md shadow-sm">
                  <input type="text" name="company-website" class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2"
                         placeholder="..." v-model="details.abbreviation">
                </div>
              </div>
            </div>

            <div class="grid grid-cols-3 gap-6">
              <div class="col-span-3 sm:col-span-2">
                <label for="company-website" class="block text-sm font-medium text-gray-700">
                  Labels
                </label>
                <div class="mt-1 flex rounded-md shadow-sm">
                  <input type="text" name="company-website" class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2"
                         placeholder="label1, label2">
                </div>
              </div>
            </div>

            
            <div class="grid grid-cols-3 gap-6">
              <div class="col-span-3 sm:col-span-2">
                <label for="company-website" class="block text-sm font-medium text-gray-700">
                  Statuses
                </label>
                <div class="mt-1 flex rounded-md shadow-sm">
                  <input type="text" name="company-website" class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2"
                         placeholder="label1, label2">
                </div>
              </div>
            </div>

<!--            @change="onChange(column.status, $event)"-->
            <draggable
                v-model="statuses"
                :animation="200"
                item-key="id"
                class="bg-gray-100 pl-2 pt-2 list-group min-h-[250px]"
                group="tasks"
                ghost-class="ghost" >

            <template #item="{ element }">
              <div class="list-group-item">
                <div class="bg-white shadow rounded px-3 pt-1 pb-2 border border-white mt-3 cursor-move" draggable="false" style="max-width: 300px">
                  <div class="">
<!--                    <p class="text-gray-700 font-semibold font-sans tracking-wide text-sm">-->
                      <div class="grid grid-cols-6 grid-rows-2 w-full">
                        <div class="col-span-5" >
                          {{ element.name }}
                        </div>
                      <div class="bg-indigo-400 text-white rounded px-3 py-0.5 cursor-pointer" @click="deleteStatus(element)">
                        <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                          <path fill-rule="evenodd" d="M9 2a1 1 0 00-.894.553L7.382 4H4a1 1 0 000 2v10a2 2 0 002 2h8a2 2 0 002-2V6a1 1 0 100-2h-3.382l-.724-1.447A1 1 0 0011 2H9zM7 8a1 1 0 012 0v6a1 1 0 11-2 0V8zm5-1a1 1 0 00-1 1v6a1 1 0 102 0V8a1 1 0 00-1-1z" clip-rule="evenodd" />
                        </svg>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </template>

              <template #footer>
                <button class="bg-indigo-200 m-2 p-2 rounded" @click="addStatus()">
                  Add status
                </button>
              </template>
            </draggable>
          <div class="px-4 py-3 bg-gray-50 text-right sm:px-6">
            <button class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                    @click="save()">
              Save
            </button>
            <button class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-gray-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 ml-2" >
              Cancel
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
  </div>
</template>

<script setup lang="ts">
import {computed, ref} from "vue";
import draggable from "vuedraggable";
import {
  IssueStatusesClient,
  ProjectDetailsDto,
  ProjectsClient, UpdateProjectDto, UpdateProjectStatusDto
} from "~/src/services/api.generated.clients";

const route = useRoute()
const projectId = computed(() => parseInt(route.query.projectId as string))
const details = ref<ProjectDetailsDto>()

const router = useRouter()
const save = async function(){
  try {
    if (!details.value)
      return;
    
    const client = new ProjectsClient();
    const dto = new UpdateProjectDto({
      id: projectId.value,
      name: details.value.name,
      abbreviation: details.value.abbreviation,
      statuses: statuses.value.map(x => new UpdateProjectStatusDto(x))
    })
    await client.update(dto)
    await router.push(`/issues/?projectId=${projectId.value}`)
  } catch (e) {
    console.log("Failed saving project", e)
    alert("Failed saving project")
  }
}
const addStatus = function(){
  const name = prompt("Name of the new status")
  if (name)
    statuses.value.push({id: null, name: name})
}

interface IssueStatusViewModel
{
  id: number | null
  name: string
}

const statuses = ref<IssueStatusViewModel[]>([])

const deleteStatus = function(status: IssueStatusViewModel){
  const confirmResult = confirm(`Do you want to remove '${status.name}' status?`)
  if (confirmResult)
    statuses.value = statuses.value.filter(x => x !== status)
}

const init = async function(){
  try {
    const client = new ProjectsClient();
    details.value = await client.details(projectId.value)
    const statusClient = new IssueStatusesClient();
    const projectStatuses = await statusClient.getForProject(projectId.value)
    statuses.value = projectStatuses
  } catch (e) {
    console.log("Failed loading project", e)
    alert("Failed loading project")
  }
}

onMounted(() => init())
    
</script>

<style scoped>

</style>