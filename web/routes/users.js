const config = require('../config/db');
const express = require('express');
const jwt = require('jsonwebtoken');
const passport = require('passport');
const router = express.Router();
const User = require('../model/user');


// Registration
router.post('/register', function(req, res, next) {
  let new_user = new User({
    first_name: req.body.first_name,
    last_name: req.body.last_name,
    username: req.body.username,
    email: req.body.email,
    organization: req.body.organization,
    password: req.body.password,
    user_type: req.body.user_type
  });

  User.add_user(new_user, function(err, user) {
    if (err) {
      res.json({success: false, message: 'Failed to register user!'});
    } else {
      res.json({success: true, messsage: 'User registered successfully!'});
    }
  });
});

// Auth
router.post('/auth', function(req, res, next) {
  const username = req.body.username;
  const password = req.body.password;

  User.get_user_by_username(username, function(err, user) {
    if (err) {
      throw err;
    }
    if (!user) {
      return res.json({success: false, message: 'User not found!'});
    }

    User.compare_password(password, user.password, function(err, isMatch){
      if (err) {
        throw err;
      }
      if (isMatch) {
        const token = jwt.sign({data: user}, config.secret, {
          expiresIn: 1800
        });

        res.json({
          success: true,
          token: 'jwt ' + token,
          user: {
            id: user._id,
            name: user.first_name,
            username: user.username,
            email: user.email
          }
        });
      } else {
        return res.json({success: false, message: 'Incorrect password!'});
      }
    });
  });
});

// Profile
router.get('/profile', passport.authenticate('jwt', {session: false}), function(req, res, next) {
  res.json({user: req.user});
});

// Reset
router.post('/reset', function(req, res, next) {
  const email = req.body.email;

  User.get_user_by_email(email, function(err, user) {
    if (err) {
      throw err;
    }

    if (!user) {
      return res.json({success: false, message: 'User not found!'});
    }

    res.json({success: true, message: 'Email sent!'});
  });
});

module.exports = router;
