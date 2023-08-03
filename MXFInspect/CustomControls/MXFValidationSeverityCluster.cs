using BrightIdeasSoftware;
using Myriadbits.MXF;
using System;
using System.Diagnostics.Metrics;


namespace Myriadbits.MXFInspect.CustomControls
{
    public class MXFValidationSeverityCluster : ClusteringStrategy
    {
        public override string GetClusterDisplayLabel(ICluster cluster)
        {
            MXFValidationSeverity? severity = cluster.ClusterKey as MXFValidationSeverity?;

            return severity.ToString();
        }

        public override object GetClusterKey(object model)
        {
            return (model as MXFValidationResult)?.Severity ?? null; 
        }
    }
}
