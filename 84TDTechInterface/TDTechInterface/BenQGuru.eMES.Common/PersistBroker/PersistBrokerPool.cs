using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using BenQGuru.eMES.Common.Config;

namespace BenQGuru.eMES.Common.PersistBroker
{
    public class PersistBrokerPool
    {
        DomainSetting _CurrentDomainSetting = Config.ConfigSection.Current.DomainSetting;

        private static PersistBrokerPool _Pool = null;
        private Dictionary<int, IPersistBroker> _PoolingDictionary = new Dictionary<int, IPersistBroker>();
        private CultureInfo _CultureInfo;

        private object _Lock = new object();

        public static PersistBrokerPool Pool(CultureInfo cultureInfo)
        {
            if (_Pool == null)
            {
                _Pool = new PersistBrokerPool();
                _Pool._CultureInfo = cultureInfo;
            }
            return _Pool;
        }

        public void InitPool()
        {
            if (_CurrentDomainSetting.IsPool != 0)
            {
                _PoolingDictionary.Clear();
                for (int i = 0; i < _CurrentDomainSetting.PoolSize; i++)
                {
                    _PoolingDictionary.Add(i, GetPersistBroker());
                }
            }
        }

        public void ClearPool()
        {
            if (_CurrentDomainSetting.IsPool != 0)
            {
                _PoolingDictionary.Clear();
            }
        }

        public IPersistBroker RetriveFromPool()
        {
            return this.RetriveFromPool(_CurrentDomainSetting.IsPool != 0);
        }

        public IPersistBroker RetriveFromPool(bool usePool)
        {
            if (usePool)
            {
                lock (_Pool)
                {
                    if (_PoolingDictionary.Count == 0)
                    {
                        _Pool.InitPool();
                    }

                    foreach (IPersistBroker persistBroker in _PoolingDictionary.Values)
                    {
                        if (!persistBroker.IsInTransaction)
                        {
                            return persistBroker;
                        }
                    }

                    _PoolingDictionary.Add(_PoolingDictionary.Count, GetPersistBroker());

                    return (IPersistBroker)_PoolingDictionary[_PoolingDictionary.Count - 1];
                }
            }
            else
            {
                return GetPersistBroker();
            }
        }

        public IPersistBroker RetriveFromPool(string connectString)
        {
            return new OLEDBPersistBroker(connectString, _CultureInfo);
        }

        private IPersistBroker GetPersistBroker()
        {
            if (_CurrentDomainSetting.PersistBrokerType == "ODPPersistBroker")
            {
                return new ODPPersistBroker(_CurrentDomainSetting.GetSelectedConnectString(), _CultureInfo);
            }
            else
            {
                return new OLEDBPersistBroker(_CurrentDomainSetting.GetSelectedConnectString(), _CultureInfo);
            }
        }
    }
}
