using System;
using System.Collections;

namespace UserControl
{
	public enum MessageType 
	{
		Normal, 
		Debug, 
		Error,
		DisplayError,	// 仅显示的时候用Error状态，在IsSuccess中返回True
		Success,
		Input,
		Performance,
		Data, //一个指令操作中只能有一个DATA返回
        DCTData, //DCT数据区域显示的数据
        DCTClear //先清屏,再显示
	}

	public class MessageTypeCollection:ArrayList
	{
		public MessageTypeCollection():base()
		{
		}
		public  int Add(MessageType value)
		{
			this.Add(value); 
			return this.Count ;
		}
		public  MessageType Objects(int index)
		{
			return (MessageType)this[index];
		}
	}

	public class Message
	{
		public string id = string.Empty; 
		public MessageType Type = MessageType.Normal;
		public string  Body = string.Empty ;
		public object[] Values = null;
		public System.Exception  Exception = null;
        public int LineNo = 5;//用于DCT上显示行号
		public Message()
		{
		}	

		public Message( MessageType type, string body , object[] values)
		{
			this.Type = type;
			this.Body  = body;
			this.Values =values;
		}	

		public Message(MessageType type,string body):this(type,body, null)
		{
		}

        public Message(int lineno, MessageType type, string body)
            : this(type, body, null)
        {
            this.LineNo = lineno;
        }

		public Message(string body):this(MessageType.Normal, body, null)
		{
		}

		public Message(System.Exception exception):this(MessageType.Error, "", null)
		{
			this.Exception = exception;
			this.Type = MessageType.Error;

		}

		/*public MessageEventArgs(string body):this(string.Empty, body)
		{
		}*/

		public Message Clone()
		{
			Message messageEventArgs = new Message();
			messageEventArgs.Type    = this.Type;
			messageEventArgs.Body	= this.Body;
			messageEventArgs.Values  = this.Values;
			messageEventArgs.Exception  = this.Exception;		

			return messageEventArgs;
		}
		public string Debug()
		{
			string s=this.Type+";"+this.Body+";";
			if (Exception !=null)
			{
				s=s+Exception.Message+";"+Exception.Source+";"+Exception.StackTrace+";" ;
				if (Exception.InnerException!=null)
					s=s	+Exception.InnerException.Message+";"+Exception.Source+";"+Exception.StackTrace+";";
			}
			if (this.Values !=null)
			{
				for (int i=0;i<this.Values.Length;i++)
				{
					s=s+ this.Values[i].ToString();
				}
			}
			return s+"。";
		}
		public override string ToString()
		{
			string s=this.Type+";"+this.Body+";";
			if (Exception !=null)
			{
				s=s+Exception.Message+";"+Exception.TargetSite+";";
				if (Exception.InnerException!=null)
					s=s	+Exception.InnerException.Message+";"+Exception.TargetSite+";";
			}
			return s+"。";
		}


	}

	/// <summary>
	/// 指令操作产生的消息集合
	/// 一个集合中 DATA只能出现一个
	/// </summary>
	public class Messages
	{
		private ArrayList messageList;
		public Messages()
		{
			messageList=new ArrayList();
		}
		public  int Add(Message value)
		{
			if (value.Type ==MessageType.Data )
				if (GetData()!=null)
					DeleteData();
					//throw new Exception("$Error_Messages_Data_Repeated");
			int i= messageList.Add(value); 
			return i;
		}
		public Message GetData()
		{
			for (int i=0;i<messageList.Count;i++ )
			{
				if (this.Objects(i).Type ==MessageType.Data )
					return this.Objects(i);
			}
			return null;
		}

		public void DeleteData()
		{
			for (int i=0;i<messageList.Count;i++ )
			{
				if (this.Objects(i).Type ==MessageType.Data )
					this.messageList.RemoveAt(i);
			}
		}
		public int Count()
		{
			return this.messageList.Count;
		}

		public  Message Objects(int index)
		{
			return (Message)messageList[index];
		}
		public bool IsSuccess()
		{
			if (messageList.Count==0)
				return true;

			for (int i=0;i<messageList.Count;i++ )
			{
				if (this.Objects(i).Type ==MessageType.Error )
					return false;
			}
			return true;
		}
		public string Debug()
		{
			string s="";
			for (int i=0;i<messageList.Count;i++ )
			{
				if ((this.Objects(i).Type ==MessageType.Debug)
					||(this.Objects(i).Type ==MessageType.Error)
                    || (this.Objects(i).Type == MessageType.Success)
					||(this.Objects(i).Type ==MessageType.Data)
					||(this.Objects(i).Type ==MessageType.Performance)
					)
				{
					s=s+this.Objects(i).Debug();
				}
			}
			return s;
		}
		public string functionList()
		{
			string s="";
			for (int i=0;i<messageList.Count;i++ )
			{
				if ((this.Objects(i).Type ==MessageType.Debug)
					||(this.Objects(i).Type ==MessageType.Error)
                    || (this.Objects(i).Type == MessageType.Success)
					)
				{
					s=s+this.Objects(i).ToString();
				}
			}
			return s;
		}
		public string OutPut()
		{
			string s="";
			for (int i=0;i<messageList.Count;i++ )
			{
				if ((this.Objects(i).Type ==MessageType.Error)
                    || (this.Objects(i).Type == MessageType.Success)
					)
				{
					if (this.Objects(i).Body==string.Empty)
					{
						if (this.Objects(i).Exception==null)
						{
							s=s+"Exception.Message not eixt!!";
						}
						else
						{
							s=s+this.Objects(i).Exception.Message;
						}
					}
					else
						s=s+this.Objects(i).Body;
				}
			}
			return s;
		}

		public override string ToString()
		{
			string s="";
			for (int i=0;i<messageList.Count;i++ )
			{
				s=s+this.Objects(i).ToString();
			}
			return s;
		}
		public void AddMessages(Messages messages)
		{
			for (int i=0;i<messages.Count() ;i++)
			{
				this.Add(messages.Objects(i));
			}
		}

		public void ClearMessages()
		{
			this.messageList.Clear();
		}

		/// <summary>
		/// 将Error类型的Message，转换为DisplayError
		/// </summary>
		public void IgnoreError()
		{
			for (int i = 0; i < messageList.Count; i++)
			{
				Message msg = (Message)messageList[i];
				if (msg.Type == MessageType.Error)
					msg.Type = MessageType.DisplayError;
				messageList[i] = msg;
			}
		}

	}


}
