const express = require("express");
const router = express.Router();
const User = require("../models/user");

// get all users
router.get("/", async (request, response) => {
  try {
    const users = await User.find();
    response.json(users);
  } catch (error) {
    response.status(500).json({ message: error.message });
  }
});

// get one
router.get("/:id", getUser, async (request, response) => {
  try {
    response.json(response.user);
  } catch (error) {
    response.status(500).json({ message: error.message });
  }
});

// create one
router.post("/", async (request, response) => {
  const user = new User({
    name: request.body.name,
    email: request.body.email,
  });
  try {
    const newUser = await user.save();
    response.status(201).json(newUser);
  } catch (error) {
    response.status(400).json({ message: error.message });
  }
});

// update one --> patch is a partial update
router.patch("/:id", getUser, async (request, response) => {
  if (request.body.name != null) {
    response.user.name = request.body.name;
  }
  if (request.body.email != null) {
    response.user.email = request.body.email;
  }
  try {
    const updatedUser = await response.user.save();
    response.json(updatedUser);
  } catch (error) {
    response.status(400).json({ message: error.message });
  }
});

// delete one
router.delete("/:id", getUser, async (request, response) => {
  try {
    await response.user.remove();
    response.json({ message: "User deleted" });
  } catch (error) {
    response.status(500).json({ message: error.message });
  }
});

//middleware
async function getUser(request, response, next) {
  let user;
  try {
    user = await User.findById(request.params.id);
    if (user == null) {
      return response.status(404).json({ message: "User not found" });
    }
  } catch (error) {
    return response.status(500).json({ message: error.message });
  }

  response.user = user;
  next(); //go to next middleware, or the request
}

module.exports = router;
