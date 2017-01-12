using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LindCore.LindAspects.Attributes
{
    /// <summary>
    /// 表示用于Caching特性的缓存方式。
    /// </summary>
    public enum CachingMethod
    {
        /// <summary>
        /// 表示需要从缓存中获取对象。如果缓存中不存在所需的对象，系统则会调用实际的方法获取对象，
        /// 然后将获得的结果添加到缓存中。
        /// </summary>
        Get,
        /// <summary>
        /// 表示需要将对象存入缓存。此方式会调用实际方法以获取对象，然后将获得的结果添加到缓存中，
        /// 并直接返回方法的调用结果。
        /// </summary>
        Put,
        /// <summary>
        /// 表示需要将对象从缓存中移除。
        /// </summary>
        Remove
    }
    /// <summary>
    /// 缓存拦截器
    /// </summary>
    public class CachingAspectAttribute : BeforeAspectAttribute
    {
        string[] invalidKeyList;
        /// <summary>
        /// 缓存方式
        /// </summary>
        CachingMethod cachingMethod;
        /// <summary>
        /// 缓存键前缀
        /// </summary>
        string prefix = "Lind_Caching_";

        /// <summary>
        /// 初始化缓存拦截器
        /// </summary>
        /// <param name="cachingMethod"></param>
        public CachingAspectAttribute(CachingMethod cachingMethod, params string[] invalidKeyList)
        {
            this.cachingMethod = cachingMethod;
            this.invalidKeyList = invalidKeyList;
        }

        /// <summary>
        /// 指定方法对应的缓存失效
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodInfo"></param>
        void RemoveCache(MethodInfo methodInfo)
        {
            #region 缓存失效
            if (invalidKeyList != null && invalidKeyList.Any())
            {
                foreach (var item in invalidKeyList)
                {
                    //put,remove与get方法需要在一个类里
                    var removeKey = prefix + methodInfo.ReflectedType.FullName + "." + item;
                    RedisClient.RedisManager.Instance.GetDatabase().KeyDelete(removeKey);
                }
            }
            #endregion
        }

        /// <summary>
        /// 组装方法参数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        List<object> InitParams(InvokeContext context, MethodInfo methodInfo)
        {
            var method = context.Method;
            List<object> paramList = new List<object>();
            if (context.Parameters != null && context.Parameters.Any())
            {
                foreach (var item in context.Parameters)
                    paramList.Add(item.Para);
            }
            return paramList;
        }
        /// <summary>
        /// 有返回值的方法拦截动作
        /// </summary>
        /// <param name="context"></param>
        public override object FuncInvoke(InvokeContext context, MethodInfo methodInfo)
        {
            var paramList = InitParams(context, methodInfo);
            var obj = Activator.CreateInstance(methodInfo.ReflectedType);
            switch (cachingMethod)
            {
                case CachingMethod.Get:
                    #region 读缓存
                    //redis键名，在put和get时使用
                    var key = prefix + context.Method.MethodName;
                    //hashset键名,参数组合
                    var param = string.Join("_", context.Parameters.Select(i => i.Para));
                    if (!RedisClient.RedisManager.Instance.GetDatabase().KeyExists(key))
                    {
                        var objValue = methodInfo.Invoke(obj, paramList.ToArray());
                        RedisClient.RedisManager.Instance.GetDatabase().HashSet(key, param, Lind.DDD.Utils.SerializeMemoryHelper.SerializeToJson(objValue));
                        return objValue;
                    }
                    var entity = RedisClient.RedisManager.Instance.GetDatabase().HashGet(key, param);
                    return Lind.DDD.Utils.SerializeMemoryHelper.DeserializeFromJson<object>(entity.ToString());
                #endregion
                case CachingMethod.Remove:
                case CachingMethod.Put:
                    #region 缓存失效
                    var putvalue = methodInfo.Invoke(obj, paramList.ToArray());
                    RemoveCache(methodInfo);
                    return putvalue;
                #endregion
                default:
                    throw new InvalidOperationException("无效的缓存方式。");
            }
        }

        /// <summary>
        /// 无返回值的方法拦截
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodInfo"></param>
        public override void ActionInvoke(InvokeContext context, MethodInfo methodInfo)
        {
            var obj = Activator.CreateInstance(methodInfo.ReflectedType);
            var paramList = InitParams(context, methodInfo);
            //执行目前方法，目前方法出现异常，缓存不清除
            methodInfo.Invoke(obj, paramList.ToArray());
            //清除缓存
            RemoveCache(methodInfo);
        }
    }
}
