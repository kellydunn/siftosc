using Sifteo;
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Bespoke.Common;
using Bespoke.Common.Osc;
using YamlDotNet.Core;
using YamlDotNet.Converters;
using YamlDotNet.RepresentationModel;

namespace SiftOsc {
  public class SiftOscCubeService {
    private Dictionary<IPEndPoint, List<String>> endpointCallbacks;

    public SiftOscCubeService(Dictionary<IPEndPoint, List<String>> endpointCallbacks) {}
  }
}