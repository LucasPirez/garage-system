let isCoolDown = false

export const triggerCoolDown = ({ time }: { time?: number } = {}): boolean => {
  if (isCoolDown) {
    return false
  }

  isCoolDown = true
  setTimeout(() => (isCoolDown = false), time ?? 5000)

  return isCoolDown
}
