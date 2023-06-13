using HashCheck;
using System.Collections.Generic;

namespace HashCheck.InterProcAPI.Server.Model
{
    public interface IChatLogRepository
    {
        void Add(HashCheckRequest chatLog);
        IEnumerable<HashCheckRequest> GetAll();
    }
}
