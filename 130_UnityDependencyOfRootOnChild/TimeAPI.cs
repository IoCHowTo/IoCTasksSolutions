using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace UnityDependencyOfRootOnChild
{
    /// <summary>
    /// An interface for a service providing
    /// current time.
    /// </summary>
    public interface ITimeAPI
    {
        /// <summary>
        /// Gets the current time
        /// </summary>
        /// <returns>A task which upon completion will return current date and time</returns>
        Task<DateTime> GetNow(GetNowContext context);
    }

    /// <summary>
    /// Payload for <see cref="ITimeAPI.GetNow"/>
    /// </summary>
    public class GetNowContext
    {
        public ILog Log { get; private set; }

        public GetNowContext(ILog log)
        {
            Log = log;
        }
    }
    
    /// <summary>
    /// An online implementation of <see cref="ITimeAPI"/> using 
    /// http://www.timeapi.org
    /// </summary>
    public class TimeAPI : ITimeAPI
    {
        /// <summary>
        /// Constructor with resource intensive initialization
        /// </summary>
        public TimeAPI()
        {
            Startup();
        }

        public async Task<DateTime> GetNow(GetNowContext context)
        {
            var http = new HttpClient {BaseAddress = new Uri("http://www.timeapi.org")};
            http.DefaultRequestHeaders.UserAgent.Clear();
            http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/*"));

            var result = await http.GetAsync("utc/now");

            if (!result.IsSuccessStatusCode)
            {
                context.Log.Info("Cannot fetch value");

                throw new InvalidOperationException();
            }

            var r = await result.Content.ReadAsStringAsync();
            context.Log.Info("Fetched data {0}", r);
            return DateTime.Parse(r);
        }

        /// <summary>
        /// This method is to emulate a long initialization time.
        /// Any issue is not related to this one...
        /// </summary>
        private void Startup()
        {
            Thread.Sleep(10000);
        }
    }
}