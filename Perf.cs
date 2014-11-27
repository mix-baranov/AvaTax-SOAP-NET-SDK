using System;
using Avalara.AvaTax.Adapter.TaxService;
using Avalara.AvaTax.Adapter.AddressService;
using Avalara.AvaTax.Adapter.Utililty;
using Avalara.AvaTax.Adapter.AccountService;

namespace Avalara.AvaTax.Adapter
{
	/// <summary>
	/// Provides a mechanism for recording the span of time a call or block of code takes to execute.
	/// </summary>
	/// <example>
	/// <code>
	/// Perf monitor = new Perf();
	/// monitor.Start();
	/// //insert executing code here
	/// monitor.Stop();
	/// Console.WriteLine(monitor.ElapsedSeconds());
	/// </code>
	/// </example>
	internal class Perf
	{
		DateTime _start;
		DateTime _end;
		bool _running;
		private AvaLogger _avaLog=AvaLogger.GetLogger();
		/// <summary>
		/// Initializes a new intance of the Perf class.
		/// </summary>
		/// <remarks>
		/// Implicitly calls the <see cref="Perf.Started"/> method. However, for greater precision, it is recommended that
		/// the executing code explicitly call the <see cref="Perf.Started"/> method immediately prior to the code that is 
		/// to be monitored.
		/// </remarks>
		public Perf()
		{
		}

		/// <summary>
		/// The <see cref="DateTime"/> at which the monitor was started.
		/// </summary>
		public DateTime Started
		{
			get { return _start; }
		}

		/// <summary>
		/// The <see cref="DateTime"/> at which the monitor was stopped.
		/// </summary>
		public DateTime Stopped
		{
			get { return _end; }
		}

		/// <summary>
		/// Starts the monitoring clock.
		/// </summary>
		public void Start()
		{
			_running = true;
			_start = DateTime.Now;

		}

		/// <summary>
		/// Stops the monitoring clock.
		/// </summary>
		public void Stop(TaxSvc svc,string transactionId)
		{
			_end = DateTime.Now;

			if (!_running)
			{
				throw new ApplicationException("An attempt was made to stop the performance timekeeper that was not running. Call the Start method prior to calling Stop method.");
			}
			try
			{
				//call Ping Method and send ClientMetric
                svc.Ping(Utilities.BuildAuditMetrics("", transactionId, _end.Subtract(_start).Milliseconds));
			}
			catch(Exception ex)
			{
				_avaLog.Error(string.Format("Error sending ClientMetrics: {0}", ex.Message)); 
			}
			_running = false;
		}


        /// <summary>
        /// Stops the monitoring clock.
        /// </summary>
        public void Stop(TaxSvc svc, ref Proxies.TaxSvcProxy.ProxyGetTaxResult result)
        {
            _end = DateTime.Now;

            if (!_running)
            {
                throw new ApplicationException("An attempt was made to stop the performance timekeeper that was not running. Call the Start method prior to calling Stop method.");
            }
            try
            {
                if (Utilities.HasClientMetricMessage(result.Messages))
                {
                    //call Ping Method and send ClientMetric
                    //Sample Entry - ClientMetrics:17849,ClientDuration,1500
                    svc.Ping(Utilities.BuildAuditMetrics("", result.TransactionId, _end.Subtract(_start).Milliseconds));
                }
            }
            catch (Exception ex)
            {
                _avaLog.Error(string.Format("Error sending ClientMetrics: {0}", ex.Message));
            }
            _running = false;
        }

        /// <summary>
        /// Stops the monitoring clock.
        /// </summary>
        public void Stop(AccountSvc svc, ref Proxies.AccountSvcProxy.ProxyCompanyFetchResult result)
        {
            _end = DateTime.Now;

            if (!_running)
            {
                throw new ApplicationException("An attempt was made to stop the performance timekeeper that was not running. Call the Start method prior to calling Stop method.");
            }
            try
            {
                if (Utilities.HasClientMetricMessage(result.Messages))
                {
                    //call Ping Method and send ClientMetric
                    //Sample Entry - ClientMetrics:17849,ClientDuration,1500
                    svc.Ping(Utilities.BuildAuditMetrics("", result.TransactionId, _end.Subtract(_start).Milliseconds));
                }
            }
            catch (Exception ex)
            {
                _avaLog.Error(string.Format("Error sending ClientMetrics: {0}", ex.Message));
            }
            _running = false;
        }

