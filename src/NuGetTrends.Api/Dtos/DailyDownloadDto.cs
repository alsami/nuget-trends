using System;

namespace NuGetTrends.Api.Dtos
{
    public class DailyDownloadDto
    {
        public long? Count { get; }
        
        public DateTime Date { get; }

        public DailyDownloadDto(long? count, DateTime date)
        {
            Count = count;
            Date = date;
        }
    }
}