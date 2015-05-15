using System;

namespace Avalara.AvaTax.Adapter
{
	/// <summary>
	/// Transaction details to store in log file
	/// </summary>
	internal class Transaction
	{
		public Transaction()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Custom constructor
		/// </summary>
		/// <param name="service">Name of the web service (e.g. TaxSvc)</param>
		/// <param name="operation">Operation name (e.g. GetTax)</param>
		public Transaction(string service, string operation)
		{
			serviceName = service;
			this.operation = operation;			
		}

		#region Properties

		/// <summary>
		/// Unique id for this transaction
		/// </summary>
		public string TransactionId
		{
			get { return transactionId; }
			set { transactionId = value; }
		}

		/// <summary>
		/// Client Timestamp
		/// </summary>
		public DateTime ClientTimestamp
		{
			get { return clientTimestamp; }
			set { clientTimestamp = value; }
		}

		/// <summary>
		/// AccountId of caller
		/// </summary>
		public int AccountId
		{
			get { return accountId; }
			set { accountId = value; }
		}

		/// <summary>
		/// Client machine name (from Profile)
		/// </summary>
		public string MachineName
		{
			get { return machineName; }
			set { machineName = (value != null) ? value.ToUpper() : ""; }
		}

		/// <summary>
		/// Web service name (e.g. TaxSvc)
		/// </summary>
		public string ServiceName
		{
			get { return serviceName; }
			set { serviceName = value; }
		}

		/// <summary>
		/// Operation name (e.g. GetTax)
		/// </summary>
		public string Operation
		{
			get { return operation; }
			set { operation = value; }
		}

		/// <summary>
		/// DocumentCode or other entity reference code depending on the operation
		/// </summary>
		public string ReferenceCode
		{
			get { return referenceCode; }
			set { referenceCode = value; }
		}

		/// <summary>
		/// Duration of the transaction in milliseconds
		/// </summary>
		public double ClientDuration
		{
			get { return clientDuration; }
			set { clientDuration = value; }
		}		

		/// <summary>
		/// Result code of this transaction (Success, Warning, Error, Exception)
		/// </summary>
		public byte SeverityLevelId
		{
			get { return severityLevelId; }
            set { severityLevelId = ++value; }
		}

		/// <summary>
		/// ErrorMessage, if any
		/// </summary>
		public string ErrorMessage
		{
			get { return errorMessage; }
			set { errorMessage = value; }
		}
		#endregion Properties

		/// <summary>
		/// Logs the transaction details to log file
		/// </summary>
		public void Log()
		{		
			try
			{
				string severityLevel = "NONE";
				Type type = typeof(LogLevel);
				if(severityLevelId >= (byte)LogLevel.DEBUG && severityLevelId <= (byte)LogLevel.FATAL)
				{
					severityLevel = Enum.GetNames(type)[severityLevelId];
				}
				
				LogLevel currentSeverityLevel = AvaLoggerConfiguration.ConvertToLogLevel(severityLevel);

                if ((AvaLoggerConfiguration.CurrentLogLevel <= currentSeverityLevel) && (currentSeverityLevel != LogLevel.NONE))
				{
					string data = string.Format("{0},{1},{2},{3},{4},{5},\"{6}\",{7},{8},\"{9}\"",
								  transactionId, clientTimestamp, accountId, machineName, serviceName, 
								  operation, referenceCode, clientDuration, severityLevelId, errorMessage);

					Utilities.FileWriter(AvaLoggerConfiguration.TransactionsFileName, data);
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.WriteLine("An error occured while logging transaction details. "+ ex.Message);
			}
		}

		private string transactionId = string.Empty;
		private DateTime clientTimestamp = DateTime.UtcNow;
		private int accountId = 0;
		private string machineName = string.Empty;
		private string serviceName = string.Empty;
		private string operation = string.Empty;
		private string referenceCode = string.Empty;
		private double clientDuration = 0;
		private byte severityLevelId = (byte)SeverityLevel.Success;
		private string errorMessage = string.Empty;
	}
}

