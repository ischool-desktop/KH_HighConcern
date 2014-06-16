﻿using FISCA.DSAUtil;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLock_KH.DAO
{
    public class UDTTransfer
    {
        /// <summary>
        /// 取的被鎖定班級名稱
        /// </summary>
        /// <returns></returns>
        public static List<string> GetClassLockNameList()
        {
            List<string> retVal = new List<string>();
            AccessHelper _AccessHelper = new AccessHelper();
            foreach (UDT_ClassLock data in _AccessHelper.Select<UDT_ClassLock>())
                retVal.Add(data.ClassName);
            return retVal;
        }

        /// <summary>
        /// 建立使用到的 UDT Table
        /// </summary>
        public static void CreateUDTTable()
        {
            FISCA.UDT.SchemaManager Manager = new SchemaManager(new DSConnection(FISCA.Authentication.DSAServices.DefaultDataSource));
            Manager.SyncSchema(new UDT_ClassLock());
        }
    }
}
