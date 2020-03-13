using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkBench.DAL;
using WorkBench.Models;
using WorkBench.SpModels;

namespace WorkBench.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class spEmployeeController : ControllerBase
    {
        EmployeesDataAccessLayer employeeDataAccessLayer = new EmployeesDataAccessLayer();
        [Route("[action]")]
        public IActionResult Index()
        {
            try
            {
                List<spgetempperformance> employeeDAL = new List<spgetempperformance>();
                employeeDAL = employeeDataAccessLayer.GetAllEmployeees().ToList();

                return Ok(employeeDAL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // GET: Student/Details/5  
        public ActionResult Details(int id)
        {
            return Ok();
        }

        // GET: Student/Create  
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: Student/Create  
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("[action]")]
        public IActionResult Create([FromBody] Employees emp)
        {
            try
            {
                // TODO: Add insert logic here  
                if (ModelState.IsValid)
                {
                    employeeDataAccessLayer.PostEmployee(emp);
                    return Created("", employeeDataAccessLayer);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: Student/Edit/5  
        public ActionResult Edit(int id)
        {
            return Ok();
        }

        // POST: Student/Edit/5  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here  

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }

        // GET: Student/Delete/5  
        public ActionResult Delete(int id)
        {
            return Ok();
        }

        // POST: Student/Delete/5  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here  

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }
    }
}