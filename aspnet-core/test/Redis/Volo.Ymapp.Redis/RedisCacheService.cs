using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Redis
{
    public class RedisCacheService : StackExchangeRedis
    {
        private readonly string _connectionConfig;

        public RedisCacheService(string connectionConfig, ILogger logger)
            : base(connectionConfig, logger)
        {
            _connectionConfig = connectionConfig;
        }

        public void ConnectionChanged(Dictionary<string, string> dicChanged)
        {
            string monitorKey = _connectionConfig;
            if (dicChanged.ContainsKey(monitorKey) && dicChanged[monitorKey] != null)
            {
                base.ConnectionChanged(monitorKey, dicChanged[monitorKey]);
            }
        }

    }
}
