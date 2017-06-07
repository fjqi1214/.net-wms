using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.Common.Domain
{
	[Serializable]	
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class FieldMapAttribute : Attribute
    {
        #region Fields

        protected bool _AllowNull;
        protected BlobTypes _BlobType;
        protected Type _DataType;
        protected string _DefaultValue;
        protected string _FieldName;
        protected int _Size;
        protected int _Scale;
        protected int _IdentIncr;
        protected bool _Identity;
        protected int _IdentSeed;
        protected bool _VariantSize;

        #endregion

        public FieldMapAttribute(string fieldName, BlobTypes blobType)
            : this(fieldName, typeof(byte[]), true)
        {
            this._BlobType = blobType;
        }

        public FieldMapAttribute(string fieldName, int identSeed, int identIncr)
            : this(fieldName, typeof(int), false)
        {
            this._Identity = true;
            this._IdentSeed = identSeed;
            this._IdentIncr = identIncr;
        }

        public FieldMapAttribute(string fieldName, Type dataType, bool allowNull)
        {
            this._Identity = false;
            this._IdentSeed = 1;
            this._IdentIncr = 1;
            this._BlobType = BlobTypes.None;
            this._FieldName = fieldName.ToLower().Trim();
            this._DataType = dataType;
            this._AllowNull = allowNull;
            this._Scale = 0;
        }

        public FieldMapAttribute(string fieldName, Type dataType, int size, bool allowNull)
            : this(fieldName, dataType, allowNull)
        {
            this._Size = size;
        }

        public FieldMapAttribute(string fieldName, Type dataType, int size, int scale, bool allowNull)
            : this(fieldName, dataType, size, allowNull)
        {
            this._Scale = scale;
        }

        public FieldMapAttribute(string fieldName, Type dataType, int size, bool allowNull, bool variantSize)
            : this(fieldName, dataType, size, allowNull)
        {
            this._VariantSize = variantSize;
        }

        public FieldMapAttribute(string fieldName, Type dataType, int size, bool allowNull, string defaultValue)
            : this(fieldName, dataType, size, allowNull)
        {
            this._DefaultValue = defaultValue;
        }

        #region Properties

        public bool AllowNull
        {
            get
            {
                return this._AllowNull;
            }
            set
            {
                this._AllowNull = value;
            }
        }

        public BlobTypes BlobType
        {
            get
            {
                return this._BlobType;
            }
            set
            {
                this._BlobType = value;
            }
        }

        public Type DataType
        {
            get
            {
                return this._DataType;
            }
            set
            {
                this._DataType = value;
            }
        }

        public string DefaultValue
        {
            get
            {
                return this._DefaultValue;
            }
            set
            {
                this._DefaultValue = value;
            }
        }

        public string FieldName
        {
            get
            {
                return this._FieldName;
            }
            set
            {
                this._FieldName = value.ToLower().Trim();
            }
        }

        public int Size
        {
            get
            {
                return this._Size;
            }
            set
            {
                this._Size = value;
            }
        }

        public int Scale
        {
            get
            {
                return this._Scale;
            }
            set
            {
                this._Scale = value;
            }
        }

        public int IdentIncr
        {
            get
            {
                return this._IdentIncr;
            }
            set
            {
                this._IdentIncr = value;
            }
        }

        public bool Identity
        {
            get
            {
                return this._Identity;
            }
            set
            {
                this._Identity = value;
            }
        }

        public int IdentSeed
        {
            get
            {
                return this._IdentSeed;
            }
            set
            {
                this._IdentSeed = value;
            }
        }

        public bool VariantSize
        {
            get
            {
                return this._VariantSize;
            }
            set
            {
                this._VariantSize = value;
            }
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return _FieldName.GetHashCode();
        }

        #endregion
	}
}
