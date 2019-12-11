using SignalWire.Relay;
using SignalWire.Relay.Calling;
using System;

namespace Realtime_Dashboard_Example.Consumers
{
    [Serializable]
    internal class PhoneConsumer : Consumer
    {
        
        private Guid _ConsumptionId = new Guid();

        public event EventHandler<Helpers.RealTimeEventArgs> RealTimeEvent;

        private Helpers.DashboardDataModel _data = new Helpers.DashboardDataModel();

        protected override void Setup()
        {
            _ConsumptionId = Guid.NewGuid();
        }

        protected virtual void OnRealTimeEvent(Helpers.RealTimeEventArgs e)
        {
            RealTimeEvent?.Invoke(this, e);
        }

        protected override void OnIncomingCall(Call call)
        {
            AnswerResult resultAnswer = call.Answer();
            if (!resultAnswer.Successful)
            {
            }
            else
            {
                // incoming call failed
            }

            // Log Data
            PhoneCall theCall = call as PhoneCall;
            _data = new Helpers.DashboardDataModel()
            {
                Id = new System.Guid(),
                Direction = "inbound",
                ConnectId = theCall.ID,
                StartDateTime = DateTime.UtcNow,
                State = theCall.State.ToString(),
                To = theCall.To,
                From = theCall.From,
                Duration = 0,
                Disposition = "",
                EndDateTime = null,
                Context = call.Context,
                StartDateTimeFormatted = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss tt"),
                DurationFormatted = "00:00",
                EndDateTimeFormatted = ""
            };

            OnRealTimeEvent(new Helpers.RealTimeEventArgs()
            {
                ConsumptionId = _ConsumptionId,
                Type = "phone",
                Payload = _data
            });

            FlowRouter(call);
        }

        public void TerminateCall(Call call)
        {
            _data.EndDateTime = DateTime.UtcNow;
            _data.EndDateTimeFormatted = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss tt");
            _data.Duration = 0;//(_data.EndDateTime - _data.StartDateTime).Value;
            _data.DurationFormatted = (_data.EndDateTime - _data.StartDateTime).Value.ToString();
            _data.State = "ended"; // call.State.ToString();
            _data.Disposition = "Complete";
            call.Hangup();
        }

        public void FlowRouter(Call call)
        {
            // Grab FlowTypes From DataSource
            switch (call.Context)
            {
                default:
                    call.PlayTTS("Welcome to SignalWire!");
                    break;
            }
            TerminateCall(call);
        }
    }
}