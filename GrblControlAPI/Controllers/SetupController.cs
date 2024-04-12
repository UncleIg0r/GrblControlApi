using GrblControlAPI.Models;
using GrblControlAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GrblControlAPI.Controllers;

public class SetupController : Controller
{
    [HttpGet]
    [Route("Connect")]
    public ActionResult<StatusResponse> Connect(SerialPortSettings settings)
    {
        string message = string.Empty;
        try
        {
            StaticCnc.Connect(settings);
            StaticCnc.GetStatus();
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return new StatusResponse(StaticCnc.CurrentStatus, StaticCnc.Speed, StaticCnc.IsConnected, message);
    }
    [HttpGet]
    [Route("SetSetting")]
    public ActionResult<StatusResponse> SetSetting(GrblSettingName name, string value)
    {
        string message = string.Empty;
        try
        {
            if(!StaticCnc.IsConnected) throw new GrblCncNotConnectedException();
            StaticCnc.SetCncSetting(name, value);
            StaticCnc.GetStatus();
        }
        catch(Exception ex)
        {
            message = ex.Message;
        }
        return new StatusResponse(StaticCnc.CurrentStatus, StaticCnc.Speed, StaticCnc.IsConnected, message);
    }
    [HttpGet]
    [Route("SetSpeed")]
    public ActionResult<StatusResponse> SetSpeed(int speed)
    {
        string message = string.Empty;
        try
        {
            if (!StaticCnc.IsConnected) throw new GrblCncNotConnectedException();
            StaticCnc.SetCncSpeed(speed);
            StaticCnc.GetStatus();
        }
        catch( Exception ex)
        {
            message = ex.Message;
        }
        return new StatusResponse(StaticCnc.CurrentStatus, StaticCnc.Speed, StaticCnc.IsConnected, message);
    }
    [HttpGet]
    [Route("Disconect")]
    public ActionResult<StatusResponse> Disconnect()
    {
        string message = string.Empty;
        try
        {
            if (!StaticCnc.IsConnected) throw new GrblCncNotConnectedException();
            StaticCnc.Disconnect();
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return new StatusResponse(StaticCnc.CurrentStatus, StaticCnc.Speed, StaticCnc.IsConnected, message);
    }
}
