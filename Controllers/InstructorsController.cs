using ContosoUniversity_TARpe21.Data;
using ContosoUniversity_TARpe21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity_TARpe21.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext _context;

        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instructors.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Instructor = await _context.Instructors
                .Include(s => s.CourseAssignments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Instructor == null)
            {
                return NotFound();
            }
            return View(Instructor);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,CourseAssignments,HireDate,OfficeAssignments")] Instructor Instructor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Instructor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. "
                    + "Try again, and if the problem persist "
                    + "see your system administrator.");
            }
            return View(Instructor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Instructor = await _context.Instructors.FindAsync(id);
            if (Instructor == null)
            {
                return NotFound();
            }
            return View(Instructor);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var InstructorToUpdate = await _context.Instructors.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Instructor>(InstructorToUpdate, "", s => s.FullName,
                s => s.CourseAssignments, s => s.HireDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persist " +
                        "see your system administrator.");
                }
            }
            return View(InstructorToUpdate);
        }
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Instructor = await _context.Instructors
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ID == id);
            if (Instructor == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Deletion has failed, Please try again, and if the problem persists "
                    + "see your system administrator.";
            }
            return View(Instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var Instructor = await _context.Instructors.FindAsync(id);
            if (Instructor == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Instructors.Remove(Instructor);
                await _context.SaveChangesAsync();
                return Redirect(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Index), new
                {
                    id = id,
                    SaveChangesError = true
                });
            }
        }
    }
}
