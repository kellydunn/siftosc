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
  public class SiftOscCube {
    private Cube cube;

    // cube has many services
    private Dictionary<String, List<SiftOscCubeService>> services;

    public SiftOscCube(Cube c, Dictionary<String, List<SiftOscCubeService>> services) {}
  }
}