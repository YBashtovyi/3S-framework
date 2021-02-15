using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;

namespace App.Business.Tests.TestData
{
    public static class NotificationHelper
    {
        public static Notification CreateNotification(Organization organization, EnumRecord state, EnumRecord type)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                CreateInOneSignalDate = DateTime.UtcNow,
                OneSignalId = Guid.NewGuid(),
                StateId = state.Id,
                State = state,
                TypeId = type.Id,
                Type = type,
                Organization = organization,
                OrganizationId = organization.Id,
                FailedMessages = 0,
                SuccessfulMessages = 0,
                Errored = 0
            };

            return notification;
        }


        public static NotificationReceiver CreateNotificationReceiver(Organization organization, Notification notification, Employee employee)
        {
            var notificationReceiver = new NotificationReceiver
            {
                Id = Guid.NewGuid(),
                Notification = notification,
                NotificationId = notification.Id,
                Receiver = employee,
                ReceiverId = employee.Id,
                OrganizationId = organization.Id,
                Organization = organization
            };

            return notificationReceiver;
        }
    }
}
