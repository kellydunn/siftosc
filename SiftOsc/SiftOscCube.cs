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
    private OscClient client;

    public SiftOscCube(Cube cube, OscClient client) {
      this.cube = cube;
      this.client = client;
    }

    public void setCube(Cube c) {
      this.cube = c;
    }

    public void OnButton(Cube c, bool pressed){}

    public void OnTilt(Cube c, int x, int y, int z) {    }

    public void OnShakeStarted(Cube c) {    }

    public void OnShakeStopped(Cube C, int duration) {    }

    public void OnFlip(Cube c, bool isFacingUp) {    }

    public void OnNeighborAdd(Cube c, Cube.Side cSide, Cube neighbor, Cube.Side neighborSide) {    }

    public void OnNeighborRemove(Cube c, Cube.Side cSide, Cube neighbor, Cube.Side neighborSide) {    }
  }
}