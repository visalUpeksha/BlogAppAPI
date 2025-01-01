using BlogAppAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAppAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            try
            {
                var blog = await _context.Blogs.Include(b => b.SubmittedBy).FirstOrDefaultAsync(b => b.BlogID == id);
                if (blog == null)
                    return NotFound();
                return blog;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> CreateBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBlog), new { id = blog.BlogID }, blog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditBlog(int id, Blog blog)
        {
            if (id != blog.BlogID)
                return BadRequest();

            _context.Entry(blog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
