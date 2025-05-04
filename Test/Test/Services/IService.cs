using Test.Models.DTOs;

namespace Test.Services;

public interface IService
{
    Task<List<RobotDto>> GetRobots();
    Task<bool> PostRobot(string name);
    Task<bool> DeleteRobot(string name);
}