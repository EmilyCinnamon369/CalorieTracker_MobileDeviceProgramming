using System;

namespace CalorieTracker_MobileDeviceProgramming.Models
{
    public class ActivityEntry
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public int CaloriesBurned { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}