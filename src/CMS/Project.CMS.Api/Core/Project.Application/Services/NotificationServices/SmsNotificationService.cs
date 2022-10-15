using Project.Domain.Contracts.Services.NotificationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.NotificationServices
{
    public class SmsNotificationService : INotificationService
    {
        public Task Send(string destination, string context, string content)
        {
            throw new NotImplementedException();
        }
    }
}
