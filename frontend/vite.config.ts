import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    resolve: {
        '@tabler/icons-react': '@tabler/icons-react/dist/esm/icons/index.mjs'
    }
})
