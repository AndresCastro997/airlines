  using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace aspnetdemo2 {
  
  public class Aerolinea {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nombre { get; set; }  
        public string PaisOrigen { get; set; }
        public int Calificacion { get; set; }
        
        }

}