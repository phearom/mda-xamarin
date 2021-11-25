using System;
using System.Threading.Tasks;

namespace xapp001
{
    public interface IIPService
    {
        Task<CoverageResult> CheckCoverage();
        Task<AuthorizationResult> DoAuthorization(string login_hint, string trx_id);
        void Dispose();
    }
}
