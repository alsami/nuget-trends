using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGetTrends.Api.Dtos;
using NuGetTrends.Api.Models;
using NuGetTrends.Api.Services;
using NuGetTrends.Data;

namespace NuGetTrends.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly NuGetTrendsContext _context;
        private readonly IPackageHistoryService _packageHistoryService;

        public PackageController(NuGetTrendsContext context, IPackageHistoryService packageHistoryService)
        {
            _context = context;
            _packageHistoryService = packageHistoryService;
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<object>>> Search([FromQuery] string q)
            => await _context.PackageDownloads
                .AsNoTracking()
                .Where(p => p.PackageIdLowered.Contains(q.ToLower(CultureInfo.InvariantCulture)))
                .OrderByDescending(p => p.LatestDownloadCount)
                .Take(20)
                .Select(p => new
                {
                    p.PackageId,
                    p.LatestDownloadCount,
                    IconUrl = p.IconUrl ?? "https://www.nuget.org/Content/gallery/img/default-package-icon.svg"
                })
                .ToListAsync();

        [HttpGet("history/{id}")]
        public Task<PackageDownloadDto> GetDownloadHistory([FromRoute] string id, [FromQuery] int months = 3, [FromQuery] HistoryGroupingType groupBy = HistoryGroupingType.Week)
        {
            return _packageHistoryService.LoadHistoryAsync(new HistoryLoadModel(id, months, groupBy));
        }
    }
}
