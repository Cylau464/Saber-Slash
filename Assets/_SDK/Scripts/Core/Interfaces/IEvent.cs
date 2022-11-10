using System.Collections.Generic;

namespace apps
{
    public enum ADMediation { Ironsource }
    public interface IEvent
    {

        /// <summary>
        /// To send a custom event.
        /// </summary>
        /// <param name="eventName"> The event name that we will send. </param>
        /// <param name="value"> The value of event, example: The score.</param>
        void CustomEvent(string eventName);

        /// <summary>
        /// To send a session event.
        /// </summary>
        /// <param name="sessionName"> The session name that we will send. </param>
        /// <param name="statue"> The session statue that we will send it can be started or completed. </param>
        void SessionEvent(string sessionName, SessionStatue statue);

        /// <summary>
        /// To send a ProgressEvent event.
        /// </summary>
        void ProgressStartedEvent(ProgressStartInfo progressInfo);

        /// <summary>
        /// To send a ProgressEvent event.
        /// </summary>
        void ProgressFailedEvent(ProgressFailedInfo progressInfo);
        
        /// <summary>
        /// To send a ProgressEvent event.
        /// </summary>
        void ProgressCompletedEvent(ProgressCompletedInfo progressInfo);

        /// <summary>
        /// To send some error events.
        /// </summary>
        /// <param name="severity"> The error type. </param>
        /// <param name="message"> The message content in the error. </param>
        void ErrorEvent(ErrorSeverity severity, string message);

        /// <summary>
        /// To send some products buying in the game.
        /// </summary>
        /// <param name="productIAPID"> product ID of the IAP. </param>
        /// <param name="price"> The price that spended on this product. </param>
        void IAPEvent(InAppInfo info);

        /// <summary>
        /// To send the revenue of the ads.
        /// </summary>
        /// <param name="mediation"> The mediation name ex: Ironsource, Applovin... </param>
        /// <param name="impressionData"> The data of the impression </param>
        void ADRevenueEvent(object impressionData);

        /// <summary>
        /// Send events on show some ADS.
        /// </summary>
        /// <param name="adType"> The ads type example Rewardedvideo. </param>
        /// <param name="placementName"> The placement name of this ad. </param>
        void AdEvent(EventADSName eventADSName, AdType adType, string placement, EventADSResult result);
    }
}