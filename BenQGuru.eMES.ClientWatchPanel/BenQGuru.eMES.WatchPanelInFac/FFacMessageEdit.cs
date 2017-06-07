using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class FFacMessageEdit : Form
    {
        public FFacMessageEdit()
        {
            InitializeComponent();
        }

        private void FFacMessageEdit_Load(object sender, EventArgs e)
        {
            this.richTextMessage.ForeColor = Color.Yellow;
            this.richTextMessage.Rtf = FacConfigMessage.CommonInfo;
        }

        private void tsbtTextColor_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorDialog1.Color = this.richTextMessage.ForeColor;
                if (colorDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    richTextMessage.SelectionColor = colorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtTextSize_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(richTextMessage.SelectionFont == null))
                {
                    this.fontDialog1.Font = richTextMessage.SelectionFont;
                }
                else
                {
                    fontDialog1.Font = null;
                }
                fontDialog1.ShowApply = true;
                if (fontDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    richTextMessage.SelectionFont = fontDialog1.Font;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtTextWide_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(richTextMessage.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = richTextMessage.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = richTextMessage.SelectionFont.Style ^ FontStyle.Bold;

                    richTextMessage.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtTextItaclic_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(richTextMessage.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = richTextMessage.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = richTextMessage.SelectionFont.Style ^ FontStyle.Italic;

                    richTextMessage.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtTextUnderLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(richTextMessage.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = richTextMessage.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = richTextMessage.SelectionFont.Style ^ FontStyle.Underline;

                    richTextMessage.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtTextRollback_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextMessage.CanUndo)
                {
                    richTextMessage.Undo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtTextRedo_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextMessage.CanRedo)
                {
                    richTextMessage.Redo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtSave_Click(object sender, EventArgs e)
        {
            FacConfigMessage.CommonInfo = this.richTextMessage.Rtf;
            FacConfigMessage.CommonText = this.richTextMessage.Text;


        }

        private void tsbtCut_Click(object sender, EventArgs e)
        {
            try
            {
                richTextMessage.Cut();
            }
            catch
            {
                MessageBox.Show("Unable to cut document content.", "RTE - Cut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbtCopy_Click(object sender, EventArgs e)
        {
            try
            {
                richTextMessage.Copy();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to copy document content.", "RTE - Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbtPaste_Click(object sender, EventArgs e)
        {
            try
            {
                richTextMessage.Paste();
            }
            catch
            {
                MessageBox.Show("Unable to copy clipboard content to document.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbtLeft_Click(object sender, EventArgs e)
        {
            richTextMessage.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void tsbtCenter_Click(object sender, EventArgs e)
        {
            richTextMessage.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void tsbtRight_Click(object sender, EventArgs e)
        {
            richTextMessage.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void tsbtSame_Click(object sender, EventArgs e)
        {
            try
            {
                richTextMessage.BulletIndent = 10;
                richTextMessage.SelectionBullet = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
    }
}