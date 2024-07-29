using AlbBlogger1.Data;
using AlbBlogger1.Repositories;

namespace AlbBlogger1.Services;

public class ReplyService : IReplyService
{
    private readonly ReplyRepository _replyRepository;

    public ReplyService(ReplyRepository repository)
    {
        _replyRepository = repository;
    }

    public async Task<Reply> GetReplyByIdAsync(int id)
    {
        return await _replyRepository.GetById(id);
    }

    public async Task CreatReplyAsync(Reply reply)
    {
        await _replyRepository.Create(reply);
    }
    public async Task<IEnumerable<Reply>> GetRepliesByPostIdAsync(int postId)
    {
        return await _replyRepository.GetRepliesByPostIdAsync(postId);
    }
}

public interface IReplyService
{
    Task<IEnumerable<Reply>> GetRepliesByPostIdAsync(int postId);
    Task CreatReplyAsync(Reply reply);
    Task<Reply> GetReplyByIdAsync(int id);
}