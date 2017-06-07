using System;
using System.Collections.Generic;
using System.Text;


namespace BenQGuru.eMES.SAPDataTransferService
{
    public class ScheduledServiceTimer : System.Timers.Timer
    {
        private ScheduledServiceEntity m_Entity = null;
        private bool m_Disabled = false;

        public ScheduledServiceTimer()
        {

        }

        public ScheduledServiceTimer(double interval) : base(interval)
        {

        }

        public bool Disabled
        {
            get
            {
                return m_Disabled;
            }
            set
            {
                m_Disabled = value;
            }
        }

        public ScheduledServiceEntity ScheduledServiceEntity
        {
            get
            {
                return this.m_Entity;
            }
            set
            {
                this.m_Entity = value;
            }
        }

    }
}
