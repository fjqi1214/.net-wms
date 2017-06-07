using System;
using System.Data;

namespace UpdaterConfig
{
	/// <summary>
	/// UpdaerConfigInfo 的摘要说明。
	/// </summary>
	///
	[Serializable]
	public class UpdaerConfigInfo
	{
		private DataSet ds = new DataSet();
		public UpdaerConfigInfo()
		{
			DataTable dt = new DataTable("UpdateInfo");
			dt.Columns.Add("SourcePath",typeof(string));
			dt.Columns.Add("TargetPath",typeof(string));
			dt.Columns.Add("TargetVIRPath",typeof(string));
			dt.Columns.Add("UpdateTime",typeof(DateTime));
			dt.Columns.Add("Week",typeof(DayOfWeek));
			dt.Columns.Add("Day",typeof(int));
			dt.Columns.Add("NeedAutoUpdate",typeof(bool));

			ds.Tables.Add(dt);
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public string SourcePath = String.Empty;

		public string TargetPath = String.Empty;

		public string TargetVIRPath = String.Empty;

		public DateTime UpdateTime ;

		public DayOfWeek Week ;

		public int Day ;

		public bool NeedAutoUpdate = false;

		public void SaveToFile(string fileName)
		{
			DataRow dr = ds.Tables[0].NewRow();

			dr["SourcePath"] = this.SourcePath;
			dr["TargetPath"] = this.TargetPath;
			dr["TargetVIRPath"] = this.TargetVIRPath;
			dr["UpdateTime"] = this.UpdateTime;
			dr["Week"] = this.Week;
			dr["Day"] = this.Day;
			dr["NeedAutoUpdate"] = this.NeedAutoUpdate;

			ds.Tables[0].Rows.Add(dr);
			ds.AcceptChanges();

			ds.WriteXml(fileName);
		}

		public void LoadFromFile(string fileName)
		{
			ds.ReadXml(fileName);

			DataRow dr = ds.Tables[0].Rows[0];

			this.SourcePath = Convert.ToString(dr["SourcePath"]);
			this.TargetPath = Convert.ToString(dr["TargetPath"]);
			this.TargetVIRPath = Convert.ToString(dr["TargetVIRPath"]);
			this.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
			this.Week = (DayOfWeek)(dr["Week"]);
			this.Day = Convert.ToInt32(dr["Day"]);
			this.NeedAutoUpdate =Convert.ToBoolean( dr["NeedAutoUpdate"]);

		}
	}
}
