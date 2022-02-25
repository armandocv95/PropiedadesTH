using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropiedadesApp.Data
{
    public class Property
    {
        public int id { get; set; }
        public string title { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime disabled_at { get; set; }
        public string status { get; set; }
        public ICollection<Activity> activities { get; set; }
    }

    public class Activity
    {
        public int id { get; set; }
        public int property_id { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string status { get; set; }
        public Property property { get; set; }
        public ICollection<Survey> surveys { get; set; }
    }

    public class Survey
    {
        public int id { get; set; }
        public int activity_id { get; set; }
        public String answers { get; set; }
        public DateTime created_at { get; set; }
        public Activity activity { get; set; }
    }
}
