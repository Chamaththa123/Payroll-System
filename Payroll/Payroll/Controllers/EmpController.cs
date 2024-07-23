using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payroll.interfaces;
using Payroll.models;
using System;
using System.Collections.Generic;

namespace Payroll.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        // Private readonly field to hold the employee service instance
        private readonly IEmpService empService;

        // Constructor that accepts an IEmpService instance to interact with employee data
        public EmpController(IEmpService employeeService)
        {
            empService = employeeService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<Response> GetAllEmployees()
        {
            try
            {
                // Call the service to get the list of employees
                var employees = empService.getEmp();
                if (employees == null || employees.Count == 0)
                {
                    var res = new Response
                    {
                        Status = 404,
                        Message = "No employees found."
                    };
                    return NotFound(res); // Return 404 with the response object
                }
                return Ok(employees); // Return 200 with the list of employees
            }
            catch (Exception ex)
            {
                var errorResponse = new Response
                {
                    Status = 500,
                    Message = $"Internal server error: {ex.Message}"
                };
                return StatusCode(500, errorResponse); // Return 500 with the error response object
            }
        }
    }
}
