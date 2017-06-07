using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.BaseSetting
{

    public class JobFacade : MarshalByRefObject
    {
        private FacadeHelper _helper = null;
        private IDomainDataProvider _domainDataProvider = null;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        public JobFacade()
        {
        }

        public JobFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        #region UserSchedulerJob

        public UserSchedulerJob CreateNewUserSchedulerJob()
        {
            return new UserSchedulerJob();
        }

        public void AddUserSchedulerJob(UserSchedulerJob userSchedulerJob)
        {
            string command = string.Empty;
            command += "BEGIN ";
            command += "dbms_scheduler.create_job(job_name => '{0}', ";
            command += "job_type => '{1}', ";
            command += "job_action => '{2}', ";
            command += "start_date => sysdate, ";
            command += "repeat_interval => '{3}', ";
            command += "comments => '{4}'); ";

            command += "END; ";
            command = string.Format(command, 
                userSchedulerJob.JobName.Trim().ToUpper(),
                userSchedulerJob.JobType.Trim().ToUpper(),
                userSchedulerJob.JobAction.Trim().ToUpper(),
                userSchedulerJob.RepeatInterval.Trim().ToUpper(),
                userSchedulerJob.Comments.Trim().ToUpper());

            this.DataProvider.CustomExecute(new SQLCondition(command));
        }

        public void DeleteUserSchedulerJob(UserSchedulerJob userSchedulerJob)
        {
            string command = string.Empty;
            command += "BEGIN ";
            command += "dbms_scheduler.drop_job('{0}', TRUE); ";
            command += "END; ";
            command = string.Format(command, userSchedulerJob.JobName.Trim().ToUpper());

            this.DataProvider.CustomExecute(new SQLCondition(command));
        }

        public void DeleteUserSchedulerJob(UserSchedulerJob[] userSchedulerJob)
        {
            if (userSchedulerJob != null && userSchedulerJob.Length > 0)
            {
                foreach (UserSchedulerJob job in userSchedulerJob)
                {
                    DeleteUserSchedulerJob(job);
                }
            }
        }

        public object GetUserSchedulerJob(string jobName)
        {
            string sql = "";
            sql += "SELECT job_name, job_type, job_action, TO_DATE(TO_CHAR(start_date, 'YYYY-MM-DD HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS') AS start_date, ";
            sql += "repeat_interval, enabled, state, TO_DATE(TO_CHAR(last_start_date, 'YYYY-MM-DD HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS') AS last_start_date, ";
            sql += "ROUND(EXTRACT(SECOND FROM last_run_duration), 0) AS last_run_duration, TO_DATE(TO_CHAR(next_run_date, 'YYYY-MM-DD HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS') AS next_run_date, comments ";
            sql += "FROM user_scheduler_jobs ";
            sql += "WHERE job_name = '{0}' ";
            sql += "ORDER BY job_name ";
            sql = string.Format(sql, jobName.Trim().ToUpper());

            object[] list = this.DataProvider.CustomQuery(typeof(UserSchedulerJob), new SQLCondition(sql));
            if (list != null && list.Length > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public object[] QueryUserSchedulerJob(int inclusive, int exclusive)
        {
            string sql = "";
            sql += "SELECT job_name, job_type, job_action, TO_DATE(TO_CHAR(start_date, 'YYYY-MM-DD HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS') AS start_date, ";
            sql += "repeat_interval, enabled, state, TO_DATE(TO_CHAR(last_start_date, 'YYYY-MM-DD HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS') AS last_start_date, ";
            sql += "ROUND(EXTRACT(SECOND FROM last_run_duration), 0) AS last_run_duration, TO_DATE(TO_CHAR(next_run_date, 'YYYY-MM-DD HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS') AS next_run_date, comments ";
            sql += "FROM user_scheduler_jobs ";
            sql += "ORDER BY job_name";

            return this.DataProvider.CustomQuery(typeof(UserSchedulerJob), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryUserSchedulerJobCount()
        {
            string sql = "SELECT COUNT(*) FROM USER_SCHEDULER_JOBS ";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public void EnableUserSchedulerJob(string jobName)
        {
            string command = string.Empty;
            command += "BEGIN ";
            command += "dbms_scheduler.enable('{0}'); ";
            command += "END; ";
            command = string.Format(command, jobName.Trim().ToUpper());

            this.DataProvider.CustomExecute(new SQLCondition(command));
        }

        public void DisableUserSchedulerJob(string jobName)
        {
            string command = string.Empty;
            command += "BEGIN ";
            command += "dbms_scheduler.disable('{0}'); ";
            command += "END; ";
            command = string.Format(command, jobName.Trim().ToUpper());

            this.DataProvider.CustomExecute(new SQLCondition(command));
        }

        public void StopUserSchedulerJob(string jobName)
        {
            string command = string.Empty;
            command += "BEGIN ";
            command += "dbms_scheduler.stop_job('{0}', TRUE); ";
            command += "END; ";
            command = string.Format(command, jobName.Trim().ToUpper());

            this.DataProvider.CustomExecute(new SQLCondition(command));
        }

        public void RunUserSchedulerJob(string jobName)
        {
            string command = string.Empty;
            command += "BEGIN ";
            command += "dbms_scheduler.run_job('{0}', TRUE); ";
            command += "END; ";
            command = string.Format(command, jobName.Trim().ToUpper());

            this.DataProvider.CustomExecute(new SQLCondition(command));
        }

        #endregion

        #region JobLog

        public JobLog CreateNewJobLog()
        {
            return new JobLog();
        }

        public void AddJobLog(JobLog jobLog)
        {
            this._helper.AddDomainObject(jobLog);
        }

        public void DeleteJobLog(JobLog jobLog)
        {
            this._helper.DeleteDomainObject(jobLog);
        }

        public void UpdateJobLog(JobLog jobLog)
        {
            this._helper.UpdateDomainObject(jobLog);
        }

        public object GetJobLog(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(JobLog), new object[] { serial });
        }

        public object[] QueryJobLog(string jobID, int date, int inclusive, int exclusive)
        {
            string sql = "SELECT {0} FROM tbljoblog WHERE 1 = 1 ";
            sql += GetSqlConditionForQueryJobLog(jobID, date);
            
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(JobLog)), jobID.Trim().ToUpper());

            return this.DataProvider.CustomQuery(typeof(JobLog), new PagerCondition(sql, "serial DESC",inclusive, exclusive));
        }

        public int QueryJobLogCount(string jobID, int date)
        {
            string sql = "SELECT COUNT(*) FROM tbljoblog WHERE 1 = 1 ";
            sql += GetSqlConditionForQueryJobLog(jobID, date);

            sql = string.Format(sql);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetSqlConditionForQueryJobLog(string jobID, int date)
        {
            string sql = string.Empty;

            if (jobID.Trim().Length > 0)
            {
                sql += string.Format("AND UPPER(jobid) = '{0}' ", jobID.Trim().ToUpper());
            }

            if (date > 0)
            {
                sql += string.Format("AND TO_CHAR(startdatetime, 'YYYYMMDD') = '{0}' ", date.ToString());
            }

            return sql;
        }

        #endregion
    }
}
