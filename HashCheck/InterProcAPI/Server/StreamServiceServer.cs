using HashCheck.InterProcAPI.Server.Infrastructure;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace HashCheck.InterProcAPI.Server
{
    public class StreamServiceServer
    {
        [Import]
        private Logger m_logger = null;

        public StreamServiceServer()
        {
            MefManager.Container.ComposeParts(this);
        }

        public void SubscribeLogger()
        {
            m_logger.GetLogsAsObservable().Subscribe((x) => Debug.WriteLine("привет"));
        }
    }
}
