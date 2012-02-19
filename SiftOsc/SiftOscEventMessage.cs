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
  public class SiftOscEventMessage {
    private String eventMessage;
    private OscClient client;
    private IPEndPoint server;
    private List<Object> oscMessageParams;

    public SiftOscEventMessage(String message, OscClient client, IPEndPoint server) {
      this.eventMessage = message;
      this.client = client;
      this.server = server;
    }

    public void generateFromYaml(YamlNode endpointMessageNode) {
      this.eventMessage = endpointMessageNode.ToString();
    }

    public void OnButton(Cube c, bool pressed){
      OscMessage message = new OscMessage(this.server, this.eventMessage, this.client);
      message.Append(pressed);
      message.Send();
    }

    public void OnTilt(Cube c, int x, int y, int z){
      OscMessage message = new OscMessage(this.server, this.eventMessage, this.client);
      message.Append(x);
      message.Append(y);
      message.Append(z);
      message.Send(this.server);
    }

    public void OnShakeStarted(Cube c){
      OscMessage message = new OscMessage(this.server, this.eventMessage, this.client);
      message.Send(this.server);
    }

    public void OnShakeStopped(Cube C, int duration){
      OscMessage message = new OscMessage(this.server, this.eventMessage, this.client);
      message.Append(duration);
      message.Send(this.server);
    }

    public void OnFlip(Cube c, bool isFacingUp){
      OscMessage message = new OscMessage(this.server, this.eventMessage, this.client);
      message.Append(isFacingUp);
      message.Send(this.server);
    }

    public void OnNeighborAdd(Cube c, Cube.Side cSide, Cube neighbor, Cube.Side neighborSide){}
    public void OnNeighborRemove(Cube c, Cube.Side cSide, Cube neighbor, Cube.Side neighborSide){}
  }
}