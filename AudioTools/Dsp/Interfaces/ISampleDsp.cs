namespace AudioTools.Dsp.Interfaces
{
    public interface ISampleDsp
    {
        float Transform(float sample);
        
        /*
        string[] GetParameters();

        void SetParameter(string name, float value);

        float GetParameter(string name);
        */
    }
}
