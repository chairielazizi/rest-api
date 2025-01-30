require("dotenv").config();

const express = require("express");
const app = express();

const mongoose = require("mongoose");

mongoose.connect(process.env.DATABASE_URL).then(() => {
  console.log("Connected to MongoDB atlas");
});
const db = mongoose.connection;
db.on("error", (err) => console.error(err));

app.listen(3000, () => {
  console.log("Server started on port 3000");
});
