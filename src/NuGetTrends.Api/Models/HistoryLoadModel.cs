namespace NuGetTrends.Api.Models
{
    public class HistoryLoadModel
    {
        public string PackageId { get; }

        public int QuantityMonths { get; }

        public HistoryGroupingType HistoryGroupingType { get; }

        public HistoryLoadModel(string packageId, int quantityMonths = 3, HistoryGroupingType historyGroupingType = HistoryGroupingType.Week)
        {
            PackageId = packageId;
            QuantityMonths = quantityMonths;
            HistoryGroupingType = historyGroupingType;
        }
    }
}
