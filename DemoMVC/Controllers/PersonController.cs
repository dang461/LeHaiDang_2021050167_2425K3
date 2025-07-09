using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DemoMVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbcontext _context;
        public PersonController(ApplicationDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Person.ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perSon = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (perSon == null)
            {
                return NotFound();
            }

            return View(perSon);
        }
        public IActionResult Create()
        {
            bool isFirstPerson = !_context.Person.Any();

            var person = new Person
            {
                PersonId = isFirstPerson ? "" : GenerateNewPersonId()
            };

            ViewBag.IsFirstPerson = isFirstPerson;
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
    public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)
    {
        bool isFirstPerson = !_context.Person.Any();

        if (isFirstPerson)
        {
          
            if (string.IsNullOrWhiteSpace(person.PersonId) || !System.Text.RegularExpressions.Regex.IsMatch(person.PersonId, @"^PS\d+$"))
            {
                ModelState.AddModelError("PersonId", "PersonId phải bắt đầu bằng 'PS' và theo sau là số, ví dụ: PS01 hoặc PS001.");
            }
        }
        else
        {
            
            person.PersonId = GenerateNewPersonId();
        }

        if (ModelState.IsValid)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Thêm mới thành công!";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.IsFirstPerson = isFirstPerson;
        return View(person);
    }

        public async Task<IActionResult> Edit(String id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String id, [Bind("PersonId,FullName,Address")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private bool PersonExists(string id)
        {
            return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> Delete(String id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }
            var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConFirmed(String id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Person' is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private string GenerateNewPersonId()
        {
            var lastPerson = _context.Person
                .OrderByDescending(p => p.PersonId)
                .FirstOrDefault();

            if (lastPerson == null || string.IsNullOrEmpty(lastPerson.PersonId))
                return "PS01"; 

            string lastId = lastPerson.PersonId;

           
            string prefix = new string(lastId.TakeWhile(char.IsLetter).ToArray());
            string numberPart = new string(lastId.SkipWhile(char.IsLetter).ToArray());

            if (string.IsNullOrEmpty(numberPart)) numberPart = "0";

            int nextNumber = int.Parse(numberPart) + 1;

          
            string formattedNumber = nextNumber.ToString("D" + numberPart.Length);

            return prefix + formattedNumber;
        }

    }
}