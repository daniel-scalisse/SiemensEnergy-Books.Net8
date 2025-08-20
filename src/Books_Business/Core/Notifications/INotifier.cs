using System.Collections.Generic;

namespace Books_Business.Core.Notifications
{
    public interface INotifier
    {
        void Handle(Notification notification);
        List<Notification> GetNotifications();
        bool HasNotification();
    }
}