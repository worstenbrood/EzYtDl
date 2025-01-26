namespace AudioTools.Dsp.Interfaces
{
    public interface ISampleDsp
    {
        bool Enabled { get; set; }
        float Wet { get; set; }
        float Dry { get; set; }

        float Transform(float sample);
        
        /*
        string[] GetParameters();

        void SetParameter(string name, float value);

        float GetParameter(string name);
        */
    }
}
