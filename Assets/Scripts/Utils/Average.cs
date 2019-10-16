
public class Average {

    float runningAverage;
    float[] values;
    int index;
    
    public Average ( int binCount ) {
        values = new float[binCount];
    }

    public float GetNext(float value) {
        runningAverage = runningAverage * (float)values.Length;
        runningAverage -= values[index];
        values[index] = value;
        runningAverage += values[index];
        runningAverage = runningAverage / (float) values.Length;
        index = ( index == values.Length-1 )? 0 : index + 1;
        return runningAverage;
    }
}