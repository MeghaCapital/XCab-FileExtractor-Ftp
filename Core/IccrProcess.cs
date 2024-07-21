using Quartz;
using System;

namespace Core
{

    public interface IccrProcess : IJob
    {

        // INotification notification;
        void Start();
        void Stop();
        String Name();
    }
}
