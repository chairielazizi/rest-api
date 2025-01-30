const express = require("express");
const router = express.Router();
const User = require("../models/user");

// get all users
router.get("/", (request, response) => {
  response.send("get all users");
});

// get one
router.get("/:id", (request, response) => {
  response.send("get one user - " + request.params.id);
});

// create one
router.post("/", (request, response) => {
  response.send("create one user");
});

// update one --> patch is a partial update
router.patch("/:id", (request, response) => {
  response.send("update one user");
});

// delete one
router.delete("/:id", (request, response) => {
  response.send("delete one user");
});

module.exports = router;
