using Google.Protobuf.WellKnownTypes;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HashCheck.InterProcAPI.Client;

public class GrpcServiceClient : IDisposable
{
    private readonly HashCheckLocalhost.HashCheckLocalhostClient m_client;
    private readonly GrpcChannel m_channel;
    private bool disposedValue;

    public GrpcServiceClient()
    {
        var https = false;

        if (https)
        {
            var httpHandler = new HttpClientHandler();

            m_channel = GrpcChannel.ForAddress("https://localhost:50052", new GrpcChannelOptions { HttpHandler = httpHandler });
            m_client = new HashCheckLocalhost.HashCheckLocalhostClient(
                m_channel
                    .Intercept(new ClientIdInjector())
                    .Intercept(new HeadersInjector())
            );
        }
        else
        {
            m_channel = GrpcChannel.ForAddress("http://localhost:50052");
            m_client = new HashCheckLocalhost.HashCheckLocalhostClient(
                m_channel
                    .Intercept(new ClientIdInjector())
                    .Intercept(new HeadersInjector())
            );
        }
    }

    public async Task Write(HashCheckRequest chatLog)
    {
        await m_client.GetLocalObjectHashAsync(chatLog);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                m_channel.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
