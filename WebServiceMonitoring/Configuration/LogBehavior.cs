using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace LogService.Configuration

{
    public class LogBehavior : BehaviorExtensionElement, IEndpointBehavior
    {

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

            InspectMessage _inspector = new InspectMessage();

            clientRuntime.MessageInspectors.Add(_inspector);


        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }

        public override Type BehaviorType
        {
            get { return typeof(LogBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new LogBehavior();
        }

     
    }
}
