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
    private Dictionary<String, List<IPEndPoint>> cubeEndPoints;

    public SiftOscCube(Cube cube, OscClient client, Dictionary<String, List<IPEndPoint>> cubeEndPoints) {
      this.cube = cube;
      this.client = client;
      this.cubeEndPoints = (cubeEndPoints != null)? cubeEndPoints : new Dictionary<String, List<IPEndPoint>>();
    }

    public void setCube(Cube c) {
      this.cube = c;
    }

    public void generateFromYaml(YamlMappingNode cubeEvents) {
      foreach (var cubeEvent in cubeEvents.Children) {
        String cubeEventName = (((YamlScalarNode)cubeEvent.Key).Value);
        Log.Debug("  " + cubeEventName);

        YamlMappingNode eventEndpoints = (YamlMappingNode)cubeEvent.Value;

        List<IPEndPoint> endPoints = new List<IPEndPoint>();

        foreach (var eventEndpoint in eventEndpoints.Children) {
          SiftOscCubeEvent siftOscCubeEvent = new SiftOscCubeEvent(null, null);
          siftOscCubeEvent.generateFromYaml(eventEndpoint);
          IPEndPoint endPoint = siftOscCubeEvent.getEndPoint();
          endPoints.Add(endPoint);
        }

        cubeEndPoints.Add(cubeEventName, endPoints);
      }
    }
  }
}