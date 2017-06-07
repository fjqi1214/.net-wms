using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.IO;
using System.Configuration;
using System.Reflection;
using BenQGuru.eMES.SAPDataTransferInterface;

namespace BenQGuru.eMES.SAPDataTransferService
{
    public partial class Engine : ServiceBase
    {
        private FileSystemWatcher m_Watcher = null;
        private bool m_ServiceRunning = false;
        private List<ServiceTimer> serviceTimers = new List<ServiceTimer>();

        private Timer m_TimerChecker = null;
        private const double C_INTERVALOFCHECKER = 30 * 1000;  // half of one minute
        private const double C_DAILYINTERVAL = 24 * 60 * 60 * 1000;  // One day
        private List<ScheduledServiceTimer> scheduledServiceTimers = new List<ScheduledServiceTimer>();

        public Engine()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // For service
            this.PrepareTimers();
            StartWatcher();
            m_ServiceRunning = true;

            // for scheduled job
            this.PrepareScheduledTimers();
            this.CheckScheduledServices();
        }

        private void CheckScheduledServices()
        {
            if (m_TimerChecker == null)
            {
                m_TimerChecker = new Timer(C_INTERVALOFCHECKER);
                m_TimerChecker.AutoReset = true;
                m_TimerChecker.Elapsed += new ElapsedEventHandler(m_TimerChecker_Elapsed);
            }
            m_TimerChecker.Enabled = true;
            m_TimerChecker.Start();
            m_TimerChecker_Elapsed(m_TimerChecker, null);
        }

