using Grpc.Core;

namespace HashCheck.InterProcAPI
{
    public class InterProcServer
    {
        private Server GrpcServer { get; set; }

        public InterProcServer()
        {
            GrpcServer = new Server()
            {
                Services = { HashCheckLocalhost.BindService(new InterProcService()) },
                Ports = { new ServerPort("127.0.0.1", 50051, ServerCredentials.Insecure) }
            };
            GrpcServer.Start();
        }

        public void ShutdownAsync() => GrpcServer.ShutdownAsync().Wait();
    }
}
