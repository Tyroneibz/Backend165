use SkiServiceDB

db.users.insertOne({
  username: "user123",
  password: "mySecurePassword" // This should be a hashed password in a real application
})
