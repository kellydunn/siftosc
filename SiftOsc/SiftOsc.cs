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

    private Dictionary<int, SiftOscCube> siftOscCubes;
    private Dictionary<SiftOscCube, String> siftOscCallbacks;
    private Dictionary<String, IPEndPoint> siftOscEndpoints;
    private Dictionary<String, List<String>> siftOscServices;

    override public int FrameRate {
      get { return 20; }
    }

    override public void Setup() {
      endPoint = new IPEndPoint(IPAddress.Loopback, 9002);

      for(int i = 0; i < this.CubeSet.toArray().Length; i++) {
        this.siftOscCubes[i].setCube(this.CubeSet[i]);
      }
    }

    public void addCube(int id, SiftOscCube c) {
      this.siftOscCubes.Add(id, c);
    }

    public void setClient(OscClient client) {
      this.client = client;
    }

    static void Main(string[] args) {
      SiftOsc app = new SiftOsc();
      OscClient client = new OscClient(IPAddress.Loopback, 7001);
      app.setClient(client);
      app.siftOscCubes = new Dictionary<int, SiftOscCube>();
      app.siftOscCallbacks = new Dictionary<SiftOscCube, String>();
      app.siftOscEndpoints = new Dictionary<String, IPEndPoint>();
      app.siftOscServices = new Dictionary<String, List<String>>();

      StreamReader input = new StreamReader("config.yml");
      StringReader content = new StringReader(input.ReadToEnd());

      Log.Debug(content.ToString());

      var yaml = new YamlStream();
      yaml.Load(content);
      YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

      Dictionary<String, List<String>> services = new Dictionary<String, List<String>>();

      foreach (var cubeID in mapping.Children) {
        String cubeIDName = (((YamlScalarNode)cubeID.Key).Value);
        SiftOscCube cube = new SiftOscCube(null, client);
        app.addCube(Int32.Parse(cubeIDName), cube);

        YamlMappingNode cubeEvents = (YamlMappingNode)cubeID.Value;

        foreach (var cubeEvent in cubeEvents.Children) {
          String cubeEventName = (((YamlScalarNode)cubeEvent.Key).Value);
          Log.Debug(cubeEventName);
          YamlMappingNode serviceEndpoints = (YamlMappingNode)cubeEvent.Value;

          foreach (var serviceEndpoint in serviceEndpoints.Children) {
            String serviceEndpointName = (((YamlScalarNode)serviceEndpoint.Key).Value);
            Log.Debug(serviceEndpointName);
            YamlSequenceNode messages = (YamlSequenceNode)serviceEndpoint.Value;

            foreach(var message in messages.Children) {
              String messageName = message.ToString();
              Log.Debug(messageName);
            }
          }
        }
      }

      app.Run();
    }
  }
}

