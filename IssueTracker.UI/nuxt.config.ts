import { defineNuxtConfig } from 'nuxt3'

export default defineNuxtConfig({
    build: {
        transpile: ['@headlessui/vue', '@heroicons/vue'],
        postcss: {
            postcssOptions: {
                plugins: {
                    tailwindcss: {},
                    autoprefixer: {},
                },
            },
        },
    },
    css: ['~/assets/css/tailwind.css'],
})
