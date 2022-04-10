using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Contracts.Services.NotificationServices
{
    public interface INotificationService
    {
        Task Send(string destination, string content, string subject = null);
    }
}
