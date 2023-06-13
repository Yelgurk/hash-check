using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HashCheck.InterProcAPI.Server.Infrastructure;
using HashCheck.InterProcAPI.Server.Model;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace HashCheck.InterProcAPI.Server.Grpc;

[Export]
public class GrpcService : HashCheckLocalhost.HashCheckLocalhostBase
{
    [Import]
    private Logger m_logger = null;

    [Import]
    private ChatService m_chatService = null;
    private readonly Empty m_empty = new Empty();

    public override Task<Empty> GetLocalObjectHash(HashCheckRequest request, ServerCallContext context)
    {
        m_logger.Info($"{context.Peer} {request}");

        m_chatService.Add(request);

        return Task.FromResult(m_empty);
    }
}
