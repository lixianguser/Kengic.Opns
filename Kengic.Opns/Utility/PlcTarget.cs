using System;
using System.Collections.Generic;
using Siemens.Engineering;
using Siemens.Engineering.HW;
using Siemens.Engineering.SW;
using Siemens.Engineering.Online;

namespace Kengic.Opns.Utility
{
    public class PlcTarget : ProjectInfos
    {
        private OnlineProvider _onlineProvider;
        public DeviceItem DeviceItem;
        public PlcSoftware PlcSoftware;
        public IEnumerable<object> MenuSelectionProvider;

        public PlcTarget(IEnumerable<object> menuSelectionProvider)
        {
            MenuSelectionProvider = menuSelectionProvider;
        }

        public IEngineeringObject GetObject(Type item)
        {
            foreach (IEngineeringObject engineeringObject in MenuSelectionProvider)
            {
                IEngineeringObject parent = engineeringObject;
                if (item == typeof(DeviceItem))
                {
                    while (!(parent is DeviceItem)) parent = parent.Parent;
                    return DeviceItem = (DeviceItem)parent;
                }

                if (item == typeof(PlcSoftware))
                {
                    while (!(parent is PlcSoftware)) parent = parent.Parent;
                    return PlcSoftware = (PlcSoftware)parent;
                }
            }

            return null;
        }

        public OnlineState GetOnlineState()
        {
            _onlineProvider = DeviceItem.GetService<OnlineProvider>();
            return _onlineProvider.State;
        }

        public void GoOffline()
        {
            if (_onlineProvider == null)
                return;
            if (GetOnlineState() == OnlineState.Offline)
                return;
            if (_onlineProvider.Configuration.IsConfigured)
            {
                _onlineProvider.GoOffline();
            }
        }
    }
}