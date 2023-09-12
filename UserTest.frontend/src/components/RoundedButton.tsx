import { ReactNode } from 'react'

interface ButtonProps {
	color: string
	children: ReactNode
	type: 'button' | 'submit' | 'reset'
	isDisabled?: boolean
	onClick?: () => void
}

export const RoundedButton = ({ color, children, type, isDisabled = false, onClick }: ButtonProps) => {
	return (
		<button
			type={type}
			className='rounded-md my-3 py-3 px-4 font-semibold whitespace-nowrap leading-4 text-white w-full'
			style={{ backgroundColor: color }}
			onClick={onClick}
			disabled={isDisabled}
		>
			{children}
		</button>
	)
}
