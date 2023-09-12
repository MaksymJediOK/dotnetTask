import { createApi } from '@reduxjs/toolkit/query'
import { baseQueryWithToken } from './baseQueryWithToken.ts'

export const authApi = createApi({
	reducerPath: 'auth/api',
	baseQuery: baseQueryWithToken,
	endpoints: () => ({

	}),
})
