using BrightIdeasSoftware;
using Myriadbits.MXF;
using System;
using System.Diagnostics.Metrics;


namespace Myriadbits.MXFInspect.CustomControls
{
    public class MXFObjectCluster : ClusteringStrategy
    {
        public override string GetClusterDisplayLabel(ICluster cluster)
        {
            Type type = cluster.ClusterKey as Type;
            return type.Name;
        }

        public override object GetClusterKey(object model)
        {
            return model.GetType(); 
        }
    }
}
