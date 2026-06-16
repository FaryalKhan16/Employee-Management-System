using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmployeeContext _context;

        public LoginController(EmployeeContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState != null)
            {
                var user = await _context.Logins.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                if (user != null)
                {

                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserId", user.Id.ToString());

                    // 🔹 Redirect to dashboard or homepage
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid email or password.";
                    return View();
                }
            }
            return View();
        }

        public async Task<IActionResult> GetEmployess()
        {
            var employess = await _context.EmpSalary.FromSqlRaw("EXEC GetEmployeeSalaryDetails").ToListAsync();
            return View(employess);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View(); // This returns the AddEmployee.cshtml form
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee emp)
        {
            if (ModelState.IsValid)
            {
                //emp.EId = 0; // Ensure identity field is not explicitly set
                _context.Employees.Add(emp);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Employee added successfully.";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Something went wrong!";
            return View(emp);

        }


        [HttpGet]
        public IActionResult AddProject()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddProject(Project project)
        {
            if (ModelState != null)
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Project submitted successfully!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Something went wrong!";
            return View(project);
        }


        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.EmpProject.FromSqlRaw("EXEC GetEmployeeProjects").ToListAsync();
            return View(projects);
        }

        public IActionResult Index()
        {
            
            ViewBag.EmployeeCount = _context.Employees.Count();
            ViewBag.ProjectCount = _context.Projects.Count();

            ViewBag.ProjectStatus = _context.Projects
                .GroupBy(p => p.PStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.EmployeeStats = _context.Employees
                .GroupBy(e => e.Department)
                .Select(g => new { Department = g.Key, Count = g.Count() })
                .ToList();

            return View();
        }

        [HttpGet]
        public IActionResult AddLeave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLeave(EmployeeLeave empleave)
        {
            if (ModelState != null)
            {
                _context.EmployeeLeaves.Add(empleave);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Leave submitted successfully!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Something went wrong!";
            return View(empleave);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeWithSalary emp)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EId == emp.e_id);
            if (employee != null)
            {
                employee.FirstName = emp.FirstName;
                employee.LastName = emp.LastName;
                employee.Email = emp.Email;
                employee.Contact = emp.Contact;
                employee.Department = emp.Department;
                employee.Degree = emp.Degree;

                var salary = _context.Salaries.FirstOrDefault(s => s.SId == emp.e_id);
                if (salary != null)
                {
                    salary.Total = emp.Total;
                }

                _context.SaveChanges();
                TempData["Success"] = "Employee Updated successfully!";
                return RedirectToAction("Index");
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult DeleteEmployee(int e_id)
        {
            var projects = _context.Projects.Where(p => p.EId == e_id).ToList();
            _context.Projects.RemoveRange(projects);

            var salary = _context.Salaries.FirstOrDefault(s => s.SId == e_id);
            if (salary != null)
                _context.Salaries.Remove(salary);

            var emp = _context.Employees.FirstOrDefault(e => e.EId == e_id);
            if (emp != null)
                _context.Employees.Remove(emp);

            _context.SaveChanges();
            TempData["Success"] = "Employee Deleted successfully!";
            return RedirectToAction("Index");

            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult UpdateProject(EmpProjects emp)
        {
            var project = _context.Projects.FirstOrDefault(p => p.PId == emp.p_id);

            if (project != null)
            {
                project.PName = emp.p_name;
                project.Duedate = emp.duedate;
                project.Subdate = emp.subdate;
                project.PStatus = emp.p_status;

                _context.SaveChanges();
                TempData["Success"] = "Project Updated successfully!";
                return RedirectToAction("Index");
            }

            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult DeleteProject(int p_id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.PId == p_id);

            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
                TempData["Success"] = "Project Deleted successfully!";
                return RedirectToAction("Index");
            }

            return Json(new { success = true });
        }





    }
}
