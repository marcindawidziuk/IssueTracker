<template>
  <div>
  <h3 class="text-xl">Issues - WinGen</h3>

  <div class="min-h-screen flex overflow-x-scroll py-12">

    <div v-for="status in statuses" class="bg-gray-100 rounded-lg px-3 py-3 column-width rounded mr-4">
      <p class="text-gray-700 font-semibold font-sans tracking-wide text-sm">{{ status.name }}</p>
      <div>

        <div v-for="issue in issues">
          <IssueCard :title="issue.title" :avatar="`https://avatars.dicebear.com/api/initials/${issue.userName}.svg`" />
        </div>
        
        <IssueCard title="My very long and long issue" tag-name="tag" avatar="https://avatars.dicebear.com/api/initials/marcin%20dawidziuk.svg" />
        
      </div>
    </div>



  </div>
  </div>
</template>

<script setup lang="ts">

import {IssueDto, IssuesClient, IssueStatusDto, IssueStatusesClient} from "~/src/services/api.generated.clients";
import {ref} from "vue";
import IssueCard from "~/components/IssueCard.vue";

const route = useRoute()

const statuses = ref<IssueStatusDto[]>()
const issues = ref<IssueDto[]>()

const init = async function (){
  try {
    const projectId = route.params.id as unknown as number
    const issuesClient = new IssuesClient();
    issues.value = await issuesClient.search(projectId)
    

    const projectsClient = new IssueStatusesClient();
    statuses.value = await projectsClient.getForProject(projectId)
    
  } catch (e) {
    console.log("Failed loading the board", e)
    alert("Failed loading the board")
  }

}

onMounted(() => {
  init()
})

</script>

<style scoped>

</style>