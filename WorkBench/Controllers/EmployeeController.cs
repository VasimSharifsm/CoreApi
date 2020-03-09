using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkBench.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkBench.Models;
using Microsoft.AspNetCore.Authorization;

namespace WorkBench.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [Route("[action]")]
        public IActionResult Index()
        {
            try
            {
                List<Employees> Emp = new List<Employees>();
                Emp = _employeeRepository.GetAllEmployees().ToList();
                return Ok(Emp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("[action]/{id?}")]
        public IActionResult Details(int? id)
        {
            try
            {
                Employees model = _employeeRepository.GetEmployee(id ?? 1);
                return Ok(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employees employee)
        {
            try
            {
                Employees newemployee = _employeeRepository.PostEmployee(employee);
                return Created("Created New Employee", employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult UpdateEmployee(Employees employeechanges)
        {
            try
            {
                if (employeechanges.Id.ToString() != null)
                {
                    Employees newemployee = _employeeRepository.PutEmployee(employeechanges);
                    return Ok();
                }
                else
                {
                    return NotFound("Id Not found to update");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("[action]/{id?}")]
        public IActionResult DeleteEmployee(int Id)
        {
            try
            {
                if (Id.ToString() != null)
                {
                    Employees newemployee = _employeeRepository.DeleteEmployee(Id);
                    return Ok();
                }
                else
                {
                    return NotFound("Id Not found to Delete");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}