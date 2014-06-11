﻿using Campus.DocumentValidator;
using FISCA;
using KH_HighConcern.DAO;
using KH_HighConcern.ImportExport.ValidationRule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FISCA.Permission;
using FISCA.Presentation;

namespace KH_HighConcern
{
    public class Program
    {
           static BackgroundWorker _bgLLoadUDT = new BackgroundWorker();
           [MainMethod()]
           public static void Main()
           {
               _bgLLoadUDT.DoWork+=_bgLLoadUDT_DoWork;
               _bgLLoadUDT.RunWorkerCompleted += _bgLLoadUDT_RunWorkerCompleted;
               _bgLLoadUDT.RunWorkerAsync();
           
           }

           static void _bgLLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
           {

               // 匯入高關懷特殊身份
               Catalog catalog01 = RoleAclSource.Instance["學生"]["功能按鈕"];
               catalog01.Add(new RibbonFeature("KH_HighConcern_ImportHighConcern", "匯入高關懷特殊身份"));

               RibbonBarItem item01 = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"];
               item01["匯入"]["其它相關匯入"]["匯入高關懷特殊身份"].Enable = UserAcl.Current["KH_HighConcern_ImportHighConcern"].Executable;
               item01["匯入"]["其它相關匯入"]["匯入高關懷特殊身份"].Click += delegate {
                   new ImportExport.ImportHighConcern().Execute();
               };
           }

           static void _bgLLoadUDT_DoWork(object sender, DoWorkEventArgs e)
           {
               UDTTransfer.CreateUDTTable();

               #region 自訂驗證規則
               FactoryProvider.FieldFactory.Add(new FieldValidatorFactory());
               #endregion
           }
    }
}