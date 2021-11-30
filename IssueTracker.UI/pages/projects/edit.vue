<template>
  <div v-if="details">
    
    <TabbedContent :tabs="['Details', 'Users']">
      <template v-slot:tab-1> 
        <div class="md:grid md:grid-cols-3 md:gap-6">
          <div class="mt-5 md:mt-0 md:col-span-2">
            <div class="shadow sm:rounded-md sm:overflow-hidden">
              <div class="px-4 py-5 bg-white space-y-6 sm:p-6">
                <div class="grid grid-cols-3 gap-6">
                  <div class="col-span-3 sm:col-span-2">
                    <label class="block text-sm font-medium text-gray-700" for="company-website">
                      Title
                    </label>
                    <div class="mt-1 flex rounded-md shadow-sm">
                      <input v-model="details.name" class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2"
                             name="company-website"
                             placeholder="..." type="text">
                    </div>
                  </div>
                </div>

                <div class="grid grid-cols-3 gap-6">
                  <div class="col-span-3 sm:col-span-2">
                    <label class="block text-sm font-medium text-gray-700" for="company-website">
                      Abbreviation
                    </label>
                    <div class="mt-1 flex rounded-md shadow-sm">
                      <input v-model="details.abbreviation" class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2"
                             name="company-website"
                             placeholder="..." type="text">
                    </div>
                  </div>
                </div>

                <div class="grid grid-cols-3 gap-6">
                  <div class="col-span-3 sm:col-span-2">
                    <label class="block text-sm font-medium text-gray-700" for="company-website">
                      Labels
                    </label>
                    <div class="mt-1 flex rounded-md shadow-sm">
                      <input class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2" name="company-website"
                             placeholder="label1, label2"
                             type="text">
                    </div>
                  </div>
                </div>


                <div class="grid grid-cols-3 gap-6">
                  <div class="col-span-3 sm:col-span-2">
                    <label class="block text-sm font-medium text-gray-700" for="company-website">
                      Statuses
                    </label>
                    <div class="mt-1 flex rounded-md shadow-sm">
                      <input class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2" name="company-website"
                             placeholder="label1, label2"
                             type="text">
                    </div>
                  </div>
                </div>

                <!--            @change="onChange(column.status, $event)"-->
                <draggable
                    v-model="statuses"
                    :animation="200"
                    class="bg-gray-100 pl-2 pt-2 list-group min-h-[250px]"
                    ghost-class="ghost"
                    group="tasks"
                    item-key="id">

                  <template #item="{ element }">
                    <div class="list-group-item">
                      <div class="bg-white shadow rounded px-3 pt-1 pb-2 border border-white mt-3 cursor-move"
                           draggable="false" style="max-width: 300px">
                        <div class="">
                          <!--                    <p class="text-gray-700 font-semibold font-sans tracking-wide text-sm">-->
                          <div class="grid grid-cols-6 grid-rows-2 w-full">
                            <div class="col-span-5">
                              {{ element.name }}
                            </div>
                            <div class="bg-indigo-400 text-white rounded px-3 py-0.5 cursor-pointer"
                                 @click="deleteStatus(element)">
                              <svg class="h-5 w-5" fill="currentColor" viewBox="0 0 20 20"
                                   xmlns="http://www.w3.org/2000/svg">
                                <path clip-rule="evenodd"
                                      d="M9 2a1 1 0 00-.894.553L7.382 4H4a1 1 0 000 2v10a2 2 0 002 2h8a2 2 0 002-2V6a1 1 0 100-2h-3.382l-.724-1.447A1 1 0 0011 2H9zM7 8a1 1 0 012 0v6a1 1 0 11-2 0V8zm5-1a1 1 0 00-1 1v6a1 1 0 102 0V8a1 1 0 00-1-1z"
                                      fill-rule="evenodd"/>
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
                  <button
                      class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                      @click="save()">
                    Save
                  </button>
                  <button
                      class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-gray-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 ml-2">
                    Cancel
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </template>
      
      <template v-slot:tab-2>
        <div class="m-3">
          <ul class="flex flex-col divide divide-y">
            <li v-for="user in users" class="flex flex-row">
              <div class="select-none cursor-pointer flex flex-1 items-center p-4">
                <div class="flex flex-col w-10 h-10 justify-center items-center mr-4">
                  <a class="block relative" href="#">
                    <img alt="profil" class="mx-auto object-cover rounded-full h-10 w-10 " :src="avatarUrl(user.name, user.email)"/>
                  </a>
                </div>
                <div class="flex-1 pl-1 mr-16">
                  <div class="font-medium dark:text-white">
                    {{ user.name }}
                  </div>
                  <div class="text-gray-600 dark:text-gray-200 text-sm">
                    Developer
                  </div>
                </div>
                <div class="text-gray-600 dark:text-gray-200 text-xs">
                  6:00 AM
                </div>
              </div>
            </li>
          </ul>
          <button @click="addUser()" class="bg-gray-500 text-white p-2 rounded">
            Add user
          </button>
        </div>
      </template>
      
    </TabbedContent>

    

  </div>
</template>

<script lang="ts" setup>
import draggable from "vuedraggable";
import {avatarUrl} from '~/src/services/utils';
import {computed, ref} from "vue";
import {
  IssueStatusesClient,
  ProjectDetailsDto,
  ProjectsClient,
  ProjectUserDto,
  UpdateProjectDto,
  UpdateProjectStatusDto,
  UsersClient
} from "~/src/services/api.generated.clients";
import TabbedContent from "~/components/TabbedContent.vue";

const route = useRoute()
const projectId = computed(() => parseInt(route.query.projectId as string))
const details = ref<ProjectDetailsDto>()

const router = useRouter()
const save = async function () {
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
const addStatus = function () {
  const name = prompt("Name of the new status")
  if (name)
    statuses.value.push({id: null, name: name})
}

const addUser = async function () {
  try {
    const userEmail = prompt("Please enter the email")
    if (!userEmail)
      return;
    
    const userClient = new UsersClient();
    await userClient.addUserToProject(userEmail, projectId.value)
    users.value = await userClient.usersForProject(projectId.value)
  } catch (e) {
    console.log("Failed adding user", e)
    alert("Failed adding user")
  }

}

interface IssueStatusViewModel {
  id: number | null
  name: string
}

const statuses = ref<IssueStatusViewModel[]>([])
const users = ref<ProjectUserDto[]>([])

const deleteStatus = function (status: IssueStatusViewModel) {
  const confirmResult = confirm(`Do you want to remove '${status.name}' status?`)
  if (confirmResult)
    statuses.value = statuses.value.filter(x => x !== status)
}

const init = async function () {
  try {
    const client = new ProjectsClient();
    details.value = await client.details(projectId.value)
    const statusClient = new IssueStatusesClient();
    const projectStatuses = await statusClient.getForProject(projectId.value)
    statuses.value = projectStatuses

    const usersClient = new UsersClient();
    users.value = await usersClient.usersForProject(projectId.value)

  } catch (e) {
    console.log("Failed loading project", e)
    alert("Failed loading project")
  }
}

onMounted(() => init())

</script>

<style scoped>

</style>