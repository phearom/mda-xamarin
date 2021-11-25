using System.Text;
using System.Threading.Tasks;
using IPificationSDK;
using Xamarin.Forms;


[assembly: Dependency(typeof(xapp001.iOS.IPServiceImpl))]
namespace xapp001.iOS
{

    public class IPServiceImpl : IIPService
    {
        public IPServiceImpl()
        {
            InitIPConfiguration();
        }
        public void InitIPConfiguration()
        {
            IPConfiguration.Shared.CoverageServiceEndpoint = IPConstants.CoverageServiceEndpoint;
            IPConfiguration.Shared.AuthServiceEndpoint = IPConstants.AuthServiceEndpoint;
            IPConfiguration.Shared.ClientId = IPConstants.ClientId;
            IPConfiguration.Shared.RedirectUri = IPConstants.RedirectUri;
        }
        public Task<CoverageResult> CheckCoverage()
        {


            var tcs = new TaskCompletionSource<CoverageResult>();

            var coverageService = new IPificationSDK.CoverageService();
            coverageService.CallbackSuccess = (response) =>
            {
                System.Console.WriteLine("OnCoverageDidCompleted");
                System.Console.WriteLine(response.IsAvailable);
                var CvResult = new CoverageResult
                {
                    IsAvailable = response.IsAvailable,
                    OperatorCode = response.OperatorCode

                };
                tcs.SetResult(CvResult);
            };
            coverageService.CallbackFailed = (error) =>
            {
                System.Console.WriteLine("OnCoverageDidError");
                System.Console.WriteLine(error.ErrorMessage);
                var CvResult = new CoverageResult
                {
                    IsAvailable = false,
                    ErrorMessage = error.ErrorMessage
                };

                tcs.SetResult(CvResult);
            };

            coverageService.CheckCoverage();
            return tcs.Task;

            //var coverageCallback = new IPCoverageCallback
            //{
            //    OnCoverageDidComplete = (response) =>
            //    {
            //        System.Console.WriteLine("OnCoverageDidComplete");
            //        System.Console.WriteLine(response.IsAvailable);
            //        var CvResult = new CoverageResult
            //        {
            //            IsAvailable = response.IsAvailable
            //        };
            //        tcs.SetResult(CvResult);
            //    },
            //    OnCoverageDidError = (error) =>
            //    {
            //        System.Console.WriteLine("OnCoverageDidError");
            //        System.Console.WriteLine(error.ErrorMessage);
            //        var CvResult = new CoverageResult
            //        {
            //            IsAvailable = false,
            //            ErrorMessage = error.ErrorMessage
            //        };

            //        tcs.SetResult(CvResult);
            //    }
            //};
            //coverageService.RegisterCallbackWithCoverageCallback(coverageCallback);
            //coverageService.CheckCoverage();

            //return tcs.Task;


        }


        Task<AuthorizationResult> IIPService.DoAuthorization(string login_hint, string trx_id)
        {
            var tcs = new TaskCompletionSource<AuthorizationResult>();
            var authorizationService = new IPificationSDK.AuthorizationService();

            authorizationService.CallbackSuccess = (response) =>
            {
                System.Console.WriteLine("OnAuthDidCompleted");
                System.Console.WriteLine(response.Code);
                System.Console.WriteLine(response.State);
                System.Console.WriteLine(response.PlainResponse);
                var AuthResult = new AuthorizationResult();
                if (response.Code != null)
                {
                    AuthResult.IsSuccess = true;
                    AuthResult.Code = response.Code;
                    AuthResult.State = response.State;
                    AuthResult.FullResponse = response.PlainResponse;
                }
                else
                {
                    AuthResult.IsSuccess = false;
                }

                tcs.SetResult(AuthResult);
            };
            authorizationService.CallbackFailed = (error) =>
            {
                System.Console.WriteLine("OnAuthDidError");
                var AuthResult = new AuthorizationResult
                {
                    IsSuccess = false,
                    ErrorMessage = error.ErrorMessage
                };
                tcs.SetResult(AuthResult);
            };
            var authRe = new IPificationSDK.Builder();
            var state = IPConstants.ClientId + "f6675545a97f44cc795b8b8fccc12062" + login_hint + trx_id + IPConstants.RedirectUri;
            authRe.AddQueryParamWithKey("login_hint", login_hint);
            authRe.AddQueryParamWithKey("trx_id", trx_id);
            authRe.SetScopeWithValue("openid ip:phone_verify ip:mobile_id");
            authRe.SetStateWithValue(CreateMD5(state));

            var req = authRe.Build;
            authorizationService.DoAuthorization(req);
            return tcs.Task;

            //var authCallback = new IPAuthorizationCallback
            //{
            //    OnAuthDidComplete = (response) =>
            //    {
            //        System.Console.WriteLine("OnAuthDidComplete");
            //        System.Console.WriteLine(response.Code);
            //        var AuthResult = new AuthorizationResult();
            //        if (response.Code != null)
            //        {
            //            AuthResult.IsSuccess = true;
            //        }
            //        else
            //        {
            //            AuthResult.IsSuccess = false;
            //        }
            //        AuthResult.Code = response.Code;
            //        tcs.SetResult(AuthResult);
            //    },
            //    OnAuthDidError = (error) =>
            //    {
            //        System.Console.WriteLine("OnAuthDidError");
            //        var AuthResult = new AuthorizationResult
            //        {
            //            IsSuccess = false,
            //            ErrorMessage = error.ErrorMessage
            //        };
            //        tcs.SetResult(AuthResult);
            //    }
            //};
            //authorizationService.RegisterCallbackWithAuthCallback(authCallback);
            //var authRe = new IPificationSDK.Builder();
            //authRe.AddQueryParamWithKey("login_hint", login_hint);
            //var req = authRe.Build;

            //authorizationService.DoAuthorization(req);

            //return tcs.Task;
        }

        public void Dispose()
        {
            // do nothing
        }

        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }

}