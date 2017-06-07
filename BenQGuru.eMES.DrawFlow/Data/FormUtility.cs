using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BenQGuru.eMES.DrawFlow.Data
{
    public sealed class FormUtility
    {
        /// <summary>
        /// </summary>
        public static bool PointInRect(Rectangle rect, Point pt)
        {
            if (pt.X > rect.X && pt.Y > rect.Y
                && (pt.X < rect.X + rect.Width) && (pt.Y < rect.Y + rect.Height))
                return true;
            return false;
        }

        /// <summary>
        /// </summary>
        public static bool MessageBoxOK(string messageText)
        {
            if (MessageBox.Show(messageText, "友情提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                return true;
            return false;
        }
    }
}
