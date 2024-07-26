using DTC.DefaultRepository.Models.Base;
using DTC.T;
using MongoDB.Bson.Serialization.Attributes;


namespace Project.Net8.Models.Core
{
    public class DiaGioiHanhChinhModel : Audit,TEntity<string>
    {
        
        public  string IdDonViCha { get ; set;  }
        
        public  string Code { get; set; }
        
        public int Level { get; set; }
        
       
    }

    public class DiaGioiHanhChinhShortModel 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public  string Code { get; set; }
        public string Name { get; set; }

    }
}
