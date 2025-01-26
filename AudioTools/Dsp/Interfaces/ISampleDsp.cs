namespace AudioTools.Dsp.Interfaces
{
    public interface ISampleDsp
    {
        bool Enabled { get; set; }
        float Transform(float sample);
        
        /*
        string[] GetParameters();
        void SetParameter(string name, float value);
        string GetParameterName(int index);
        float GetParameter(string name);
        */
    }
}
