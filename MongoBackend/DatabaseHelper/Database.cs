using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoBackend.Models;
using System.Text.Json.Nodes;

namespace MongoBackend.DatabaseHelper
{
    public class Database
    {
        //Método regresa todos los usuarios
        public List<User> getUsers()
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://root:root1234@cluster0.cv1da9a.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");

            var users = db.GetCollection<BsonDocument>("Users");

            List<BsonDocument> userArray = users.Find(new BsonDocument()).ToList();

            List<User> userList = new List<User>();

            foreach (BsonDocument bsonUser in userArray)
            {
                User user = BsonSerializer.Deserialize<User>(bsonUser);
                userList.Add(user);
            }

            return userList;
        }


        public User getUser(string index)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://root:root1234@cluster0.cv1da9a.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");

            var users = db.GetCollection<BsonDocument>("Users");

            List<BsonDocument> userArray = users.Find("{ \"_id\" : ObjectId(\"" + index + "\") }").ToList();

            
            User user= new User();
            foreach (BsonDocument bsonUser in userArray)
            {
                user = BsonSerializer.Deserialize<User>(bsonUser);
                
            }

            return user;
        }

        public void insertUser(User user)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://root:root1234@cluster0.cv1da9a.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");

            var users = db.GetCollection<BsonDocument>("Users");

            var doc = new BsonDocument
            {
                { "name", user.name },
                { "email", user.email },
                { "phone", user.phone },
                { "address", user.address},
                { "dateIn", user.dateIn }
            };

            users.InsertOne(doc);
        }


        public void deleteUser(string index)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://root:root1234@cluster0.cv1da9a.mongodb.net/test");
            ObjectId indexId = ObjectId.Parse(index);
            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");
            ObjectId obdId= ObjectId.Parse(index);
            db.GetCollection<BsonDocument>("Users").DeleteOne("{ \"_id\" : ObjectId(\""+index+"\") }");
            
        }


        public void updateUser(User user)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://root:root1234@cluster0.cv1da9a.mongodb.net/test");
            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");
            
            
          var filter = Builders<BsonDocument>.Filter.Eq("_id",user._id);
          var doc = new BsonDocument
            {
                { "name", user.name },
                { "email", user.email },
                { "phone", user.phone },
                { "address", user.address}
                
            };
            

            db.GetCollection<BsonDocument>("Users").FindOneAndUpdate(filter, doc);
            




        }

    }
}
