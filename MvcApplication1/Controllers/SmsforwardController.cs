using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Twilio;
using Twilio.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace MvcApplication1.Controllers
{
    public class SmsforwardController : TwilioController
    {
        public void Forward(SmsRequest request)
        {
            
            var client = new TwilioRestClient(ServerSettings.AccountSID, ServerSettings.AuthToken);
            client.SendSmsMessage(ServerSettings.twilionumber, ServerSettings.forwardedNumber, request.Body);
           
        }   
    }
}
