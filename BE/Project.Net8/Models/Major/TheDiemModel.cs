using DTC.DefaultRepository.Constants;
using DTC.DefaultRepository.Models.Base;
using DTC.T;
using MongoDB.Bson.Serialization.Attributes;
using Project.Net8.Models.Core;
using System.Runtime.CompilerServices;
using CoreModel = DTC.DefaultRepository.Models.Core.CoreModel;

namespace Project.Net8.Models.Major;

public class TheDiemModel : Audit, TEntity<string>
{

    #region model cho TheDiem của bài 5
    public string TenThe { get; set; }// có thể bỏ này vì trong audit đã có rồi
    public string LoaiThe { get; set; }
    public int DiemCanTren { get; set; }
    public int DiemCanDuoi { get; set; }
}

    #endregion




