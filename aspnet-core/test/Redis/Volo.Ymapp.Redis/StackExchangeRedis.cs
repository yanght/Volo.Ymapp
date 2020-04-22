using Serilog;
using ServiceStack.Text;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Redis
{
    public abstract class StackExchangeRedis
    {
        private string connectionconfig = "";
        private ILogger _logger;

        public StackExchangeRedis(string ConnectionConfig, ILogger logger)
        {
            _logger = logger;
            connectionconfig = ConnectionConfig;
            CreateConnection();
        }
        private ConnectionMultiplexer RedisCon = null;
        private ConfigurationOptions configurationOptions = null;
        private IDatabase db = null;
        private void CreateConnection()
        {
            configurationOptions = ConfigurationOptions.Parse(connectionconfig);
            RedisCon = ConnectionMultiplexer.Connect(configurationOptions);
            RedisCon.ConnectionRestored += ConnMultiplexer_ConnectionRestored;
            RedisCon.ConnectionFailed += ConnMultiplexer_ConnectionFailed;
            RedisCon.ErrorMessage += ConnMultiplexer_ErrorMessage;
            RedisCon.ConfigurationChanged += ConnMultiplexer_ConfigurationChanged;
            RedisCon.HashSlotMoved += ConnMultiplexer_HashSlotMoved;
            RedisCon.InternalError += ConnMultiplexer_InternalError;
            RedisCon.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;
            db = RedisCon.GetDatabase();
        }
        public void ConnectionChanged(string key, string strConnection)
        {
            try
            {
                _logger.Information($"StackExchangeRedis.ConnectionChanged链接变更事件触发，key：{key}，strConnection：{strConnection}");
                connectionconfig = strConnection;
                CreateConnection();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, $"StackExchangeRedis.ConnectionChanged链接变更事件异常，key：{key}，strConnection：{strConnection}");
            }
        }

        #region Redis事件
        /// <summary>
        /// 重新配置广播时（通常意味着主从同步更改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            _logger.Information($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生内部错误时（主要用于调试）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_InternalError(object sender, InternalErrorEventArgs e)
        {
            _logger.Information($"{nameof(ConnMultiplexer_InternalError)}: {e.Exception}");
        }

        /// <summary>
        /// 更改集群时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            _logger.Information($"{nameof(ConnMultiplexer_HashSlotMoved)}: {nameof(e.OldEndPoint)}-{e.OldEndPoint} To {nameof(e.NewEndPoint)}-{e.NewEndPoint}, ");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            _logger.Information($"{nameof(ConnMultiplexer_ConfigurationChanged)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            _logger.Error(new Exception("Redis 发生错误"), $"{nameof(ConnMultiplexer_ErrorMessage)}: {e.Message}");
            //ErrorStore.LogInfo($"{nameof(ConnMultiplexer_ErrorMessage)}: {e.Message}");
        }


        /// <summary>
        /// 物理连接失败时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _logger.Information($"{nameof(ConnMultiplexer_ConnectionFailed)}: {e.Exception}");
        }

        /// <summary>
        /// 建立物理连接时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnMultiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            _logger.Information($"{nameof(ConnMultiplexer_ConnectionRestored)}: {e.Exception}");
        }
        #endregion

        public string Prefix { get; set; } = "PTV1_";
        #region 辅助方法
        private string PrefixedKey(string key)
        {
            return string.Concat(Prefix, key);
        }
        private string ToJson<T>(T value)
        {
            if (value is string || typeof(T).IsPrimitive)
            {
                return value.ToString();
            }
            else
            {
                return JsonSerializer.SerializeToString(value);
            }
        }
        private T ToObj<T>(RedisValue value)
        {
            if (value.IsNull)
            {
                return default(T);
            }
            else
            {
                if (typeof(T).IsPrimitive)
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                else
                {
                    return JsonSerializer.DeserializeFromString<T>(value);
                }
            }
        }
        private List<string> ConvetList(RedisValue[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }
            List<string> result = new List<string>();
            foreach (var item in values)
            {
                result.Add(item);
            }
            return result;
        }
        private List<string> ConvetList(IEnumerable<RedisKey> values)
        {
            List<string> result = new List<string>();
            foreach (var item in values)
            {
                result.Add(item);
            }
            return result;
        }
        private List<T> ConvetList<T>(RedisValue[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = ToObj<T>(item);
                result.Add(model);
            }
            return result;
        }

        private Dictionary<string, T> ConvertDictionary<T>(HashEntry[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }
            Dictionary<string, T> result = new Dictionary<string, T>();
            foreach (var item in values)
            {
                result.Add(item.Name, ToObj<T>(item.Value));
            }
            return result;
        }
        private HashSet<string> ConvetSet(RedisValue[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }
            HashSet<string> result = new HashSet<string>();
            foreach (var item in values)
            {
                result.Add(item.ToString());
            }
            return result;
        }
        private HashSet<T> ConvetSet<T>(RedisValue[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }
            HashSet<T> result = new HashSet<T>();
            foreach (var item in values)
            {
                result.Add(ToObj<T>(item));
            }
            return result;
        }
        private RedisValue[] ConvertRedisValue(string[] values)
        {

            RedisValue[] result = new RedisValue[values.Length];
            if (values.Length > 0)
            {
                int i = 0;
                foreach (var item in values)
                {
                    result[i] = item;
                    i++;
                }
            }
            return result;
        }
        private RedisValue[] ConvertRedisValue(int[] values)
        {
            RedisValue[] result = new RedisValue[values.Length];
            if (values.Length > 0)
            {
                int i = 0;
                foreach (var item in values)
                {
                    result[i] = item;
                    i++;
                }
            }
            return result;
        }
        private RedisValue[] ConvertRedisValue<T>(List<T> list)
        {
            RedisValue[] result = new RedisValue[list.Count];
            if (list.Count > 0)
            {
                int i = 0;
                foreach (var item in list)
                {
                    result[i] = ToJson(item);
                    i++;
                }
            }
            return result;
        }
        private HashEntry[] ConvertHashEntry<T>(Dictionary<string, T> dic)
        {
            HashEntry[] result = new HashEntry[dic.Count];
            if (dic.Count > 0)
            {
                int i = 0;
                foreach (KeyValuePair<string, T> item in dic)
                {
                    result[i] = new HashEntry(item.Key, ToJson(item.Value));
                    i++;
                }
            }
            return result;
        }
        private SortedSetEntry[] ConvertSortedSetEntry<T>(Dictionary<T, double> dic)
        {
            SortedSetEntry[] result = new SortedSetEntry[dic.Count];
            if (dic.Count > 0)
            {
                int i = 0;
                foreach (KeyValuePair<T, double> item in dic)
                {
                    result[i] = new SortedSetEntry(ToJson(item.Key), item.Value);
                    i++;
                }
            }
            return result;
        }

        private RedisKey[] ConvertRedisKey(string[] keys)
        {

            RedisKey[] result = new RedisKey[keys.Length];
            if (keys.Length > 0)
            {
                int i = 0;
                foreach (var item in keys)
                {
                    result[i] = PrefixedKey(item); ;
                    i++;
                }
            }
            return result;
        }

        #endregion
        #region 基本读写
        /// <summary>
        /// 根据Key获取值，只对string类型有效
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/get</remarks>
        public string Get(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<string>(key);
            }
            var task = db.StringGetAsync(PrefixedKey(key));
            return task.Result;
        }
        /// <summary>
        /// 根据传入的key获取一条记录的值
        /// </summary>
        /// <typeparam name="T">泛型约束为引用类型</typeparam>
        /// <param name="key"></param>
        /// <returns>如果键不存在，返回null</returns>
        /// <remarks>https://redis.io/commands/get</remarks>
        public T Get<T>(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<T>(key);
            }
            var task = db.StringGetAsync(PrefixedKey(key));
            return ToObj<T>(task.Result);
        }

        /// <summary>
        /// 获取所有(一个或多个)给定 key 的值
        /// </summary>
        /// <typeparam name="T">泛型约束为引用类型</typeparam>
        /// <param name="key"></param>
        /// <returns>如果键不存在，返回null</returns>
        /// <remarks>https://redis.io/commands/get</remarks>
        public List<T> Get<T>(string[] key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>("");
            }
            var task = db.StringGetAsync(ConvertRedisKey(key));
            return ConvetList<T>(task.Result);
        }
        /// <summary>
        /// 判断Key在本数据库内是否已被使用(包括各种类型、内置集合等等)
        /// </summary>
        /// <param name="key"></param>
        /// <returns>存在为true 不存在=false</returns>
        /// <remarks>https://redis.io/commands/exists</remarks>
        public bool ContainsKey(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.KeyExistsAsync(PrefixedKey(key)).Result;
        }
        /// <summary>
        /// 根据传入的key修改一条记录的值，当key不存在则添加,存在则覆盖。 默认3个小时后过期    注意：当前不支持Bool类型操作，如果传的是Bool类型Get的时候是无法转换的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/set</remarks>
        public bool Set<T>(string key, T value)
        {
            return Set(key, value, DateTime.Now.AddHours(3));
        }
        /// 根据传入的key修改一条记录的值，当key不存在则添加,存在则覆盖   注意：当前不支持Bool类型操作，如果传的是Bool类型Get的时候是无法转换的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        /// <param name="Minutes">过期时间分钟</param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/set</remarks>
        public bool Set<T>(string key, T value, int minutes)
        {
            return Set(key, value, DateTime.Now.AddMinutes(minutes));
        }
        /// <summary>
        /// 根据传入的key修改一条记录的值，当key不存在则添加,存在则覆盖。 注意：当前不支持Bool类型操作，如果传的是Bool类型Get的时候是无法转换的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        /// <param name="Expire">过期时间</param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/set</remarks>
        public bool Set<T>(string key, T value, DateTime Expire)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.StringSetAsync(PrefixedKey(key), ToJson(value), Expire - DateTime.Now).Result;
        }
        /// <summary>
        /// 注意：当前不支持Bool类型操作，如果传的是Bool类型Get的时候是无法转换的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="Expire"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/set</remarks>
        public bool Set<T>(string key, T value, TimeSpan? Expire)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.StringSetAsync(PrefixedKey(key), ToJson(value), Expire).Result;
        }
        /// <summary>
        /// Value是Bool类型的Set操作专用方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="Expire"></param>
        /// <returns></returns>
        public bool SetBool(string key, bool value, DateTime Expire)
        {
            if (!RedisCon.IsConnected)
            {
                return false;
            }
            return db.StringSetAsync(PrefixedKey(key), value, Expire - DateTime.Now).Result;
        }
        /// <summary>
        /// 根据传入的key移除键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/del</remarks>
        public bool Remove(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            var task = db.KeyDeleteAsync(PrefixedKey(key));
            return task.Result;
        }
        /// <summary>
        /// 根据指定的Key，将值减去指定值(仅整型有效)
        /// </summary>
        /// <param name="key"></param>
        /// <returns>返回减去后的值</returns>
        /// <remarks>https://redis.io/commands/decrby</remarks>
        /// <remarks>https://redis.io/commands/decr</remarks>
        public long DecrementValue(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(key);
            }
            return db.StringDecrementAsync(PrefixedKey(key)).Result;

        }
        /// <summary>
        /// 根据指定的Key，将值减去指定值(仅整型有效)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count">减去的数量</param>
        /// <returns>返回减去后的值</returns>
        /// <remarks>https://redis.io/commands/decrby</remarks>
        /// <remarks>https://redis.io/commands/decr</remarks>
        public long DecrementValue(string key, int count)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(key);
            }
            return db.StringDecrementAsync(PrefixedKey(key), count).Result;
        }
        /// <summary>
        /// 根据指定的Key，将值加1(仅整型有效)
        /// </summary>
        /// <param name="key"></param>
        /// <returns>返回加后的值</returns>
        /// <remarks>https://redis.io/commands/incrby</remarks>
        /// <remarks>https://redis.io/commands/incr</remarks>
        public long IncrementValue(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(key);
            }
            return db.StringIncrementAsync(PrefixedKey(key)).Result;
        }
        /// <summary>
        /// 根据指定的Key，将值加上指定值(仅整型有效)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count">递增数</param>
        /// <returns>返回加后的值</returns>
        /// <remarks>https://redis.io/commands/incrby</remarks>
        /// <remarks>https://redis.io/commands/incr</remarks>
        public long IncrementValue(string key, int count)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(key);
            }
            return db.StringIncrementAsync(PrefixedKey(key), count).Result;
        }
        /// <summary>
        /// 根据指定的key设置一项的到期时间（DateTime）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expireAt"></param>
        /// <returns>1 if the timeout was set. 0 if key does not exist or the timeout could not be set.</returns>
        /// <remarks>If key is updated before the timeout has expired, then the timeout is removed as if the PERSIST command was invoked on key.
        /// For Redis versions &lt; 2.1.3, existing timeouts cannot be overwritten. So, if key already has an associated timeout, it will do nothing and return 0. Since Redis 2.1.3, you can update the timeout of a key. It is also possible to remove the timeout using the PERSIST command. See the page on key expiry for more information.</remarks>
        /// <remarks>https://redis.io/commands/expire</remarks>
        /// <remarks>https://redis.io/commands/pexpire</remarks>
        /// <remarks>https://redis.io/commands/persist</remarks>
        public bool ExpireEntryAt(string key, DateTime expireAt)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.KeyExpireAsync(PrefixedKey(key), expireAt).Result;
        }
        /// <summary>
        /// 根据指定的key设置一项的到期时间（DateTime）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expireAt"></param>
        /// <returns>1 if the timeout was set. 0 if key does not exist or the timeout could not be set.</returns>
        /// <remarks>If key is updated before the timeout has expired, then the timeout is removed as if the PERSIST command was invoked on key.
        /// For Redis versions &lt; 2.1.3, existing timeouts cannot be overwritten. So, if key already has an associated timeout, it will do nothing and return 0. Since Redis 2.1.3, you can update the timeout of a key. It is also possible to remove the timeout using the PERSIST command. See the page on key expiry for more information.</remarks>
        /// <remarks>https://redis.io/commands/expire</remarks>
        /// <remarks>https://redis.io/commands/pexpire</remarks>
        /// <remarks>https://redis.io/commands/persist</remarks>
        public bool ExpireEntryAt(string key, TimeSpan expireAt)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.KeyExpireAsync(PrefixedKey(key), expireAt).Result;
        }
        /// <summary>
        /// 获取指定Key的项距离失效点的TimeSpan
        /// </summary>
        /// <param name="key"></param>
        /// <returns>TTL, or nil when key does not exist or does not have a timeout.</returns>
        /// <remarks>https://redis.io/commands/ttl</remarks>
        public TimeSpan GetTimeToLive(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<TimeSpan>(key);
            }
            TimeSpan? timeSpan = db.KeyTimeToLiveAsync(PrefixedKey(key)).Result;
            if (timeSpan.HasValue)
            {
                return timeSpan.Value == TimeSpan.MaxValue ? TimeSpan.FromSeconds(-1) : timeSpan.Value;
            }
            return TimeSpan.FromSeconds(-2);
        }
        /// <summary>
        /// 从数据库中查找名称相等的Keys的集合
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public List<string> SearchKeys(string pattern)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<string>>(pattern);
            }
            var server = RedisCon.GetServer(configurationOptions.EndPoints[0]);
            return ConvetList(server.Keys(0, PrefixedKey(pattern)));
        }
        #endregion
        #region List
        /// <summary>
        /// 将一个值插入到List<T>的最前面  client.LPush  
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        /// <returns>The length of the list after the push operations.</returns>
        /// <remarks>https://redis.io/commands/lpush 当When参数传递等于When.Always 时调用lpush 底层命令</remarks>
        /// <remarks>https://redis.io/commands/lpushx  当When参数传递不等于When.Always 时调用lpushx 底层命令 </remarks>
        public void PrependItemToList<T>(string listId, T value)
        {
            if (!RedisCon.IsConnected)
            {
                return;
            }
            db.ListLeftPushAsync(PrefixedKey(listId), ToJson(value));
        }
        /// <summary>
        ///  将一个值插入到List<T>的最后面   client.RPush ,需要一次插入多条，请调用AddRangeToList做批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        /// <returns>The length of the list after the push operation.</returns>
        /// <remarks>https://redis.io/commands/rpush 当When参数传递不等于When.Always 时调用rpush 底层命令</remarks>
        /// <remarks>https://redis.io/commands/rpushx  当When参数传递不等于When.Always 时调用rpushx 底层命令</remarks>
        public void AddItemToList<T>(string listId, T value)
        {
            if (!RedisCon.IsConnected)
            {
                return;
            }
            db.ListRightPushAsync(PrefixedKey(listId), ToJson(value));
        }
        /// <summary> 
        /// 将一个元素存入指定ListId的List<T>的尾部    client.RPush 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        /// <returns>The length of the list after the push operation.</returns>
        /// <remarks>https://redis.io/commands/rpush 当When参数传递不等于When.Always 时调用rpush 底层命令</remarks>
        /// <remarks>https://redis.io/commands/rpushx  当When参数传递不等于When.Always 时调用rpushx 底层命令</remarks>
        public void PushItemToList<T>(string listId, T value)
        {
            AddItemToList(listId, value);
        }
        /// <summary>
        /// 批量插入List，插入到List<T>的最后面 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="values"></param>
        /// <returns>The length of the list after the push operation.</returns>
        /// <remarks>https://redis.io/commands/rpush 当When参数传递不等于When.Always 时调用rpush 底层命令</remarks>
        /// <remarks>https://redis.io/commands/rpushx  当When参数传递不等于When.Always 时调用rpushx 底层命令</remarks>
        public void AddRangeToList<T>(string listId, List<T> values)
        {
            if (!RedisCon.IsConnected)
            {
                return;
            }
            db.ListRightPushAsync(PrefixedKey(listId), ConvertRedisValue(values));
        }
        /// <summary>
        /// 返回List字符串列表
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>如果键不存在或者获取到的List长度为0，返回null</returns>
        /// <remarks>https://redis.io/commands/lrange</remarks>
        public List<string> GetAllItemsFromList(string listId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<string>>(listId);
            }
            return ConvetList(db.ListRangeAsync(PrefixedKey(listId)).Result);
        }
        /// <summary>
        /// 获取指定ListId的内部List<string>的所有值，支持泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <returns>如果键不存在或者获取到的List长度为0，返回null</returns>
        /// <remarks>https://redis.io/commands/lrange</remarks>
        public List<T> GetAllItemsFromList<T>(string listId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(listId);
            }
            return ConvetList<T>(db.ListRangeAsync(PrefixedKey(listId)).Result);
        }
        /// <summary>
        /// 获取指定ListId的内部List<string>中指定下标范围的数据，支持泛型
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>如果键不存在或者获取到的List长度为0，返回null</returns>
        /// <remarks>https://redis.io/commands/lrange</remarks>
        public List<T> GetRangeFromList<T>(string listId, int startingFrom, int count)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(listId);
            }
            return ConvetList<T>(db.ListRangeAsync(PrefixedKey(listId), startingFrom, startingFrom + count - 1).Result);
        }
        /// <summary>
        /// 移除指定ListId的内部List<string>中第二个参数值相等的那一项
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        /// <returns>返回被删除的元素数量</returns>
        /// <remarks>https://redis.io/commands/lrem</remarks>
        public long RemoveItemFromList<T>(string listId, T value)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(listId);
            }
            return db.ListRemoveAsync(PrefixedKey(listId), ToJson(value)).Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        /// <param name="count">count > 0: 从头往尾移除值为 value 的元素。count小于0 从尾往头移除值为 value 的元素count = 0: 移除所有值为 value 的元素。</param>
        /// <returns>返回删除元素的个数</returns>
        /// <remarks>https://redis.io/commands/lrem</remarks>
        public long RemoveItemFromList<T>(string listId, T value, int count = 0)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(listId);
            }
            return db.ListRemoveAsync(PrefixedKey(listId), ToJson(value), count).Result;
        }
        /// <summary>
        /// 从指定ListId的List<T>末尾移除一项并返回   client.RPop  返回并弹出指定Key关联的链表中的最后一个元素，即尾部元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <returns>The element being popped.</returns>
        /// <remarks>https://redis.io/commands/rpop</remarks>
        public string PopItemFromList(string listId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<string>(listId);
            }
            var task = db.ListRightPopAsync(PrefixedKey(listId));
            return task.Result;
        }
        /// <summary>
        /// 从指定ListId的List<T>末尾移除一项并返回   client.RPop  返回并弹出指定Key关联的链表中的最后一个元素，即尾部元素
        /// </summary>
        /// <typeparam name="T">仅限引用类型，弹出值类型请用可空值类型</typeparam>
        /// <param name="listId"></param>
        /// <returns>如果键不存在，返回null</returns>
        /// <remarks>https://redis.io/commands/rpop</remarks>
        public T PopItemFromList<T>(string listId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<T>(listId);
            }
            var task = db.ListRightPopAsync(PrefixedKey(listId));
            return ToObj<T>(task.Result);
        }
        /// <summary>
        /// 根据ListId，获取内置的List<T>的项数
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The length of the list at key.</returns>
        /// <remarks>https://redis.io/commands/llen</remarks>
        public long GetListCount(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(key);
            }
            var task = db.ListLengthAsync(PrefixedKey(key));
            return task.Result;
        }

        /// <summary>
        /// 根据ListId和下标获取一项
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="listIndex"></param>
        /// <returns>The requested element, or nil when index is out of range.</returns>
        /// <remarks>https://redis.io/commands/lindex</remarks>
        public string GetItemFromList(string listId, int listIndex)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<string>(listId);
            }
            return db.ListGetByIndexAsync(PrefixedKey(listId), listIndex).Result;
        }
        /// <summary>
        /// 根据ListId和下标获取一项
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="listIndex"></param>
        /// <returns>如果键不存在，返回null</returns>
        /// <remarks>https://redis.io/commands/lindex</remarks>
        public T GetItemFromList<T>(string listId, int listIndex)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<T>(listId);
            }
            return ToObj<T>(db.ListGetByIndexAsync(PrefixedKey(listId), listIndex).Result);
        }
        #endregion

        #region Set
        /// <summary>
        /// 添加一个项到内部的Set，对应 SADD 如果需要一次插入多条，请调用 批量插入方式AddRangeToSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <returns>True if the specified member was not already present in the set, else False</returns>
        /// <remarks>https://redis.io/commands/sadd</remarks>
        public bool AddItemToSet<T>(string setId, T item)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(setId);
            }
            return db.SetAddAsync(PrefixedKey(setId), ToJson(item)).Result;
        }
        /// <summary>
        /// 添加一个项到内部的Set，对应 SADD  批量插入方式AddRangeToSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <returns>The number of elements that were added to the set, not including all the elements already present into the set.</returns>
        /// <remarks>https://redis.io/commands/sadd</remarks>
        public void AddRangeToSet<T>(string setId, List<T> list)
        {
            if (!RedisCon.IsConnected)
            {
                return;
            }
            db.SetAddAsync(PrefixedKey(setId), ConvertRedisValue(list));
        }
        /// <summary>
        /// 移除item后，将元素从一个集合移动到另一个集合的开头  对应SMOVE 命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromSetId"></param>
        /// <param name="toSetId"></param>
        /// <param name="item"></param>
        /// <returns>1 if the element is moved. 0 if the element is not a member of source and no operation was performed.</returns>
        /// <remarks>https://redis.io/commands/smove</remarks>
        public void MoveBetweenSets<T>(string fromSetId, string toSetId, T item)
        {
            if (!RedisCon.IsConnected)
            {
                return;
            }
            db.SetMoveAsync(PrefixedKey(fromSetId), PrefixedKey(toSetId), ToJson(item));
        }
        /// <summary>
        /// 获取指定SetId的内部HashSet<T>的所有值 
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>All elements of the set.</returns>
        /// <remarks>https://redis.io/commands/smembers</remarks>
        public HashSet<string> GetAllItemsFromSet(string setId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<HashSet<string>>(setId);
            }
            return ConvetSet<string>(db.SetMembersAsync(PrefixedKey(setId)).Result);
        }
        /// <summary>
        /// 获取指定SetId的内部HashSet<T>的所有值 
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>All elements of the set.</returns>
        /// <remarks>https://redis.io/commands/smembers</remarks>
        public HashSet<T> GetAllItemsFromSet<T>(string setId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<HashSet<T>>(setId);
            }
            return ConvetSet<T>(db.SetMembersAsync(PrefixedKey(setId)).Result);
        }

        /// <summary>
        ///从指定SetId的内部HashSet<T>中移除与第二个参数值相等的那一项
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        /// <returns>True if the specified member was already present in the set, else False</returns>
        /// <remarks>https://redis.io/commands/srem</remarks>
        public void RemoveItemFromSet<T>(string setId, T item)
        {
            if (!RedisCon.IsConnected)
            {
                return;
            }
            db.SetRemoveAsync(PrefixedKey(setId), ToJson(item));
        }
        /// <summary>
        /// 从指定setId的集合中获取随机项(非排序集合,zset是不支持随机获取的命令)
        /// </summary>
        /// <param name="setId"></param>
        /// <returns>The randomly selected element, or nil when key does not exist</returns>
        /// <remarks>https://redis.io/commands/srandmember</remarks>
        /// <remarks>时间复杂度O(1)</remarks>
        public T GetRandomItemFromSet<T>(string setId) where T : class
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<T>(setId);
            }
            return ToObj<T>(db.SetRandomMemberAsync(PrefixedKey(setId)).Result);
        }
        /// <summary>
        /// 从指定setId 的集合获取一定数量的随机项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setId"></param>
        /// <param name="count"></param>
        /// <returns>The randomly selected element, or nil when key does not exist</returns>
        /// <remarks>https://redis.io/commands/srandmember</remarks>
        /// <remarks>时间复杂度O(n) n为count 元素的数量,尽量不要获取太多元素</remarks>
        public List<T> GetRandomMembersItemFromSet<T>(string setId, long count) where T : class
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(setId);
            }
            return ConvetList<T>(db.SetRandomMembersAsync(PrefixedKey(setId), count).Result);
        }
        /// <summary>
        /// 根据SetId，获取内置的HashSet<T>的项数
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The cardinality (number of elements) of the set, or 0 if key does not exist.</returns>
        /// <remarks>https://redis.io/commands/scard</remarks>
        public long GetSetCount(string setId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(setId);
            }
            return db.SetLengthAsync(PrefixedKey(setId)).Result;
        }
        /// <summary>
        /// 判断指定SetId的HashSet<T>中是否包含指定的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setId"></param>
        /// <returns>1 if the element is a member of the set. 0 if the element is not a member of the set, or if key does not exist.</returns>
        /// <remarks>https://redis.io/commands/sismember</remarks>
        public bool SetContainsItem<T>(string setId, T item)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(setId);
            }
            return db.SetContainsAsync(PrefixedKey(setId), ToJson(item)).Result;
        }
        /// <summary>
        /// 根据Key设置一个值，仅仅当Key不存在时有效，如Key已存在则不修改(只支持字符串)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>True if the string was set, false otherwise.</returns>
        /// <remarks>https://redis.io/commands/set  当传入When.Always 调用此命令,此时覆盖时调用</remarks>
        /// <remarks>https://redis.io/commands/msetnx  当传入When.NotExists 调用此命令</remarks>
        public bool SetEntryIfNotExists(string key, string value)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.StringSetAsync(PrefixedKey(key), value, null, When.NotExists).Result;
        }
        #endregion

        #region SortedSet

        /// <summary>
        /// 添加一个项到内部的排序List<T>，其中重载方法多了个score：排序值。优先按照score从小->大排序，否则按值小到大排序。支持泛型
        /// 如果需要一次插入多条，请调用AddItemToSortedSet的批量插入方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setId"></param>
        /// <param name="value"></param>
        /// <returns>True if the value was added, False if it already existed (the score is still updated)</returns>
        /// <remarks>https://redis.io/commands/zadd</remarks>
        public void AddItemToSortedSet<T>(string setId, T value, double score)
        {
            if (!RedisCon.IsConnected)
            {
                return;
            }
            db.SortedSetAddAsync(PrefixedKey(setId), ToJson(value), score);
        }
        /// <summary>
        ///批量插入数据
        /// </summary>
        /// <returns>The number of elements added to the sorted sets, not including elements already existing for which the score was updated.</returns>
        /// <remarks>https://redis.io/commands/zadd</remarks>
        public void AddItemToSortedSet<T>(string setId, Dictionary<T, double> dic)
        {
            if (!RedisCon.IsConnected)
            {
                return;
            }
            db.SortedSetAddAsync(PrefixedKey(setId), ConvertSortedSetEntry(dic));
        }
        /// <summary>
        /// 获取已排序集合的项的数目
        /// </summary>
        /// <param name="setId"></param>
        /// <returns>The cardinality (number of elements) of the sorted set, or 0 if key does not exist.</returns>
        /// <remarks>https://redis.io/commands/zcard</remarks>
        public long GetSortedSetCount(string setId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(setId);
            }
            return db.SortedSetLengthAsync(PrefixedKey(setId)).Result;
        }
        /// <summary>
        /// 获取已排序集合的项的数目，支持下标以及score筛选
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="min">最低分</param>
        /// <param name="max">最高分</param>
        /// <returns>The cardinality (number of elements) of the sorted set, or 0 if key does not exist.</returns>
        /// <remarks>https://redis.io/commands/zcard</remarks>
        /// <remarks>https://redis.io/commands/zcount  当min=-Infinity &&max= +Infinity 调用zcount <remarks>
        public long GetSortedSetCount(string setId, double min, double max)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(setId);
            }
            return db.SortedSetLengthAsync(PrefixedKey(setId), min, max).Result;
        }
        /// <summary>
        /// 通过索引区间返回有序集合成指定区间内的成员
        /// </summary>
        /// <param name="setId"></param>
        /// <returns>List of elements in the specified range.</returns>
        /// <remarks>https://redis.io/commands/zrange    Order order = Order.Ascending</remarks>
        public List<string> GetAllItemsFromSortedSet(string setId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<string>>(setId);
            }
            return ConvetList(db.SortedSetRangeByRankAsync(PrefixedKey(setId)).Result);
        }
        /// <summary>
        ///  获取指定ListId的内部已排序List<T>的所有值，支持泛型
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>List of elements in the specified range.</returns>
        /// <remarks>https://redis.io/commands/zrange    Order order = Order.Ascending</remarks>
        public List<T> GetAllItemsFromSortedSet<T>(string setId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(setId);
            }
            return ConvetList<T>(db.SortedSetRangeByRankAsync(PrefixedKey(setId)).Result);
        }
        /// <summary>
        /// 获取指定ListId的内部已排序List<string>的所有值，倒序
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>List of elements in the specified range.</returns>
        /// <remarks>https://redis.io/commands/zrange    Order order = Order.Ascending</remarks>
        /// <remarks>https://redis.io/commands/zrevrange   Order order = Order.Descending</remarks>
        public List<string> GetAllItemsFromSortedSetDesc(string setId, long start = 0, long stop = -1, Order order = Order.Descending)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<string>>(setId);
            }
            return ConvetList(db.SortedSetRangeByRankAsync(PrefixedKey(setId), start, stop, order).Result);
        }
        /// <summary>
        ///  获取指定ListId的内部已排序List<T>的所有值，倒序，支持泛型
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>List of elements in the specified range.</returns>
        /// <remarks>https://redis.io/commands/zrange    Order order = Order.Ascending</remarks>
        /// <remarks>https://redis.io/commands/zrevrange   Order order = Order.Descending</remarks>
        public List<T> GetAllItemsFromSortedSetDesc<T>(string setId, long start = 0, long stop = -1, Order order = Order.Descending)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(setId);
            }
            return ConvetList<T>(db.SortedSetRangeByRankAsync(PrefixedKey(setId), start, stop, order).Result);
        }
        /// <summary>
        /// 获取指定SetId的内部List<T>中指定下标范围的数据
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="startingFrom"></param>
        /// <param name="count"></param>
        /// <returns>List of elements in the specified range.</returns>
        /// <remarks>https://redis.io/commands/zrange    Order order = Order.Ascending</remarks>
        /// <remarks>https://redis.io/commands/zrevrange   Order order = Order.Descending</remarks>
        public List<T> GetRangeFromSortedSet<T>(string setId, long startingFrom, long count, Order order = Order.Ascending)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(setId);
            }
            return ConvetList<T>(db.SortedSetRangeByRankAsync(PrefixedKey(setId), startingFrom, startingFrom + count - 1).Result);
        }
        /// <summary>
        /// 获取指定SetId的内部List<T>中指定下标范围的数据，倒序
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="startingFrom"></param>
        /// <param name="count"></param>
        /// <returns>List of elements in the specified range.</returns>
        /// <remarks>https://redis.io/commands/zrevrange   Order order = Order.Descending</remarks>
        public List<T> GetRangeFromSortedSetDesc<T>(string setId, long startingFrom, long count)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(setId);
            }
            return ConvetList<T>(db.SortedSetRangeByRankAsync(PrefixedKey(setId), startingFrom, startingFrom + count - 1, Order.Descending).Result);
        }
        /// <summary>
        /// 根据List和值，获取内置的排序后的List<string>的下标 
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="value"></param>
        /// <returns>如果没有找到该项，返回-1</returns>
        /// <remarks>https://redis.io/commands/zrank   当 Order order = Order.Ascending 调用zrank 命令</remarks>
        /// <remarks>https://redis.io/commands/zrevrank  当 Order order = Order.Ascending 调用zrank 命令</remarks>
        public long GetItemIndexInSortedSet(string setId, string value, Order order = Order.Descending)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(setId);
            }
            long? index = db.SortedSetRankAsync(PrefixedKey(setId), value, order).Result;
            return index.HasValue ? index.Value : -1;
        }
        /// <summary>
        /// 根据List和值，获取内置的排序后的List<string>的下标  倒序
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="value"></param>
        /// <returns>如果没有找到该项，返回-1</returns>
        /// <remarks>https://redis.io/commands/zrevrank  当 Order order = Order.Ascending 调用zrank 命令</remarks>
        public long GetItemIndexInSortedSetDesc(string setId, string value)
        {
            return GetItemIndexInSortedSet(setId, value, Order.Descending);
        }
        /// <summary>
        /// 获取指定SetId的内部List<string>中按照score由高->低排序后的分值范围的数据
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="startingFrom"></param>
        /// <param name="count"></param>
        /// <returns>List of elements in the specified score range.</returns>
        /// <remarks>https://redis.io/commands/zrevrangebyscore  When order= Order.Descending</remarks>
        public List<T> GetRangeFromSortedSetByHighestScore<T>(string setId, double start, double stop)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(setId);
            }
            return ConvetList<T>(db.SortedSetRangeByScoreAsync(PrefixedKey(setId), start, stop, Exclude.None, Order.Descending).Result);
        }
        /// <summary>
        /// 获取指定SetId的内部List<string>中按照score由高->低排序后的分值范围的数据
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="startingFrom"></param>
        /// <param name="count"></param>
        /// <returns>List of elements in the specified score range.</returns>
        /// <remarks>https://redis.io/commands/zrangebyscore</remarks>
        /// <remarks>https://redis.io/commands/zrevrangebyscore</remarks>
        public List<T> GetRangeFromSortedSetByScore<T>(string setId, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(setId);
            }
            return ConvetList<T>(db.SortedSetRangeByScoreAsync(PrefixedKey(setId), start, stop, exclude, order, skip, take).Result);
        }
        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="fromScore"></param>
        /// <param name="toScore"></param>
        /// <returns>The number of elements removed.</returns>
        /// <remarks>https://redis.io/commands/zremrangebyscore</remarks>
        public long RemoveRangeFromSortedSetByScore(string setId, double fromScore, double toScore)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(setId);
            }
            return db.SortedSetRemoveRangeByScoreAsync(PrefixedKey(setId), fromScore, toScore).Result;
        }

        /// <summary>
        /// 根据传入的ListId和值获取内置List<string>项的score
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="value"></param>
        /// <returns>The score of the member.</returns>
        /// <remarks>https://redis.io/commands/zscore</remarks>
        public double GetItemScoreInSortedSet(string setId, string value)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<double>(setId);
            }
            double? score = db.SortedSetScoreAsync(PrefixedKey(setId), value).Result;
            return score.HasValue ? score.Value : 0D;
        }
        /// <summary>
        ///判断SortedSet是否包含一个键
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        ///<remarks>https://redis.io/commands/zrank   当 Order order = Order.Ascending 调用zrank 命令</remarks>
        public bool SortedSetContainsItem(string setId, string item)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(setId);
            }
            long? index = db.SortedSetRankAsync(PrefixedKey(setId), item).Result;
            return index.HasValue ? true : false;
        }
        /// <summary>
        /// 判断SortedSet是否包含一个键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setId"></param>
        /// <returns></returns>
        ///<remarks>https://redis.io/commands/zrank   当 Order order = Order.Ascending 调用zrank 命令</remarks>
        public bool SortedSetContainsItem<T>(string setId, T item)
        {
            return SortedSetContainsItem(setId, ToJson(item));
        }
        /// <summary>
        /// 从指定SetId的内部List<T>中移除与第二个参数值相等的那一项
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        /// <param name="noOfMatches"></param>
        /// <returns>True if the member existed in the sorted set and was removed; False otherwise.</returns>
        /// <remarks>https://redis.io/commands/zrem</remarks>
        public bool RemoveItemFromSortedSet(string setId, string value)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(setId);
            }
            return db.SortedSetRemoveAsync(PrefixedKey(setId), value).Result;
        }
        /// <summary>
        ///从指定SetId的内部List<T>中移除与第二个参数值相等的那一项
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        /// <returns>True if the member existed in the sorted set and was removed; False otherwise.</returns>
        /// <remarks>https://redis.io/commands/zrem</remarks>
        public bool RemoveItemFromSortedSet<T>(string setId, T value)
        {
            return RemoveItemFromSortedSet(setId, ToJson(value));
        }
        /// <summary>
        /// 升序队列，弹出第一个，并删除 限制引用类型，值类型，须调用PopValueWithLowestScoreFromSortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setId"></param>
        /// <returns>键不存在时候，弹出NULL</returns>
        /// <remarks>https://redis.io/commands/zrange    Order order = Order.Ascending</remarks>
        /// <remarks>https://redis.io/commands/zrem</remarks>
        public T PopItemWithLowestScoreFromSortedSet<T>(string setId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<T>(setId);
            }
            RedisValue[] rs = db.SortedSetRangeByRankAsync(PrefixedKey(setId), 0, 1).Result;
            if (rs.Length == 0)
            {
                return default(T);
            }
            db.SortedSetRemove(PrefixedKey(setId), rs[0]);
            return ToObj<T>(rs[0]);
        }
        /// <remarks>https://redis.io/commands/zrange    Order order = Order.Ascending</remarks>
        /// <remarks>https://redis.io/commands/zrem</remarks>
        public T PopItemWithHighestScoreFromSortedSet<T>(string setId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<T>(setId);
            }
            RedisValue[] rs = db.SortedSetRangeByRankAsync(PrefixedKey(setId), 0, 1, Order.Descending).Result;
            if (rs.Length == 0)
            {
                return default(T);
            }
            db.SortedSetRemoveAsync(PrefixedKey(setId), rs[0]);
            return ToObj<T>(rs[0]);
        }
        #endregion

        #region HashSet
        /// <summary>
        /// 设置一个键值对入Hash表，如果哈希表的key存在则覆盖
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="hashId">哈希键名</param>
        /// <param name="value">哈希键值</param>
        /// <returns>1表示新的Field被设置了新值，0表示Field已经存在，用新值覆盖原有值。 </returns>
        /// <remarks>https://redis.io/commands/hset  当When参数传递不等于When.Always 时调用hset 底层命令</remarks>
        public bool SetEntryInHash(string key, string hashId, string value)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            var task = db.HashSetAsync(PrefixedKey(key), hashId, value);
            return task.Result;
        }
        /// <summary>
        /// 设置一个键值对入Hash表，如果哈希表的key存在则覆盖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键名</param>
        /// <param name="hashId">哈希键名</param>
        /// <param name="value">哈希键值</param>
        /// <returns>True表示新的Field被设置了新值，False表示Field已经存在，用新值覆盖原有值。 </returns>
        /// <remarks>https://redis.io/commands/hset  当When参数传递不等于When.Always 时调用hset 底层命令</remarks>
        public bool SetEntryInHash<T>(string key, string hashId, T value)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            var task = db.HashSetAsync(PrefixedKey(key), hashId, ToJson(value));
            return task.Result;
        }
        /// <remarks>https://redis.io/commands/hmset</remarks>
        public void SetRangeInHash<T>(string key, Dictionary<string, T> keyValuePairs)
        {
            if (!RedisCon.IsConnected)
            {
                return;
            }
            db.HashSetAsync(PrefixedKey(key), ConvertHashEntry(keyValuePairs));
        }
        /// <summary>
        /// 当哈希表的key未被使用时，设置一个键值对入Hash表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <param name="value"></param>
        /// <returns>1表示新的Field被设置了新值，0表示Key或Field已经存在，该命令没有进行任何操作。 </returns>
        /// <remarks>https://redis.io/commands/hsetnx 当When参数传递不等于When.Always 时调用hsetnx 底层命令</remarks>
        public bool SetEntryInHashIfNotExists(string key, string hashId, string value)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.HashSetAsync(PrefixedKey(key), hashId, value, When.NotExists).Result;
        }
        /// <summary>
        /// 当哈希表的key未被使用时，设置一个键值对如Hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <param name="value"></param>
        /// <returns>1表示新的Field被设置了新值，0表示Key或Field已经存在，该命令没有进行任何操作。 </returns>
        /// <remarks>https://redis.io/commands/hset  当When参数传递不等于When.Always 时调用hset 底层命令</remarks>
        /// <remarks>https://redis.io/commands/hsetnx 当When参数传递不等于When.Always 时调用hsetnx 底层命令</remarks>
        public bool SetEntryInHashIfNotExists<T>(string key, string hashId, T value)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.HashSetAsync(PrefixedKey(key), hashId, ToJson(value), When.NotExists).Result;
        }
        /// <summary>
        /// 根据key获取该Hash下的所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns>如果键不存在或者获取到的List长度为0，返回null</returns>
        /// <remarks>List of values in the hash, or an empty list when key does not exist.</remarks>
        /// <remarks>https://redis.io/commands/hvals</remarks>
        public List<string> GetHashValues(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<string>>(key);
            }
            RedisValue[] rs = db.HashValuesAsync(PrefixedKey(key)).Result;
            return ConvetList<string>(rs);
        }
        /// <summary>
        /// 根据HashId获取该HashId下的所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>如果键不存在或者获取到的List长度为0，返回null</returns>
        /// <remarks>List of values in the hash, or an empty list when key does not exist.</remarks>
        /// <remarks>https://redis.io/commands/hvals</remarks>
        public List<T> GetHashValues<T>(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(key);
            }
            RedisValue[] rs = db.HashValuesAsync(PrefixedKey(key)).Result;
            return ConvetList<T>(rs);
        }
        /// <summary>
        /// 根据HashId和Hash表的Key获取多个值(支持多个key)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <returns>如果键不存在或者获取到的List长度为0，返回null</returns>
        /// <remarks>https://redis.io/commands/hget</remarks>
        /// <remarks>https://redis.io/commands/hmget  hashIds 有值时</remarks>
        public List<string> GetValuesFromHash(string key, params string[] hashIds)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<string>>(key);
            }
            return ConvetList<string>(db.HashGetAsync(PrefixedKey(key), ConvertRedisValue(hashIds)).Result);
        }
        /// <summary>
        /// 根据HashId和Hash表的Key获取多个值(支持多个key)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <returns>如果键不存在或者获取到的List长度为0，返回null</returns>
        /// <remarks>https://redis.io/commands/hget</remarks>
        /// <remarks>https://redis.io/commands/hmget  hashIds 有值时</remarks>
        public List<T> GetValuesFromHash<T>(string key, params string[] hashIds)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<T>>(key);
            }
            return ConvetList<T>(db.HashGetAsync(PrefixedKey(key), ConvertRedisValue(hashIds)).Result);
        }
        /// <summary>
        /// 获取键下某个HashID的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <returns>The value associated with field, or nil when field is not present in the hash or key does not exist.</returns>
        /// <remarks>https://redis.io/commands/hget</remarks>
        public string GetValueFromHash(string key, string hashId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<string>(key);
            }
            var task = db.HashGetAsync(PrefixedKey(key), hashId);
            return task.Result;
        }
        /// <summary>
        ///  获取键下某个HashID的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <returns>当字段不存在或者 key 不存在时返回NULL</returns>
        /// <remarks>https://redis.io/commands/hget</remarks>
        public T GetValueFromHash<T>(string key, string hashId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<T>(key);
            }
            var task = db.HashGetAsync(PrefixedKey(key), hashId);
            return ToObj<T>(task.Result);
        }
        /// <summary>
        /// 获取键指定下的所有hash Key
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns>如果键不存在或者获取到的List长度为0，返回null</returns>
        /// <remarks>https://redis.io/commands/hkeys</remarks>
        public List<string> GetHashKeys(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<List<string>>(key);
            }
            var task = db.HashKeysAsync(PrefixedKey(key));
            return ConvetList(task.Result);
        }
        /// <summary>
        /// 获取指定键名下的所有Key数量
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>The number of fields in the hash, or 0 when key does not exist.</returns>
        /// <remarks>https://redis.io/commands/hlen</remarks>
        public long GetHashCount(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(key);
            }
            var task = db.HashLengthAsync(PrefixedKey(key));
            return task.Result;
        }
        /// <summary>
        /// 判断指定键的哈希表中是否包含指定的hashId
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <returns>1 if the hash contains field. 0 if the hash does not contain field, or key does not exist.</returns>
        /// <remarks>https://redis.io/commands/hexists</remarks>
        public bool HashContainsEntry(string key, string hashId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            var task = db.HashExistsAsync(PrefixedKey(key), hashId);
            return task.Result;
        }
        /// <summary>
        /// 根据指定键获取所有的hash键值对
        /// </summary>
        /// <param name="key"></param>
        /// <returns>如果键不存在或者获取到的Dictionary长度为0，返回null</returns>
        /// <remarks>https://redis.io/commands/hgetall</remarks>
        public Dictionary<string, string> GetAllEntriesFromHash(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<Dictionary<string, string>>(key);
            }
            return ConvertDictionary<string>(db.HashGetAllAsync(PrefixedKey(key)).Result);
        }
        /// <summary>
        /// 根据指定键获取所有的hash键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>如果键不存在或者获取到的Dictionary长度为0，返回null</returns>
        /// <remarks>https://redis.io/commands/hgetall</remarks>
        public Dictionary<string, T> GetAllEntriesFromHash<T>(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<Dictionary<string, T>>(key);
            }
            return ConvertDictionary<T>(db.HashGetAllAsync(PrefixedKey(key)).Result);
        }
        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns>The number of fields that were removed.</returns>
        /// <remarks>https://redis.io/commands/hdel</remarks>
        public bool RemoveEntryFromHash(string key, string hashId)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.HashDeleteAsync(PrefixedKey(key), hashId).Result;
        }
        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns>The number of fields that were removed.</returns>
        /// <remarks>https://redis.io/commands/hdel</remarks>
        public long RemoveEntryFromHash(string key, params string[] hashIds)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(key);
            }
            return db.HashDeleteAsync(PrefixedKey(key), ConvertRedisValue(hashIds)).Result;
        }
        /// <summary>
        /// 将指定HashId的哈希表中的值加上指定值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <param name="incrementBy"></param>
        /// <returns>The value at field after the increment operation.</returns>
        /// <remarks>The range of values supported by HINCRBY is limited to 64 bit signed integers.</remarks>
        /// <remarks>https://redis.io/commands/hincrby</remarks>
        public long IncrementValueInHash(string key, string hashId, int incrementBy)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(key);
            }
            return db.HashIncrementAsync(PrefixedKey(key), hashId, incrementBy).Result;
        }


        #endregion
        #region 地理信息GEO
        /// <summary>
        /// 添加一个地理坐标,存在修改，不存在添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">当前坐标成员描述</param>
        /// <param name="lng">经度</param>
        /// <param name="lat">维度</param>
        /// <returns>True if the specified member was not already present in the set, else False.</returns>
        /// <remarks>https://redis.io/commands/geoadd</remarks>
        public bool AddGeoMember(string key, string member, double lng, double lat)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.GeoAddAsync(PrefixedKey(key), lng, lat, member).Result;
        }
        /// <summary>
        /// 删除一个坐标
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns>True if the member existed in the sorted set and was removed; False otherwise.</returns>
        /// <remarks>https://redis.io/commands/zrem</remarks>
        public bool RemoveGeoMember(string key, string member)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<bool>(key);
            }
            return db.GeoRemoveAsync(PrefixedKey(key), member).Result;
        }
        /// <summary>
        /// 搜索区域内的member 默认不排序
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <param name="radius">搜索范围</param>
        /// <param name="unit">范围单位默认米</param>
        /// <returns>The results found within the radius, if any.</returns>
        /// <remarks>https://redis.io/commands/georadius</remarks>
        public GeoRadiusResult[] FindGeoMembersInRadius(string key, double lng, double lat, double radius)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<GeoRadiusResult[]>(key);
            }
            return db.GeoRadiusAsync(PrefixedKey(key), lng, lat, radius, GeoUnit.Meters).Result;
        }
        /// <summary>
        /// 搜索区域内的member带距离
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <param name="radius">搜索范围</param>
        /// <param name="unit">范围单位默认米</param>
        /// <param name="count">count选项去获取前 N 个匹配元素</param>
        /// <param name="order">排序方式</param>
        /// <returns>The results found within the radius, if any.</returns>
        /// <remarks>https://redis.io/commands/georadius</remarks>
        public GeoRadiusResult[] FindGeoMembersInRadius(string key, double lng, double lat, double radius, GeoUnit unit = GeoUnit.Meters, int count = -1, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<GeoRadiusResult[]>(key);
            }
            return db.GeoRadiusAsync(PrefixedKey(key), lng, lat, radius, unit, count, order, options).Result;
        }
        /// <summary>
        /// 获取2个member间的距离
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="fromMember">元素1</param>
        /// <param name="toMember">元素2</param>
        /// <param name="unit">单位，默认米</param>
        /// <returns>计算出的距离会以双精度浮点数的形式被返回。 如果给定的位置元素不存在， 那么命令返回空值</returns>
        /// <remarks>https://redis.io/commands/geodist</remarks>
        public double? GetGeoMemberDistance(string key, string fromMember, string toMember, GeoUnit unit = GeoUnit.Meters)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<double>(key);
            }
            return db.GeoDistanceAsync(PrefixedKey(key), fromMember, toMember, unit).Result;
        }
        /// <summary>
        /// 获取member的位置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns>The command returns an array where each element is a two elements array representing longitude and latitude (x,y) of each member name passed as argument to the command.Non existing elements are reported as NULL elements of the array.</returns>
        /// <remarks>https://redis.io/commands/geopos</remarks>
        public GeoPosition?[] GetGeoCoordinates(string key, string[] members)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<GeoPosition?[]>(key);
            }
            return db.GeoPositionAsync(PrefixedKey(key), ConvertRedisValue(members)).Result;
        }
        /// <summary>
        /// 获取member的位置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns>The command returns an array where each element is a two elements array representing longitude and latitude (x,y) of each member name passed as argument to the command.Non existing elements are reported as NULL elements of the array.</returns>
        /// <remarks>https://redis.io/commands/geopos</remarks>
        public GeoPosition?[] GetGeoCoordinates<T>(string key, List<T> list)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<GeoPosition?[]>(key);
            }
            return db.GeoPositionAsync(PrefixedKey(key), ConvertRedisValue(list)).Result;
        }
        /// <summary>
        /// 获取member的位置Geohash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns>The command returns an array where each element is the Geohash corresponding to each member name passed as argument to the command.</returns>
        /// <remarks>https://redis.io/commands/geohash</remarks>
        public string GetGeohashes(string key, string members)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<string>(key);
            }
            return db.GeoHashAsync(PrefixedKey(key), members).Result;
        }
        /// <summary>
        /// 获取多个member的位置Geohash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns>The command returns an array where each element is the Geohash corresponding to each member name passed as argument to the command.</returns>
        /// <remarks>https://redis.io/commands/geohash</remarks>
        public string[] GetGeohashes<T>(string key, List<T> list)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<string[]>(key);
            }
            return db.GeoHashAsync(PrefixedKey(key), ConvertRedisValue(list)).Result;
        }
        #endregion

        /// <summary>
        /// 根据Key获取当前存储的值是什么类型（None = 0,String = 1,List = 2,Set = 3,SortedSet = 4,Hash = 5,）
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Type of key, or none when key does not exist.</returns>
        /// <remarks>https://redis.io/commands/type</remarks>
        public RedisType GetEntryType(string key)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<RedisType>(key);
            }
            return db.KeyTypeAsync(PrefixedKey(key)).Result;
        }
        /// <summary>
        /// 计数方法
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="ExpireEntryIn">有效期（单位：秒）</param>
        /// <returns></returns>
        public long GetRepeatNum(string key, int ExpireEntryIn)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<long>(key);
            }
            long num = IncrementValue(key);
            if (num == 1 || GetTimeToLive(key).Seconds < 0)
            {
                ExpireEntryAt(key, new TimeSpan(0, 0, ExpireEntryIn));
            }
            return num;
        }
        /// <summary>
        /// 根据Key获取剩余的时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hasPrefix">true:表示已经有PTV1_前缀</param>
        /// <returns>null表示永不过期或者已经过期</returns>
        /// <remarks>https://redis.io/commands/ttl</remarks>
        /// <remarks>返回key剩余的过期时间。 这种反射能力允许Redis客户端检查指定key在数据集里面剩余的有效期。
        /// 在Redis 2.6和之前版本，如果key不存在或者已过期时返回-1。
        /// 从Redis2.8开始，错误返回值的结果有如下改变：如果key不存在或者已过期，返回 -2 如果key存在并且没有设置过期时间（永久有效），返回 -1 。</remarks>
        public TimeSpan? KeyTimeToLive(string key, bool hasPrefix = true)
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<TimeSpan>(key);
            }
            if (!hasPrefix)
            {
                key = PrefixedKey(key);
            }
            return db.KeyTimeToLiveAsync(key).Result;
        }
        /// <summary>
        /// 获取Redis缓存所有的Key
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllKeys()
        {
            if (!RedisCon.IsConnected)
            {
                return GetDefaultT<IEnumerable<string>>("");
            }
            var server = RedisCon.GetServer(configurationOptions.EndPoints[0]);
            return ConvetList(server.Keys());
        }

        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T GetDefaultT<T>(string CacheKey)
        {
            _logger.Information($"Redis IsConnected=False，返回默认值，操作的缓存Key：{CacheKey}");
            return default(T);
        }
    }
}
