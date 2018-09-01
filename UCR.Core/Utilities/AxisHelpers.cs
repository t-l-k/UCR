﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidWizards.UCR.Core.Utilities
{
    public class DeadzoneHelper
    {
        //private double gapPercent;
        private int _percentage;
        private double _scaleFactor;
        private long _deadzoneCutoff;
        
        public int Percentage
        {
            get => _percentage;
            set
            {
                _percentage = value;
                PrecalculateValues();
            }
        }

        private void PrecalculateValues()
        {
            if (_percentage == 0)
            {
                _deadzoneCutoff = 0;
                _scaleFactor = 100.0;
            }
            else
            {
                // work out how much we have to deform the input scale by the output scale
                _scaleFactor = 100.0 / (100 - _percentage);
                _deadzoneCutoff = (long)((_percentage / 100.0) * Constants.AxisMaxAbsValue);
            }
        }

        public long ApplyRangeDeadZone(long value)
        {
            var absValue = Math.Abs(value);
            if (absValue <= _deadzoneCutoff)
            {
                return 0;
            }

            var sign = Math.Sign(value);
            var adjustedValue = (absValue - _deadzoneCutoff) * sign;
            var newValue = (long)(adjustedValue * _scaleFactor);
            //Debug.WriteLine($"Pre-DZ: {value}, Post-DZ: {newValue}, Cutoff: {_deadzoneCutoff}");
            return newValue;
        }
    }
}
