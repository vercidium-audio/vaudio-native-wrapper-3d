using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    /// <summary>Settings that control how sound energy is absorbed by the air over distance</summary>
    public class AirAbsorptionSettings
    {
        public IntPtr native;
        private readonly bool owns;

        // Native holds a raw function pointer into these delegates, invoked from native worker
        // threads that the CLR doesn't scan the same way as managed call stacks. A managed field
        // reference alone isn't a reliable guarantee against collection for this pattern, so pin
        // them explicitly for as long as native might call back into them.
        GCHandle lfHandle;
        GCHandle hfHandle;

        /// <summary>Create a new AirAbsorptionSettings with default settings</summary>
        public AirAbsorptionSettings()
        {
            native = AirAbsorptionSettingsBindings.Create();
            owns = true;
        }

        public AirAbsorptionSettings(IntPtr native)
        {
            this.native = native;
        }

        /// <summary>Relative humidity as a percentage (0–1). Defaults to 0.1f</summary>
        public float Humidity
        {
            get => AirAbsorptionSettingsBindings.GetHumidity(native);
            set => AirAbsorptionSettingsBindings.SetHumidity(native, value).ThrowIfError();
        }

        /// <summary>Air temperature in degrees Celsius. Defaults to 26</summary>
        public float Temperature
        {
            get => AirAbsorptionSettingsBindings.GetTemperature(native);
            set => AirAbsorptionSettingsBindings.SetTemperature(native, value).ThrowIfError();
        }

        /// <summary>Atmospheric pressure in Pascals. Defaults to 101325</summary>
        public float Pressure
        {
            get => AirAbsorptionSettingsBindings.GetPressure(native);
            set => AirAbsorptionSettingsBindings.SetPressure(native, value).ThrowIfError();
        }

        public VAResult Validate() => AirAbsorptionSettingsBindings.Validate(native);

        public VAResult Destroy()
        {
            var result = AirAbsorptionSettingsBindings.Destroy(native);
            native = IntPtr.Zero;
            return result;
        }

        ~AirAbsorptionSettings()
        {
            if (owns && native != IntPtr.Zero)
                LogSettings.Warn("AirAbsorptionSettings was garbage collected without calling Destroy() first.");
        }

        public AirAbsorptionFormulaDelegate SetCustomFormulaLF(Func<float, float> value)
        {
            if (lfHandle.IsAllocated)
                lfHandle.Free();

            if (value != null)
            {
                float callback(float distance) => value(distance);
                var del = (AirAbsorptionFormulaDelegate)callback;
                lfHandle = GCHandle.Alloc(del);

                AirAbsorptionSettingsBindings.SetCustomFormulaLF(native, Marshal.GetFunctionPointerForDelegate(del)).ThrowIfError();
                return del;
            }
            else
            {
                AirAbsorptionSettingsBindings.SetCustomFormulaLF(native, IntPtr.Zero).ThrowIfError();
                return null;
            }
        }

        public AirAbsorptionFormulaDelegate SetCustomFormulaHF(Func<float, float> value)
        {
            if (hfHandle.IsAllocated)
                hfHandle.Free();

            if (value != null)
            {
                float callback(float distance) => value(distance);
                var del = (AirAbsorptionFormulaDelegate)callback;
                hfHandle = GCHandle.Alloc(del);

                AirAbsorptionSettingsBindings.SetCustomFormulaHF(native, Marshal.GetFunctionPointerForDelegate(del)).ThrowIfError();
                return del;
            }
            else
            {
                AirAbsorptionSettingsBindings.SetCustomFormulaHF(native, IntPtr.Zero).ThrowIfError();
                return null;
            }
        }
    }
}