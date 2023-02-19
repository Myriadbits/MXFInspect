namespace Myriadbits.MXF
{
    public class TaskReport
    {
        public string Description { get; set; }
        public int Percent { get; set; }

        public TaskReport(int percent, string desc)
        {
            Description = desc;
            Percent = percent;
        }
    }
}