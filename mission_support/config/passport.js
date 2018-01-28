const jwt_strat = require('passport-jwt').Strategy;
const extract_jwt = require('passport-jwt').ExtractJwt;
const User = require('../model/user');
const config = require('../config/db');

module.exports = function(passport){
  let opts = {};
  opts.jwtFromRequest = extract_jwt.fromAuthHeaderWithScheme('jwt');
  opts.secretOrKey = config.secret;
  passport.use(new jwt_strat(opts, function(payload, done){
    User.get_user_by_id(payload._doc._id, function(err, user){
      if (err) {
        return done(err, false);
      }
      if (user) {
        return done(null, user);
      } else {
        return done(null, false);
      }
    });
  }));
}
