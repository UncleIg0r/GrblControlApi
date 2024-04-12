using GrblControlAPI.Models;
using GrblControlAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GrblControlAPI.Controllers;

public class MoveController : Controller
{
    [HttpGet]
    [Route("MoveToAbsolutePoint")]
    public ActionResult<StatusResponse> MoveToAbsPoint(Point3f point)
    {
        string message = string.Empty;
        try
        {
            if (!StaticCnc.IsConnected) throw new GrblCncNotConnectedException();
            StaticCnc.MoveToAbsPoint(point);
            StaticCnc.GetStatus();
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return new StatusResponse(StaticCnc.CurrentStatus, StaticCnc.Speed, StaticCnc.IsConnected, message);
    }
    [HttpGet]
    [Route("MoveToAdditionPoint")]
    public ActionResult<StatusResponse> MoveToAddPoint(Point3f point)
    {
        string message = string.Empty;
        try
        {
            if (!StaticCnc.IsConnected) throw new GrblCncNotConnectedException();
            StaticCnc.MoveToAddPoint(point);
            StaticCnc.GetStatus();
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return new StatusResponse(StaticCnc.CurrentStatus, StaticCnc.Speed, StaticCnc.IsConnected, message);
    }
}
