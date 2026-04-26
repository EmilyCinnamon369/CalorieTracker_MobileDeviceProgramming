using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker_MobileDeviceProgramming.Models
{
    public class MealEntry
    {
        public int Id { get; set; }
        [Required] public string MealName { get; set; }
        [Required] public string MealDescription { get; set; }
        public int MealCalories { get; set; }
        public DateTime MealDate { get; set; }
    }
}
