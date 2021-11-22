<template>
  <div class="bg-gray-800 h-screen">
    <div class="container max-w-full mx-auto py-24 px-6">
      <div class="font-sans">
        <div class="max-w-sm mx-auto px-6">
          <div class="relative flex flex-wrap">
            <div class="w-full relative">
              <div class="mt-6">
                <div class="text-center font-semibold text-gray-100 text-xl">
                  Issue Tracker
                </div>

                <slot></slot>

              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "account"
}
</script>

<style scoped>

</style>