﻿<template>
  <div>
    <div class="flex justify-between">
      <div class="flex">
        
        <h3 class="text-xl m-2">Issues</h3>
      </div>
      <div class="flex">

        <router-link :to="`/issues/add?projectId=${projectId}`" class="bg-gray-800 text-gray-50 p-2 rounded m-2">
          <div class="inline-block items-center align-middle">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
            </svg>
          </div>
          <span class="text-sm">
            Add
          </span>
        </router-link>
        <router-link class="rounded p-2 h-8  mt-2" :to="`/projects/edit?projectId=${projectId}`">
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z" />
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
          </svg>
        </router-link>

        <div class="bg-gray-50 rounded w-24 py-1 px-2 mt-2">
          <button @click="isKanbanView = false"
                  :class="!isKanbanView ? 'bg-gray-100 ring-2 ring-blue-300' : 'bg-gray-300'"
                  class=" rounded p-2 ">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
              <path fill-rule="evenodd" d="M3 4a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zm0 4a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zm0 4a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zm0 4a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1z" clip-rule="evenodd" />
            </svg>
          </button>

          <button @click="isKanbanView = true"
                  class="rounded p-2 "
                  :class="isKanbanView ? 'bg-gray-100 ring-2 ring-blue-300' : 'bg-gray-300'">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 17V7m0 10a2 2 0 01-2 2H5a2 2 0 01-2-2V7a2 2 0 012-2h2a2 2 0 012 2m0 10a2 2 0 002 2h2a2 2 0 002-2M9 7a2 2 0 012-2h2a2 2 0 012 2m0 10V7m0 10a2 2 0 002 2h2a2 2 0 002-2V7a2 2 0 00-2-2h-2a2 2 0 00-2 2" />
            </svg>
          </button>
        </div>
      </div>
    </div>
    

    <div class="flex -space-x-1 overflow-hidden mt-2 ml-3 p-1">
      <img v-for="user in userDropdowns" 
           @click="filterByUser(user)"
           class="inline-block h-6 w-6 rounded-full ring-2" 
           :class="[filterUser === user ? 'ring-gray-500' : 'ring-white' , !!filterUser && filterUser !== user ? 'opacity-90' : 'z-10']"
           :src="avatarUrl(user.name)" 
           :title="user.name"
           :alt="user.name">
    </div>

    <div class="flex -space-x-1 overflow-hidden mt-2 ml-3 mb-2">
      <div v-for="label in labels" 
           @click="filterByLabel(label)">
        <IssueLabel :colour="label.labelColour" :name="label.name"
                    class="m-1"
                    :class="filterLabel === label ? 'ring-2 ring-gray-800' : ''" />
      </div>
    </div>


    <div v-if="isKanbanView" class="flex py-5">

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
              @dragenter="column.isDragging = true"
              @dragend="column.isDragging = false"
              @dragleave="column.isDragging = false"
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
    
<!--    List view-->
    <div v-else>

      <div v-for="column in issuesByColumn" class="bg-gray-200 rounded-lg px-3 py-3 column-width rounded mr-4 min-w-[300px] h-full mb-5">
        <p class="text-gray-700 font-semibold font-sans tracking-wide text-sm">{{ column.status.name }}</p>
        <draggable
            v-model="column.issues"
            @change="onChange(column.status, $event)"
            :animation="200"
            item-key="id"
            class="list-group min-h-[50px]"
            group="tasks"
            ghost-class="ghost"
            @dragenter="column.isDragging = true"
            @dragend="column.isDragging = false"
            @dragleave="column.isDragging = false"
            @start="dragging = true"
            @end="dragging = false"
        >

          <template #item="{ element }">
            <div class="list-group-item">
              <IssueRow
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
        </draggable>
        <div v-if="!column.isDragging && !column.issues.length" class="absolute -mt-8 ml-1 text-gray-500">
          No issues to show
        </div>
        <div class="hidden mt-4">
          
          <router-link :to="`/issues/add?projectId=${projectId}`" class="bg-gray-800 text-gray-50 p-2 rounded mt-5 w-max">
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
      </div>


    </div>
  </div>
</template>

<script setup lang="ts">

import draggable from "vuedraggable";
import IssueRow from "~/components/IssueRow.vue"
import {avatarUrl, loadUserDropdowns, UserDropdownValue} from '~/src/services/utils';
import {
  IssueDto,
  IssuesClient,
  IssueStatusDto,
  IssueStatusesClient,
  LabelDto, LabelsClient
} from "~/src/services/api.generated.clients";
import {ref, computed, watch} from "vue";
import IssueCard from "~/components/IssueCard.vue";
import {IssuesByColumn} from "~/src/services/issuesByColumn";
import {userStore} from "~/stores/userStore";
import IssueLabel from "~/components/IssueLabel.vue";

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
        issues: filteredIssues.value!.filter(a => a.statusId == x.id),
        isDragging: false
      })
    })
    issuesByColumn.value = i;
}

const isKanbanView = ref()

const userDropdowns = ref<UserDropdownValue[]>([])
const filterUser = ref<UserDropdownValue | null>(null)
watch(filterUser, () => {
  refreshIssuesByColumn()
})

const filterByUser = async function(user: UserDropdownValue){
  if (filterUser.value === user)
    filterUser.value = null
  else
    filterUser.value = user
}

const filterLabel = ref<LabelDto | null>(null)
const labels = ref<LabelDto[]>([])
watch(filterLabel, () => {
  refreshIssuesByColumn()
})

const filterByLabel = async function(label: LabelDto){
  if (filterLabel.value === label)
    filterLabel.value = null
  else
    filterLabel.value = label
}

const projectId = computed(() => {
  return parseInt(route.query.projectId as unknown as string)
})


const filteredIssues = computed(function () {
  if (!issues.value)
    return []
  let issuesList = issues.value
  if (filterUser.value)
    issuesList = issuesList.filter(x => x.assignedUserId == filterUser.value?.id);
  if (filterLabel.value)
    issuesList = issuesList.filter(x => x.labels!.some(l => l.id === filterLabel.value?.id));
  return issuesList
})

const init = async function (){
  try {
    const projectId = route.query.projectId as unknown as number
    const issuesClient = new IssuesClient();
    issues.value = await issuesClient.search(projectId)

    const projectsClient = new IssueStatusesClient();
    statuses.value = await projectsClient.getForProject(projectId)
    userDropdowns.value = await loadUserDropdowns(projectId)
    const labelsClient = new LabelsClient()
    labels.value = await labelsClient.labelsForProject(projectId)
    
    refreshIssuesByColumn()
  } catch (e) {
    console.log("Failed loading the board", e)
    alert("Failed loading the board")
  }
}

const onChange = async function(status: IssueStatusDto, arg: any){
  console.log('onChange', arg)
  const addedIssue =  arg.added?.element as IssueDto
  const moved =  arg.moved?.element as IssueDto
  if (moved){
    const status = moved.statusId
    const column = issuesByColumn.value!.filter(x => x.status.id == status)[0]
    const issuesForStatus = column.issues
    const client = new IssuesClient();
    await new Promise(f => setTimeout(f, 1000));
    await client.reorderIssues(issuesForStatus.map(x => x.id));
    return;
  }
  console.log('moved', moved)
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