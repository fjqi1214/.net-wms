using System;
using System.Data;
using System.Drawing;
using System.Collections;
using BenQGuru.eMES.DrawFlow.Controls;

namespace BenQGuru.eMES.DrawFlow.Data
{
   /// <summary>
   /// 进程信息接口
   /// </summary>
   public interface IProcessInfo
   {
      string ProcessName
      {
          get; set;
      }
      string ProcessID
      {
          get; set;
      }     
      int ProcessStatus
      {
          get;
      }
      Point CenterPoint
      {
          get;
      }
      PointCollection HotPoints
      {
          get;
      }
      FlowButtonCollection InFlows
      {
          get;
      }
      FlowButtonCollection OutFlows
      {
          get;
      }   
      //FlowButton LeftFlow
      //{
      //    get; set;
      //}
      //FlowButton RightFlow
      //{
      //    get; set;
      //}
      //FlowButton UpFlow
      //{
      //    get; set;
      //}
      //FlowButton DownFlow
      //{
      //    get; set;
      //}
      string WorkFlowID
      {
          get; set;
      }
   
   }
}