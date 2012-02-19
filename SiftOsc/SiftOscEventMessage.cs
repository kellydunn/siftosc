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

    public SiftOscEventMessage(String message) {
      this.eventMessage = (message != null)? message : null;
    }

    public void generateFromYaml(YamlNode endpointMessageNode) {
      this.eventMessage = endpointMessageNode.ToString();
      Log.Debug("      " + this.eventMessage);
    }
  }
}