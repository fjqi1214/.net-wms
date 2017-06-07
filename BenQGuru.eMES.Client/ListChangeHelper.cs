using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.Client
{
    public class ListChangeHelper<T>
    {
        private List<T> _AddList = new List<T>();
        private List<T> _DeleteList = new List<T>();

        public T[] AddList
        {
            get
            {
                return this._AddList.ToArray();
            }
        }

        public T[] DeleteList
        {
            get
            {
                return this._DeleteList.ToArray();
            }
        }

        public void Add(T item)
        {
            if (this._DeleteList.Contains(item))
            {
                this._DeleteList.Remove(item);
            }
            else if (!this._AddList.Contains(item))
            {
                this._AddList.Add(item);
            }
        }

        public void Delete(T item)
        {
            if (this._AddList.Contains(item))
            {
                this._AddList.Remove(item);
            }
            else if (!this._DeleteList.Contains(item))
            {
                this._DeleteList.Add(item);
            }
        }

        public void Clear()
        {
            this._AddList.Clear();
            this._DeleteList.Clear();
        }

        public bool IsDirty
        {
            get
            {
                return this._AddList.Count > 0 || this._DeleteList.Count > 0;
            }
        }
    }
}
