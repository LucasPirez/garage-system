interface Props {
  onClick: () => void
}

export const ButtonClose = ({ onClick }: Props) => {
  return (
    <button
      className="ml-2 p-1 rounded-full text-gray-600  hover:bg-gray-200 focus:outline-none focus:ring-2"
      onClick={onClick}
      aria-label="close">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 20 20"
        fill="currentColor"
        className="size-6">
        <path d="M6.28 5.22a.75.75 0 0 0-1.06 1.06L8.94 10l-3.72 3.72a.75.75 0 1 0 1.06 1.06L10 11.06l3.72 3.72a.75.75 0 1 0 1.06-1.06L11.06 10l3.72-3.72a.75.75 0 0 0-1.06-1.06L10 8.94 6.28 5.22Z" />
      </svg>
    </button>
  )
}
