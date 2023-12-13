using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorTraining.Models;

[Table("Tbl_Blog")]
public class BlogDataModel
{
    [Key]
    public int Blog_Id { get; set; }
    public string Blog_Title { get; set; }
    public string Blog_Author { get; set; }
    public string Blog_Content { get; set; }
}


public enum EnumRespType
{
    None,
    Success,
    Information,
    Warning,
    Error,
    Confirm
}

public class ResponseModel
{
    public ResponseModel()
    {
    }

    public ResponseModel(string respCode, string respDesp, EnumRespType respType)
    {
        RespCode = respCode;
        RespDesp = respDesp;
        RespType = respType;
    }

    public string RespCode { get; set; }
    public string RespDesp { get; set; }
    public EnumRespType RespType { get; set; }
}

public class BaseSubResponseModel
{
    public ResponseModel Response { get; set; }
}

public enum EnumFormType
{
    None,
    List,
    Detail,
    Create,
    Edit
}

public class PageSettingModel
{
    public PageSettingModel() { }
    public PageSettingModel(int pageNo, int pageSize, int pageCount = 0)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        PageCount = pageCount;
    }

    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
}