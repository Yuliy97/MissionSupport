# redux-ava

> Write [AVA](https://github.com/sindresorhus/ava) tests for [redux](https://github.com/reactjs/redux) pretty quickly

[![Build Status](https://travis-ci.org/sotojuan/redux-ava.svg?branch=master)](https://travis-ci.org/sotojuan/redux-ava)

**Note:** Tests that use this module and fail will not have [`power-assert`](https://github.com/power-assert-js/power-assert) enhancements. This is a current AVA issue, but it'll be fixed in the future.

## Install

```
npm install --save-dev redux-ava
```

## API

### actionTest(actionCreator, data, type, [description])

#### actionCreator

Type: `function`

The action creator you want to test

#### data

Type: anything or `null`

The data your action creator function takes in. If it doesn't take any data, use `null`.

#### type

Type: `object`

The type you expect your action creator to return.

#### description

Type: `string`

Optional test description.

### reducerTest(reducer, stateBefore, action, stateAfter, [description])

#### reducer

Type: `function`

The reducer you want to test.

#### stateBefore

Type: `object`

The state you expect before the reducer is ran.

#### action

Type: `object`

The action you want to give to the reducer. This is different from `actionTest` in that you pass an action object, not an action creator function. You may use a call to your action creator function as an argument provided it returns an action object. See the examples below.

#### stateAfter

Type: `object`

The state you expect after the reducer is ran.

#### description

Type: `string`

Optional test description.

## Examples

This is an AVA port of [tape-redux](https://github.com/KaleoSoftware/tape-redux). For more documentation, check there.

Let's test an action creator:

```js
import test from 'ava'
import {actionTest} from 'redux-ava'

import {openMenu, getUser} from '../actions'

test('openMenu action', actionTest(openMenu, null, {type: 'OPEN_MENU'}))
test('getUser action', actionTest(getUser, 1, {type: 'GET_USER', id: 1}))
```

And now a reducer:

```js
import test from 'ava'
import {reducerTest} from 'redux-ava'

import app from '../reducers'
import {openMenu, getUser} from '../actions'

test('app reducer handles openMenu', reducerTest(
  app,
  {menuOpen: false, user: null},
  openMenu(),
  {menuOpen: true, user: null}
))

test('app reducer handles getUser', reducerTest(
  app,
  {menuOpen: false, user: null},
  getUser(1),
  {menuOpen: false, user: 1}
))
```

## License

MIT © [Juan Soto](http://juansoto.me)
