import { combineReducers, configureStore } from '@reduxjs/toolkit'

const rootReducer = combineReducers({})

export const store = configureStore({
	reducer: rootReducer,
	devTools: true,
	middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(),
})
