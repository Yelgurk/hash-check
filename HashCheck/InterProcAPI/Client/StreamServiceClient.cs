using Google.Protobuf.WellKnownTypes;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace HashCheck.InterProcAPI.Client;

public class StreamServiceClient : BindableBase, IDisposable
{
    private readonly GrpcServiceClient m_chatService = new GrpcServiceClient();

    public ObservableCollection<string> ChatHistory { get; } = new ObservableCollection<string>();
    private readonly object m_chatHistoryLockObject = new object();

    public string Name
    {
        get { return m_name; }
        set { SetProperty(ref m_name, value); }
    }
    private string m_name = "anonymous";

    private bool disposedValue;

    public DelegateCommand<string> WriteCommand { get; }

    public StreamServiceClient()
    {
        WriteCommand = new DelegateCommand<string>(WriteCommandExecute);
    }

    private async void WriteCommandExecute(string content)
    {
        await m_chatService.Write(new HashCheckRequest {
            ObjectURI = content
        });
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                m_chatService.Dispose();
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
