using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using Twilio;
using Twilio.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace MvcApplication1.Controllers
{

    public class CallforwardController : TwilioController
    {

        public ActionResult Forward(VoiceRequest request)
        {
            var twiml = new TwilioResponse();
            Dialsettings dialsettings = new Dialsettings();
            dialsettings.action = ServerSettings.serveraddress + "/Callforward/Callended";
            dialsettings.timeout = "20";
            //Just delete if you dont want to record the call
            dialsettings.record = "record-from-answer";

            return TwiML(twiml.Dial(ServerSettings.forwardedNumber, dialsettings));
        }
        public void CallEnded(VoiceRequest request)
        {
    
        }    
        
    }
}
