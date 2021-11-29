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
          </div>
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
</template>

<script setup lang="ts">
import {computed, ref} from "vue";
import {EditProjectDto, ProjectDetailsDto, ProjectsClient} from "~/src/services/api.generated.clients";

const route = useRoute()
const projectId = computed(() => parseInt(route.query.projectId as string))
const details = ref<ProjectDetailsDto>()

const router = useRouter()
const save = async function(){
  try {
    if (!details.value)
      return;
    
    const client = new ProjectsClient();
    const dto = new EditProjectDto({
      id: projectId.value,
      name: details.value.name,
      abbreviation: details.value.abbreviation
    })
    await client.update(dto)
    await router.push(`/issues/?projectId=${projectId.value}`)
  } catch (e) {
    console.log("Failed saving project", e)
    alert("Failed saving project")
  }

}

const init = async function(){
  try {
    const client = new ProjectsClient();
    details.value = await client.details(projectId.value)
  } catch (e) {
    console.log("Failed loading project", e)
    alert("Failed loading project")
  }
}

onMounted(() => init())
    
</script>

<style scoped>

</style>