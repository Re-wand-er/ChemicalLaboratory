import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
// export default defineConfig({
//   plugins: [react()],
// })

export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    strictPort: true, // Чтобы порт не прыгал
    proxy: {
      '/api': {
        target: 'https://localhost:54889',
        changeOrigin: true,
        secure: false, // Игнорировать ошибки SSL
      }
    }
  }
})