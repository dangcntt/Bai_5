using DTC.DefaultRepository.Constants;
using DTC.DefaultRepository.Models.Base;
using DTC.T;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Project.Net8.Models.Major
{
   
    public class MenuCitizenModel : Audit, TEntity<string>
    {
        public string Path { get; set; }
        public string Icon { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; } = 0;

        [BsonIgnore]
        public bool IsChecked { get; set; } = false;
        
        
        public int SoLuongTin { get; set; }


        public bool IsMenuCitizen { get; set; } = false; 





    }





    public class MenuCongDanList
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string ListAction { get; set; }
    }
    public class MenuCongDanListShort
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Resource
        {
            get
            {
                return FormatterString.ConvertToUnsign(Name).Replace(" ", "");
            }
            set
            {
            }
        }
        public string ListAction { get; set; }
    }


    
    public class MenuCongDanShort
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    
    }
}