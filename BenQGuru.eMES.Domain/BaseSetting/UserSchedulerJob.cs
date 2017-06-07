using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.BaseSetting
{
    #region UserSchedulerJob

    /// <summary>
	///	UserSchedulerJob
	/// </summary>
    [Serializable, TableMap("USER_SCHEDULER_JOBS", "JOB_NAME")]
	public class UserSchedulerJob : DomainObject
	{
		public UserSchedulerJob()
		{
		}		
					
		///<summary>
		///JobName
		///</summary>	
		[FieldMapAttribute("JOB_NAME", typeof(string), 30, false)]
		public string JobName;		
			
		///<summary>
		///JobSubName
		///</summary>	
		[FieldMapAttribute("JOB_SUBNAME", typeof(string), 30, true)]
		public string JobSubName;		
			
		///<summary>
		///JobCreator
		///</summary>	
		[FieldMapAttribute("JOB_CREATOR", typeof(string), 30, true)]
		public string JobCreator;		
			
		///<summary>
		///ClientID
		///</summary>	
		[FieldMapAttribute("CLIENT_ID", typeof(string), 64, true)]
		public string ClientID;		
			
		///<summary>
		///GlobalUID
		///</summary>	
		[FieldMapAttribute("GLOBAL_UID", typeof(string), 32, true)]
		public string GlobalUID;		
			
		///<summary>
		///PROGRAMOWNER
		///</summary>	
		[FieldMapAttribute("PROGRAM_OWNER", typeof(string), 4000, true)]
		public string ProgramOwner;		
			
		///<summary>
		///PROGRAMName
		///</summary>	
		[FieldMapAttribute("PROGRAM_NAME", typeof(string), 4000, true)]
		public string ProgramName;		
			
		///<summary>
		///JobType
		///</summary>	
		[FieldMapAttribute("JOB_TYPE", typeof(string), 16, true)]
		public string JobType;		
			
		///<summary>
		///JobAction
		///</summary>	
		[FieldMapAttribute("JOB_ACTION", typeof(string), 4000, true)]
		public string JobAction;		
			
		///<summary>
		///NUMBEROFARGUMENTS
		///</summary>	
		[FieldMapAttribute("NUMBER_OF_ARGUMENTS", typeof(int), 38, true)]
		public int NumberOfArguments;		
			
		///<summary>
		///SCHEDULEOWNER
		///</summary>	
		[FieldMapAttribute("SCHEDULE_OWNER", typeof(string), 4000, true)]
		public string ScheduleOwner;		
			
		///<summary>
		///SCHEDULEName
		///</summary>	
		[FieldMapAttribute("SCHEDULE_NAME", typeof(string), 4000, true)]
		public string ScheduleName;		
			
		///<summary>
		///SCHEDULEType
		///</summary>	
		[FieldMapAttribute("SCHEDULE_TYPE", typeof(string), 12, true)]
		public string ScheduleType;		
			
		///<summary>
		///STARTDate
		///</summary>	
		[FieldMapAttribute("START_DATE", typeof(DateTime), 8, true)]
		public DateTime StartDate;		
			
		///<summary>
		///REPEATINTERVAL
		///</summary>	
		[FieldMapAttribute("REPEAT_INTERVAL", typeof(string), 4000, true)]
		public string RepeatInterval;		
			
		///<summary>
		///EVENTQUEUEOWNER
		///</summary>	
		[FieldMapAttribute("EVENT_QUEUE_OWNER", typeof(string), 30, true)]
		public string EventQueueOwner;		
			
		///<summary>
		///EVENTQUEUEName
		///</summary>	
		[FieldMapAttribute("EVENT_QUEUE_NAME", typeof(string), 30, true)]
		public string EventQueueName;		
			
		///<summary>
		///EVENTQUEUEAGENT
		///</summary>	
		[FieldMapAttribute("EVENT_QUEUE_AGENT", typeof(string), 30, true)]
		public string EventQueueAgent;		
			
		///<summary>
		///EVENTCONDITION
		///</summary>	
		[FieldMapAttribute("EVENT_CONDITION", typeof(string), 4000, true)]
		public string EventCondition;		
			
		///<summary>
		///EVENTRule
		///</summary>	
		[FieldMapAttribute("EVENT_RULE", typeof(string), 65, true)]
		public string EventRule;		
			
		///<summary>
		///ENDDate
		///</summary>	
		[FieldMapAttribute("END_DATE", typeof(DateTime), 8, true)]
		public DateTime EndDate;		
			
		///<summary>
		///JobCLASS
		///</summary>	
		[FieldMapAttribute("JOB_CLASS", typeof(string), 30, true)]
		public string JobClass;		
			
		///<summary>
		///ENABLED
		///</summary>	
		[FieldMapAttribute("ENABLED", typeof(string), 5, true)]
		public string Enabled;		
			
		///<summary>
		///AUToDROP
		///</summary>	
		[FieldMapAttribute("AUTO_DROP", typeof(string), 5, true)]
		public string AutoDrop;		
			
		///<summary>
		///ResourceTARTABLE
		///</summary>	
		[FieldMapAttribute("RESTARTABLE", typeof(string), 5, true)]
		public string Restartable;		
			
		///<summary>
		///STATE
		///</summary>	
		[FieldMapAttribute("STATE", typeof(string), 15, true)]
		public string State;		
			
		///<summary>
		///JobPRIORITY
		///</summary>	
		[FieldMapAttribute("JOB_PRIORITY", typeof(int), 38, true)]
		public int JobPriority;		
			
		///<summary>
		///RUNCount
		///</summary>	
		[FieldMapAttribute("RUN_COUNT", typeof(int), 38, true)]
		public int RunCount;		
			
		///<summary>
		///MaxRUNS
		///</summary>	
		[FieldMapAttribute("MAX_RUNS", typeof(int), 38, true)]
		public int MaxRuns;		
			
		///<summary>
		///FAILURECount
		///</summary>	
		[FieldMapAttribute("FAILURE_COUNT", typeof(int), 38, true)]
		public int FailureCount;		
			
		///<summary>
		///MaxFAILUResource
		///</summary>	
		[FieldMapAttribute("MAX_FAILURES", typeof(int), 38, true)]
		public int MaxFailures;		
			
		///<summary>
		///RETryCount
		///</summary>	
		[FieldMapAttribute("RETRY_COUNT", typeof(int), 38, true)]
		public int RetryCount;		
			
		///<summary>
		///LASTSTARTDate
		///</summary>	
		[FieldMapAttribute("LAST_START_DATE", typeof(DateTime), 8, true)]
		public DateTime LastStartDate;		
			
		///<summary>
		///LASTRUNDURATION
		///</summary>	
        [FieldMapAttribute("LAST_RUN_DURATION", typeof(int), 38, true)]
        public int LastRunDuration;		
			
		///<summary>
		///NExTRUNDate
		///</summary>	
		[FieldMapAttribute("NEXT_RUN_DATE", typeof(DateTime), 8, true)]
		public DateTime NextRunDate;		
			
		///<summary>
		///SCHEDULELIMIT
		///</summary>	
        [FieldMapAttribute("SCHEDULE_LIMIT", typeof(int), 38, true)]
        public int ScheduleLimit;		
			
		///<summary>
		///MaxRUNDURATION
		///</summary>	
        [FieldMapAttribute("MAX_RUN_DURATION", typeof(int), 38, true)]
        public int MaxRunDuration;		
			
		///<summary>
		///LOGGINGLEVEL
		///</summary>	
		[FieldMapAttribute("LOGGING_LEVEL", typeof(string), 4, true)]
		public string LoggingLevel;		
			
		///<summary>
		///SToPONWINDOWCLOSE
		///</summary>	
		[FieldMapAttribute("STOP_ON_WINDOW_CLOSE", typeof(string), 5, true)]
		public string StopOnWindowClose;		
			
		///<summary>
		///INSTANCESTICKINESS
		///</summary>	
		[FieldMapAttribute("INSTANCE_STICKINESS", typeof(string), 5, true)]
		public string InstanceStickiness;		
			
		///<summary>
		///RAIsEEVENTS
		///</summary>	
		[FieldMapAttribute("RAISE_EVENTS", typeof(string), 4000, true)]
		public string RaiseEvents;		
			
		///<summary>
		///SYSTEM
		///</summary>	
		[FieldMapAttribute("SYSTEM", typeof(string), 5, true)]
		public string System;		
			
		///<summary>
		///JobWEIGHT
		///</summary>	
		[FieldMapAttribute("JOB_WEIGHT", typeof(int), 38, true)]
		public int JobWeight;		
			
		///<summary>
		///NLSENV
		///</summary>	
		[FieldMapAttribute("NLS_ENV", typeof(string), 4000, true)]
		public string NLSEnv;		
			
		///<summary>
		///SOURCE
		///</summary>	
		[FieldMapAttribute("SOURCE", typeof(string), 128, true)]
		public string Source;		
			
		///<summary>
		///DESTINATION
		///</summary>	
		[FieldMapAttribute("DESTINATION", typeof(string), 128, true)]
		public string Destination;		
			
		///<summary>
		///CompleteMENTS
		///</summary>	
		[FieldMapAttribute("COMMENTS", typeof(string), 240, true)]
		public string Comments;		
			
		///<summary>
		///FlagS
		///</summary>	
		[FieldMapAttribute("FLAGS", typeof(int), 38, true)]
		public int Flags;
	}
	
	#endregion
}
