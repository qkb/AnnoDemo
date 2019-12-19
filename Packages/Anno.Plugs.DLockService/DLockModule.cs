﻿using System;
using System.Threading;
using Anno.EngineData;

namespace Anno.Plugs.DLockService
{
    /// <summary>
    /// 分布式锁服务
    /// </summary>
    public class DLockModule : BaseModule
    {
        public ActionResult EnterLock()
        {
            var dlKey = RequestString("DLKey");
            var timeOut = RequestInt32("TimeOut")??5000;
            var owner = RequestString("Owner");
            var locker=new  LockInfo()
            {
                Key = dlKey,
                Time = timeOut,
                Owner=owner,
                EnterTime=DateTime.Now,
                Type=ProcessType.Enter
            };
            var rlt = DLockCenter.Enter(locker);
            return rlt;
        }

        public ActionResult DisposeLock()
        {
            var dlKey = RequestString("DLKey");
            var owner = RequestString("Owner");
            var locker = new LockInfo();
            locker.Key = dlKey;
            locker.Owner = owner;
            DLockCenter.Free(locker);
            return new ActionResult(true, null, null, "DisposeLock Message");
        }
    }
}
