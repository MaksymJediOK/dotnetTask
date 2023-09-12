import { Link } from 'react-router-dom'

const Header = () => {
	return (
		<div className='flex justify-between pt-3 container bg-black'>
			<Link to='/login' className='font-semibold text-white no-underline'>
				Login
			</Link>
			<Link to='/reg' className='font-semibold text-white no-underline'>
				Registration
			</Link>
			<Link to='/passed' className='font-semibold text-white no-underline'>
				Passed tests
			</Link>
			<Link to='/assigned' className='font-semibold text-white no-underline'>
				Assigned tests
			</Link>
		</div>
	)
}

export { Header }