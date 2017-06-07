using System;

namespace BenQGuru.eMES.Common.Domain
{
	/// <summary>
	/// TableMapAttribute 的摘要说明。
	/// </summary>
	[Serializable]
	[AttributeUsage(AttributeTargets.Class)]
	public class TableMapAttribute : Attribute
	{
		public TableMapAttribute(string tableName, string keyFields):this(tableName, keyFields, true)
		{
		}

		public TableMapAttribute(string tableName, string keyFields, bool clusteredPrimaryKey)
		{
			this._clusteredPK = clusteredPrimaryKey;
			this. _keyFields = keyFields;
			this._tableName = tableName;
			this._keyType = KeyTypes.Primary;
		}

		public string TableName
		{
			get
			{
				return this._tableName;
			}
			set
			{
				this._tableName = value;
			}
		}

		public KeyTypes KeyType
		{
			get
			{
				return this._keyType;
			}
			set
			{
				this._keyType = value;
			}
		}

		public string  KeyFields
		{
			get
			{
				return this._keyFields;
			}
			set
			{
				this._keyFields = value;
			}
		}

		public string[] GetKeyFields()
		{
			char[] chArray1 = new char[1] { ',' } ;
			string[] textArray1 = this._keyFields.ToLower().Split(chArray1);
			for (int num1 = 0; num1 < textArray1.Length; num1++)
			{
				textArray1[num1] = textArray1[num1].Trim();
			}
			return textArray1;
		}

		public override int GetHashCode()
		{
			return _tableName.GetHashCode ();
		}


		// Fields
		protected bool _clusteredPK;
		protected string _keyFields;
		protected string _tableName;
		protected KeyTypes _keyType;
	}
}
