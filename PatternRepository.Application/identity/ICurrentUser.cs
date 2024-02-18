using JobFinder.Infrastructure.Identity;

namespace JobFinder.Infrastructure.Identity;
public interface ICurrentUser
{
    Guid UserId { get; }
    public string? Username { get; }
}
