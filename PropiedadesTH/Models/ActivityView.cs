

using PropiedadesApp.Data;

namespace PropiedadesTH.Models
{
    public class ActivityView
    {
        public int id { get; set; }
        public int property_id { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string status { get; set; }
        public string condicion { get; set; }


        public static List<ActivityView> validarCondicion(List<Activity> actividades)
        {
            List<ActivityView> activities = new List<ActivityView>();
            for (int x = 0; x < actividades.Count; x++)
            {
                ActivityView act = new ActivityView();
                act.id = actividades[x].id;
                act.property_id = actividades[x].property_id;
                act.title = actividades[x].title;
                act.schedule = actividades[x].schedule;
                act.created_at = actividades[x].created_at;
                act.updated_at = actividades[x].updated_at;
                act.status = actividades[x].status;

                activities.Add(act);

                if (activities[x].status.CompareTo("active") == 0 && activities[x].schedule >= DateTime.Today)
                {
                    activities[x].condicion = "Pendiente a Realizar";
                }
                else if (activities[x].status.CompareTo("active") == 0 && activities[x].schedule < DateTime.Today)
                {
                    activities[x].condicion = "Atrasada";
                }
                else if (activities[x].status.CompareTo("done") == 0)
                {
                    activities[x].condicion = "Finalizada";
                }
                else
                {
                    activities[x].condicion = "Cancelada";
                }
            }
            return activities;
        }
    }

}
