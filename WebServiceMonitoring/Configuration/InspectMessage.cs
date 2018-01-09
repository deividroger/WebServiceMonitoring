using Log.Model;
using Log.Repository;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace LogService.Configuration
{
    public class InspectMessage : IClientMessageInspector
    {
        private string _requestValue;
        private string _operactionAction;

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            
            new LoggerDataBase().RecordLogWS(new LogDto
            {
                Action = _operactionAction,
                InValue = _requestValue,
                OutValue = reply.ToString()
            });

        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            _requestValue = request.ToString();
            _operactionAction = request.Headers.Action;

            return new object();
        }

    }
}
