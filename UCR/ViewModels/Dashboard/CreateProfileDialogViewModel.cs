﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HidWizards.UCR.Core.Managers;
using HidWizards.UCR.Core.Models;
using HidWizards.UCR.ViewModels.Controls;
using HidWizards.UCR.ViewModels.DeviceViewModels;

namespace HidWizards.UCR.ViewModels.Dashboard
{
    public class CreateProfileDialogViewModel
    {

        public string Title { get; set; }

        public DeviceSelectControlViewModel InputControl { get; set; }
        public DeviceSelectControlViewModel OutputControl { get; set; }
        
        public CreateProfileDialogViewModel()
        {
        }

        public CreateProfileDialogViewModel(string title, DevicesManager devicesManager)
        {
            Title = title;

            var inputDevices = GetDeviceList(devicesManager.GetAvailableDeviceList(DeviceIoType.Input));
            var outputDevices = GetDeviceList(devicesManager.GetAvailableDeviceList(DeviceIoType.Output));

           InputControl = new DeviceSelectControlViewModel("Input Devices", inputDevices);
           OutputControl = new DeviceSelectControlViewModel("Output Devices", outputDevices);
        }

        private ObservableCollection<DeviceViewModel> GetDeviceList(List<DeviceGroup> devices)
        {
            var result = new ObservableCollection<DeviceViewModel>();

            foreach (var deviceGroup in devices)
            {
                foreach (var device in deviceGroup.Devices)
                {
                    result.Add(new DeviceViewModel(device));
                }
            }

            if (result.Count > 0) result[0].FirstElement = true;

            return result;
        }
    }
}
