using System;
using System.Web.UI.WebControls;

using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain ;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// DataPreparationHelper 的摘要说明。
	/// </summary>
	public class PhysicalLayoutDataPreparationHelper
	{	
		private DropDownList _segment = null,_stepSequence = null,_resource = null;
		private DropDownListBuilder _segmentBuilder = null,_stepSequenceBuilder = null,_resourceBuilder = null;

		private BaseModelFacade _facade = null;

		public PhysicalLayoutDataPreparationHelper(DropDownList segment,DropDownList stepSequence,DropDownList resource,IDomainDataProvider dataProvider)
		{
			this._segment = segment;
			this._stepSequence = stepSequence;
			this._resource = resource;

			this._facade = new FacadeFactory(dataProvider).CreateBaseModelFacade();

			if ( this._segment != null )
			{	
				this._segment.SelectedIndexChanged += new EventHandler(_segment_SelectedIndexChanged);
				this._segment.AutoPostBack = true;

				this._segmentBuilder = new DropDownListBuilder( this._segment );
				this._segmentBuilder.HandleGetObjectList += new GetObjectListDelegate( this._getSegmentList );
			}

			if ( this._stepSequence != null )
			{
				this._stepSequence.SelectedIndexChanged += new EventHandler(_stepSequence_SelectedIndexChanged);
				this._stepSequence.AutoPostBack = true;

				this._stepSequenceBuilder = new DropDownListBuilder( this._stepSequence );
				this._stepSequenceBuilder.HandleGetObjectList += new GetObjectListDelegate( this._getStepSequenceList );
			}

			if ( this._resource != null )
			{
				this._resourceBuilder = new DropDownListBuilder( this._resource );
				this._resourceBuilder.HandleGetObjectList += new GetObjectListDelegate( this._getResourceList );    
			}
		}

		public void Load()
		{			
			if ( this._segment != null && this._segmentBuilder != null )
			{
				if ( !this._segment.Page.IsPostBack )
				{
					this._segmentBuilder.Build("SegmentCode", "SegmentCode");
					this._segment.Items.Insert(0, "");
				}
			}
		}

		private void _segment_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( this._stepSequence != null && this._stepSequenceBuilder != null )
			{
				this._stepSequenceBuilder.Build("StepSequenceCode", "StepSequenceCode");
				this._stepSequence.Items.Insert(0, "");
			}

			if ( this._resource != null )
			{
				this._resource.Items.Clear();
			}
		}

		private void _stepSequence_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( this._resource != null && this._resourceBuilder != null )
			{
				this._resourceBuilder.Build("ResourceCode", "ResourceCode");
				this._resource.Items.Insert(0, "");
			}
		}

		private object[] _getSegmentList()
		{
			return this._facade.GetAllSegment();
		}

		private object[] _getStepSequenceList()
		{
			return this._facade.GetStepSequenceBySegmentCode( this._segment.SelectedValue );
		}

		private object[] _getResourceList()
		{
			return this._facade.GetResourceByStepSequenceCode( this._stepSequence.SelectedValue );
		}
	}

	public class RouteDataPreparationHelper
	{
		private DropDownList _model = null,_item = null,_mo = null,_op = null;
		private DropDownListBuilder _modelBuilder = null,_itemBuilder = null,_moBuilder = null,_opBuilder = null;

		private ModelFacade _modelFacade = null;
		private MOFacade _moFacade = null;		
		private ItemFacade _itemFacade = null;

		public RouteDataPreparationHelper(DropDownList model,DropDownList item,DropDownList mo,DropDownList op,IDomainDataProvider dataProvider)
		{
			this._model = model;
			this._item = item;
			this._mo = mo;
			this._op = op;

			this._modelFacade = new FacadeFactory(dataProvider).CreateModelFacade();
			this._moFacade = new FacadeFactory(dataProvider).CreateMOFacade();
			this._itemFacade = new FacadeFactory(dataProvider).CreateItemFacade();

			if( this._model != null )
			{
				this._model.SelectedIndexChanged +=new EventHandler(_model_SelectedIndexChanged);
				this._model.AutoPostBack = true;
				
				this._modelBuilder = new DropDownListBuilder( this._model );
				this._modelBuilder.HandleGetObjectList += new GetObjectListDelegate( this._getModelList );  
			}

			if( this._item != null )
			{
				this._item.SelectedIndexChanged +=new EventHandler(_item_SelectedIndexChanged);
				this._item.AutoPostBack = true;
				
				this._itemBuilder = new DropDownListBuilder( this._item );
				this._itemBuilder.HandleGetObjectList += new GetObjectListDelegate( this._getItemList );  
			}

			if( this._mo != null )
			{				
				this._mo.SelectedIndexChanged +=new EventHandler(_mo_SelectedIndexChanged);
				this._mo.AutoPostBack = true;

				this._moBuilder = new DropDownListBuilder( this._mo );
				this._moBuilder.HandleGetObjectList += new GetObjectListDelegate( this._getMOList );  
			}
			
			if( this._op != null )
			{				
				this._opBuilder = new DropDownListBuilder( this._op );
				this._opBuilder.HandleGetObjectList += new GetObjectListDelegate( this._getOperationList );  
			}
		}

		public void Load()
		{
			if( this._model != null && this._modelBuilder != null )
			{
				if ( !this._model.Page.IsPostBack )
				{
					this._modelBuilder.Build("ModelCode", "ModelCode");
					this._model.Items.Insert(0, "");
				}
			}
		}

		private void _model_SelectedIndexChanged(object sender, EventArgs e)
		{
			if( this._item != null && this._itemBuilder != null )
			{
				this._itemBuilder.Build("ItemCode", "ItemCode");
				this._item.Items.Insert(0, "");
			}

			if ( this._mo != null )
			{
				this._mo.Items.Clear();
			}

			if ( this._op != null )
			{
				this._op.Items.Clear();
			}
		}

		private void _item_SelectedIndexChanged(object sender, EventArgs e)
		{
			if( this._mo != null && this._moBuilder != null )
			{
				this._moBuilder.Build("MOCode", "MOCode");
				this._mo.Items.Insert(0, "");
			}			
		}

		private void _mo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( this._op != null && this._opBuilder != null )
			{				
				this._opBuilder.Build("OPCode", "OPCode");
				this._op.Items.Insert(0, "");
			}
		}

		private object[] _getModelList()
		{
			return this._modelFacade.GetAllModels();
		}

		private object[] _getItemList()
		{
			if ( this._model.SelectedValue == string.Empty )
			{
				return null;
			}

			return this._modelFacade.GetSelectedItems( this._model.SelectedValue, string.Empty, 1, int.MaxValue );
		}

		private object[] _getMOList()
		{
			if ( this._item.SelectedValue == string.Empty )
			{
				return null;
			}

			return this._moFacade.QueryMO( string.Empty, this._item.SelectedValue, string.Empty, string.Empty, string.Empty, string.Empty, 1, int.MaxValue );
		}

		private object[] _getOperationList()
		{
			return this._moFacade.GetAllOperationsByMoCode( this._mo.SelectedValue );
		}
	}
}
