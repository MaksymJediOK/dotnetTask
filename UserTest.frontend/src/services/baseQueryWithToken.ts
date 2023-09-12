import { fetchBaseQuery } from '@reduxjs/toolkit/query'

const baseQueryWithToken = fetchBaseQuery({
	baseUrl: import.meta.env.VITE_APP_URL,
	prepareHeaders: (headers) => {
		const token = localStorage.getItem('token')
		if (token) {
			headers.set('Authorization', `Bearer ${token}`)
		}
		return headers
	},
})

export { baseQueryWithToken }