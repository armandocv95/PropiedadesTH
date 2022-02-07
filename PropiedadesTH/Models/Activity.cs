namespace PropiedadesTH.Models
{
    public class Activity
    {
        public int id { get; set; }
        public int property_id { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string status { get; set; }
        public string condicion { get; set; }
        public Property property { get; set; }
        public ICollection<Survey> surveys { get; set; }


        public static List<Activity> validarCondicion(List<Activity> activities)
        {
            for (int x = 0; x < activities.Count; x++)
            {
                if (activities[x].status.CompareTo("active") == 0 && activities[x].schedule >= DateTime.Today)
                {
                    activities[x].condicion = "Pediente a Realizar";
                }
                else if (activities[x].status.CompareTo("Activo") == 0 && activities[x].schedule < DateTime.Today)
                {
                    activities[x].condicion = "Atrasada";
                }
                else if (activities[x].status.CompareTo("done") == 0)
                {
                    activities[x].condicion = "Finalizada";
                }
            }
            return activities;
        }
    }
}
