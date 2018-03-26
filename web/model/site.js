const config = require('../config/db');
const mongoose = require('mongoose');

const site_schema = mongoose.Schema({
  site_name: {
    type: String,
    required: true
  },
  site_address: {
    type: String,
    required: true
  },
  created_on: {
    type: Date,
    required: true,
    default: Date.now
  }
});

const site = module.exports = mongoose.model('Site', site_schema);

module.exports.add_site = function(new_site, callback) {
  new_site.save(callback);
}
