using System.Collections.Generic;

namespace NuGetTrends.Api.Dtos
{
    public class PackageDownloadDto
    {
        public string PackageId { get; }

        public string IconUrl { get; }

        public ICollection<DailyDownloadDto> Downloads { get; }

        public PackageDownloadDto(string packageId, string iconUrl, ICollection<DailyDownloadDto> downloads)
        {
            PackageId = packageId;
            IconUrl = iconUrl;
            Downloads = downloads;
        }
    }
}
