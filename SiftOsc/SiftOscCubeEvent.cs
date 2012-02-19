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
  public class SiftOscCubeEvent {
    private IPEndPoint endPoint;

    public SiftOscCubeEvent() {}

    public void generateFromYaml(KeyValuePair<YamlNode, YamlNode>eventEndpoint) {
      String eventEndpointName = (((YamlScalarNode)eventEndpoint.Key).Value);

      String[] data = eventEndpointName.Split(':');
      this.endPoint = new IPEndPoint(IPAddress.Parse(data[0]), Int32.Parse(data[1]));

      Log.Debug("    " + eventEndpointName);
      YamlSequenceNode endpointMessages = (YamlSequenceNode)eventEndpoint.Value;

      List<String> messages = new List<String>();
      foreach(var endpointMessage in endpointMessages.Children) {
        SiftOscEventMessage message = new SiftOscEventMessage(null);
        message.generateFromYaml(endpointMessage);
      }
    }

    public IPEndPoint getEndPoint() {
      return this.endPoint;
    }

    public void OnButton(Cube c, bool pressed){}
    public void OnTilt(Cube c, int x, int y, int z){}
    public void OnShakeStarted(Cube c){}
    public void OnShakeStopped(Cube C, int duration){}
    public void OnFlip(Cube c, bool isFacingUp){}
    public void OnNeighborAdd(Cube c, Cube.Side cSide, Cube neighbor, Cube.Side neighborSide){}
    public void OnNeighborRemove(Cube c, Cube.Side cSide, Cube neighbor, Cube.Side neighborSide){}
  }
}