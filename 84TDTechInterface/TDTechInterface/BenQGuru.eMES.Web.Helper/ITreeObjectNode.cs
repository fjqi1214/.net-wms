using System;
using System.Collections;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// ITreeObjectNode 的摘要说明。
	/// </summary>
	public interface ITreeObjectNode
	{
		string ID
		{
			get;set;
		}

		string Text
		{
			get;set;
		}

		ITreeObjectNode Parent
		{
			get;set;
		}

		ITreeObjectNode Root
		{
			get;
		}
		
		void AddSubTreeObjectNode(ITreeObjectNode node);

		void DeleteSubTreeObjectNode(ITreeObjectNode node);

		void MoveTreeObjectNode(ITreeObjectNode parent);

		void Update();

		TreeObjectNodeSet GetSubLevelChildrenNodes();

		ITreeObjectNode GetTreeObjectNodeByID(string id);

		TreeObjectNodeSet GetChainFromRoot();
		
		bool IsEqual(ITreeObjectNode node);

		TreeObjectNodeSet GetAllNodes();	
	}

	/// <summary>
	/// Set 的摘要说明。
	/// override Contains method it necessary
	/// </summary>
	[Serializable]
	public class Set : ICollection
	{
		/// <summary>
		/// Constructs a new Set object with zero elements.
		/// </summary>
		public Set()
		{
			myset = new ArrayList();
		}
    
		/// <summary>
		/// Constructs a new Set object with listed values.  If there are any duplications
		/// of the same element, these duplicates are ignored.
		/// </summary>
		/// <param name="values">a list of objects separated by commas.</param>
		public Set(params object[] values)
		{
			myset = new ArrayList();
			DoAdd(values);
		}

		/// <summary>
		/// Turn an ICollection into a Set.  Duplicates are removed.
		/// </summary>
		/// <param name="collection">the ICollection to be put into the Set.</param>
		public Set(ICollection collection)
		{
			myset = new ArrayList();
			object[] myarray = new ArrayList(collection).ToArray();
			DoAdd(myarray);
		}

		/// <summary>
		/// Adds an object to the Set.  If the object already exists in the Set,
		/// the object is ignored.
		/// </summary>
		/// <param name="obj">the object to be added to the Set.</param>
		/// <returns>a boolean value, true if the object was added to the Set and
		/// false if the object was already in the Set.</returns>
		public bool Add(object obj)
		{
			if (! Contains(obj) && obj != null) 
			{
				myset.Add(obj);
				HomogenityTest();
				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// sammer kong
		/// </summary>
		/// <param name="index"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public bool Insert(int index,object obj)
		{
			Set s = new Set();
			s.AddRange( this );

			this.Clear();
			
			this.Add( obj );
			this.AddRange( s );
			return false;
		}

		public void AddRange(ICollection collection) 
		{
			foreach(object o in collection)
			{
				this.Add( o );
			}
		}

		public void Clear() 
		{
			this.myset.Clear();
		}

		/// <summary>
		/// Performs multiple Add()'s on a list of objects.
		/// </summary>
		/// <param name="values">a list of objects separated by commas.</param>
		public void Add(params object[] values)
		{
			DoAdd(values);
		}
    
		/// <summary>
		/// Returns the number of elements in the Set.
		/// </summary>
		public int Count
		{
			get
			{
				return myset.Count;
			}
		}
    
		/// <summary>
		/// Tells whether an object is in a Set or not.
		/// </summary>
		/// <param name="obj">the test case for the Set.</param>
		/// <returns>a boolean value, true if the object is in the set.</returns>
		public virtual bool Contains(object obj)
		{
			if ( obj == null )
				return false;
			if (myset.Count == 0)
				return false;
			if (Homogenous && myset[0].GetType() == obj.GetType() )
			{
				foreach(object o in this.myset)
				{
					if( o.Equals(obj) )
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				return myset.Contains(obj);
			}
		}
    
		/// <summary>
		/// A boolean state of whether the elements in the Set are of the same Type
		/// or not.
		/// </summary>
		public bool Homogenous
		{
			get
			{
				return sametype;
			}
		}
    
		/// <summary>
		/// Removes an object from the Set.
		/// </summary>
		/// <param name="obj">the object to be removed from the Set.</param>
		/// <returns>a boolean value, true if the object was in the Set and
		/// false if it was not.</returns>
		public bool Remove(object obj)
		{			
			if (! Contains(obj))
				return false;
			myset.Remove(obj);
			HomogenityTest();
			return true;
		}

		/// <summary>
		/// Removes a series of objects from a Set.
		/// </summary>
		/// <param name="values">a list of objects to be removed.</param>
		public void Remove(params object[] values)
		{
			//Performs multiple removes, then tests for Homogenity.
			foreach (object o in values)
				myset.Remove(o);
			HomogenityTest();
		}
    
		/// <summary>
		/// Overrides the Equals method for Sets, based on the hashcodes of the objects.
		/// </summary>
		/// <param name="obj">an object to be compared to.</param>
		/// <returns>true if the two sets contain the same elements.</returns>
		public override bool Equals(object obj)
		{
			//Modified to check for more obvious signs of equality before
			//calling GetHashCode, the worst case scenario for Equals.
			Set newset;
			if (! (obj is Set))
				return false;
			else 
				newset = (Set)obj;  
			if (Homogenous != newset.Homogenous)
				return false;
			else if (Count != newset.Count)
				return false;
			else return GetHashCode().Equals(newset.GetHashCode());
		}
    
		/// <summary>
		/// Indexed property notation for Sets.
		/// </summary>
		public object this[int i] 
		{
			get 
			{
				return myset[i];
			}
		}
    
		/// <summary>
		/// Used to convert a Set to an Array, as well as to enhance the
		/// efficiency of the union, intersect and difference operators.
		/// </summary>
		/// <returns>an array of objects.</returns>
		public object[] ToArray()
		{
			return myset.ToArray();
		}

		public ArrayList ToArrayList()
		{
			return this.myset;
		}

		/// <summary>
		/// CopyTo copies the elements of the Set into an array.
		/// </summary>
		/// <param name="array">The one dimensional array that is the destination
		/// the elements copied from the Set.  The array must have zero-based indexing.</param>
		/// <param name="i">The zero-based index in array in which copying begins.</param>
		public void CopyTo(Array array, int i)
		{
			myset.CopyTo(array,i);
		}
    
		/// <summary>
		/// Overloaded operator for subset notation.
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>true if S is a subset of T.</returns>
		public static bool operator<=(Set lhs, Set rhs)
		{
			if (lhs.Count > rhs.Count)
				return false;
			bool isASubset = true;
			int i = 0;
			while (isASubset && i < lhs.Count)
			{
				if (! rhs.Contains(lhs[i]))
					isASubset = false;
				i++;
			}

			return isASubset;
		}

		/// <summary>
		/// Overloaded operator for superset notation.
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>true if S is a superset of T.</returns>
		public static bool operator >=(Set lhs, Set rhs)
		{
			return rhs <= lhs;
		}

		/// <summary>
		/// Overloaded operator for proper subset notation.
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>true if S is a proper subset of T.</returns>
		public static bool operator<(Set lhs, Set rhs)
		{
			if (lhs.Count >= rhs.Count)
				return false;
			if (lhs == rhs)
				return false;
			else
				return (lhs <= rhs);
		}

		/// <summary>
		/// Overloaded operator for proper superset notation.
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>true if S is a proper superset of T.</returns>
		public static bool operator>(Set lhs, Set rhs)
		{
			return rhs < lhs;
		}

		/*/// <summary>
		/// Overloaded operator for two Sets being the same (i.e. they are both non-proper
		/// subsets of each other).
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>true if S.Equals(T).</returns>
		public static bool operator==(Set lhs, Set rhs)
		{
		  return lhs.Equals(rhs);
		}

		/// <summary>
		/// Overloaded operator for !=.
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>true if S.Equals(T) is false.</returns>
		/// 
		public static bool operator!=(Set lhs, Set rhs)
		{
		  return (! lhs.Equals(rhs));
		}*/
    
		/// <summary>
		/// Gets all subsets of the Set.
		/// </summary>
		/// <returns>a Set of subsets of the this Set.</returns>
		public Set Subsets()
		{
			Set newset = new Set();
			for (int i = 0; i < Count; i++)
				newset.Add(Subsets(i).ToArray());
			return newset;
		}

		/// <summary>
		/// Gets all subsets of the Set of Count count.
		/// </summary>
		/// <param name="count">number of elements in each subset.</param>
		/// <returns>a Set of subsets with the desired number of elements.</returns>
		public Set Subsets(int count)
		{
			int difference = Count - count;
			//Precondition:  Both superset and subset have more than 0 elements, and
			//superset has more elements than subset.  Otherwise, return empty set.
			if (difference < 0 || count < 1 || myset.Count < 1)
			{
				return new Set();
			}
				//Definition:  A subset with the same number of elements as the superset
				//is the superset.
			else if (difference == 0)
			{
				Set s = new Set(myset);
				Set t = new Set();
				t.Add(s);
				return t;
			}
				//Definition:  A set of subsets with count == 1 is a set of count == 1 with
				//each element a set.
			else if (count == 1)
			{
				ArrayList oneset = new ArrayList();
				foreach (object o in myset)
					oneset.Add(new Set(o));
				return new Set(oneset.ToArray());;
			}
				//Process:  return a set of subsets where sets.Count == superset.Count, and
				//each subset is missing one unique element from the superset.
			else 
			{
				ArrayList subset = new ArrayList();
				Set objectset, newset;
				foreach (object o in myset)
				{
					objectset = new Set(o);
					newset = new Set(myset) - objectset;
					subset.Add(newset);
				}
				//Base: If performing another SubSet operation would make difference == 0,
				//return the Set.
				if (difference == 1)
				{
					return new Set(subset.ToArray());
				}
					//Recursive:
				else
				{
					Set recurse = new Set();
					foreach (Set s in subset) 
					{
						Set newsubset = s.Subsets(count);
						foreach (Set t in newsubset)
							recurse.Add(t);
					}
					return recurse;
				}
			}
		}
		/// <summary>
		/// Returns a string representation of the Set.
		/// </summary>
		/// <returns>a string in the form <code>(a, b, c)</code>.</returns>
		public override string ToString()
		{
			string s = "(";
			foreach (object o in myset)
				if (o != myset[myset.Count-1])
					s += o + ", ";
				else
					s += o;
			s += ")";
			return s;
		}
    
		/// <summary>
		/// A delegate definition for changing an object's value.  
		/// </summary>
		public delegate object IDictionary(object o);
    
		/// <summary>
		/// Applies a IDictionary function to every object in a Set.
		/// </summary>
		/// <param name="s">The Set to be Mapped.</param>
		/// <param name="m">The IDictionary function to apply to the Set.</param>
		/// <returns>A new Set with Mapped values.</returns>
		public Set MapToSet(Set s, IDictionary m)
		{
			Set newset = new Set();
			object[] array = new Object[s.Count];
			int i = 0;
			foreach (object o in s)
				array[i++] = m(o);
			newset.Add(array);
			return newset;
		}
    
		/// <summary>
		/// Overloaded operator for the union of two Sets.
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>the union of Sets S and T (S V T).</returns>
		public static Set operator+(Set lhs, Set rhs)
		{
			Set newset = new Set();
			newset.Add(lhs.ToArray());
			newset.Add(rhs.ToArray());
			return newset;
		}

		/// <summary>
		/// Overloaded operator for the intersection of two Sets.
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>the intersection of Sets S and T (S ^ T).</returns>
		public static Set operator*(Set lhs, Set rhs)
		{
			Set newset = new Set();
			Set smaller = lhs.Count > rhs.Count ? rhs : lhs;
			Set larger = smaller == lhs ? rhs : lhs;
			object[] array = new object[smaller.Count];
			int i = 0;
			foreach (object o in smaller)
				if (larger.Contains(o))
				{
					array[i++] = o;
				}
			newset.Add(array);
			return newset;
		}
		/// <summary>
		/// Overloaded operator for subtracting one Set from the other.
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>A set with all the elements in Set S, but without the
		/// elements of Set T (S - T).</returns>
		public static Set operator-(Set lhs, Set rhs)
		{
			Set newset = new Set(lhs.ToArray());
			newset.Remove(rhs.ToArray());
			return newset;
		}

		/// <summary>
		/// Overloaded operator for complimenting one Set from the other.  This is
		/// the same as (S + T) - (S * T).
		/// </summary>
		/// <param name="lhs">a Set (S).</param>
		/// <param name="rhs">a Set (T).</param>
		/// <returns>A set with all the uncommon elements of S and T.</returns>
		public static Set operator/(Set lhs, Set rhs)
		{
			Set s = lhs + rhs;
			Set t = lhs * rhs;
			return s - t;
		}
    
		/// <summary>
		/// This function determines the cross product of two Sets.  Two cautions should
		/// be taken when using CrossProduct:
		/// <list type="1">
		/// <item>For large sets, this method can take a long time.</item>
		/// <item>If Set s has any elements that are also in this Set, the list of Sets will
		/// contain (one or more) Sets with fewer elements than the majority.</item>
		/// </list>
		/// </summary>
		/// <param name="s">A second Set to be crossed with the original Set.</param>
		/// <returns>a Set representing the cross product of two Sets.</returns>
		public Set CrossProduct(Set s)
		{
			Set newset = new Set();
			foreach(object o in myset)
				foreach(object p in s)
					newset.Add(new Set(o,p));
			return newset;
		}
    
		/// <summary>
		/// Serves as a hash function for the Set type.
		/// </summary>
		/// <returns>an integer code for hashtables.</returns>
		public override int GetHashCode()
		{
			int hash = 0;
			foreach (object o in myset)
				hash += o.GetHashCode();
			return hash;
		}
		/// <summary>
		/// Required in order to reference objects in a Set through the <code>foreach</code>
		/// method.
		/// </summary>
		/// <returns>an enumerator for the SetCollections.Set class.</returns>
		public IEnumerator GetEnumerator()
		{
			return myset.GetEnumerator();
		}

		/// <summary>
		/// Gets an object that can be used to synchronize access to the Set.
		/// </summary>
		public object SyncRoot
		{
			get
			{
				return myset.SyncRoot;
			}
		}

		/// <summary>
		/// Gets a value indicating whether access to the Set is synchronized (thread safe).
		/// </summary>
		public bool IsSynchronized
		{
			get
			{
				return myset.IsSynchronized;
			}
		}
    
		//Homogenity Test is called whenever a Set is modified:  
		//items added or removed.  Returns a boolean in case I ever want to call
		//HomogenityTest during a predicate.
		private bool HomogenityTest()
		{
			Type type;
			if (myset.Count > 0)
			{
				if (myset.Count == 1)
				{
					//A Set with one item is considered homogenous
					sametype = true;
					return true;
				}
				type = myset[0].GetType();
				foreach (object o in myset)
					if (o.GetType() != type)
					{
						sametype = false;
						return false;
					}
				sametype = true;
				if (myset[0] is IComparable)
					myset.Sort();
			}
			//The empty set is considered homogenous, so no else case needed.
			sametype = true;
			return true;      
		}

		private void DoAdd(object[] values)
		{
			//performs multiple adds to the arraylist, then tests for 
			//homogenity.  More efficient than before, only tests once
			//rather than multiple times for each add.
			foreach (object o in values)
				if (! Contains(o) && o != null) 
					myset.Add(o);
			HomogenityTest();
		}
		private bool sametype;
		protected ArrayList myset;
	}


	/// <summary>
	/// TreeObjectNodeHelper 的摘要说明。
	/// </summary>
	public class TreeObjectNodeHelper
	{
		private class _parentChild
		{
			private string _pid = "";
			public string PID
			{
				get
				{
					return this._pid;
				}
			}
			private string _cid = "";
			public string CID
			{
				get
				{
					return this._cid;
				}
			}

			public _parentChild(string pid,string cid)
			{
				this._pid = pid;
				this._cid = cid;
			}

			public override bool Equals(object obj)
			{
				if( this._cid == ((_parentChild)obj)._cid &&
					this._pid == ((_parentChild)obj)._pid )
				{
					return true;
				}
				return false;
			}

			public override int GetHashCode()
			{
				return base.GetHashCode ();
			}
		}

		public TreeObjectNodeHelper()
		{			
		}

		public string GenerateTreeObjectID()
		{
			return System.Guid.NewGuid().ToString();
		}

		private TreeObjectNodeSet _nodeSet = new TreeObjectNodeSet();
		private TreeObjectNodeSet _nodeInfoSet = new TreeObjectNodeSet();
		
		public ITreeObjectNode GetTreeObjectNodeByID(string id)
		{	
			ITreeObjectNode ret = null;
			foreach(ITreeObjectNode node in this._nodeInfoSet)
			{
				if( node.ID.ToUpper() == id.ToUpper() )
				{
					ret = node;
					break;
				}
			}
			return ret;
		}
		
		public void AddSubTreeObjectNode(ITreeObjectNode parent,ITreeObjectNode sub)
		{
			sub.Parent = parent;
			_parentChild pc = new _parentChild(parent.ID,sub.ID);
			this._nodeSet.Add( pc );

			this._nodeInfoSet.Add( parent );
			this._nodeInfoSet.Add( sub );
		}

		public void AddSubTreeObjectNodeRange(ITreeObjectNode parent,ITreeObjectNode[] subs)
		{
			if ( subs != null )
			{
				foreach ( ITreeObjectNode sub in subs)
				{
					this.AddSubTreeObjectNode( parent, sub );
				}
			}
		}

		#region Private
		private _parentChild _find(string pid,string cid)
		{
			foreach(_parentChild pc in this._nodeSet)
			{
				if( pc.PID ==pid &&
					pc.CID == cid )
				{
					return pc;
				}
			}

			ExceptionManager.Raise(this.GetType(), "$Error_Child_Not_Found", string.Format("[$pid={0},$cid={1}]", pid, cid) );	

			return null;
		}
		private TreeObjectNodeSet _find(string pid)
		{
			TreeObjectNodeSet cSet = new TreeObjectNodeSet();
			foreach(_parentChild pc in this._nodeSet)
			{
				if( pc.PID == pid )
				{
					cSet.Add( pc );
				}
			}
			return cSet;
		}

		private ITreeObjectNode _findNode(string id)
		{
			foreach(ITreeObjectNode node in this._nodeInfoSet)
			{
				if( node.ID.ToUpper() == id.ToUpper() )
				{
					return node;
				}
			}
			return null;
		}
		#endregion

		public void DeleteSubTreeObjectNode(ITreeObjectNode parent,ITreeObjectNode sub)
		{
			this._nodeSet.Remove( this._find(parent.ID,sub.ID) );
			this._nodeInfoSet.Remove( this._findNode( sub.ID ) );
		}

		public void Update(ITreeObjectNode node)
		{
			this._nodeInfoSet.Remove( node );
			this._nodeInfoSet.Add( node );
		}

		public void MoveTreeObjectNode(ITreeObjectNode from,ITreeObjectNode to)
		{
			this._nodeSet.Remove( this._find(from.Parent.ID,from.ID) );
			this._nodeSet.Add( new _parentChild(to.ID,from.ID) );

			foreach(ITreeObjectNode node in this._nodeInfoSet)
			{
				if( node.ID == from.ID )
				{
					node.Parent = to;
					return;
				}
			}

			ExceptionManager.Raise(this.GetType(), "$Error_TreeObjectNode_Not_Found", string.Format("[$NodeID=]", from.ID) );	
		}

		public ITreeObjectNode GetRoot()
		{
			foreach(ITreeObjectNode node in this._nodeInfoSet)
			{
				if( node.Parent == null )
				{
					return node;
				}
			}
			return null;
		}

		public TreeObjectNodeSet GetChainFromRoot(ITreeObjectNode node)
		{
			TreeObjectNodeSet nset = new TreeObjectNodeSet();
			nset.Add( node );

			while( node.Parent != null )
			{
				nset.Insert(0, node.Parent);
				node = node.Parent;
			}

			return nset;
		}

		public TreeObjectNodeSet GetSubLevelChildrenNodes(ITreeObjectNode parent)
		{
			TreeObjectNodeSet ret = new TreeObjectNodeSet();
			TreeObjectNodeSet nset = this._find( parent.ID ) ;

			foreach(_parentChild pc in nset)
			{
				ret.Add( this._findNode( pc.CID ) );
			}
			return ret;
		}
		
		public bool IsEqual(ITreeObjectNode ln,ITreeObjectNode rn)
		{			
			if( ln.ID.ToUpper() == rn.ID.ToUpper() )
			{
				return true;
			}
			return false;
		}

		public TreeObjectNodeSet GetAllNodes(ITreeObjectNode parent)
		{
			TreeObjectNodeSet nSet = new TreeObjectNodeSet();

			nSet.Add(parent);

			foreach ( ITreeObjectNode node in parent.GetSubLevelChildrenNodes() )
			{
				nSet.AddRange( node.GetAllNodes() );
			}

			return nSet;
		}
	}


	/// <summary>
	/// TreeObjectNodeSet 的摘要说明。
	/// </summary>
	public class TreeObjectNodeSet : Set
	{
		public TreeObjectNodeSet()
		{
		}	
	}
}
