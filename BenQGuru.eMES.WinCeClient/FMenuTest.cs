using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace BenQGuru.eMES.WinCeClient
{

    public partial class FMenuTest : Form
    {
        Assembly assembly;
        public FMenuTest()
        {
            InitializeComponent();

            FLogin formLogin = new FLogin();
            //if (formLogin.ShowDialog() == DialogResult.Yes)
            formLogin.ShowDialog();
            if (formLogin.Authenticated)
            {
                //测试菜单
                #region 
                //daniel 2015/3/5 Test项主菜单
                //MenuItem testMainItem = new MenuItem();
                //testMainItem.Text = "模具出入库管理";
                //mainMenu.MenuItems.Add(testMainItem);

                //NewMenuItem menuItemInStore = new NewMenuItem();
                //menuItemInStore.Text = "模具入库管理";
                //menuItemInStore.MenuInfo = "BenQGuru.eMES.WinCeClient.FMoldInStore";
                //menuItemInStore.Click += new EventHandler(menuItem_Click);
                //testMainItem.MenuItems.Add(menuItemInStore);

                //NewMenuItem menuItemOutStore = new NewMenuItem();
                //menuItemOutStore.Text = "模具出库管理";
                //menuItemOutStore.MenuInfo = "BenQGuru.eMES.WinCeClient.FMoldOutStore";
                //menuItemOutStore.Click += new EventHandler(menuItem_Click);
                //testMainItem.MenuItems.Add(menuItemOutStore);
                //NewMenuItem menuItemOutDetail = new NewMenuItem();

                //NewMenuItem menuItemInDetail = new NewMenuItem();
                //menuItemInDetail.Text = "模具入库明细";
                //menuItemInDetail.MenuInfo = "BenQGuru.eMES.WinCeClient.FMoldInStoreDetail";
                //menuItemInDetail.Click += new EventHandler(menuItem_Click);
                //testMainItem.MenuItems.Add(menuItemInDetail);

                //menuItemOutDetail.Text = "模具出库明细";

                //menuItemOutDetail.MenuInfo = "BenQGuru.eMES.WinCeClient.FMoldOutStoreDetail";
                //menuItemOutDetail.Click += new EventHandler(menuItem_Click);
                //testMainItem.MenuItems.Add(menuItemOutDetail);
                #endregion

                //daniel 2015/3/17 获取WINCEPDA菜单，目前是两级菜单
                try
                {
                    DataTable dtMenu = formLogin.WebServiceFacade.GetAllMenuWithUrl(ApplicationService.Current().UserCode);
                    if (dtMenu != null && dtMenu.Rows.Count > 0)
                    {
                        DataRow[] pRows = dtMenu.Select(" PMENUCODE='WINCEPDA'");
                        if (pRows != null && pRows.Length > 0)
                        {
                            foreach (DataRow pItem in pRows)
                            {
                                MenuItem pMainItem = new MenuItem();
                                pMainItem.Text = pItem["MENUDESC"].ToString();
                                mainMenu.MenuItems.Add(pMainItem);
                                string menucode = pItem["MENUCODE"].ToString();
                                DataRow[] subRows = dtMenu.Select(string.Format(" PMENUCODE='{0}'", menucode));
                                if (subRows != null && subRows.Length > 0)
                                {
                                    foreach (DataRow item in subRows)
                                    {
                                        NewMenuItem menuItem = new NewMenuItem();
                                        menuItem.Text = item["MENUDESC"].ToString();
                                        menuItem.MenuInfo = item["FORMURL"].ToString();
                                        menuItem.Click += new EventHandler(menuItem_Click);
                                        pMainItem.MenuItems.Add(menuItem);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("菜单获取失败，请重新登录 "+ex.Message, Enums.WinCE_MsgBox_Title_Tips);
                    Application.Exit();
                }

                //#endif

            }
            else
            {
                Application.Exit();
            }
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            string typeName = (sender as NewMenuItem).MenuInfo;
            string titleText = (sender as NewMenuItem).Text;
            try
            {
                if (panel1.Controls.Count > 0)
                {
                    if (panel1.Controls[0].GetType().FullName == typeName)
                    {
                        return;
                    }
                }

                if (assembly == null)
                {
                    assembly = Assembly.Load("BenQGuru.eMES.WinCeClient");
                }
                object obj = assembly.CreateInstance(typeName);
                if (obj == null)
                {
                    MessageBox.Show("对象创建失败" + typeName);
                }


                //if (obj is Form)
                //{
                //    Form form = obj as Form;
                //    form.Dock = DockStyle.Fill;
                //    form.Show();
                //    panel1.Controls.Add(form);
                //}

                if (obj is UserControl)
                {
                    panel1.Controls.Clear();
                    UserControl uc = obj as UserControl;
                    uc.Dock = DockStyle.Fill;
                    uc.BackColor = Color.White;
                    panel1.Controls.Add(uc);
                    //daniel 2015/3/6 主页面显示标题信息
                    this.Text = titleText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void meuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FMenuTest_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }

    public class NewMenuItem : MenuItem
    {
        public string MenuInfo { get; set; }
    }
}