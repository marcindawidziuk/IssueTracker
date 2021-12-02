<template>

  <div>
    <router-link v-if="details?.projectId" :to="`/issues/?projectId=${details.projectId}`">Back</router-link>
    <div class="md:grid md:grid-cols-3 md:gap-6">
      <div class="mt-5 md:mt-0 md:col-span-2">
          <div class="shadow sm:rounded-md sm:overflow-hidden">
            <div class="px-4 py-5 bg-white space-y-6 sm:p-6">
              <div class="grid grid-cols-3 gap-6">
                <div class="col-span-3 sm:col-span-2">
                  <label class="block text-sm font-medium text-gray-700">
                    Title
                  </label>
                  <div class="mt-1 flex rounded-md shadow-sm">
                    <input type="text" name="company-website" class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2" 
                           placeholder="..." v-model="title">
                  </div>
                </div>
              </div>

              <div class="grid grid-cols-3 gap-6">
                <div class="col-span-3 sm:col-span-2">
                  <label class="block text-sm font-medium text-gray-700">
                    Labels
                  </label>
                  <div class="mt-1 flex rounded-md shadow-sm">
<!--                    <input type="text" name="company-website" class="focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2" placeholder="label1, label2">-->
                    <LabelsInput v-if="details" :project-id="details.projectId" v-model:labels="labels" />
                  </div>
                </div>
              </div>

              <div class="grid grid-cols-3 gap-6">
                <div class="col-span-3 sm:col-span-2">
                  <label class="block text-sm font-medium text-gray-700">
                    Assigned User
                  </label>
                  <div class="mt-1 flex rounded-md shadow-sm">
                    <select v-model="selectedUserId" class="p-1 m-1">
                      <option v-for="user in userDropdowns"
                              :value="user.id">
                        {{ user.name }}
                      </option>
                    </select>
                  </div>
                </div>
              </div>

              <div class="grid grid-cols-3 gap-6">
                <div class="col-span-3 sm:col-span-2">
                  <label class="block text-sm font-medium text-gray-700">
                    Status
                  </label>
                  <div class="mt-1 flex rounded-md shadow-sm">
                    <select v-model="selectedStatus" class="p-1 m-1">
                      <option v-for="status in statuses"
                              :value="status">
                        {{status.name}}
                      </option>
                    </select>
                  </div>
                </div>
              </div>
              
              <div>
                <label for="about" class="block text-sm font-medium text-gray-700">
                  Description
                </label>
                <div class="mt-1">
                  <textarea id="about" name="about" rows="10" 
                            class="shadow-sm focus:ring-indigo-500 focus:border-indigo-500 mt-1 block w-full sm:text-sm border border-gray-300 rounded-md p-2" 
                            v-model="description"></textarea>
                </div>
                <p class="mt-2 text-sm text-gray-500">
                  Feel free to use markdown
                </p>
              </div>

              <div></div>

              <div>
                <label class="block text-sm font-medium text-gray-700">
                  Attachments 
                </label>
                <div class="mt-1 flex justify-center px-6 pt-5 pb-6 border-2 border-gray-300 border-dashed rounded-md">
                  <div class="space-y-1 text-center">
                    <svg class="mx-auto h-12 w-12 text-gray-400" stroke="currentColor" fill="none" viewBox="0 0 48 48" aria-hidden="true">
                      <path d="M28 8H12a4 4 0 00-4 4v20m32-12v8m0 0v8a4 4 0 01-4 4H12a4 4 0 01-4-4v-4m32-4l-3.172-3.172a4 4 0 00-5.656 0L28 28M8 32l9.172-9.172a4 4 0 015.656 0L28 28m0 0l4 4m4-24h8m-4-4v8m-12 4h.02" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                    </svg>
                    <div class="flex text-sm text-gray-600">
                      <label for="file-upload" class="relative cursor-pointer bg-white rounded-md font-medium text-indigo-600 hover:text-indigo-500 focus-within:outline-none focus-within:ring-2 focus-within:ring-offset-2 focus-within:ring-indigo-500">
                        <span>Upload a file</span>
                        <input id="file-upload" name="file-upload" type="file" class="sr-only">
                      </label>
                      <p class="pl-1">or drag and drop</p>
                    </div>
                    <p class="text-xs text-gray-500">
                      PNG, JPG, GIF up to 10MB
                    </p>
                  </div>
                </div>
              </div>
            </div>
            <div class="px-4 py-3 bg-gray-50 text-right sm:px-6">
              <button class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              @click="save()">
                Save
              </button>
              <button class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500" >
                Cancel
              </button>
            </div>
          </div>
      </div>
    </div>
  </div>

</template>

<script setup lang="ts">
import {
  AddIssueDto,
  IssueDetailsDto,
  IssuesClient,
  IssueStatusDto,
  IssueStatusesClient, LabelDto, ProjectUserDto, UpdateIssueDto, UsersClient
} from "~/src/services/api.generated.clients";
import {ref} from "vue";
import {useFetch} from "#app";
import {loadUserDropdowns, UserDropdownValue} from "~/src/services/utils";
import LabelsInput from "~/components/LabelsInput.vue";

const statuses = ref<IssueStatusDto[]>([])
const selectedStatus = ref<IssueStatusDto>()
const selectedUserId = ref<number | null>(null)

const users = ref<ProjectUserDto[]>([])

const userDropdowns = ref<UserDropdownValue[]>([])
const route = useRoute()
const router = useRouter()
const details = ref<IssueDetailsDto>()
const title = ref("")
const description = ref("")
const labels = ref<LabelDto[]>([])


const init = async function (){
  const issueId = parseInt(route.params.id as string)
  const issuesClient = new IssuesClient();
  const issueDetails = await issuesClient.get(issueId)
  details.value = issueDetails;
  
  labels.value = issueDetails.labels!

  userDropdowns.value = await loadUserDropdowns(issueDetails.projectId)
  
  selectedUserId.value = issueDetails.assignedUserId
  
  title.value = issueDetails.title ?? ""
  description.value = issueDetails.description ?? ""

  const client = new IssueStatusesClient()
  statuses.value = await client.getForProject(issueDetails.projectId)
  selectedStatus.value = statuses.value.find(x => x.id == issueDetails.statusId)
  
}

const save = async function (){
  try {
    if (!details.value)
      return;
    if (!selectedStatus.value)
      return;
    
    const client = new IssuesClient();
    const dto = new UpdateIssueDto({
      id: details.value.id,
      title: title.value,
      text: description.value,
      assignedUserId: selectedUserId.value ?? null,
      statusId: selectedStatus.value.id,
      labelIds: labels.value.map(a => a.id)
    })
    await client.update(dto)
    await router.push('/issues/?projectId=' + details.value?.projectId)
  } catch (e) {
    console.log("Failed adding an issue", e)
    alert("Failed adding an issue")
  }
}

onMounted(async () => {
  await init()
})

</script>

<style scoped>

</style>