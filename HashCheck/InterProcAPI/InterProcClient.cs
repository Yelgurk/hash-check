using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck.InterProcAPI
{
    public class InterProcClient
    {
        private Channel Channel;
        private HashCheckLocalhost.HashCheckLocalhostClient LocalClient;

        public InterProcClient(string[] objectPaths)
        {
            Channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
            LocalClient = new HashCheckLocalhost.HashCheckLocalhostClient(Channel);
            LocalClient.LocalObjectHashCalc(new HashCheckRequest { ObjectURI = { objectPaths } });
            Channel.ShutdownAsync().Wait();
        }
    }
}
