using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.WatchPanelNew
{
    /// <summary>
    ///  This event handler is for the relation event of parent form and child form
    /// </summary>
    /// <typeparam name="TEventArgs">the event arges you want to tell the event handler</typeparam>
    /// <param name="sender">the object who fired this event</param>
    /// <param name="e">the event arges</param>
    public delegate void ParentChildRelateEventHandler<TEventArgs>(object sender, TEventArgs e)
        where TEventArgs : EventArgs;

    /// <summary>
    ///  This event args is for the relation of parent form and child form
    /// </summary>
    /// <typeparam name="TType">The type that you want to tell the args to transfer</typeparam>
    public class ParentChildRelateEventArgs<TType> : EventArgs
    {
        /// <summary>
        ///  Contructor of ParentChildRelateEventArgs
        /// </summary>
        /// <param name="t">the Type you want to transfer</param>
        public ParentChildRelateEventArgs(TType t)
        {
            this.m_CustomObject = t;
        }

        private TType m_CustomObject;
        /// <summary>
        ///  Get or Set the object that can make you transfer it between parent form and child form
        /// </summary>
        public TType CustomObject
        {
            get { return m_CustomObject; }
            set { m_CustomObject = value; }
        }
    }
}
