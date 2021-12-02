<template>
  <div>
    <h3 class="text-xl m-2">Issues - WinGen</h3>
    <router-link :to="`/projects/edit?projectId=${projectId}`">Edit</router-link>
    <router-link :to="`/issues/add?projectId=${projectId}`" class="bg-gray-800 text-gray-50 p-2 rounded m-2">
        <div class="inline-block items-center align-middle">
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
          </svg>
        </div>
        <span>
        Add issue
        </span>
    </router-link>

    <div class="flex -space-x-1 overflow-hidden mt-2 ml-3">
      <img v-for="user in userDropdowns" 
           @click="filterByUser(user)"
           class="inline-block h-6 w-6 rounded-full ring-2" 
           :class="filterUser === user ? 'ring-gray-500' : 'ring-white' "
           :src="avatarUrl(user.name)" 
           :title="user.name"
           :alt="user.name">
    </div>


    <div class="min-h-screen flex overflow-x-scroll py-5">

      <div v-for="column in issuesByColumn" class="bg-gray-100 rounded-lg px-3 py-3 column-width rounded mr-4 min-w-[300px] h-full">
        <p class="text-gray-700 font-semibold font-sans tracking-wide text-sm">{{ column.status.name }}</p>
          <draggable
              v-model="column.issues"
              @change="onChange(column.status, $event)"
              :animation="200"
              item-key="id"
              class="list-group h-full"
              group="tasks"
              ghost-class="ghost"
              @start="dragging = true"
              @end="dragging = false"
          >

            <template #item="{ element }">
              <div class="list-group-item">
                <IssueCard 
                    @self-assign="selfAssignIssue(element)"
                    :labels="element.labels"
                    :key="element.id" 
                    :id="element.id"
                    :title="element.title"
                    :avatar="element.userName ? avatarUrl(element.userName) : null"
                    :reference="element.reference"
                />
              </div>
            </template>
            <template #footer>
              <div class="mt-4">
                
              <router-link :to="`/issues/add?projectId=${projectId}`" class="bg-gray-800 text-gray-50 p-2 rounded mt-4 w-max">
                <div class="inline-block items-center align-middle">
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
                  </svg>
                </div>
                <span>
        Add issue
        </span>
              </router-link>
              </div>
            </template>
          </draggable>
          
      </div>



    </div>
  </div>
</template>

<script setup lang="ts">

import draggable from "vuedraggable";
import {avatarUrl, loadUserDropdowns, UserDropdownValue} from '~/src/services/utils';
import {IssueDto, IssuesClient, IssueStatusDto, IssueStatusesClient} from "~/src/services/api.generated.clients";
import {ref, computed} from "vue";
import IssueCard from "~/components/IssueCard.vue";
import {IssuesByColumn} from "~/src/services/issuesByColumn";
import {userStore} from "~/stores/userStore";

const route = useRoute()

const statuses = ref<IssueStatusDto[]>()
const issues = ref<IssueDto[]>()

const issuesByColumn = ref<IssuesByColumn[]>()

const refreshIssuesByColumn = function (){
  console.log('refreshing columns')
    const i: IssuesByColumn[] = []
    statuses.value?.forEach(x => {
      i.push({
        status: x,
        issues: filteredIssues.value!.filter(a => a.statusId == x.id)
      })
    })
    issuesByColumn.value = i;
}


const userDropdowns = ref<UserDropdownValue[]>([])
const filterUser = ref<UserDropdownValue | null>(null)
watch(filterUser, () => {
  refreshIssuesByColumn()
})

const projectId = computed(() => {
  return parseInt(route.query.projectId as unknown as string)
})

const filterByUser = async function(user: UserDropdownValue){
  if (filterUser.value === user)
    filterUser.value = null
  else
    filterUser.value = user
}

const filteredIssues = computed(function () {
  if (filterUser.value)
    return issues.value?.filter(x => x.assignedUserId == filterUser.value?.id);
  return issues.value
})

const init = async function (){
  try {
    const projectId = route.query.projectId as unknown as number
    const issuesClient = new IssuesClient();
    issues.value = await issuesClient.search(projectId)

    const projectsClient = new IssueStatusesClient();
    statuses.value = await projectsClient.getForProject(projectId)
    userDropdowns.value = await loadUserDropdowns(projectId)
    refreshIssuesByColumn()
  } catch (e) {
    console.log("Failed loading the board", e)
    alert("Failed loading the board")
  }
}

const onChange = async function(status: IssueStatusDto, arg: any){
  const addedIssue =  arg.added?.element as IssueDto
  if (!addedIssue)
    return;
  
  const client = new IssuesClient();
  await client.updateStatus(addedIssue.id, status.id);
}

const selfAssignIssue = async function(issue: IssueDto){
  try {
    const client = new IssuesClient();
    await client.assignToMyself(issue.id);
    issue.assignedUserId = userStore.getState().user!.id
    issue.userName = userStore.getState().user!.name
  } catch (e) {
    console.log("Failed self assigning issue", e)
    alert("Failed self assigning issue")
  }

}

onMounted(() => {
  init()
})

</script>

<style scoped>

</style>