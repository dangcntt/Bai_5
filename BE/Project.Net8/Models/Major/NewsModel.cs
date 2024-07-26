using DTC.DefaultRepository.Constants;
using DTC.DefaultRepository.Models.Base;
using DTC.T;
using MongoDB.Bson.Serialization.Attributes;
using Project.Net8.Models.Core;
using CoreModel = DTC.DefaultRepository.Models.Core.CoreModel;

namespace Project.Net8.Models.Major;

public class NewsModel: Audit, TEntity<string>
{
    public string Content { get; set; }
    
    public MenuCongDanShort Menu {  get; set; }
    
    
    public CoreModel MenuMobi {  get; set; } // MENU HIỂN THỊ DƯỚI ỨNG DỰNG ĐIỆN THOẠI 
    
    
    public CoreModel Service  {  get; set; } // BÀI VIẾT CÓ LIÊN KẾT ĐẾN DỊCH VỤ
    
    
    public DateTime? PublicationDate { get; set; }
    
    [BsonIgnore]
    public string PublicationDateShow
    {
        get { return PublicationDate.HasValue ? PublicationDate.Value.ToLocalTime().ToString(FormatTime.FORMAT_DATE_CORE) : ""; }
    }
    
    
    public bool IsPublish { get; set; } = false;
    public string Describe { get; set; }
    
    public List<FileShortModel> Files { get; set; }
    
    public FileShortModel FileImage { get; set; }
}


public class NewMobiModelShort
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
        
    public FileShortModel FileImage { get; set; }

    public string UrlImg
    {
        get
        {
            try
            {
                if (FileImage != null)
                {
                    return "https://demotiendoduan.dongthap.gov.vn/api/v1/files/view/" + FileImage.FileId;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
    }
        
    public string Describe { get; set; }

}