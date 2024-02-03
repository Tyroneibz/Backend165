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

NuGetPackete
C:\Users\tyron\.nuget\packages\microsoft.aspnetcore.authentication.jwtbearer\8.0.1\
C:\Users\tyron\.nuget\packages\microsoft.identitymodel.tokens\7.3.0\
C:\Users\tyron\.nuget\packages\mongodb.driver\2.23.1\
C:\Users\tyron\.nuget\packages\swashbuckle.aspnetcore\6.4.0\
C:\Users\tyron\.nuget\packages\system.identitymodel.tokens.jwt\7.3.0\


