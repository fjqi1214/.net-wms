using System;

namespace BenQGuru.eMES.Common.Domain
{
	/// <summary>
	/// FieldRelationMapAtrribute 的摘要说明。
	/// </summary>
	[Serializable]	
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class FieldRelationMapAtrribute : Attribute
	{
		public FieldRelationMapAtrribute(string parentTableName, string childTableName, string parentKeys, string childKeys)
		{
			this._parentTableName	= parentTableName.ToLower().Trim();
			this._parentKeys			= parentKeys.ToLower().Trim();
			this._childTableName		= childTableName.ToLower().Trim();
			this._childKeys				= childKeys.ToLower().Trim();
		}

		public string  ParentTableName
		{
			get
			{
				return this._parentTableName;
			}
			set
			{
				this._parentTableName = value;
			}
		}

		public string  ChildTableName
		{
			get
			{
				return this._childTableName;
			}
			set
			{
				this._childTableName = value.ToLower().Trim();
			}
		}

		public string  ParentKeys
		{
			get
			{
				return this._parentKeys;
			}
			set
			{
				this._parentKeys = value.ToLower().Trim();
			}
		}

		public string  ChildKeys
		{
			get
			{
				return this._childKeys;
			}
			set
			{
				this._childKeys = value.ToLower().Trim();
			}
		}

		public string[] GetParentKeys()
		{
			char[] chArray1 = new char[1] { ',' } ;
			string[] textArray1 = this._parentKeys.ToLower().Split(chArray1);
			for (int num1 = 0; num1 < textArray1.Length; num1++)
			{
				textArray1[num1] = textArray1[num1].Trim();
			}
			return textArray1;
		}

		public string[] GetChildKeys()
		{
			char[] chArray1 = new char[1] { ',' } ;
			string[] textArray1 = this._childKeys.ToLower().Split(chArray1);
			for (int num1 = 0; num1 < textArray1.Length; num1++)
			{
				textArray1[num1] = textArray1[num1].Trim();
			}
			return textArray1;
		}

		// Fields
		protected string _parentTableName;
		protected string _childTableName;
		protected string _parentKeys;
		protected string _childKeys;
	}
}
