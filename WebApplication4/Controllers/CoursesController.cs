﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;

namespace WebApplication4.Controllers
{
    public class CoursesController : Controller
    {
        private readonly DbcoursesContext _context;

        public CoursesController(DbcoursesContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var dbcoursesContext = _context.Courses.Include(c => c.Category).Include(c => c.DifficultyLevel).Include(c => c.Teachers).Include(c => c.PhotoUrl);
            return View(await dbcoursesContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.DifficultyLevel)
                .Include(c => c.Teachers)
                .Include(c => c.PhotoUrl)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Category1");
            ViewData["DifficultyLevelId"] = new SelectList(_context.DifficultyLevels, "Id", "DifLevel");
            ViewData["TeachersId"] = new SelectList(_context.Teachers, "Id", "FullName");
            ViewData["PhotoUrlId"] = new SelectList(_context.Photos, "Id", "PhotoUrl");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DifficultyLevelId,CategoryId,TeachersId, PhotoUrlId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Category1", course.CategoryId);
            ViewData["DifficultyLevelId"] = new SelectList(_context.DifficultyLevels, "Id", "DifLevel", course.DifficultyLevelId);
            ViewData["TeachersId"] = new SelectList(_context.Teachers, "Id", "FullName", course.TeachersId);
            ViewData["PhotoUrlId"] = new SelectList(_context.Photos, "Id", "PhotoUrl", course.PhotoUrlId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Category1", course.CategoryId);
            ViewData["DifficultyLevelId"] = new SelectList(_context.DifficultyLevels, "Id", "DifLevel", course.DifficultyLevelId);
            ViewData["TeachersId"] = new SelectList(_context.Teachers, "Id", "FullName", course.TeachersId);
            ViewData["PhotoUrlId"] = new SelectList(_context.Photos, "Id", "PhotoUrl", course.PhotoUrlId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DifficultyLevelId,CategoryId,TeachersId,PhotoUrlId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Category1", course.CategoryId);
            ViewData["DifficultyLevelId"] = new SelectList(_context.DifficultyLevels, "Id", "DifLevel", course.DifficultyLevelId);
            ViewData["TeachersId"] = new SelectList(_context.Teachers, "Id", "FullName", course.TeachersId);
            ViewData["PhotoUrlId"] = new SelectList(_context.Photos, "Id", "PhotoUrl", course.PhotoUrlId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.DifficultyLevel)
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
