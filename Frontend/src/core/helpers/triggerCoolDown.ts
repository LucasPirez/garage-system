let isCoolDown = false

export const triggerCoolDown = (): boolean => {
  if (isCoolDown) {
    return false
  }

  isCoolDown = true
  setTimeout(() => (isCoolDown = false), 5000)

  return isCoolDown
}
