<template>
  <div>
    <input type="text" name="company-website" class="relative focus:ring-indigo-500 focus:border-indigo-500 flex-1 block w-full rounded-none rounded-r-md sm:text-sm border-gray-300 p-2"
           placeholder="label1, label2"
          v-model="labelName"
           @keydown.tab.prevent="addLabel()"
          @keydown.enter="addLabel()">
    <div v-if="true" class="origin-top-right absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none" role="menu" aria-orientation="vertical" aria-labelledby="menu-button" tabindex="-1">
      <div class="py-1" role="none">
        <a v-for="label in filteredLabels" href="#" class="text-gray-700 block px-4 py-2 text-sm hover:bg-gray-100" role="menuitem" tabindex="-1" id="menu-item-0" >
          {{ label.name }}
        </a>
      </div>
    </div>
    <div class="flex">
      <IssueLabel v-for="label in labels" :colour="label.labelColour" :name="label.name" 
      @click="removeLabel(label)"/>
      </div>
  </div>
</template>

<script setup lang="ts">
import {ref, watch, PropType} from "vue";
import {LabelDto, LabelsClient} from "~/src/services/api.generated.clients";
import IssueLabel from "~/components/IssueLabel.vue";

const props = defineProps({
  projectId: {
    type: Number,
    required: true
  },
  labels: {
    type: Array as PropType<LabelDto[]>,
    default: []
  }
})

const labelName = ref<string>("")
const filteredLabels = ref<LabelDto[]>([])

async function searchLabels(){
  try {
    const client = new LabelsClient();
    const labels = await client.searchLabelsForProject(props.projectId, labelName.value)
    filteredLabels.value = labels.filter(x => props.labels.some(t => t.id === x.id) == false)
  } catch (e) {
    filteredLabels.value = []
    console.log("Failed searching labels", e)
  }
}

watch(labelName, () => searchLabels())

const emit = defineEmits(['update:labels'])

function addLabel(){
  if (!filteredLabels.value)
    return;
    // tags.value.push(labelName.value)
  emit('update:labels', [...props.labels, filteredLabels.value[0]])
  labelName.value = ""
}

const removeLabel = function(label: LabelDto){
  emit('update:labels', props.labels.filter(x => x.id !== label.id))
}
</script>

<style scoped>

</style>