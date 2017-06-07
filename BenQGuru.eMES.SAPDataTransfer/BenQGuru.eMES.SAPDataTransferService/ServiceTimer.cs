using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.SAPDataTransferService
{
    public class ServiceTimer : System.Timers.Timer
    {
        private ServiceEntity m_Entity = null;
        private bool m_Disabled = false;

        public ServiceTimer()
        {

        }

        public ServiceTimer(double interval)
            : base(interval)
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

        public ServiceEntity ServiceEntity
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
