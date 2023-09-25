using ContosoUniversity_TARpe21.Data;
using ContosoUniversity_TARpe21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity_TARpe21.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;
        public DepartmentsController (SchoolContext context)
        {
            _context = context;
        }

        //get Index
        public async Task<IActionResult> Index()
        {
            var schoolcontext = _context.Departments.Include(d => d.Administrator);
            return View(await schoolcontext.ToListAsync());
        }
        //get Details
        public async Task<IActionResult> Details(int? Id) 
        {
        if(Id == null) 
            {
            return NotFound();
            }
            string query = "SELECT * FROM Department WHERE DepartmentID = {0}";
            var department = await _context.Departments
                .FromSqlRaw(query, Id)
                .Include(d => d.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if(department == null) 
            {
                return NotFound();
            }
            return View(department);
        }

        //get create

        public async Task<IActionResult> Create()
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructors,"ID","FullName");
            return View();
        }

        //post Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("DepartmentID,Name,Budget,StartDate,RowVersion,InstructorID")]Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");   
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName",department.InstructorID);
            return View(department);
        }

        //get edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depratment = await _context.Departments
                .Include(i => i.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartmentID == id);   
            if (depratment == null) 
            {
                return NotFound();
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName",depratment.InstructorID);
            return View(depratment);
        }
        //post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, byte[] rowversion)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departmentToUpdate = await _context.Departments
               .Include(i => i.Administrator)
               .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (departmentToUpdate == null) 
            { 
                Department DeletedDepartment = new Department();
                await TryUpdateModelAsync(DeletedDepartment);
                ModelState.AddModelError(string.Empty, "Unable To Save Changes, The Department was deleted by another user");
                ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", DeletedDepartment.InstructorID);
                return View(DeletedDepartment);  
            }
            _context.Entry(departmentToUpdate).Property("RowVersion").OriginalValue = rowversion;

            if (await TryUpdateModelAsync<Department>(departmentToUpdate, "", s => s.Name, s => s.StartDate, s => s.Budget, s => s.InstructorID)) 
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex) 
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Department)exceptionEntry.Entity;
                    var dataBaseEntry = exceptionEntry.GetDatabaseValues();

                    if (dataBaseEntry != null) 
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save Changes. The Deparment has been deleted by another user"); 
                    }
                    else
                    {
                        var databaseValues = (Department)dataBaseEntry.ToObject();

                        if(databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
                        }
                        if (databaseValues.Budget != clientValues.Budget)
                        {
                            ModelState.AddModelError("Budget", $"Current value: {databaseValues.Budget}");
                        }
                        if (databaseValues.StartDate != clientValues.StartDate)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.StartDate}");
                        }
                        if (databaseValues.InstructorID != clientValues.InstructorID)
                        {
                            Instructor databaseInstructors = await _context.Instructors.FirstOrDefaultAsync(i => i.ID == databaseValues.InstructorID);
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.InstructorID}");
                        }
                        ModelState.AddModelError(string.Empty, "The Record you attempted to edit " + "was modified by another user after you got the original value. The" + "Edit operation was cancelled and the current values in the database" + "have been displayed. If you still want to edit this record. Click" + " The save button again. OtherWise clcik the back to list hyperlink.");
                        departmentToUpdate.RowVersion = databaseValues.RowVersion;
                        ModelState.Remove("Rowversion");
                    }
                }
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", departmentToUpdate.InstructorID);
            return View(departmentToUpdate);
        }

        //get delete

        public async Task<IActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return NotFound();
            }
            var depratment = await _context.Departments
                 .Include(d => d.Administrator)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (depratment == null) 
            {
                if(concurrencyError.GetValueOrDefault()) 
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound(); 
            }
            if(concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "The Record you attempted to edit " + "was modified by another user after you got the original value. The" + "Edit operation was cancelled and the current values in the database" + "have been displayed. If you still want to edit this record. Click" + " The save button again. OtherWise clcik the back to list hyperlink.";
            }
            return View(depratment);
        }
        // post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department department)
        {
            try
            {
                if(await _context.Departments.AnyAsync(m=>m.DepartmentID == department.DepartmentID))
                {
                    _context.Departments.Remove(department);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex) 
            {
                return RedirectToAction(nameof(Delete), new { concurrencyError = true, id = department.DepartmentID });
            }
        }
    }
}
