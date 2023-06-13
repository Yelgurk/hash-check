
using HashCheck.InterProcAPI.Server.Infrastructure;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace HashCheck.InterProcAPI.Server.Model;

[Export]
public class ChatService
{
    [Import]
    private Logger m_logger = null;

    [Import]
    private IChatLogRepository m_repository = null;
    private event Action<HashCheckRequest> Added;

    public void Add(HashCheckRequest chatLog)
    {
        m_logger.Info($"{chatLog}");

        m_repository.Add(chatLog);
        Added?.Invoke(chatLog);
    }

    public IObservable<HashCheckRequest> GetChatLogsAsObservable()
    {
        var oldLogs = m_repository.GetAll().ToObservable();
        var newLogs = Observable.FromEvent<HashCheckRequest>((x) => Added += x, (x) => Added -= x);

        return oldLogs.Concat(newLogs);
    }
}
