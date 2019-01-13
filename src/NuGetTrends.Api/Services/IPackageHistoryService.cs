using System.Threading.Tasks;
using NuGetTrends.Api.Dtos;
using NuGetTrends.Api.Models;

namespace NuGetTrends.Api.Services
{
    public interface IPackageHistoryService
    {
        Task<PackageDownloadDto> LoadHistoryAsync(HistoryLoadModel historyLoadModel);
    }
}