        /// <summary>
        /// Stops the monitoring clock.
        /// </summary>
        public void Stop(AccountSvc svc, ref Proxies.AccountSvcProxy.ProxyNexusFetchResult result)
        {
            _end = DateTime.Now;

            if (!_running)
            {
                throw new ApplicationException("An attempt was made to stop the performance timekeeper that was not running. Call the Start method prior to calling Stop method.");
            }
            try
            {
                if (Utilities.HasClientMetricMessage(result.Messages))
                {
                    //call Ping Method and send ClientMetric
                    //Sample Entry - ClientMetrics:17849,ClientDuration,1500
                    svc.Ping(Utilities.BuildAuditMetrics("", result.TransactionId, _end.Subtract(_start).Milliseconds));
                }
            }
            catch (Exception ex)
            {
                _avaLog.Error(string.Format("Error sending ClientMetrics: {0}", ex.Message));
            }
            _running = false;
        }

		/// <summary>
		/// Stops the monitoring clock.
		/// </summary>
		public void Stop(AddressSvc svc,ref Avalara.AvaTax.Adapter.Proxies.AddressSvcProxy.ProxyValidateResult result)
		{
			_end = DateTime.Now;

			if (!_running)
			{
				throw new ApplicationException("An attempt was made to stop the performance timekeeper that was not running. Call the Start method prior to calling Stop method.");
			}
			
			try
			{							
				if(Utilities.HasClientMetricMessage(result.Messages))
				{					
					//call Ping Method and send ClientMetric
					//Sample Entry - ClientMetrics:17849,ClientDuration,1500
                    svc.Ping(Utilities.BuildAuditMetrics("", result.TransactionId, _end.Subtract(_start).Milliseconds));
				}
			}
			catch(Exception ex)
			{
				_avaLog.Error(string.Format("Error sending ClientMetrics: {0}", ex.Message)); 
			}

			_running = false;
		}

		public void Stop()
		{
			_end = DateTime.Now;

			if (!_running)
			{
				throw new ApplicationException("An attempt was made to stop the performance timekeeper that was not running. Call the Start method prior to calling Stop method.");
			}
			_running = false;
		}


		/// <summary>
		/// The number of fractional hours elapsed while monitoring.
		/// </summary>
		/// <returns>The number of hours as a fractional number.</returns>
		public double ElapsedHours()
		{
			return this.ElapsedMinutes() / 60;
		}

		/// <summary>
		/// The number of fractional hours elapsed while monitoring.
		/// </summary>
		/// <param name="precision">The decimal precision desired.</param>
		/// <returns>The number of hours expressed as a fractional number rounded to a specified precision.</returns>
		public double ElapsedHours(int precision)
		{
			double hours = this.ElapsedHours();
			return Math.Round(hours, precision);
		}

		/// <summary>
		/// The number of fractional minutes elapsed while monitoring.
		/// </summary>
		/// <returns>The number of minutes as a fractional number.</returns>
		public double ElapsedMinutes()
		{
			return this.ElapsedSeconds() / 60;
		}

		/// <summary>
		/// The number of fractional minutes elapsed while monitoring.
		/// </summary>
		/// <param name="precision">The decimal precision desired.</param>
		/// <returns>The number of minutes expressed as a fractional number rounded to a specified precision.</returns>
		public double ElapsedMinutes(int precision)
		{
			double minutes = this.ElapsedMinutes();
			return Math.Round(minutes, precision);
		}

		/// <summary>
		/// The number of fractional seconds elapsed while monitoring.
		/// </summary>
		/// <returns>The number of seconds as a fractional number.</returns>
		public double ElapsedSeconds()
		{
			return this.ElapsedTimeSpan().TotalSeconds;
		}

		/// <summary>
		/// The number of fractional seconds elapsed while monitoring.
		/// </summary>
		/// <param name="precision">The decimal precision desired.</param>
		/// <returns>The number of seconds expressed as a fractional number rounded to a specified precision.</returns>
		public double ElapsedSeconds(int precision)
		{
			double seconds = this.ElapsedSeconds();
			return Math.Round(seconds, precision);
		}

		/// <summary>
		/// The span of time elapsed between <see cref="Perf.Start"/> and <see cref="Perf.Stop()"/>;
		/// </summary>
		/// <returns>The time elapsed expressed as a <see cref="TimeSpan"/> object.</returns>
		/// <remarks>
		/// <seealso cref="Perf.ElapsedSeconds()"/><seealso cref="Perf.ElapsedMinutes()"/><seealso cref="Perf.ElapsedHours()"/>
		/// </remarks>
		public TimeSpan ElapsedTimeSpan()
		{
			if (_running)
			{
				throw new ApplicationException("An attempt was made to determine the time elapsed without first stopping the performance timekeeper. Call the Stop method prior to accessing this member.");
			} 
			else
			{
				return (_end.Subtract(_start));
			}
		}
	}
}
