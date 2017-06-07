/***********************************************************************
 * Module:  IArrowInfo.cs
 * Author:  Administrator
 * Purpose: Definition of the Interface Flow.Data.IArrowInfo
 ***********************************************************************/

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using BenQGuru.eMES.DrawFlow.Controls;

namespace BenQGuru.eMES.DrawFlow.Data
{
   /// <summary>
   /// 流程信息接口
   /// </summary>
   public interface IArrowInfo
   {
      string ArrowName
      {
          get; set;
      }
      string ArrowID
      {
          get; set;
      }
      int ArrowStatus
      {
          get;
      }
      bool DoubleArrow
      {
          get;
      }
      string Condition
      {
          get; set;
      }
      PointCollection HotPoints
      {
          get;
      }
      FunctionButtonCollection FromProcesses
      {
          get;
      }
      FunctionButtonCollection ToProcesses
      {
          get;
      }
      RectangleCollection LinkArea
      {
          get;
      }
   
   }
}