using System;
using RSG;

namespace ConnectApp.Models.ActionModel {
    public class MessengerScreenActionModel : BaseActionModel {
        public Action pushToNotificatioins;
        public Action pushToDiscoverChannels;
        public Action<string> pushToChannel;
        public Func<IPromise> fetchChannels;
        public Action<string, string> joinChannel;
    }
}