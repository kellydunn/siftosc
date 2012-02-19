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
  public class SiftOsc : BaseApp {
    private OscClient client;
    private IPEndPoint renoiseEndPoint;
    private IPEndPoint endPoint;
    private int port;

    private Dictionary<String, IPEndPoint> oscEndpoints;
    private Dictionary<String, List<String>> oscServices;

    override public int FrameRate {
      get { return 20; }
    }

    override public void Setup() {
      client = new OscClient(IPAddress.Loopback, 7001);
      endPoint = new IPEndPoint(IPAddress.Looback, 9002);

      for(int i = 0; i < this.CubeSet.toArray().Length; i++) {
        new SiftOscCube(this.CubeSet[i], client, null);
      }
    }

    public void OnShakeStarted(Cube c) {
      OscMessage oscMessage = new OscMessage(endPoint, "/renoise/transport/start");
    }

    public void OnShakeStopped(Cube C, int duration) {
      OscMessage oscMessage = new OscMessage(endPoint, "/renoise/transport/start");
    }

    public void OnTilt(Cube c, int x, int y, int z) {
      OscMessage oscMessage = new OscMessage(endPoint, "/siftosc/tilt", client);
      oscMessage.Append(x);
      oscMessage.Append(y);
      oscMessage.Append(z);
      Log.Debug("x: " + x + "y: " + y + "z: " + z);
      oscMessage.Send(endPoint);
    }

    static void Main(string[] args) {
      SiftOsc app = new SiftOsc();
      app.oscEndpoints = new Dictionary<String, IPEndPoint>();
      app.oscServices = new Dictionary<String, List<String>>();

      StreamReader input = new StreamReader("config.yml");
      StringReader content = new StringReader(input.ReadToEnd());
      var yaml = new YamlStream();
      yaml.Load(content);
      YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

      Dictionary<String, List<String>> services = new Dictionary<String, List<String>>();
      foreach (var entry in mapping.Children) {
        String service = (((YamlScalarNode)entry.Key).Value);
        Log.Debug(service);
        String[] serviceData = service.Split(':');
        IPAddress endpointAddress = IPAddress.Parse(serviceData[0]);
        IPEndPoint endpoint = new IPEndPoint(endpointAddress, Int32.Parse(serviceData[1]));

        List<String> callbacks = new List<String>();
        services.Add(service, callbacks);
      }

      app.Run();
    }
  }
}

