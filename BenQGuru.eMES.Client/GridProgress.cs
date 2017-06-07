using System;
using System.Collections.Generic;
using System.Text;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.Drawing;

namespace BenQGuru.eMES.Client
{
    class GridProgress : IUIElementDrawFilter
    {
        #region IUIElementDrawFilter Members

        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            
            if (drawParams.Element is EmbeddableUIElementBase)
            {
                CellUIElement oCellUIElement = (CellUIElement)drawParams.Element.GetAncestor(typeof(CellUIElement));
                UltraGridCell oCell = oCellUIElement.Cell;

                if (oCell == null || oCell.Column.Key.ToLower() != "percent")
                    return false;

                Rectangle rect = new Rectangle();

                if (oCell.Value == null)
                    return false;

                int nCellWidth = drawParams.Element.RectInsideBorders.Width;

                int cellValue = Convert.ToInt32(oCell.Value);
                float percent = (float)(cellValue);
                percent = percent / (float)100;

                int nFillWidth = (int)(nCellWidth * percent);


                rect = new Rectangle(drawParams.Element.RectInsideBorders.X,
                                                 drawParams.Element.RectInsideBorders.Y,
                                                nFillWidth,
                                                 drawParams.Element.RectInsideBorders.Height);

                if (drawParams.DrawPhase == Infragistics.Win.DrawPhase.AfterDrawBackColor)
                {
                    Brush brush = new SolidBrush(Color.DarkOrange);
                    drawParams.Graphics.FillRectangle(brush, rect);

                    Font font = drawParams.Font;//new Font("Arial", 9);
                    string text = oCell.Value.ToString() + "%";
                    int nStringWidth = (int)drawParams.Graphics.MeasureString(text, font).Width;

                    int left = oCellUIElement.RectInsideBorders.X + oCellUIElement.RectInsideBorders.Width / 2;
                    left = left - nStringWidth / 2;
                    int top = oCellUIElement.RectInsideBorders.Y;

                    // If the text is going to be drawn over the flood filled area then make sure
                    // the color is different from the flood color otherwise it won't show up.
                    if (left < rect.Right)
                    {
                        brush.Dispose();
                        brush = new SolidBrush(Color.Black);
                    }
                    Brush brushFont = new SolidBrush(Color.Black);
                    drawParams.Graphics.DrawString(text, font, brushFont, left, top);

                    brush.Dispose();
                    return true;
                }
            }

            return false;
        }

        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            if (!(drawParams.Element is EmbeddableUIElementBase))
                return Infragistics.Win.DrawPhase.None;
            else
                return Infragistics.Win.DrawPhase.AfterDrawBackColor;
            
        }

        #endregion
    }
}
