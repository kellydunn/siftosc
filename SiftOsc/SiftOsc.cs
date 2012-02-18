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

      IPAddress ipAddress;
      bool demoing = true;
      if(demoing) {
        ipAddress = IPAddress.Parse("10.10.10.189");
      } else {
        ipAddress = IPAddress.Loopback;
      }

      renoiseEndPoint = new IPEndPoint(ipAddress, 9001);
      endPoint = new IPEndPoint(ipAddress, 9002);

      this.CubeSet[0].TiltEvent += OnTilt;

      this.CubeSet[1].ShakeStartedEvent += OnShakeStarted;

      this.CubeSet[2].ShakeStoppedEvent += OnShakeStopped;
      this.CubeSet[2].Image("../assets/dumb.jpg");
    }

    public void OnShakeStarted(Cube c) {
      OscMessage oscMessage = new OscMessage(endPoint, "/renoise/transport/start");
      Log.Debug("" + endPoint.Address);
      oscMessage.Send(renoiseEndPoint);
    }

    public void OnShakeStopped(Cube C, int duration) {
      if(duration > 75) {
        OscMessage oscMessage = new OscMessage(endPoint, "/renoise/transport/start");
        Log.Debug("" + endPoint.Address);
        oscMessage.Send(renoiseEndPoint);

        // Potentially the worst hack of all time
        OscMessage oscMessage2 = new OscMessage(endPoint, "/siftosc/drop", client);
        oscMessage2.Send(endPoint);
      }
    }

    public void OnTilt(Cube c, int x, int y, int z) {
      OscMessage oscMessage = new OscMessage(endPoint, "/siftosc/tilt", client);
      oscMessage.Append(x);
      oscMessage.Append(y);
      oscMessage.Append(z);
      Log.Debug("x: " + x + "y: " + y + "z: " + z);
      oscMessage.Send(endPoint);
    }

    public void setServices(Dictionary<String, List<String>> services) {
      this.oscServices = services;
    }

    public void addEndpoint(String address, IPEndPoint endpoint) {
      this.oscEndpoints.Add(address, endpoint);
    }

    static void Main(string[] args) {
      SiftOsc app = new SiftOsc();
      app.oscEndpoints = new Dictionary<String, IPEndPoint>();
      app.oscServices = new Dictionary<String, List<String>>();

      StreamReader input = new StreamReader("config.yaml");
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
        app.addEndpoint(service, endpoint);

        List<String> callbacks = new List<String>();
        services.Add(service, callbacks);
      }

      app.setServices(services);
      app.Run();
    }
  }
}

