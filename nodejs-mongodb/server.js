require("dotenv").config();

const express = require("express");
const app = express();

const mongoose = require("mongoose");

//setup the mongodb connection
mongoose.connect(process.env.DATABASE_URL).then(() => {
  console.log("Connected to MongoDB atlas");
});
const db = mongoose.connection;
db.on("error", (err) => console.error(err));

app.use(express.json()); //to enable server to accept json in post requests

//routes
const userRouter = require("./routes/users");
app.use("/users", userRouter);

app.listen(3000, () => {
  console.log("Server started on port 3000");
});
