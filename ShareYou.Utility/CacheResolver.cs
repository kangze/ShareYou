using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.Client;
using ShareYou.Model.CustomeException;

namespace ShareYou.Utility
{
    public class CacheResolver
    {
        public static MemcachedClient mc;
        static CacheResolver()
        {
            string[] serverlist = { "127.0.0.1:11211" };
            //初始化池           
            SockIOPool sock = SockIOPool.GetInstance();
            sock.SetServers(serverlist);//添加服务器列表           
            sock.InitConnections = 3;//设置连接池初始数目           
            sock.MinConnections = 3;//设置最小连接数目           
            sock.MaxConnections = 5;//设置最大连接数目           
            sock.SocketConnectTimeout = 1000;//设置连接的套接字超时。           
            sock.SocketTimeout = 3000;//设置套接字超时读取           
            sock.MaintenanceSleep = 30;//设置维护线程运行的睡眠时间。如果设置为0，那么维护线程将不会启动;           
            //获取或设置池的故障标志。           
            //如果这个标志被设置为true则socket连接失败，           
            //将试图从另一台服务器返回一个套接字如果存在的话。           
            //如果设置为false，则得到一个套接字如果存在的话。否则返回NULL，如果它无法连接到请求的服务器。           
            sock.Failover = true;            //如果为false，对所有创建的套接字关闭Nagle的算法。           
            sock.Nagle = false;
            sock.Initialize();
            mc = new MemcachedClient();

        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void SetCache(string key, object obj)
        {
            bool exists = true;
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "缓存键为null");
            mc.Delete(key);
            if (mc.Add(key, obj) == false)
            {
                throw new CacheException("存入缓存失败");
            }
        }

        /// <summary>
        ///添加缓存，设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expire"></param>
        public static void SetCache(string key, object obj, DateTime expire)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "缓存键为null");
            //设置之前，先删除这个key
            mc.Delete(key);
            if (mc.Add(key, obj, expire) == false)
            {
                throw new CacheException("存入缓存失败");
            }
        }

        public static object GetCache(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "缓存键为null");
            object obj = mc.Get(key);
            if (null == obj)
                throw new NullReferenceException("缓存数据失效");
            return obj;
        }

        public static void DeleteCache(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "缓存键为null");
            //--如果不存在这个key，就会删除失败
            mc.Delete(key); //直接删除
        }
    }
}
