import axios, { AxiosInstance } from 'axios'

export const axiosInstance: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 8000,
})

axiosInstance.interceptors.request.use(
  (config) => {
    return config
  },
  (error) => {
    console.log('Log axios response', error)
    return Promise.reject(error)
  }
)
