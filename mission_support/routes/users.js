const config = require('../config/db');
const express = require('express');
const jwt = require('jsonwebtoken');
const passport = require('passport');
const router = express.Router();
const User = require('../model/user');


// Registration
router.post('/register', function(request, response, next) {
  let new_user = new User({
    first_name: request.body.first_name,
    last_name: request.body.last_name,
    username: request.body.username,
    email: request.body.email,
    password: request.body.password
  });

  User.add_user(new_user, function(err, user) {
    if (err) {
      response.json({success: false, message: 'Failed to register user!'});
    } else {
      response.json({success: true, messsage: 'User registered successfully!'});
    }
  });
});

// Auth
router.post('/auth', function(request, response, next) {
  const username = request.body.username;
  const password = request.body.password;

  User.get_user_by_username(username, function(err, user) {
    if (err) {
      throw err;
    }
    if (!user) {
      return response.json({success: false, message: 'User not found!'});
    }

    User.compare_password(password, user.password, function(err, isMatch){
      if (err) {
        throw err;
      }
      if (isMatch) {
        const token = jwt.sign({data: user}, config.secret, {
          expiresIn: 1800
        });

        response.json({
          success: true,
          token: 'jwt' + token,
          user: {
            id: user._id,
            name: user.first_name + ' ' + user.last_name,
            username: user.username,
            email: user.email
          }
        });
      } else {
        return response.json({success: false, message: 'Incorrect password!'});
      }
    });
  });
});

// Profile
router.get('/profile', passport.authenticate('jwt', {session: false}), function(request, response, next) {
  response.json({user: request.user});
});

module.exports = router;
