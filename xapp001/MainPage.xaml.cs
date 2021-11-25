using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using Xamarin.Forms;
using xapp001.ExchangeCode;

namespace xapp001
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Hello");
            CheckCoverage();
        }

        private async void CheckCoverage()
        {
            var coverageResult = await DependencyService.Get<IIPService>().CheckCoverage();
            CoverageResultLbl.Text = "Coverage Result: " + coverageResult.IsAvailable;
            if (coverageResult.IsAvailable)
            {
                DoAuth();
            }
            else
            {
                CoverageResultLbl.Text = "Coverage Result: " + coverageResult.IsAvailable + " - errormessage: " + coverageResult.ErrorMessage + " - errorcode: " + coverageResult.ErrorCode;
            }

        }
        private async void DoAuth()
        {
            var loginHint = phoneNumber.Text;
            if (string.IsNullOrEmpty(loginHint))
            {
                AuthResultLbl.Text = "Authorization Result: Phone cannot be empty.";
                return;
            }
            var trxId = Guid.NewGuid().ToString();
            var authorizationResult = await DependencyService.Get<IIPService>().DoAuthorization(loginHint, trxId);

            if (authorizationResult.IsSuccess)
            {
                AuthResultLbl.Text = "Authorization Result: " + authorizationResult.Code;

                DoExchangeCode(authorizationResult.Code, trxId);
            }
            else
            {
                AuthResultLbl.Text = "Authorization Result: " + authorizationResult.Code + " - errormessage: " + authorizationResult.ErrorMessage;
            }

        }
        private async void DoExchangeCode(string code, string trxId)
        {
            try
            {
                var mApi = RestService.For<IExchangeCodeApi>("https://apitest.mekongsms.com");
                var request = new Dictionary<string, string> {
                    {"client_id",IPConstants.ClientId },
                    {"secret","f6675545a97f44cc795..." },
                    {"trx_id",trxId},
                    {"code",code }
                };
                var res = await mApi.PostToken(request);
                var data = res.Content;
                PhoneVerifiedLbl.Text = "PhoneVerified Result: " + data.phone_verified;
                MobileIDLbl.Text = "MobileID Result:" + data.mobile_id;

            }
            catch (ApiException ex)
            {
                PhoneVerifiedLbl.Text = ex.Message;
            }
            catch (Exception ex)
            {
                PhoneVerifiedLbl.Text = ex.Message;

            }

        }
        public void Dispose()
        {

            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            DependencyService.Get<IIPService>().Dispose();
        }
    }
}
