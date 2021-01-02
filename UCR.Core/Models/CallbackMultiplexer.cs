using System;
using System.Collections.Generic;
using HidWizards.UCR.Core.Models.Binding;

namespace HidWizards.UCR.Core.Models
{
    public class CallbackMultiplexer
    {
        private DeviceBinding.ValueChanged _mappingUpdate;
        private readonly int _index;
        private readonly List<(ulong sequence, short value)> _cache;

        public CallbackMultiplexer(List<(ulong sequence, short value)> cache, int index, DeviceBinding.ValueChanged mappingUpdate)
        {
            // The ampping update is added into the multiplexer at the beginning of the plugin

            _mappingUpdate = mappingUpdate;
            _index = index;
            _cache = cache;
        }

        public void Update(ulong sequence, short value)
        {
            if (sequence >= _cache[_index].sequence)
            {
                _cache[_index] = (sequence, value);
                _mappingUpdate(sequence, value);
            }
        }

        ~CallbackMultiplexer()
        {
            _mappingUpdate = null;
        }
    }
}
