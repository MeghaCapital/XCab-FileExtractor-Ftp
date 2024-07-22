namespace Data.Model.Tracking.IKEA
{
    public class IkeaTrackingStatisticModel
    {
        public int ExpectedFileCountForLegacyPickedUpJobs { get; set; }

        public int ExpectedFileCountForCFBPickedUpJobs { get; set; }

        public int ExpectedFileCountForCDCPickedUpJobs { get; set; }

        public int ExpectedFileCountForFutileJobs { get; set; }

        public int ExpectedFileCountForLegacyDeliveredJobs { get; set; }

        public int ExpectedFileCountForCFBDeliveredJobs { get; set; }

        public int ExpectedFileCountForCDCDeliveredJobs { get; set; }

        public int TotalExpectedFileCount { get; set; }

        public int ActualUploadedFileCount { get; set; }
    }
}
