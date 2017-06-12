using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common;
using System.Threading;

namespace ShareLib
{
    public class MutualLock : IDisposable
    {
        private static Dictionary<string, bool> shared = new Dictionary<string, bool>();

        private bool beLocked = false;
        private string identitier = string.Empty;
        public bool getLock(string identitier)
        {

            if (!shared.ContainsKey(identitier))
            {
                lock (shared)
                {
                    if (!shared.ContainsKey(identitier))
                    {

                        shared.Add(identitier, true);
                        this.identitier = identitier;
                        beLocked = true;
                        Log.Error("线程：" + Thread.CurrentThread.ManagedThreadId + " 加锁！！！ 标识符：" + this.identitier);
                        return true;

                    }
                    else
                    {
                        Log.Error("线程：" + Thread.CurrentThread.ManagedThreadId + " 加锁失败！！！ 标识符：" + this.identitier);
                        return false;
                    }

                }
            }
            Log.Error("线程：" + Thread.CurrentThread.ManagedThreadId + " 加锁失败！！！ 标识符：" + this.identitier);
            return false;
        }



        #region IDisposable 成员

        public void Dispose()
        {
            if (beLocked)
            {
                lock (shared)
                {
                    Log.Error("线程：" + Thread.CurrentThread.ManagedThreadId + " 解锁！！！移除标识符：" + this.identitier);
                    shared.Remove(this.identitier);

                }
            }

        }

        #endregion
    }
}
