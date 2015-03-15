using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Twilio;
using Twilio.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;



namespace MvcApplication1.Controllers
{


    public class ConferencedropController : TwilioController
    {
        public ActionResult Receivecall(VoiceRequest request)
        {
            String roomName = "myroom";
            Conferenceoptions converenceoptions = new Conferenceoptions();

            //Just delete if you don't want to record the conference call. 
            converenceoptions.record = "record-from-start";

            var twiml = new TwilioResponse();

            //Call the other person (You) asynchronously into the conferenceroom
            Task.Run(() => InvitePerson(request.From, roomName));

            return TwiML(twiml.DialConference(roomName, converenceoptions));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="from">number the original call is comeing from</param>
        /// <param name="room">the conference room name</param>
        public void InvitePerson(String from, String room)
        {          
                CallOptions options = new CallOptions();
                options.To = ServerSettings.forwardedNumber;
                options.From = ServerSettings.twilionumber;
                options.Url = ServerSettings.serveraddress + "/Conferencedrop/PutIntoRoom?room=" + room;
                options.Timeout = 5;

                var client = new TwilioRestClient(ServerSettings.AccountSID, ServerSettings.AuthToken);

                client.InitiateOutboundCall(options);
        }

        /// <summary>
        /// Called back when the second person is answered the call
        /// </summary>
        /// <param name="room">The name of the room</param>
        /// <returns></returns>
        public ActionResult PutIntoRoom(String room)
        {
            var twiml = new TwilioResponse();
            return TwiML(twiml.DialConference(room));
        }


        /// <summary>
        /// This event is called back when the conference call is ended
        /// </summary>
        /// <param name="request"></param>
        public void Receiverecord(VoiceRequest request)
        {
            

        }

    }
}
