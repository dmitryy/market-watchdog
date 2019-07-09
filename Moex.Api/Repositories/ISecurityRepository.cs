using Moex.Api.Contracts.History;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public interface ISecurityRepository
    {
        Task<Securities> GetAsync(string url);
    }
}
