using HashCheck;
using HashCheck.InterProcAPI.Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace HashCheck.InterProcAPI.Server.Persistence
{
    [Export(typeof(IChatLogRepository))]
    public class ChatLogRepository : IChatLogRepository
    {
        private readonly List<HashCheckRequest> m_storage = new List<HashCheckRequest>();

        public void Add(HashCheckRequest chatLog)
        {
            m_storage.Add(chatLog);
        }

        public IEnumerable<HashCheckRequest> GetAll()
        {
            return m_storage.AsReadOnly();
        }
    }
}
