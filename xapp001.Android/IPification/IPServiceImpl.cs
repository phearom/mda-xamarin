using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Util;
using Com.Ipification.Mobile.Sdk.Android;
using Com.Ipification.Mobile.Sdk.Android.Request;
using Xamarin.Forms;
using xapp001;
using xapp001.Droid;

[assembly: Dependency(typeof(DemoApplication.Droid.IPServiceImpl))]
namespace DemoApplication.Droid
{

    public class IPServiceImpl : IIPService
    {
        readonly Context context = Android.App.Application.Context;
        public IPServiceImpl()
        {
            InitConfiguration();
        }

        private void InitConfiguration()
        {
            IPConfiguration.Instance.CoverageEndpoint = Android.Net.Uri.Parse(IPConstants.CoverageServiceEndpoint);
            IPConfiguration.Instance.AuthorizationEndpoint = Android.Net.Uri.Parse(IPConstants.AuthServiceEndpoint);
            IPConfiguration.Instance.ClientId = IPConstants.ClientId;
            IPConfiguration.Instance.RedirectUri = Android.Net.Uri.Parse(IPConstants.RedirectUri);
        }

        public Task<CoverageResult> CheckCoverage()
        {
            var tcs = new TaskCompletionSource<CoverageResult>();
            var service = new CellularService(context);
            var coverageCallback = new IPCoverageCallback
            {
                OnCoverageDidComplete = (response) =>
                {
                    Log.Info("IPServiceImpl", "coverage result: " + response.IsAvailable);
                    var CvResult = new CoverageResult
                    {
                        IsAvailable = response.IsAvailable,
                        OperatorCode = response.OperatorCode
                    };
                    tcs.SetResult(CvResult);

                },
                OnCoverageDidError = (error) =>
                {
                    Log.Info("IPServiceImpl", "coverage error:" + error.ErrorMessage);
                    var CvResult = new CoverageResult
                    {
                        IsAvailable = false,
                        ErrorMessage = error.ErrorMessage,
                        ErrorCode = error.Error_code
                    };
                    tcs.SetResult(CvResult);
                }
            };
            service.CheckCoverage(coverageCallback);
            return tcs.Task;
        }

        Task<AuthorizationResult> IIPService.DoAuthorization(string login_hint, string trx_id)
        {
            var tcs = new TaskCompletionSource<AuthorizationResult>();
            var service = new CellularService(context);
            var authCallback = new IPAuthorizationCallback
            {
                OnAuthDidComplete = (response) =>
                {

                    Log.Info("IPServiceImpl", "OnAuthDidComplete");
                    Log.Info("IPServiceImpl", "code: " + response.Code);
                    var AuthResult = new AuthorizationResult();
                    if (response.Code != null)
                    {
                        AuthResult.IsSuccess = true;
                        AuthResult.Code = response.Code;
                        AuthResult.State = response.State;
                        AuthResult.FullResponse = response.ResponseData;
                    }
                    else
                    {
                        AuthResult.IsSuccess = false;
                    }

                    tcs.SetResult(AuthResult);
                },
                OnAuthDidError = (error) =>
                {

                    Log.Info("IPServiceImpl", "OnAuthDidError " + error.Error_code);
                    Log.Info("IPServiceImpl", error.ErrorMessage);
                    var AuthResult = new AuthorizationResult
                    {
                        IsSuccess = false,
                        ErrorMessage = error.ErrorMessage,
                        ErrorCode = error.Error_code
                    };
                    tcs.SetResult(AuthResult);
                }
            };

            var state = IPConstants.ClientId + "f6675545a97f44cc795b8b8fccc12062" + login_hint + trx_id + IPConstants.RedirectUri;
            var authRequestBuilder = new AuthRequest.Builder();
            authRequestBuilder.AddQueryParam("login_hint", login_hint);
            authRequestBuilder.AddQueryParam("trx_id", trx_id);
            authRequestBuilder.SetScope("openid ip:phone_verify ip:mobile_id");
            authRequestBuilder.SetState(CreateMD5(state));

            //authRequestBuilder.SetState("your_state");
            //authRequestBuilder.AddQueryParam("your_param", "your_value");

            var auth = authRequestBuilder.Build();
            service.PerformAuth(auth, authCallback);
            return tcs.Task;
        }

        public void Dispose()
        {
            CellularService.Instance.UnregisterNetwork(context);
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