        private void PrepareScheduledTimers()
        {
            List<ScheduledServiceEntity> scheduledServiceEntities = ConfigurationManager.GetSection("ScheduledServiceEntities") as List<ScheduledServiceEntity>;
            try
            {
                lock (scheduledServiceTimers)
                {
                    scheduledServiceTimers = new List<ScheduledServiceTimer>();
                    foreach (ScheduledServiceEntity sse in scheduledServiceEntities)
                    {
                        scheduledServiceTimers.Add(CreateScheduledServiceTimer(sse));
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private ScheduledServiceTimer CreateScheduledServiceTimer(ScheduledServiceEntity sse)
        {
            ScheduledServiceTimer sst = new ScheduledServiceTimer(C_DAILYINTERVAL);
            sst.AutoReset = true;
            sst.ScheduledServiceEntity = sse;
            sst.Elapsed += new ElapsedEventHandler(sst_Elapsed);
            sst.Enabled = false;
            return sst;
        }

        protected void sst_Elapsed(object sender, ElapsedEventArgs e)
        {
            ScheduledServiceEntity entity = null;

            try
            {
                entity = ((ScheduledServiceTimer)sender).ScheduledServiceEntity;
                if (entity.WaitForRun == true)  // œ–÷√◊¥Ã¨≤≈ø…“‘Run
                {
                    Assembly assembly = Assembly.LoadFrom(entity.AssemblyPath);
                    Type type = assembly.GetType(entity.Type);
                    using (ICommand command = (ICommand)Activator.CreateInstance(type))
                    {
                        entity.WaitForRun = false;
                        command.NewTransactionCode();
                        ServiceResult sr = command.Run(RunMethod.Auto);
                        if (sr.Result == false)
                        {
                            EventLog.WriteEntry("Engine.sst_Elapsed", sr.Message + " Transaction Code:" + sr.TransactionCode, EventLogEntryType.Error);
                        }
                        entity.WaitForRun = true;
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Engine.sst_Elapsed", ex.Message, EventLogEntryType.Error);
                if (entity != null)
                {
                    entity.WaitForRun = true;
                }
            }
        }

        protected void m_TimerChecker_Elapsed(object sender, ElapsedEventArgs e)
        {
            int currentHour = DateTime.Now.Hour;
            int currentMinute = DateTime.Now.Minute;

            bool hasInitizedService = false;
            foreach (ScheduledServiceTimer sst in scheduledServiceTimers)
            {
                if (sst.ScheduledServiceEntity.StartHour == currentHour &&
                    sst.ScheduledServiceEntity.StartMinutes == currentMinute &&
                    sst.Enabled == false)
                {
                    try
                    {
                        sst.Enabled = true;
                        sst.Start();
                        sst_Elapsed(sst, null);
                    }
                    catch
                    {
                    }
                }

                if (sst.Enabled == false)
                {
                    hasInitizedService = true;
                }
            }
            if (hasInitizedService == false)
            {
                this.m_TimerChecker.Stop();
                this.m_TimerChecker.Enabled = false;
            }
        }

        protected override void OnStop()
        {
            SwitchTimers(false);
            EnableWatcher(false);
            m_ServiceRunning = false;

            // For Scheduled Service
            this.StopScheduldServices();
        }

        private void StopScheduldServices()
        {
            this.m_TimerChecker.Stop();
            this.m_TimerChecker.Enabled = false;

            if (scheduledServiceTimers != null)
            {
                foreach (ScheduledServiceTimer sst in scheduledServiceTimers)
                {
                    try
                    {
                        sst.Stop();
                        sst.Enabled = false;
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected override void OnPause()
        {
            SwitchTimers(false);
            m_ServiceRunning = false;

            this.StopScheduldServices();
        }

        protected override void OnContinue()
        {
            SwitchTimers(true);
            m_ServiceRunning = true;

            this.CheckScheduledServices();
        }


        #region FileSystemWatcher
        private void StartWatcher()
        {
            AppDomainSetup domain = AppDomain.CurrentDomain.SetupInformation;
            m_Watcher = new FileSystemWatcher(Path.GetDirectoryName(domain.ConfigurationFile), Path.GetFileName(domain.ConfigurationFile));
            m_Watcher.Changed += new FileSystemEventHandler(m_Watcher_Changed);
            EnableWatcher(true);
        }

        private void EnableWatcher(bool enabled)
        {
            m_Watcher.EnableRaisingEvents = enabled;
        }

        private void m_Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (m_ServiceRunning)
            {
                SwitchTimers(false);
            }
            lock (serviceTimers)
            {
                ResetTimers();
            }
            if (m_ServiceRunning)
            {
                SwitchTimers(true);
            }
        }
        #endregion

        #region ServiceEntity Timers
        private void PrepareTimers()
        {
            List<ServiceEntity> serviceEntities = ConfigurationManager.GetSection("ServiceEntities") as List<ServiceEntity>;
            try
            {
                lock (serviceTimers)
                {
                    serviceTimers = new List<ServiceTimer>();
                    foreach (ServiceEntity se in serviceEntities)
                    {
                        serviceTimers.Add(CreateTimer(se));
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ResetTimers()
        {
            List<ServiceEntity> serviceEntities = ConfigurationManager.GetSection("ServiceEntities") as List<ServiceEntity>;
            try
            {
                foreach (ServiceTimer timer in serviceTimers)
                {
                    timer.Disabled = true;
                }
                foreach (ServiceEntity se in serviceEntities)
                {
                    ServiceTimer existTimer = serviceTimers.Find(new Predicate<ServiceTimer>(delegate(ServiceTimer service)
                    {
                        if (string.Compare(service.ServiceEntity.Key, se.Key, true) == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }));

                    if (existTimer == null)
                    {
                        serviceTimers.Add(CreateTimer(se));
                    }
                    else
                    {
                        existTimer.Interval = se.Interval * 1000;
                        existTimer.ServiceEntity = se;
                        existTimer.Disabled = false;
                    }
                }
                for (int i = serviceTimers.Count - 1; i >= 0; i--)
                {
                    if (serviceTimers[i].Disabled)
                    {
                        serviceTimers.Remove(serviceTimers[i]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SwitchTimers(bool enabled)
        {
            if (serviceTimers != null)
            {
                foreach (ServiceTimer timer in serviceTimers)
                {
                    try
                    {
                        timer.Enabled = enabled;
                        if (timer.Enabled)
                        {
                            this.timer_Elapsed(timer, null);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private ServiceTimer CreateTimer(ServiceEntity entity)
        {
            ServiceTimer timer = new ServiceTimer(entity.Interval * 1000);
            timer.ServiceEntity = entity;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
            timer.Start();

            this.timer_Elapsed(timer, null);

            return timer;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ServiceEntity entity = null;

            try
            {
                entity = ((ServiceTimer)sender).ServiceEntity;
                if (entity.Running == false)
                {
                    Assembly assembly = Assembly.LoadFrom(entity.AssemblyPath);
                    Type type = assembly.GetType(entity.Type);
                    using (ICommand command = (ICommand)Activator.CreateInstance(type))
                    {
                        entity.Running = true;
                        command.NewTransactionCode();
                        ServiceResult sr = command.Run(RunMethod.Auto);
                        if (sr.Result == false)
                        {
                            EventLog.WriteEntry("Engine.timer_Elapsed", sr.Message + " Transaction Code:" + sr.TransactionCode, EventLogEntryType.Error);
                        }
                        entity.Running = false;
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Engine.timer_Elapsed", ex.Message, EventLogEntryType.Error);
                if (entity != null)
                {
                    entity.Running = false;
                }
            }
        }
        #endregion

    }
}
