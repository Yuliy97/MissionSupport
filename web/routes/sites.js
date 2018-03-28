const config = require('../config/db');
const express = require('express');
const router = express.Router();
const Site = require('../model/site');

//Site creation
router.post('/create', function(req, res, next) {
  let new_site = new Site({
    site_name: req.body.site_name,
    site_address: req.body.site_address,
    created_on: req.body.site_date
  });

  Site.add_site(new_site, function(err, user) {
    if (err) {
      res.json({success: false, message: 'Failed to add site!'});
    } else {
      res.json({success: true, messsage: 'Site added successfully!'});
    }
  });

});

//Get sites
router.get('/all_sites', function(req, res) {
  const query = Site.find({});
  query.exec(function(err, sites) {
    if (err) {
      res.send(err);
    } else {
      res.json(sites);
    }
  });
});

module.exports = router;
