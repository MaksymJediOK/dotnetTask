import { Route, Routes } from 'react-router-dom'
import { MainPage } from './pages/MainPage.tsx'

const AppRoutes = () => {
	return (

			<Routes>
				<Route index path='/' element={<MainPage />} />

			</Routes>

	)
}

export { AppRoutes }