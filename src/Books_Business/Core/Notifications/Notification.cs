namespace Books_Business.Core.Notifications
{
    public class Notification
    {
        public string Msg { get; set; }

        public Notification(string msg)
        {
            Msg = msg;
        }
    }
}