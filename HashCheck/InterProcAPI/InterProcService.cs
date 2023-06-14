using Avalonia;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using HashCheck.Views;

namespace HashCheck.InterProcAPI;

public class InterProcService : HashCheckLocalhost.HashCheckLocalhostBase
{
    public override Task<Empty> LocalObjectHashCalc(HashCheckRequest request, ServerCallContext context)
    {
        Dispatcher.UIThread.InvokeAsync(() => {
            App.Host!.Services.GetRequiredService<HashComputator>().PathTreeParser(request.ObjectURI.ToArray());
        });
        return Task.FromResult(new Empty());
    }
}