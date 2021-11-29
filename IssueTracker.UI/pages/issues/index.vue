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

    <div class="min-h-screen flex overflow-x-scroll py-5">

      <div v-for="column in issuesByColumn" class="bg-gray-100 rounded-lg px-3 py-3 column-width rounded mr-4 min-w-[250px] h-full">
        <p class="text-gray-700 font-semibold font-sans tracking-wide text-sm">{{ column.status.name }}</p>
          <draggable
              v-model="column.issues"
              @change="onChange(column.status, $event)"
              :animation="200"
              item-key="id"
              class="list-group min-h-[250px]"
              group="tasks"
              ghost-class="ghost"
              @start="dragging = true"
              @end="dragging = false"
          >

            <template #item="{ element }">
              <div class="list-group-item">
                <IssueCard 
                    :key="element.id" 
                    :id="element.id"
                    :title="element.title"
                    :avatar="`https://avatars.dicebear.com/api/initials/${element.userName}.svg`"
                    :reference="element.reference"
                />
<!--                :avatar="`https://avatars.dicebear.com/api/initials/${element.userName}.svg`"-->
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
import {IssueDto, IssuesClient, IssueStatusDto, IssueStatusesClient} from "~/src/services/api.generated.clients";
import {ref, computed} from "vue";
import IssueCard from "~/components/IssueCard.vue";
import {IssuesByColumn} from "~/src/services/issuesByColumn";

const route = useRoute()

const statuses = ref<IssueStatusDto[]>()
const issues = ref<IssueDto[]>()

const issuesByColumn = ref<IssuesByColumn[]>()

const projectId = computed(() => {
  return parseInt(route.query.projectId as unknown as string)
})

const init = async function (){
  try {
    const projectId = route.query.projectId as unknown as number
    const issuesClient = new IssuesClient();
    issues.value = await issuesClient.search(projectId)

    const projectsClient = new IssueStatusesClient();
    statuses.value = await projectsClient.getForProject(projectId)
    
    const i: IssuesByColumn[] = []
    statuses.value?.forEach(x => {
      i.push({
        status: x,
        issues: issues.value!.filter(a => a.statusId == x.id)
      })
    })
    
    issuesByColumn.value = i
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

onMounted(() => {
  init()
})

</script>

<style scoped>

</style>