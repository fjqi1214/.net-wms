<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private void Page_Load(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.ContentType = "image/jpeg";
        Response.Clear();
        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(200, 200);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
        g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Red, 5), 5, 5, 150, 150);
        bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        Response.End();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
