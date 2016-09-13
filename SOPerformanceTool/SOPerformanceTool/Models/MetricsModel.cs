namespace SOPerformanceTool.Models
{
    public class MetricsModel
    {
        public string Month { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
        public string Scenario { get; set; }
        public string Volume { get; set; }
        public double SolutionRatio { get; set; }
        public double FCSR { get; set; }
        public double CCSR { get; set; }
        public double OPCSR { get; set; }
        public string ReplyWithoutComVolume { get; set; }
        public double OneDayRR { get; set; }
        public double ThreeDayRR { get; set; }
        public double OverallRR { get; set; }
        public double ThreeDayAR { get; set; }
        public double SevenDayAR { get; set; }
        public double OverallAR { get; set; }
        public double AveragetIRT { get; set; }
        public double NOIR { get; set; }
        public double ThreeDayHR { get; set; }
        public double SevenDayHR { get; set; }
        public double OverallHR { get; set; }
        public double ThreeDayAHR { get; set; }
        public double SevenDayAHR { get; set; }
        public double OverallAHR { get; set; }
        public double ThreeDaySDAR { get; set; }
        public double SevenDaySDAR { get; set; }
        public double OverallSDAR { get; set; }
        public double ThreeDaySDHR { get; set; }
        public double SevenDaySDHR { get; set; }
        public double OverallSDHR { get; set; }
        public double ThreeDaySDAHR { get; set; }
        public double SevenDaySDAHR { get; set; }
        public double OverallSDAHR { get; set; }
        public string SDWorkedVol { get; set; }
    }
}
