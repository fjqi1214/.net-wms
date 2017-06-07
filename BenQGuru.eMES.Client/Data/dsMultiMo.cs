﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace BenQGuru.eMES.Client.Data {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class dsMo : DataSet {
        
        private MultiMoDataTable tableMultiMo;
        
        public dsMo() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected dsMo(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["MultiMo"] != null)) {
                    this.Tables.Add(new MultiMoDataTable(ds.Tables["MultiMo"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public MultiMoDataTable MultiMo {
            get {
                return this.tableMultiMo;
            }
        }
        
        public override DataSet Clone() {
            dsMo cln = ((dsMo)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["MultiMo"] != null)) {
                this.Tables.Add(new MultiMoDataTable(ds.Tables["MultiMo"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tableMultiMo = ((MultiMoDataTable)(this.Tables["MultiMo"]));
            if ((this.tableMultiMo != null)) {
                this.tableMultiMo.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "dsMo";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/dsMo.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableMultiMo = new MultiMoDataTable();
            this.Tables.Add(this.tableMultiMo);
        }
        
        private bool ShouldSerializeMultiMo() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void MultiMoRowChangeEventHandler(object sender, MultiMoRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class MultiMoDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn column工单;
            
            private DataColumn column产品序列号;
            
            private DataColumn column产品代码;
            
            private DataColumn column已投入数量;
            
            private DataColumn column计划数量;
            
            private DataColumn column已拆解数量;
            
            private DataColumn column已脱离数量;
            
            private DataColumn column已完工数量;
            
            private DataColumn column待投入数量;
            
            internal MultiMoDataTable() : 
                    base("MultiMo") {
                this.InitClass();
            }
            
            internal MultiMoDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn 工单Column {
                get {
                    return this.column工单;
                }
            }
            
            internal DataColumn 产品序列号Column {
                get {
                    return this.column产品序列号;
                }
            }
            
            internal DataColumn 产品代码Column {
                get {
                    return this.column产品代码;
                }
            }
            
            internal DataColumn 已投入数量Column {
                get {
                    return this.column已投入数量;
                }
            }
            
            internal DataColumn 计划数量Column {
                get {
                    return this.column计划数量;
                }
            }
            
            internal DataColumn 已拆解数量Column {
                get {
                    return this.column已拆解数量;
                }
            }
            
            internal DataColumn 已脱离数量Column {
                get {
                    return this.column已脱离数量;
                }
            }
            
            internal DataColumn 已完工数量Column {
                get {
                    return this.column已完工数量;
                }
            }
            
            internal DataColumn 待投入数量Column {
                get {
                    return this.column待投入数量;
                }
            }
            
            public MultiMoRow this[int index] {
                get {
                    return ((MultiMoRow)(this.Rows[index]));
                }
            }
            
            public event MultiMoRowChangeEventHandler MultiMoRowChanged;
            
            public event MultiMoRowChangeEventHandler MultiMoRowChanging;
            
            public event MultiMoRowChangeEventHandler MultiMoRowDeleted;
            
            public event MultiMoRowChangeEventHandler MultiMoRowDeleting;
            
            public void AddMultiMoRow(MultiMoRow row) {
                this.Rows.Add(row);
            }
            
            public MultiMoRow AddMultiMoRow(string 工单, string 产品序列号, string 产品代码, string 已投入数量, long 计划数量, long 已拆解数量, long 已脱离数量, long 已完工数量, long 待投入数量) {
                MultiMoRow rowMultiMoRow = ((MultiMoRow)(this.NewRow()));
                rowMultiMoRow.ItemArray = new object[] {
                        工单,
                        产品序列号,
                        产品代码,
                        已投入数量,
                        计划数量,
                        已拆解数量,
                        已脱离数量,
                        已完工数量,
                        待投入数量};
                this.Rows.Add(rowMultiMoRow);
                return rowMultiMoRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                MultiMoDataTable cln = ((MultiMoDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new MultiMoDataTable();
            }
            
            internal void InitVars() {
                this.column工单 = this.Columns["工单"];
                this.column产品序列号 = this.Columns["产品序列号"];
                this.column产品代码 = this.Columns["产品代码"];
                this.column已投入数量 = this.Columns["已投入数量"];
                this.column计划数量 = this.Columns["计划数量"];
                this.column已拆解数量 = this.Columns["已拆解数量"];
                this.column已脱离数量 = this.Columns["已脱离数量"];
                this.column已完工数量 = this.Columns["已完工数量"];
                this.column待投入数量 = this.Columns["待投入数量"];
            }
            
            private void InitClass() {
                this.column工单 = new DataColumn("工单", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.column工单);
                this.column产品序列号 = new DataColumn("产品序列号", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.column产品序列号);
                this.column产品代码 = new DataColumn("产品代码", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.column产品代码);
                this.column已投入数量 = new DataColumn("已投入数量", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.column已投入数量);
                this.column计划数量 = new DataColumn("计划数量", typeof(long), null, System.Data.MappingType.Element);
                this.Columns.Add(this.column计划数量);
                this.column已拆解数量 = new DataColumn("已拆解数量", typeof(long), null, System.Data.MappingType.Element);
                this.Columns.Add(this.column已拆解数量);
                this.column已脱离数量 = new DataColumn("已脱离数量", typeof(long), null, System.Data.MappingType.Element);
                this.Columns.Add(this.column已脱离数量);
                this.column已完工数量 = new DataColumn("已完工数量", typeof(long), null, System.Data.MappingType.Element);
                this.Columns.Add(this.column已完工数量);
                this.column待投入数量 = new DataColumn("待投入数量", typeof(long), null, System.Data.MappingType.Element);
                this.Columns.Add(this.column待投入数量);
                this.column工单.ReadOnly = true;
                this.column产品序列号.AllowDBNull = false;
                this.column产品代码.ReadOnly = true;
                this.column已投入数量.ReadOnly = true;
                this.column计划数量.ReadOnly = true;
                this.column已拆解数量.ReadOnly = true;
                this.column已脱离数量.ReadOnly = true;
                this.column已完工数量.AllowDBNull = false;
                this.column待投入数量.ReadOnly = true;
            }
            
            public MultiMoRow NewMultiMoRow() {
                return ((MultiMoRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new MultiMoRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(MultiMoRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.MultiMoRowChanged != null)) {
                    this.MultiMoRowChanged(this, new MultiMoRowChangeEvent(((MultiMoRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.MultiMoRowChanging != null)) {
                    this.MultiMoRowChanging(this, new MultiMoRowChangeEvent(((MultiMoRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.MultiMoRowDeleted != null)) {
                    this.MultiMoRowDeleted(this, new MultiMoRowChangeEvent(((MultiMoRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.MultiMoRowDeleting != null)) {
                    this.MultiMoRowDeleting(this, new MultiMoRowChangeEvent(((MultiMoRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveMultiMoRow(MultiMoRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class MultiMoRow : DataRow {
            
            private MultiMoDataTable tableMultiMo;
            
            internal MultiMoRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableMultiMo = ((MultiMoDataTable)(this.Table));
            }
            
            public string 工单 {
                get {
                    try {
                        return ((string)(this[this.tableMultiMo.工单Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("无法获取值，因为它是 DBNull。", e);
                    }
                }
                set {
                    this[this.tableMultiMo.工单Column] = value;
                }
            }
            
            public string 产品序列号 {
                get {
                    return ((string)(this[this.tableMultiMo.产品序列号Column]));
                }
                set {
                    this[this.tableMultiMo.产品序列号Column] = value;
                }
            }
            
            public string 产品代码 {
                get {
                    try {
                        return ((string)(this[this.tableMultiMo.产品代码Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("无法获取值，因为它是 DBNull。", e);
                    }
                }
                set {
                    this[this.tableMultiMo.产品代码Column] = value;
                }
            }
            
            public string 已投入数量 {
                get {
                    try {
                        return ((string)(this[this.tableMultiMo.已投入数量Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("无法获取值，因为它是 DBNull。", e);
                    }
                }
                set {
                    this[this.tableMultiMo.已投入数量Column] = value;
                }
            }
            
            public long 计划数量 {
                get {
                    try {
                        return ((long)(this[this.tableMultiMo.计划数量Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("无法获取值，因为它是 DBNull。", e);
                    }
                }
                set {
                    this[this.tableMultiMo.计划数量Column] = value;
                }
            }
            
            public long 已拆解数量 {
                get {
                    try {
                        return ((long)(this[this.tableMultiMo.已拆解数量Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("无法获取值，因为它是 DBNull。", e);
                    }
                }
                set {
                    this[this.tableMultiMo.已拆解数量Column] = value;
                }
            }
            
            public long 已脱离数量 {
                get {
                    try {
                        return ((long)(this[this.tableMultiMo.已脱离数量Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("无法获取值，因为它是 DBNull。", e);
                    }
                }
                set {
                    this[this.tableMultiMo.已脱离数量Column] = value;
                }
            }
            
            public long 已完工数量 {
                get {
                    return ((long)(this[this.tableMultiMo.已完工数量Column]));
                }
                set {
                    this[this.tableMultiMo.已完工数量Column] = value;
                }
            }
            
            public long 待投入数量 {
                get {
                    try {
                        return ((long)(this[this.tableMultiMo.待投入数量Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("无法获取值，因为它是 DBNull。", e);
                    }
                }
                set {
                    this[this.tableMultiMo.待投入数量Column] = value;
                }
            }
            
            public bool Is工单Null() {
                return this.IsNull(this.tableMultiMo.工单Column);
            }
            
            public void Set工单Null() {
                this[this.tableMultiMo.工单Column] = System.Convert.DBNull;
            }
            
            public bool Is产品代码Null() {
                return this.IsNull(this.tableMultiMo.产品代码Column);
            }
            
            public void Set产品代码Null() {
                this[this.tableMultiMo.产品代码Column] = System.Convert.DBNull;
            }
            
            public bool Is已投入数量Null() {
                return this.IsNull(this.tableMultiMo.已投入数量Column);
            }
            
            public void Set已投入数量Null() {
                this[this.tableMultiMo.已投入数量Column] = System.Convert.DBNull;
            }
            
            public bool Is计划数量Null() {
                return this.IsNull(this.tableMultiMo.计划数量Column);
            }
            
            public void Set计划数量Null() {
                this[this.tableMultiMo.计划数量Column] = System.Convert.DBNull;
            }
            
            public bool Is已拆解数量Null() {
                return this.IsNull(this.tableMultiMo.已拆解数量Column);
            }
            
            public void Set已拆解数量Null() {
                this[this.tableMultiMo.已拆解数量Column] = System.Convert.DBNull;
            }
            
            public bool Is已脱离数量Null() {
                return this.IsNull(this.tableMultiMo.已脱离数量Column);
            }
            
            public void Set已脱离数量Null() {
                this[this.tableMultiMo.已脱离数量Column] = System.Convert.DBNull;
            }
            
            public bool Is待投入数量Null() {
                return this.IsNull(this.tableMultiMo.待投入数量Column);
            }
            
            public void Set待投入数量Null() {
                this[this.tableMultiMo.待投入数量Column] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class MultiMoRowChangeEvent : EventArgs {
            
            private MultiMoRow eventRow;
            
            private DataRowAction eventAction;
            
            public MultiMoRowChangeEvent(MultiMoRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public MultiMoRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
