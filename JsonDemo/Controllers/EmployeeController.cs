using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JsonDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    // Static Object List
    public static List<Employee> employees = new();
    

    #region Create Employee
    [HttpPost("CreateEmployee")]
    public IActionResult AddEmployee(Employee request)
    {
        employees.Add(request);
        return Ok("Employee Created Successfully!");
    } 
    #endregion

    #region All Employee List
    [HttpGet("AllEmployee")]
    public IActionResult EmployeeDetails()
    {
        return Ok(employees);
    } 
    #endregion

    #region Employee Detail by Id
    [HttpGet("Employee/{Id}")]
    public IActionResult EmployeeDetailById(int Id)
    {
        var employee = employees.Find(x => x.Id == Id);

        if(employee != null)
        {
            var response = new { Status = "Employee Details", Code = "3002", Data = employee };
            return Ok(response);
        }
        else
        {
            var response = new { Status = "Employee Record Not Found!", Code = 100 };
            return Ok(response);
        }
    } 
    #endregion

    #region Delete Employee
    [HttpDelete("Employee/{Id}")]
    public IActionResult DeleteEmployeeById(int Id)
    {
        var employee = employees.Find(x => x.Id == Id);
        if(employee != null)
        {
            employees.Remove(employee);
            var response = new { Status = "Employee Deleted!", Code = 2001 };
            return Ok(response);
        }
        else
        {
            var response = new { Status = "Employee Record Not Found!", Code = 100 };
            return Ok(response);
        }
    } 
    #endregion

    #region Update Emplyee
    [HttpPut("Employee/{Id}")]
    public IActionResult UpdateEmployeeById(int Id, Employee request)
    {
        var employee = employees.Find(x => x.Id == Id);
        
        if (employee != null)
        {
            employees.Remove(employee);

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Age = request.Age;
            
            employees.Add(employee);

            var response = new { Status = "Employee Record Updated!", Code = 2002 };
            return Ok(response);
        }
        else
        {
            var response = new { Status = "Employee Record Not Found!", Code = 100 };
            return Ok(response);
        }
    } 
    #endregion
}


public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
}
