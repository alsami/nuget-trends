using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NuGetTrends.Api.Dtos;
using NuGetTrends.Api.Models;
using NuGetTrends.Data;

namespace NuGetTrends.Api.Services
{
    public class PackageHistoryService : IPackageHistoryService
    {
        private readonly NuGetTrendsContext _context;

        public PackageHistoryService(NuGetTrendsContext context)
        {
            _context = context;
        }

        public async Task<PackageDownloadDto> LoadHistoryAsync(HistoryLoadModel historyLoadModel)
        {
//            var query = from p in _context.PackageDownloads.AsNoTracking()
//                where p.PackageId == historyLoadModel.PackageId
//                select new
//                {
//                    Id = p.PackageId,
//                    p.IconUrl,
//                    Downloads = from d in _context.DailyDownloads.AsNoTracking()
//                        where d.PackageId == p.PackageId
//                              && d.Date > DateTime.UtcNow.AddMonths(-historyLoadModel.QuantityMonths).Date
//                        select new {d.Date, d.DownloadCount}
//                        into dc
//                        let week = dc.Date.AddDays(-(int)dc.Date.DayOfWeek).Date
//                        group dc by week
//                        into dpw
//                        orderby dpw.Key
//                        select new
//                        {
//                            dpw.Key.Date,
//                            Count = dpw.Average(c => c.DownloadCount)
//                        } as object
//                } as object;
//
//            return await query.FirstOrDefaultAsync();

            var dailyDownloads = await _context.Set<DailyDownload>()
                .AsNoTracking()
                .Where(dailyDownload => dailyDownload.PackageId == historyLoadModel.PackageId
                                        && dailyDownload.Date >
                                        DateTime.UtcNow.AddMonths(-historyLoadModel.QuantityMonths).Date)
                .GroupBy()
                .Select(dailyDownload => new DailyDownloadDto(dailyDownload.DownloadCount, dailyDownload.Date))
                .ToArrayAsync();

            var packageDownloadDto = await _context
                .Set<PackageDownload>()
                .AsNoTracking()
                .Where(package => package.PackageId == historyLoadModel.PackageId)
                .Select(package => new PackageDownloadDto(package.PackageId, package.IconUrl, dailyDownloads))
                .FirstOrDefaultAsync();

            return packageDownloadDto;
        }

    }
}
