namespace AudioTools.Dsp
{
    public interface IDsp
    {
        float Transform(float sample);
        
        /*
        string[] GetParameters();

        void SetParameter(string name, float value);

        float GetParameter(string name);
        */
    }
}
