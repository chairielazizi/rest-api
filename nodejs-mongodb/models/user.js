const mongoose = require("mongoose");

const userSchema = new mongoose.Schema({
  name: {
    type: String,
    required: true,
  },
  email: {
    type: String,
    required: true,
  },
  creationDate: {
    type: Date,
    default: Date.now,
    required: true,
  },
});

// table name, schema name
module.exports = mongoose.model("User", userSchema);
