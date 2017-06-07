using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using BenQGuru.eMES.DrawFlow.Data;

namespace BenQGuru.eMES.DrawFlow.Controls
{
   /// <summary>
   /// </summary>
   public class DefineArrowButton : FlowButton
   {
      /// <summary>
      /// </summary>
      public DefineArrowButton(Point startPoint, Point endPoint)
      {
      	fStartPoint = startPoint;
      	fEndPoint = endPoint;
      	double length = Math.Sqrt(Math.Pow(fStartPoint.X - fEndPoint.X,2) + Math.Pow(fStartPoint.Y - fEndPoint.Y,2));
      	fRat = (length / ((DrawUtility.rw + DrawUtility.ah * 2) * DrawUtility.a));
      	DoubleArrow = true;
      }
   
      /// <summary>
      /// </summary>
      public override void DrawButton()
      {
      	double degree = Math.Atan((fEndPoint.Y - fStartPoint.Y) * 0.1/((fEndPoint.X - fStartPoint.X)* 0.1));
      	DrawUtility.DrawTwoArrow(this,fRat,degree,false);
      }
      
      /// <summary>
      /// </summary>
      protected override void CreateContextMenu()
      {
      }
   
      private Point fStartPoint;
      private Point fEndPoint;
      private double fRat = 1;
   
   }
}