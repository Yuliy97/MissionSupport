const bcrypt = require('bcryptjs');
const config = require('../config/db');
const mongoose = require('mongoose');

// User Schema
const user_schema = mongoose.Schema({
  first_name: {
    type: String,
    required: true
  },
  last_name: {
    type: String,
    required: true
  },
  username: {
    type: String,
    required: true,
    validate: {
      isAsync: true,
      validator: function(e, callback) {
        User.find({username: e}, function(err, docs) {
          callback(docs.length == 0);
        });
      },
      message: 'Username already exists!'
    }
  },
  email: {
    type: String,
    required: true,
    validate: {
      isAsync: true,
      validator: function(e, callback) {
        User.find({email: e}, function(err, docs) {
          callback(docs.length == 0);
        });
      },
      message: 'Email already exists!'
    }
  },
  password: {
    type: String,
    required: true
  }
});

const User = module.exports = mongoose.model('User', user_schema);

module.exports.get_user_by_id = function(id, callback) {
  User.findById(id, callback);
}

module.exports.get_user_by_username = function(username, callback) {
  const query = {username: username}
  User.findOne(query, callback);
}

module.exports.get_user_by_email = function(email, callback) {
  const query = {email: email}
  User.findOne(query, callback);
}

module.exports.add_user = function(new_user, callback) {
  bcrypt.genSalt(10, function(err, salt) {
    bcrypt.hash(new_user.password, salt, function(err, hash) {
      if (err) {
        throw err;
      }
      new_user.password = hash;
      new_user.save(callback);
    });
  });
}

module.exports.compare_password = function(candidate_pass, hash, callback) {
  bcrypt.compare(candidate_pass, hash, function(err, isMatch) {
    if (err) {
      throw err;
    }
    callback(null, isMatch);
  });
}
