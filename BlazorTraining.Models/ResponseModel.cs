namespace BlazorTraining.Models;

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
