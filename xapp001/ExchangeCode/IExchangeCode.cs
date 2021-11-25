using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace xapp001.ExchangeCode
{
    public interface IExchangeCodeApi
    {
        [Post("/getresult.aspx")]
        Task<ApiResponse<CodeResult>> PostToken([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> request);
    }


    public class CodeResult
    {

        public string error { get; set; }

        public string error_description { get; set; }

        public string phone_verified { get; set; }

        public string mobile_id { get; set; }
    }
}
