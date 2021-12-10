<template>
  <div class="px-3 h-6 rounded-full text-xs font-semibold flex items-center" :class="colourToCss(colour)">
    <span class="w-2 h-2 rounded-full mr-1" :class="colourToDotCss(colour)"></span>
    <span>{{ name }}</span>
  </div>
</template>

<script setup lang="ts">

import {LabelColour} from "~/src/services/api.generated.clients";
import {PropType} from "vue";

defineProps({
  name: {
    type: String,
    required: true
  },
  colour: {
    type: Number as PropType<LabelColour>,
    default: LabelColour.Gray
  }
})

function colourToCss(colour: LabelColour){
  switch (colour){
    case LabelColour.Slate:
      return 'text-slate-800 bg-slate-100'
    case LabelColour.Neutral:
      return 'text-neutral-800 bg-neutral-100'
    case LabelColour.Amber:
      return 'text-amber-800 bg-amber-100'
    case LabelColour.Red:
      return 'text-red-800 bg-red-100'
    case LabelColour.Orange:
      return 'text-orange-800 bg-orange-100'
    case LabelColour.Yellow:
      return 'text-yellow-800 bg-yellow-100'
    case LabelColour.Lime:
      return 'text-lime-800 bg-lime-100'
    case LabelColour.Emerald:
      return 'text-emerald-800 bg-emerald-100'
    case LabelColour.Cyan:
      return 'text-cyan-800 bg-cyan-100'
    case LabelColour.Sky:
      return 'text-sky-800 bg-sky-100'
    case LabelColour.Violet:
      return 'text-violet-800 bg-violet-100'
    case LabelColour.Purple:
      return 'text-purple-800 bg-purple-100'
    case LabelColour.Fuchia:
      return 'text-fuchsia-800 bg-fuchsia-100'
    case LabelColour.Pink:
      return 'text-pink-800 bg-pink-100'
    case LabelColour.Rose:
      return 'text-rose-800 bg-rose-100'
    case LabelColour.Green:
      return 'text-green-800 bg-green-100'
    case LabelColour.Teal:
      return 'text-teal-800 bg-teal-100'
    case LabelColour.Blue:
      return 'text-blue-800 bg-blue-100'
    case LabelColour.Indigo:
      return 'text-indigo-800 bg-indigo-100'
    case LabelColour.Gray:
      return 'text-gray-800 bg-gray-100'
      // return 'text-gray-800 bg-gray-100'
  }
  return 'text-red-800 bg-gray-100'
}

function colourToDotCss(colour: LabelColour){
  switch (colour){
    case LabelColour.Slate:
      return 'bg-slate-400'
    case LabelColour.Red:
      return 'bg-red-400'
    case LabelColour.Orange:
      return 'bg-red-400'
    case LabelColour.Yellow:
      return 'bg-yellow-400'
    case LabelColour.Lime:
      return 'bg-lime-400'
    case LabelColour.Emerald:
      return 'bg-emerald-400'
    case LabelColour.Green:
      return 'bg-green-400'
    case LabelColour.Teal:
      return 'bg-teal-400'
    case LabelColour.Cyan:
      return 'bg-cyan-400'
    case LabelColour.Sky:
      return 'bg-sky-400'
    case LabelColour.Blue:
      return 'bg-blue-400'
    case LabelColour.Indigo:
      return 'bg-indigo-400'
    case LabelColour.Violet:
      return 'bg-violet-400'
    case LabelColour.Purple:
      return 'bg-purple-400'
    case LabelColour.Fuchia:
      return 'bg-fuchsia-400'
    case LabelColour.Pink:
      return 'bg-pink-400'
    case LabelColour.Rose:
      return 'bg-rose-400'
    case LabelColour.Gray:
      return 'bg-gray-400'
    case LabelColour.Neutral:
      return 'bg-neutral-400'
    case LabelColour.Amber:
      return 'bg-amber-400'
  }
  return ''
}

</script>

<style scoped>

</style>