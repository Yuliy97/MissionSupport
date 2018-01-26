"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
// copypasted from rackt/reselect
var defaultEqualityCheck = function defaultEqualityCheck(a, b) {
  return a === b;
};

exports.default = function (func) {
  var equalityCheck = arguments.length <= 1 || arguments[1] === undefined ? defaultEqualityCheck : arguments[1];

  var lastArgs = null;
  var lastResult = null;

  return function () {
    for (var _len = arguments.length, args = Array(_len), _key = 0; _key < _len; _key++) {
      args[_key] = arguments[_key];
    }

    if (lastArgs !== null && lastArgs.length === args.length && args.every(function (value, index) {
      return equalityCheck(value, lastArgs[index]);
    })) {
      return lastResult;
    }
    lastArgs = args;
    lastResult = func.apply(undefined, args);
    return lastResult;
  };
};