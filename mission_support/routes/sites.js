const config = require('../config/db');
const express = require('express');
const router = express.Router();
const Site = require('../model/site');

//Site creation
router.post('/create', function(req, res, next) {
  let new_site = new Site({
    site_name: req.body.site_name,
    site_address: req.body.site_address,
    site_date: req.body.site_date
  });

  Site.add_site(new_site, function(err, user) {
    if (err) {
      res.json({success: false, message: 'Failed to add site!'});
    } else {
      res.json({success: true, messsage: 'Site added successfully!'});
    }
  });

});

module.exports = router;
