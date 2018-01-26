'use strict'

module.exports = function actionTest (actionCreator) {
  const params = [].slice.call(arguments, 1)
  const length = params.length
  const hasDescription = typeof params[length - 1] === 'string'
  const offset = hasDescription ? 2 : 1
  const expected = params[length - offset]
  const args = params.slice(0, length - offset)
  let description

  if (hasDescription) {
    description = params[length - 1]
  }

  return (t) => {
    t.deepEqual(actionCreator.apply(null, args), expected, description)
  }
}
