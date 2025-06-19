using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Domain.Models;

namespace TaskManager.Client.Services
{
    public class CommentService
    {
        private readonly AppDbContext dbContext;

        public CommentService(AppDbContext context)
        {
            dbContext = context;
        }

        public async Task<List<Comment>> GetCommentsForTaskAsync(int taskId)
        {
            return await dbContext.Comments
                .Where(c => c.TaskItemId == taskId)
                .Include(c => c.User)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
        }

        //public async Task DeleteCommentAsync(int commentId, int userId)
        //{
        //    var comment = await dbContext.Comments.FindAsync(commentId);
        //    if (comment == null)
        //        throw new KeyNotFoundException("Комментарий не найден.");

        //    if (comment.UserId != userId)
        //        throw new UnauthorizedAccessException("Удалять можно только свои комментарии.");

        //    dbContext.Comments.Remove(comment);
        //    await dbContext.SaveChangesAsync();
        //}

        //public async Task UpdateCommentAsync(int commentId, string newText, int userId)
        //{
        //    var comment = await dbContext.Comments.FindAsync(commentId);
        //    if (comment == null)
        //        throw new KeyNotFoundException("Комментарий не найден.");

        //    if (comment.UserId != userId)
        //        throw new UnauthorizedAccessException("Редактировать можно только свои комментарии.");

        //    comment.Text = newText;
        //    await dbContext.SaveChangesAsync();
        //}
    }
}
