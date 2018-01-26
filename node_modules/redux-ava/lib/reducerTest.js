'use strict'

const deepFreeze = require('deep-freeze')
const Immutable = require('immutable')

const isIterable = Immutable.Iterable.isIterable

module.exports = (reducer, stateBefore, action, stateAfter, description) => t => {
  deepFreeze(action)

  if (isIterable(stateBefore) && isIterable(stateAfter)) {
    return t.true(Immutable.is(reducer(stateBefore, action), stateAfter), description)
  }

  deepFreeze(stateBefore)
  t.deepEqual(reducer(stateBefore, action), stateAfter, description)
}
