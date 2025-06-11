using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImmigrationAI.Data;
using ImmigrationAI.Models;

namespace ImmigrationAI.Controllers
{
    public class ConversationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConversationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Conversations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Conversation.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Conversations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversation
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ConversationID == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // GET: Conversations/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "PasswordHash");
            return View();
        }

        // POST: Conversations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ConversationID,CreatedAt")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                conversation.ConversationID = Guid.NewGuid();
                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "PasswordHash", conversation.UserId);
            return View(conversation);
        }

        // GET: Conversations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversation.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "PasswordHash", conversation.UserId);
            return View(conversation);
        }

        // POST: Conversations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,ConversationID,CreatedAt")] Conversation conversation)
        {
            if (id != conversation.ConversationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationExists(conversation.ConversationID))
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
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "PasswordHash", conversation.UserId);
            return View(conversation);
        }

        // GET: Conversations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversation
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ConversationID == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var conversation = await _context.Conversation.FindAsync(id);
            if (conversation != null)
            {
                _context.Conversation.Remove(conversation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(Guid id)
        {
            return _context.Conversation.Any(e => e.ConversationID == id);
        }
    }
}
