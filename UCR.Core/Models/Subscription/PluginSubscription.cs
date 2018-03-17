﻿using System;
using System.Collections.Generic;
using HidWizards.UCR.Core.Managers;

namespace HidWizards.UCR.Core.Models.Subscription
{
    public class PluginSubscription
    {
        public Plugin Plugin { get; set; }
        public Guid SubscriptionStateGuid { get; set; }
        public DeviceSubscription OutputDeviceSubscription { get; set; }

        public PluginSubscription(Plugin plugin, Guid subscriptionStateGuid,
            List<DeviceSubscription> outputDeviceSubscriptions)
        {
            Plugin = plugin;
            SubscriptionStateGuid = subscriptionStateGuid;
            Plugin.Output.OutputSink = WriteOutput;
            OutputDeviceSubscription = outputDeviceSubscriptions[Plugin.Output.DeviceNumber];
        }

        private void WriteOutput(long value)
        {
            Plugin.Profile.Context.IOController.SetOutputstate(SubscriptionsManager.GetOutputSubscriptionRequest(SubscriptionStateGuid, OutputDeviceSubscription), SubscriptionsManager.GetBindingDescriptor(Plugin.Output), (int)value);
        }
    }
}
