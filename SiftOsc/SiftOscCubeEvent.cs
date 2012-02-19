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
    private Cube cube;
    private OscClient client;
    private IPEndPoint endPoint;
    private List<SiftOscEventMessage> messages;

    public SiftOscCubeEvent(Cube cube, OscClient client, IPEndPoint endPoint, List<SiftOscEventMessage> messages) {
      this.cube = cube;
      this.client = client;
      this.endPoint = endPoint;
      this.messages = messages;
    }

    public void generateFromYaml(KeyValuePair<YamlNode, YamlNode>eventEndpoint) {
      this.messages = new List<SiftOscEventMessage>();

      String eventEndpointName = (((YamlScalarNode)eventEndpoint.Key).Value);
      String[] data = eventEndpointName.Split(':');
      this.endPoint = new IPEndPoint(IPAddress.Parse(data[0]), Int32.Parse(data[1]));

      Log.Debug("    " + eventEndpointName);
      YamlSequenceNode endpointMessages = (YamlSequenceNode)eventEndpoint.Value;

      List<String> messages = new List<String>();
      foreach(var endpointMessage in endpointMessages.Children) {
        SiftOscEventMessage message = new SiftOscEventMessage(null, null, endPoint);
        message.generateFromYaml(endpointMessage);
        this.messages.Add(message);
      }
    }

    public IPEndPoint getEndPoint() {
      return this.endPoint;
    }

    public List<SiftOscEventMessage> getMessages() {
      return this.messages;
    }
  }
}