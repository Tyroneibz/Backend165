Mongo shell script um SkiServiceDB zu erstellen

use SkiServiceDB

db.createCollection("SkiOrders")
db.createCollection("users")


use SkiServiceDB
db.SkiOrders.insertOne({
  kundenname: "string",
  email: "string",
  telefon: "string",
  prioritaet: "string",
  dienstleistung: "string",
  status: "string"
})


use SkiServiceDB
db.users.insertOne({
  username: "user123",
  password: "mySecurePassword"
})


