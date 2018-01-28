// APIs being used
const bparse = require('body-parser');
const config = require('./config/db');
const cors = require('cors');
const express = require('express');
const mongoose = require('mongoose');
const path = require('path');
const passport = require('passport');

// Connect to db and display connection status
mongoose.connect(config.database);
mongoose.connection.on('connected', function() {
  console.log('Connected to db ' + config.database + ' successfully');
});

// Check if there's a connection error to db
mongoose.connection.on('error', function(err) {
  console.log('Connection to db failed due to: ' + err);
});

const app = express();
const users = require('./routes/users');

const port = 3000;

// Enable CORS MW here
app.use(cors());

// Set static folder
app.use(express.static(path.join(__dirname, 'client')));

// Enable body parser MW here
app.use(bparse.json());
app.use('/users', users);

// Enable passport MW here
app.use(passport.initialize());
app.use(passport.session());

require('./config/passport')(passport);

app.get('/', function(request, response) {
  response.send('Invalid endpoint, try again later');
});

app.listen(port, function() {
  console.log('Server has started on port ' + port + ' successfully');
});
