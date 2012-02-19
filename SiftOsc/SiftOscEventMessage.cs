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
    private IPEndPoint client;
    private IPEndPoint server;
    private List<Object> oscMessageParams;

    public SiftOscEventMessage(String message, IPEndPoint client, IPEndPoint server) {
      this.eventMessage = message;
      this.client = client;
      this.server = server;
    }

    public void generateFromYaml(YamlNode endpointMessageNode) {
      this.eventMessage = endpointMessageNode.ToString();
      Log.Debug("      " + this.eventMessage);
    }

    public void OnButton(Cube c, bool pressed){
      OscMessage message = new OscMessage(this.client, this.eventMessage, this.server);
      message.Append(pressed);
      message.Send();
    }

    public void OnTilt(Cube c, int x, int y, int z){
      OscMessage message = new OscMessage(this.client, this.eventMessage, this.server);
      message.Append(x);
      message.Append(y);
      message.Append(z);
      message.Send();
    }

    public void OnShakeStarted(Cube c){
      OscMessage message = new OscMessage(this.client, this.eventMessage, this.server);
      message.Send();
    }

    public void OnShakeStopped(Cube C, int duration){
      OscMessage message = new OscMessage(this.client, this.eventMessage, this.server);
      message.Append(duration);
      message.Send();
    }

    public void OnFlip(Cube c, bool isFacingUp){
      OscMessage message = new OscMessage(this.client, this.eventMessage, this.server);
      message.Append(isFacingUp);
      message.Send();
    }

    public void OnNeighborAdd(Cube c, Cube.Side cSide, Cube neighbor, Cube.Side neighborSide){}
    public void OnNeighborRemove(Cube c, Cube.Side cSide, Cube neighbor, Cube.Side neighborSide){}
  }
}